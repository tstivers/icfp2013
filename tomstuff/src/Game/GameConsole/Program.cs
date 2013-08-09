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

            var client = new GameClient.Services.GameClient(args[0]);

            //Log.Info(SProgramParser.Parse("(lambda (x) (fold x 0 (lambda (y z) (or y z))))"));

            //Log.Info(SProgramParser.Parse("(lambda (x_58824) (fold x_58824 (shl1 (or x_58824 (shr16 (plus (if0 (plus (plus (shl1 0) x_58824) x_58824) 1 0) 0)))) (lambda (x_58825 x_58826) (if0 x_58826 x_58825 x_58826))))"));

            //IEnumerable<Problem> problems = client.GetProblems();

            // create a solver controller
            var controller = new SimpleController(client);

            controller.Train(6);
            //controller.Guess();

            Log.Info("Finished run");
            Console.ReadKey();
        }
    }
}
