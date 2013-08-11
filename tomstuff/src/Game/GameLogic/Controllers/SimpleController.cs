using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using GameClient.Exceptions;
using GameClient.Extensions;
using GameClient.Services;
using GameClient.SExpressionTree;
using GameClient.Solvers;
using GameClient.ViewModels;
using log4net;

namespace GameClient.Controllers
{
    public class SimpleController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IGameClient _client;
        private readonly SolverBase[] _solvers;

        public SimpleController(IGameClient client)
        {
            _client = client;
            _solvers = new SolverBase[]
            {/*new Size3Solver(_client), new BruteForceSolver(_client), */ new BestGuessSolver(_client)};
        }

        public void Train(int size, TrainingOperators operators = TrainingOperators.Empty)
        {
            var problem = _client.GetTrainingProblem(size, operators);

            foreach (var solver in _solvers)
            {
                if (solver.CanSolve(problem))
                    solver.Solve(problem);
            }
        }
       
        public void Guess()
        {            
            var stopwatch = new Stopwatch();
            var random = new Random();           

            var index = 1;
            stopwatch.Start();

            while (true)
            {
                stopwatch.Restart();
                var allProblems = _client.GetProblems();

                var totalSolved = allProblems.Count(x => x.Solved);
                var totalFailed = allProblems.Count(x => !x.Solved && x.TimeLeft.HasValue && !(x.TimeLeft.Value > 0.0));                

                var problems =
                    random.Shuffle(
                        _client.GetProblems()
                            .Where(p => p.Solved == false && !p.TimeLeft.HasValue && _solvers[0].CanSolve(p))
                            .ToArray());

                if (problems.Length == 0)
                    break;

                foreach (var problem in random.Shuffle(problems))
                {
                    foreach (var solver in _solvers)
                    {
                        if (solver.CanSolve(problem))
                        {
                            Log.InfoFormat("\n[{0}] Starting problem solution {{remaining: {1}  solved: {2}  failed: {3}}}", DateTime.Now, problems.Length, totalSolved, totalFailed);
                            try
                            {
                                solver.Solve(problem);
                            }
                            catch (AlreadySolvedException)
                            {
                                Log.WarnFormat("Problem was already solved!");
                                break;
                            }
                        }
                    }

                    index++;

                    if (stopwatch.Elapsed.TotalMinutes > 4)
                    {
                        break;
                    }
                }

              
            }            
        }
    }
}
