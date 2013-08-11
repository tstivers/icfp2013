using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using GameClient.Containers;
using GameClient.Exceptions;
using GameClient.Extensions;
using GameClient.Services;
using GameClient.SExpressionTree;
using GameClient.ViewModels;
using log4net;

namespace GameClient.Solvers
{
    public class BestGuessSolver : SolverBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Random _random = new Random();
        private static Stopwatch _stopwatch = new Stopwatch();
        private static Stopwatch _totalTime = new Stopwatch();
        private static readonly IdExpression ProgId = new IdExpression("y");

        private static readonly ulong[] TestValues =
        {
            0x0000000000000000, 0x0000000000000001, 0xFFFFFFFFFFFFFFFF,
            0x0000000100000000, 0x1122334455667788, 0x8000000000000001
        };

        public BestGuessSolver(IGameClient client) : base(client)
        {
        }

        public MultiValueConcurrentDictionary<string, ProgramExpression> GenerateIndex(List<ulong> inputs,
            ProgramExpression[] programs)
        {
            var index = new MultiValueConcurrentDictionary<string, ProgramExpression>();            

            var sw = new Stopwatch();
            sw.Start();

            //JetBrains.Profiler.Core.Api.PerformanceProfiler.Begin();
            //JetBrains.Profiler.Core.Api.PerformanceProfiler.Start();

            EvalContext[] contexts = inputs.Select(input => new EvalContext(ProgId, new NumberExpression(input))).ToArray();

            Parallel.ForEach(programs, program =>
            {
                var results = new List<ulong>(contexts.Length);

                foreach(var context in contexts)
                    results.Add(program.Eval(context));

                var rs = String.Join(", ", results.Select(x => "0x" + x.ToString("X")));

                index.Add(rs, program);
            });

            //JetBrains.Profiler.Core.Api.PerformanceProfiler.Stop();
            //JetBrains.Profiler.Core.Api.PerformanceProfiler.EndSave();

            Log.DebugFormat("Generated index in {0:c} for {1} programs with {2} unique keys", sw.Elapsed, programs.Length, index.Count);

            return index;
        }

        public override bool CanSolve(Problem p)
        {           
            if (p.Operators.Contains("bonus"))
                return false;

            return true;
        }

        private void AdjustOperators(Problem p)
        {
            //if (p.Operators.Count > 5)
        }

        public override bool Solve(Problem p)
        {            
            Log.Info(p);

            var solved = false;
            var operators = p.Operators.ToArray();
            var firstPass = true;
            int sizeAdjust = 0;
            _stopwatch.Reset();
            _totalTime.Restart();

            while (!solved)
            {
                try
                {
                    solved = AttemptSolution(p, operators, sizeAdjust);
                    firstPass = false;
                }
                catch (OutOfMemoryException)
                {
                    Log.Error("Ran out of memory!");
                    if (firstPass)
                    {
                        sizeAdjust--;
                        continue;
                    }
                   
                    return false;                   
                }
                catch (ProblemExpiredException)
                {
                    Log.ErrorFormat("Problem expired :(");
                    return false;
                }                

                if (solved)
                    return true;

                if (operators.Length > 6)
                    return false;

                sizeAdjust++;
            }

            return solved;
        }

        public bool AttemptSolution(Problem p, string[] operators, int sizeAdjust)
        {
            var generator = new SProgramGenerator(p.Size, operators, _stopwatch);

            int maxSize;

            switch (operators.Length)
            {
                case 2:
                case 3:
                case 4:
                    maxSize = 12;
                    break;
                case 5:
                case 6:
                    maxSize = 11;
                    break;
                case 7:
                case 8:
                case 9:
                    maxSize = 10;
                    break;                                 
                default:
                    maxSize = 9;
                    break;
            }

            maxSize += sizeAdjust;

            if (maxSize > 11 && operators.Length > 5)
                throw new OutOfMemoryException();

            var programs = generator.GenerateProgramRange(3, maxSize, ProgId);
            if (programs.Count < 1500000 && sizeAdjust > 0)
            {
                Log.Warn("Regenerating to have a better chance of finding a solution");
                programs = generator.GenerateProgramRange(3, maxSize + 1, ProgId);
            }

            if (!programs.Any())
            {
                Log.ErrorFormat("Failed to generate possible solutions for problem {0}", p.Challenge ?? p.Id);
                return false;
            }

            var inputs = new List<ulong>(TestValues);
            var index = GenerateIndex(inputs, programs.ToArray());
            var outputs = _client.Eval(p.Id, "(lambda (x) x)", TestValues);
            if (!_stopwatch.IsRunning)
                _stopwatch.Restart();

            var os = String.Join(", ", outputs.Select(x => "0x" + x.ToString("X")));
            //Log.DebugFormat("Got output string: {0}", os);

            while (true)
            {
                if (_stopwatch.Elapsed.TotalSeconds > 300)
                    throw new ProblemExpiredException();

                if (!index.ContainsKey(os))
                {
                    Log.ErrorFormat("Failed to find solution for problem {0}", p.Id);
                    return false;
                }                

                var result = _client.Guess(p.Id, index[os].FirstOrDefault().ToString());

                if (!result.IsCorrect)
                {
                    Log.WarnFormat("Made incorrect guess, rebuilding index ({0} seconds remaining)", 300 - _stopwatch.Elapsed.TotalSeconds);
                    Log.DebugFormat("For input value {0}: theirs = {1},  mine = {2}", result.Values[0], result.Values[1],
                        result.Values[2]);
                    var newTestVal = Convert.ToUInt64(result.Values[0], 16);
                    var newTestSol = Convert.ToUInt64(result.Values[1], 16);
                    inputs.Add(newTestVal);
                    index = GenerateIndex(inputs, index[os].ToArray());
                    os = String.Format("{0}, {1}", os, "0x" + newTestSol.ToString("X"));
                    Log.DebugFormat("New solution target: {0}", os);
                }
                else
                    break;
            }
            
            Log.InfoFormat("Correctly guessed problem {0} in {1:c}", p.Id, _totalTime.Elapsed);
            Log.InfoFormat("Solution: {0}", index[os].FirstOrDefault());

            return true;
        }
    }
}
