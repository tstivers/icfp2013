using System;

namespace SExpression
{
    public class SId : SExpression
    {
        public string Id { get; set; }       

        public SId(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return String.Format("[{0}]", Id);
        }
    }
}
