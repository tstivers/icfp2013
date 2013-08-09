using GameClient.Services;
using GameClient.ViewModels;

namespace GameClient.Solvers
{
    public abstract class SolverBase
    {
        private IGameClient _client;

        public SolverBase(IGameClient client)
        {
            _client = client;
        }

        public abstract bool CanSolve(Problem p);

        public abstract void Solve(Problem p);
    }
}
