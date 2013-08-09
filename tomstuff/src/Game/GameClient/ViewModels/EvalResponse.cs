using System.Collections.Generic;

namespace GameClient.ViewModels
{
    public class EvalResponse
    {
        public string Status { get; set; }
        public List<string> Outputs { get; set; }
        public string Message { get; set; }
    }
}
