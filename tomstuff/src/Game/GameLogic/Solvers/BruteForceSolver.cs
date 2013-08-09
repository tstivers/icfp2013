using System.Linq;
using System.Reflection;
using GameClient.Services;
using GameClient.SExpressionTree;
using GameClient.ViewModels;
using log4net;

namespace GameClient.Solvers
{
    public class BruteForceSolver : SolverBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BruteForceSolver(IGameClient client) : base(client)
        {
        }

        public override bool CanSolve(Problem p)
        {
            return p.Size <= 6 && (!p.Operators.Contains("fold") || !p.Operators.Contains("tfold"));
        }

        public override bool Solve(Problem p)
        {
            var generator = new SProgramGenerator(p.Size, p.Operators.ToArray());
            var programs = generator.GeneratePrograms();

            foreach (var program in programs)
                Log.Debug(program.ToString());

            Log.InfoFormat("Generated {0} programs to check", programs.Count());          

            return false;
        }
    }
}
