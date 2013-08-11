using System;

namespace GameClient.SExpressionTree
{
    public enum Op2Codes : byte
    {
        and,
        or,
        xor,
        plus,        
    }

    public class Op2Expression : IExpression
    {
        private readonly IExpression _e0;
        private readonly IExpression _e1;
        private readonly Op2Codes _opCode;

        public Op2Expression(Op2Codes opCode, IExpression e0, IExpression e1)
        {
            _opCode = opCode;
            _e0 = e0;
            _e1 = e1;
        }

        public Op2Expression(string opCode, IExpression e0, IExpression e1)
        {
            _opCode = (Op2Codes)Enum.Parse(typeof(Op2Codes), opCode);
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
                case Op2Codes.and:
                    return val1 & val2;
                case Op2Codes.or:
                    return val1 | val2;
                case Op2Codes.xor:
                    return val1 ^ val2;
                case Op2Codes.plus:
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
