using System;

namespace GameClient.SExpressionTree
{
    public class Op1Expression : IExpression
    {
        public string Op { get; set; }
        public IExpression Expression { get; set; }

        public Op1Expression(string op, IExpression expression)
        {
            Op = op;
            Expression = expression;
        }

        #region IExpression Members

        public int Size { get { return 1 + Expression.Size; } }

        public ulong Eval(EvalContext context)
        {
            var val = Expression.Eval(context);

            switch (Op)
            {
                case "not":
                    return ~val;
                case "shl1":
                    return val << 1;
                case "shr1":
                    return val >> 1;
                case "shr4":
                    return val >> 4;
                case "shr16":
                    return val >> 16;
            }

            throw new NotImplementedException(String.Format("Operation {0} is not implemented", Op));
        }

        #endregion

        public override string ToString()
        {
            return String.Format("({0} {1})", Op, Expression);
        }
    }
}
