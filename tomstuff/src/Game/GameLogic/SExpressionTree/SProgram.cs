using System;

namespace GameClient.SExpressionTree
{
    public class SProgram : SExpression
    {
        public SIdExpression Id { get; set; }
        public SExpression Expression { get; set; }
        public override int Size { get { return 1 + Expression.Size; } }

        public SProgram(SIdExpression id, SExpression expression)
        {
            Id = id;
            Expression = expression;
        }

        public ulong[] Eval(ulong[] input)
        {
            var output = new ulong[input.Length];
            for (var i = 0; i < input.Length; i++)
                output[i] = Eval(input[i]);

            return output;
        }

        private ulong Eval(ulong input)
        {
            var context = new EvalContext();

            context[Id] = new SNumber(input);

            return Eval(context);
        }

        public override string ToString()
        {
            return String.Format("(lambda ({0}) {1})", Id, Expression);
        }

        public override ulong Eval(EvalContext context)
        {
            return Expression.Eval(context);
        }
    }
}
