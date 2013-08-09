using System;

namespace GameClient.SExpressionTree
{
    public class SIf0Expression : SExpression
    {
        public SExpression Expression1 { get; set; }
        public SExpression Expression2 { get; set; }
        public SExpression Expression3 { get; set; }

        public SIf0Expression(SExpression expression1, SExpression expression2, SExpression expression3)
        {
            Expression1 = expression1;
            Expression2 = expression2;
            Expression3 = expression3;
        }

        public override string ToString()
        {
            return String.Format("(if0 {0} {1} {2})", Expression1, Expression2, Expression3);
        }

        public override int Size { get { return 1 + Expression1.Size + Expression2.Size + Expression3.Size; } }

        public override ulong Eval(EvalContext context)
        {
            var condition = Expression1.Eval(context);

            return condition == 0 ? Expression2.Eval(context) : Expression3.Eval(context);
        }
    }
}
