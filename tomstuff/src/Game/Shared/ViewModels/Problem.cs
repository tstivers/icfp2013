namespace Shared.ViewModels
{
    public class Problem
    {
        public string Id { get; set; }
        public long Size { get; set; }
        public string[] Operators { get; set; }
        public bool Solved { get; set; }
        public long TimeLeft { get; set; }
    }
}
