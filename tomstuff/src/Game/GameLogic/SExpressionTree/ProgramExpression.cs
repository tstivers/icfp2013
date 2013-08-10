using System;

namespace GameClient.SExpressionTree
{
    public class ProgramExpression : IExpression
    {
        public IdExpression Id { get; set; }
        public IExpression Expression { get; set; }

        public ProgramExpression(IdExpression id, IExpression expression)
        {
            Id = id;
            Expression = expression;
        }

        #region IExpression Members

        public int Size { get { return 1 + Expression.Size; } }

        public ulong Eval(EvalContext context)
        {
            return Expression.Eval(context);
        }

        #endregion

        public ulong[] Eval(ulong[] input)
        {
            var output = new ulong[input.Length];
            for (var i = 0; i < input.Length; i++)
                output[i] = Eval(input[i]);

            return output;
        }

        public ulong Eval(ulong input)
        {
            var context = new EvalContext();

            context[Id] = new NumberExpression(input);

            return Eval(context);
        }

        public override string ToString()
        {
            return String.Format("(lambda ({0}) {1})", Id, Expression);
        }
    }
}
