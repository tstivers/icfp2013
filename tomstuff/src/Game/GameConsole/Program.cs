using System.Collections.Generic;
using System.Diagnostics;
using GameClient.Controllers;
using GameClient.Services;
using GameClient.ViewModels;

namespace GameConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new GameClient.Services.GameClient(args[0]);

            IEnumerable<Problem> problems = client.GetProblems();

            // create a solver controller
            var controller = new SimpleController(client);

            controller.Train(4, TrainingOperators.Empty);
            //controller.Guess();

            Debugger.Break();
        }
    }
}
