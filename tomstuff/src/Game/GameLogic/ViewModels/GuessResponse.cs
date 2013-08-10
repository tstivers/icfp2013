using System.Collections.Generic;

namespace GameClient.ViewModels
{
    public class GuessResponse
    {
        public string Status { get; set; }
        public List<string> Values { get; set; }
        public string Message { get; set; }

        public bool IsCorrect { get { return Status == "win"; } }
    }
}
