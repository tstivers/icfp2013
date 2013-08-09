using System;
using System.Collections.Generic;
using System.Reflection;
using GameClient.Controllers;
using GameClient.Services;
using GameClient.ViewModels;
using log4net;
using log4net.Config;

namespace GameConsole
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            Log.Info("Starting game console runner");

            SExpression.SProgramParser.PrettyPrint("(lambda (x_123) (if0 (not 1) 1 0))");

            return;

            var client = new GameClient.Services.GameClient(args[0]);

            IEnumerable<Problem> problems = client.GetProblems();

            // create a solver controller
            var controller = new SimpleController(client);

            controller.Train(4, TrainingOperators.Fold);
            //controller.Guess();

            Log.Info("Finished run");
            Console.ReadKey();
        }
    }
}
