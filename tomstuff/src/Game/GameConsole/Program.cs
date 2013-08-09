using System.Collections.Generic;
using System.Diagnostics;
using Shared.ViewModels;

namespace GameConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new GameClient.GameClient("0236vFGLj5xt5pYPigGQyveWYTGMysQaU4Ot3siN");

            IEnumerable<Problem> problems = client.GetProblems();

            Debugger.Break();
        }
    }
}
