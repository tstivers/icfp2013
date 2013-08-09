using System.Collections.Generic;

namespace GameClient.SExpressionTree
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
