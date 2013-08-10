using System;

namespace GameClient.SExpressionTree
{
    public class Op2Expression : IExpression
    {
        public string Op { get; set; }
        public IExpression Expression1 { get; set; }
        public IExpression Expression2 { get; set; }

        public Op2Expression(string op, IExpression expression1, IExpression expression2)
        {
            Op = op;
            Expression1 = expression1;
            Expression2 = expression2;
        }

        #region IExpression Members

        public int Size { get { return 1 + Expression1.Size + Expression2.Size; } }

        public ulong Eval(EvalContext context)
        {
            var val1 = Expression1.Eval(context);
            var val2 = Expression2.Eval(context);

            switch (Op)
            {
                case "and":
                    return val1 & val2;
                case "or":
                    return val1 | val2;
                case "xor":
                    return val1 ^ val2;
                case "plus":
                    return val1 + val2;
            }

            throw new NotImplementedException(String.Format("Operation {0} is not implemented", Op));
        }

        #endregion

        public override string ToString()
        {
            return String.Format("({0} {1} {2})", Op, Expression1, Expression2);
        }
    }
}
