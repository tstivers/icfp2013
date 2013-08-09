using System;

namespace GameClient.SExpressionTree
{
    public enum Op1Op
    {
        not,
        shl1,
        shr1,
        shr4,
        shr16
    }

    public class SOp1Expression : SExpression
    {
        public Op1Op Op { get; set; }
        public SExpression Expression { get; set; }

        public SOp1Expression(string op, SExpression expression)
        {
            Op = (Op1Op) Enum.Parse(typeof(Op1Op), op);
            Expression = expression;
        }

        public override string ToString()
        {
            return String.Format("({0} {1})", Op, Expression);
        }

        public override int Size { get { return 1 + Expression.Size; } }

        public override ulong Eval(EvalContext context)
        {
            ulong val = Expression.Eval(context);

            switch (Op)
            {
                case Op1Op.not:
                    return ~val;
                case Op1Op.shl1:
                    return val << 1;
                case Op1Op.shr1:
                    return val >> 1;
                case Op1Op.shr4:
                    return val >> 4;
                case Op1Op.shr16:
                    return val >> 16;
            }

            throw new NotImplementedException(String.Format("Operation {0} is not implemented", Op));
        }
    }
}
