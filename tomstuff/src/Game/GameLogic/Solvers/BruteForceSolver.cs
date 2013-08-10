using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameClient.Services;
using GameClient.SExpressionTree;
using GameClient.ViewModels;
using log4net;

namespace GameClient.Solvers
{
    public class BruteForceSolver : SolverBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly ulong[] TestValues =
        {
            0x0000000000000000, 0x0000000000000001, 0xFF00000000000000,
            0x0000000100000000, 0x1122334455667788, 0x8000000000000001
        };

        public BruteForceSolver(IGameClient client) : base(client)
        {
        }

        public override bool CanSolve(Problem p)
        {
            return p.Size <= 12 && (!p.Operators.Contains("fold") || !p.Operators.Contains("tfold"));
        }

        public Dictionary<string, SProgram> GenerateIndex(List<ulong> inputs, IEnumerable<SProgram> programs)
        {
            var index = new Dictionary<string, SProgram>();
            
            foreach (var program in programs)
            {
                var results = program.Eval(inputs.ToArray());
                var rs = String.Join(", ", results.Select(x => "0x" + x.ToString("X")));

                //Log.DebugFormat("{0} [{1}]", program, rs);

                index[rs] = program;
            }

            return index;
        }

        public override bool Solve(Problem p)
        {

            var generator = new SProgramGenerator(p.Size, p.Operators.ToArray());
            var programs = generator.GeneratePrograms();

            Log.InfoFormat("Generated {0} programs", programs.Count());

            if (!programs.Any())
            {
                Log.ErrorFormat("Failed to generate possible solutions for problem {0}", p.Challenge ?? p.Id);
                return false;
            }

            var inputs = new List<ulong>(TestValues);
            var index = GenerateIndex(inputs, programs);
            var outputs = _client.Eval(p.Id, "(lambda (x) x)", TestValues);
            var os = String.Join(", ", outputs.Select(x => "0x" + x.ToString("X")));                  

            while (true)
            {
                if (!index.ContainsKey(os))
                {
                    Log.ErrorFormat("Failed to find solution for problem {0}", p.Challenge ?? p.Id);
                    return false;
                }

                var result = _client.Guess(p.Id, index[os].ToString());

                if (!result.IsCorrect)
                {
                    Log.WarnFormat("Made incorrect guess for problem {0}: {1}", p.Challenge ?? p.Id, index[os]);
                    Log.InfoFormat("Adding value {0}: got {1} expected {2}", result.Values[0], result.Values[1], result.Values[2]);
                    var newTestVal = Convert.ToUInt64(result.Values[0], 16);
                    inputs.Add(newTestVal);
                    index = GenerateIndex(inputs, programs);
                    os = String.Format("{0}, {1}", os, "0x" + newTestVal.ToString("X"));                    
                }
                else
                {
                    break;
                }
            }

            Log.InfoFormat("Correctly guessed problem {0}: {1}", p.Challenge ?? p.Id, index[os]);

            return true;
        }
    }
}
