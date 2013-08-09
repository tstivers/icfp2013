using System;

namespace SExpression
{
    public class SOp2Expression : SExpression
    {
        public string Op { get; set; }
        public SExpression Expression1 { get; set; }
        public SExpression Expression2 { get; set; }        

        public SOp2Expression(string op, SExpression expression1, SExpression expression2)
        {
            Op = op;
            Expression1 = expression1;
            Expression2 = expression2;
        }

        public override string ToString()
        {
            return String.Format("({0} {1} {2})", Op, Expression1, Expression2);
        }
    }
}
