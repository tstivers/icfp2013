using System;

namespace GameClient.SExpressionTree
{
    public class ProgramExpression : IExpression
    {
        private readonly IExpression _e0;
        private readonly IdExpression _id;

        public ProgramExpression(IdExpression id, IExpression e0)
        {
            _id = id;
            _e0 = e0;
        }

        #region IExpression Members

        public int Size { get { return 1 + _e0.Size; } }

        public ulong Eval(EvalContext context)
        {
            return _e0.Eval(context);
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

            context[_id] = new NumberExpression(input);

            return Eval(context);
        }

        public override string ToString()
        {
            return String.Format("(lambda ({0}) {1})", _id, _e0);
        }
    }
}
