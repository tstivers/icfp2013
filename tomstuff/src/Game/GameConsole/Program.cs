using System;
using System.Reflection;
using GameClient.Controllers;
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

            // create a solver controller
            var controller = new SimpleController(client);

            var rnd = new Random();

            while (!Console.KeyAvailable)
                controller.Train(rnd.Next(14, 30));
            Console.ReadKey(true);

            //controller.Guess();

            Log.Info("Finished run");
            Console.ReadKey(true);
        }
    }
}
