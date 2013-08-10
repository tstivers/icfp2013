using System;

namespace GameClient.SExpressionTree
{
    public class Op2Expression : IExpression
    {
        private readonly IExpression _e0;
        private readonly IExpression _e1;
        private readonly string _opCode;

        public Op2Expression(string opCode, IExpression e0, IExpression e1)
        {
            _opCode = opCode;
            _e0 = e0;
            _e1 = e1;
        }

        #region IExpression Members

        public int Size { get { return 1 + _e0.Size + _e1.Size; } }

        public ulong Eval(EvalContext context)
        {
            var val1 = _e0.Eval(context);
            var val2 = _e1.Eval(context);

            switch (_opCode)
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

            throw new NotImplementedException(String.Format("Operation {0} is not implemented", _opCode));
        }

        #endregion

        public override string ToString()
        {
            return String.Format("({0} {1} {2})", _opCode, _e0, _e1);
        }
    }
}
