using System;

namespace GameClient.SExpressionTree
{
    public class If0Expression : IExpression
    {
        public IExpression Expression1 { get; set; }
        public IExpression Expression2 { get; set; }
        public IExpression Expression3 { get; set; }

        public If0Expression(IExpression expression1, IExpression expression2, IExpression expression3)
        {
            Expression1 = expression1;
            Expression2 = expression2;
            Expression3 = expression3;
        }

        #region IExpression Members

        public int Size { get { return 1 + Expression1.Size + Expression2.Size + Expression3.Size; } }

        public ulong Eval(EvalContext context)
        {
            var condition = Expression1.Eval(context);

            return condition == 0 ? Expression2.Eval(context) : Expression3.Eval(context);
        }

        #endregion

        public override string ToString()
        {
            return String.Format("(if0 {0} {1} {2})", Expression1, Expression2, Expression3);
        }
    }
}
