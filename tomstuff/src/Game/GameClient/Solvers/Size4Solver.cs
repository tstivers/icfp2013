using System.Diagnostics;
using System.Reflection;
using GameClient.Services;
using GameClient.ViewModels;
using log4net;

namespace GameClient.Solvers
{
    public class Size4Solver : SolverBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Size4Solver(IGameClient client) : base(client)
        {
        }

        public override bool CanSolve(Problem p)
        {
            return p.Size == 4 && !p.Operators.Contains("fold");
        }

        public override bool Solve(Problem p)
        {
            var inputs = new ulong[] {0x0000000000000000, // if NOT will end up 0xFFFFFFFFFFFFFFFFFFFFFFFFF
                0x0000000000000001, // if shl1 will end up 0x0000000000000002, shr1 will end up 0
                0xFF00000000000000 //
            };

            Debugger.Break();

            return false;
        }
    }
}
