using System.Collections.Generic;

namespace GameClient.ViewModels
{
    public class Problem
    {
        public string Challenge { get; set; }
        public string Id { get; set; }
        public long Size { get; set; }
        public List<string> Operators { get; set; }
        public bool Solved { get; set; }
        public long TimeLeft { get; set; }
    }
}
