using System;

namespace GameClient.SExpressionTree
{
    public enum Op1Codes : byte
    {
        not,
        shl1,
        shr1,
        shr4,
        shr16
    }

    public class Op1Expression : IExpression
    {
        private readonly IExpression _e0;
        private readonly Op1Codes _opCode;

        public Op1Expression(Op1Codes opCode, IExpression e0)
        {
            _opCode = opCode;
            _e0 = e0;
        }

        public Op1Expression(string opCode, IExpression e0)
        {
            _opCode = (Op1Codes)Enum.Parse(typeof(Op1Codes), opCode);
            _e0 = e0;
        }

        #region IExpression Members

        public int Size { get { return 1 + _e0.Size; } }

        public ulong Eval(EvalContext context)
        {
            var val = _e0.Eval(context);

            switch (_opCode)
            {
                case Op1Codes.not:
                    return ~val;
                case Op1Codes.shl1:
                    return val << 1;
                case Op1Codes.shr1:
                    return val >> 1;
                case Op1Codes.shr4:
                    return val >> 4;
                case Op1Codes.shr16:
                    return val >> 16;
            }

            throw new NotImplementedException(String.Format("Operation {0} is not implemented", _opCode));
        }

        #endregion

        public override string ToString()
        {
            return String.Format("({0} {1})", _opCode, _e0);
        }
    }
}
