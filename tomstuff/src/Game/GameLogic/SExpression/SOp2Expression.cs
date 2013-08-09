using System;

namespace GameClient.SExpression
{
    public enum Op2Op
    {
        and,
        or,
        xor,
        plus
    }

    public class SOp2Expression : SExpression
    {
        public Op2Op Op { get; set; }
        public SExpression Expression1 { get; set; }
        public SExpression Expression2 { get; set; }

        public SOp2Expression(string op, SExpression expression1, SExpression expression2)
        {
            Op = (Op2Op) Enum.Parse(typeof(Op2Op), op);
            Expression1 = expression1;
            Expression2 = expression2;
        }

        public override string ToString()
        {
            return String.Format("({0} {1} {2})", Op, Expression1, Expression2);
        }

        public override ulong Eval(EvalContext context)
        {
            ulong val1 = Expression1.Eval(context);
            ulong val2 = Expression2.Eval(context);

            switch (Op)
            {
                case Op2Op.and:
                    return val1 & val2;
                case Op2Op.or:
                    return val1 | val2;
                case Op2Op.xor:
                    return val1 ^ val2;
                case Op2Op.plus:
                    return val1 + val2;
            }

            throw new NotImplementedException(String.Format("Operation {0} is not implemented", Op));
        }
    }
}
