using System.Collections.Generic;
using System.Diagnostics;
using GameClient.Services;
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
            var problem = _client.GetTrainingProblem(size, operators);

            Debugger.Break();
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
