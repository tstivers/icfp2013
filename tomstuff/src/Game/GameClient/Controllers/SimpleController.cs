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

        public SimpleController(IGameClient client)
        {
            _client = client;
        }

        public void Train(int size, TrainingOperators operators)
        {
            while (true)
            {
                Problem problem = _client.GetTrainingProblem(size, operators);

                var solver = new StupidSolver(_client);

                solver.Solve(problem);

                Debugger.Break();
            }
        }

        public void Guess()
        {
            IEnumerable<Problem> problems = _client.GetProblems();

            foreach (Problem problem in problems)
            {
            }
        }
    }
}
