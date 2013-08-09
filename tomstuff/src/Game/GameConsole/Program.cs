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

            //var programs = new SProgramGenerator(10, new[] {"not", "xor", "and", "plus", "shl1", "shr16"}).GeneratePrograms();                

            //Log.InfoFormat("Generated {0} programs", programs.Count());

            //IEnumerable<Problem> problems = client.GetProblems();

            // create a solver controller
            var controller = new SimpleController(client);

            while (true)
            {
                controller.Train(5);
                Console.ReadKey();
            }

            //controller.Guess();

            Log.Info("Finished run");
            Console.ReadKey();
        }
    }
}
