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
            var client = new GameClient.Services.GameClient("0236vFGLj5xt5pYPigGQyveWYTGMysQaU4Ot3siN");

            IEnumerable<Problem> problems = client.GetProblems();

            // create a solver controller
            var controller = new SimpleController(client);

            controller.Train(3, TrainingOperators.Empty);

            Debugger.Break();
        }
    }
}
