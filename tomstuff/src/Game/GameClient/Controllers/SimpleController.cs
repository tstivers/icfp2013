using System.Collections.Generic;
using System.Diagnostics;
using GameClient.Services;
using GameClient.Solvers;
using GameClient.ViewModels;

namespace GameClient.Controllers
{
    public class SimpleController
    {
        private readonly IGameClient _client;
        private readonly SolverBase[] _solvers;

        public SimpleController(IGameClient client)
        {
            _client = client;
            _solvers = new SolverBase[]
            {
                new Size3Solver(_client),
                new Size4Solver(_client), 
            };
        }

        public void Train(int size, TrainingOperators operators)
        {
            while (true)
            {
                Problem problem = _client.GetTrainingProblem(size, operators);

                foreach (var solver in _solvers)
                {
                    if (solver.CanSolve(problem))
                        solver.Solve(problem);
                }

                Debugger.Break();
            }
        }

        public void Guess()
        {
            IEnumerable<Problem> problems = _client.GetProblems();            

            foreach (Problem problem in problems)
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
