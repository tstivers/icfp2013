using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameClient.Services;
using GameClient.ViewModels;

namespace GameClient.Solvers
{
    public class StupidSolver : SolverBase
    {
        public StupidSolver(IGameClient client) : base(client)
        {
        }

        public override bool CanSolve(Problem p)
        {
            throw new NotImplementedException();
        }

        public override void Solve(Problem p)
        {
            throw new NotImplementedException();
        }
    }
}
