using System.Collections.Generic;

namespace GameClient.SExpressionTree
{
    public class EvalContext : Dictionary<IdExpression, IExpression>
    {
        public EvalContext()
        {
        }

        public EvalContext(EvalContext parent) : base(parent)
        {
        }

        public EvalContext(IdExpression id, IExpression value)
        {
            this[id] = value;
        }
    }
}
