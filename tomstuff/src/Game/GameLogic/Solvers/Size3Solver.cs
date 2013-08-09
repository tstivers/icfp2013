using System.Diagnostics;
using System.Reflection;
using GameClient.Services;
using GameClient.ViewModels;
using log4net;

namespace GameClient.Solvers
{
    public class Size3Solver : SolverBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Size3Solver(IGameClient client) : base(client)
        {
        }

        public override bool CanSolve(Problem p)
        {
            return p.Size == 3;
        }

        public override bool Solve(Problem p)
        {
            var inputs = new ulong[] {0x0000000000000000, // if NOT will end up 0xFFFFFFFFFFFFFFFFFFFFFFFFF
                0x0000000000000001, // if shl1 will end up 0x0000000000000002, shr1 will end up 0
                0xFF00000000000000 //
            };

            var outputs = _client.Eval(p.Id, "(lambda (x) x)", inputs);

            if (outputs[0] == 0xFFFFFFFFFFFFFFFF)
                return _client.Guess(p.Id, "(lambda (x) (not x))");

            if (outputs[1] == 0x0000000000000002)
                return _client.Guess(p.Id, "(lambda (x) (shl1 x))");

            if (outputs[2] == 0x0ff0000000000000)
                return _client.Guess(p.Id, "(lambda (x) (shr4 x))");

            if (outputs[2] == 0x0000ff0000000000)
                return _client.Guess(p.Id, "(lambda (x) (shr16 x))");

            if (outputs[1] == 0x0000000000000000)
                return _client.Guess(p.Id, "(lambda (x) (shr1 x))");

            Debugger.Break();

            return false;
        }
    }
}
