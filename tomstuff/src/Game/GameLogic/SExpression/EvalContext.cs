using System.Collections.Generic;

namespace GameClient.SExpression
{
    public class EvalContext : Dictionary<SId, SExpression>
    {
        public EvalContext()
        {
        }

        public EvalContext(EvalContext parent) : base(parent)
        {
        }
    }
}
