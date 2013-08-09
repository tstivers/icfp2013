using System;
using System.Collections.Generic;
using System.Reflection;
using GameClient.Controllers;
using GameClient.Services;
using GameClient.SExpression;
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

            SProgramParser.PrettyPrint(
                "(lambda (x_65154) (fold (and (and (plus (not (shl1 (if0 (plus (plus 1 (shr4 x_65154)) 1) x_65154 x_65154))) 0) x_65154) 0) x_65154 (lambda (x_65155 x_65156) (shr4 (or x_65156 x_65155)))))");

            SProgram program = SProgramParser.Parse("(lambda (x_65154) (xor 1 1))");

            ulong[] output = program.Eval(new ulong[] {1, 2});

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
