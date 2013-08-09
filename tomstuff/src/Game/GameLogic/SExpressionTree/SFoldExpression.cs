using System;

namespace GameClient.SExpressionTree
{
    public class SFoldExpression : SExpression
    {
        public SExpression Expression1 { get; set; }
        public SExpression Expression2 { get; set; }
        public SIdExpression Id1 { get; set; }
        public SIdExpression Id2 { get; set; }
        public SExpression Expression3 { get; set; }
        public override int Size { get { return 2 + Expression1.Size + Expression2.Size + Expression3.Size; } }

        public SFoldExpression(SExpression expression1, SExpression expression2, SIdExpression id1, SIdExpression id2,
            SExpression expression3)
        {
            Expression1 = expression1;
            Expression2 = expression2;
            Id1 = id1;
            Id2 = id2;
            Expression3 = expression3;
        }

        public override string ToString()
        {
            return String.Format("(fold {0} {1} (lambda ({2} {3}) {4}))", Expression1, Expression2, Id1, Id2,
                Expression3);
        }

        public override ulong Eval(EvalContext context)
        {
            var e0 = Expression1.Eval(context);
            var e1 = Expression2.Eval(context);

            var lcontext = new EvalContext(context);

            foreach (var b in BitConverter.GetBytes(e1))
            {
                context[Id1] = new SNumber(b);
                context[Id2] = new SNumber(e1);
            }

            throw new NotImplementedException();

            return 0;
        }
    }
}
