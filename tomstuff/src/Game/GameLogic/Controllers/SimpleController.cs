using System.Reflection;
using GameClient.Services;
using GameClient.SExpressionTree;
using GameClient.Solvers;
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

        public void Guess()
        {
            var problems = _client.GetProblems();

            foreach (var problem in problems)
            {
                if (problem.Solved)
                    continue;

                foreach (var solver in _solvers)
                {
                    if (solver.CanSolve(problem))
                        solver.Solve(problem);
                }
            }
        }
    }
}
