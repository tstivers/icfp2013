using System;

namespace GameClient.SExpressionTree
{
    public class If0Expression : IExpression
    {
        private readonly IExpression _e0;
        private readonly IExpression _e1;
        private readonly IExpression _e2;

        public If0Expression(IExpression expression1, IExpression expression2, IExpression expression3)
        {
            _e0 = expression1;
            _e1 = expression2;
            _e2 = expression3;
        }

        #region IExpression Members

        public int Size { get { return 1 + _e0.Size + _e1.Size + _e2.Size; } }

        public ulong Eval(EvalContext context)
        {
            var condition = _e0.Eval(context);

            return condition == 0 ? _e1.Eval(context) : _e2.Eval(context);
        }

        #endregion

        public override string ToString()
        {
            return String.Format("(if0 {0} {1} {2})", _e0, _e1, _e2);
        }
    }
}
