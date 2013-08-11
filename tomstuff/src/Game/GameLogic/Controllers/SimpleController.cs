using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

        public void Train(string id)
        {
            var problem = new Problem
            {
                Challenge =
                    "(lambda (x_4468) (fold (shl1 x_4468) (shr4 x_4468) (lambda (x_4469 x_4470) (if0 x_4469 x_4469 x_4470))))",
                Id = id,
                Size = 11,
                Operators = new List<string> {"fold", "if0", "shl1", "shr4"}
            };

            Log.InfoFormat("Got training program: {0}", SProgramParser.Parse(problem.Challenge));

            foreach (var solver in _solvers)
            {
                if (solver.CanSolve(problem))
                    solver.Solve(problem);
            }
        }

        public void Guess()
        {
            int solved = 0, skipped = 0, expired = 0;
            var stopwatch = new Stopwatch();
            var random = new Random();

            var problems = _client.GetProblems().ToArray();
            Log.InfoFormat("Loaded {0} problems", problems.Count());

            problems =
                problems.Where(
                    problem => problem.Solved == false && !problem.TimeLeft.HasValue && _solvers[0].CanSolve(problem))
                    .ToArray();

            Log.InfoFormat("Attempting to solve {0} of them this run", problems.Length);

            var index = 1;
            stopwatch.Start();

            while (true)
            {
                foreach (var problem in random.Shuffle(problems))
                {
                    foreach (var solver in _solvers)
                    {
                        if (solver.CanSolve(problem))
                        {
                            Log.InfoFormat("\n[{0}] Solving problem [{1}/{2}]", DateTime.Now, index, problems.Length);
                            solver.Solve(problem);
                        }
                        else
                            skipped++;
                    }
                    index++;

                    if (stopwatch.Elapsed.TotalMinutes > 4)
                    {
                        break;
                    }
                }

                stopwatch.Restart();
                problems =
                    random.Shuffle(
                        _client.GetProblems()
                            .Where(p => p.Solved == false && !p.TimeLeft.HasValue && _solvers[0].CanSolve(p))
                            .ToArray());

                if (problems.Length == 0)
                    break;
            }

            Log.InfoFormat("{0} solved  {1} skipped  {2} expired", solved, skipped, expired);
        }
    }
}
