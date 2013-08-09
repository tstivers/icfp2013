using System;

namespace SExpression
{
    public class SOp1Expression : SExpression
    {
        public string Op { get; set; }
        public SExpression Expression { get; set; }        

        public SOp1Expression(string op, SExpression expression)
        {
            Op = op;
            Expression = expression;
        }

        public override string ToString()
        {
            return String.Format("({0} {1})", Op, Expression);
        }
    }
}
