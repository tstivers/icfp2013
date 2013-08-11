using System;
using System.Collections.Generic;
using System.Linq;

namespace GameClient.ViewModels
{
    public class Problem
    {
        public string Challenge { get; set; }
        public string Id { get; set; }
        public int Size { get; set; }
        public List<string> Operators { get; set; }
        public bool Solved { get; set; }
        public double? TimeLeft { get; set; }
        
        public override string ToString()
        {
            var s = String.Format("Id: {0}\nSize: {1}\nOperators: [{2}] {3}", Id, Size, Operators.Count, String.Join(",", Operators.Select(x => "\"" + x + "\"")));
            //if (Challenge != null)
            //    s += String.Format("\nChallenge: {0}", Challenge);

            return s;
        }
    }
}
