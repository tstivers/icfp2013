namespace GameClient.SExpressionTree
{
    public class NumberExpression : IExpression
    {
        private readonly ulong _value;

        public NumberExpression(string value)
        {
            ulong num;
            ulong.TryParse(value, out num);
            _value = num;
        }

        public NumberExpression(ulong value)
        {
            _value = value;
        }

        #region IExpression Members

        public int Size { get { return 1; } }

        public ulong Eval(EvalContext context)
        {
            return _value;
        }

        #endregion

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
