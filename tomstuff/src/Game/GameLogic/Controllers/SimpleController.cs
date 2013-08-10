using System.Collections.Generic;
using System.Reflection;
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
            _solvers = new SolverBase[] {/*new Size3Solver(_client), */ new BruteForceSolver(_client)};
        }

        public void Train(int size, TrainingOperators operators = TrainingOperators.Empty)
        {
            var problem = _client.GetTrainingProblem(size, operators);

            Log.InfoFormat("Got training program: {0}", SProgramParser.Parse(problem.Challenge));

            foreach (var solver in _solvers)
            {
                if (solver.CanSolve(problem))
                    solver.Solve(problem);
            }
        }

        public void Train(string id)
        {
            var problem = new Problem()
            {
                Challenge = "(lambda (y) (if0 (and (shr1 y) y) 1 (shr4 (shr1 y))))",
                Id = id,
                Size = 10,
                Operators = new List<string> { "and", "if0", "shr1", "shr4" }
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
            var problems = _client.GetProblems();

            foreach (var problem in problems)
            {
                if (problem.Solved)
                    continue;

                if (problem.TimeLeft.HasValue && problem.TimeLeft == 0.0)
                {
                    Log.WarnFormat("Skipping expired problem {0}", problem.Id);
                    continue;
                }

                foreach (var solver in _solvers)
                {
                    if (solver.CanSolve(problem))
                        solver.Solve(problem);
                }
            }
        }
    }
}
