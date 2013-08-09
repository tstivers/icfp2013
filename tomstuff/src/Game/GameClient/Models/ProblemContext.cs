using System;
using GameClient.Services;

namespace GameClient.Models
{
    public abstract class ProblemContext
    {
        private readonly IGameClient _client;

        public ProblemContext(IGameClient client)
        {
            _client = client;
        }

        public abstract long[] Eval(long[] inputs);
    }

    public class TrainingProblemContext : ProblemContext
    {
        public TrainingProblemContext(IGameClient client) : base(client)
        {
        }

        public override long[] Eval(long[] inputs)
        {
            throw new NotImplementedException();
        }
    }
}
