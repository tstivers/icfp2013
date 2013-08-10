namespace GameClient.SExpressionTree
{
    public class NumberExpression : IExpression
    {
        public ulong Number { get; set; }

        public NumberExpression(string value)
        {
            ulong num;
            ulong.TryParse(value, out num);
            Number = num;
        }

        public NumberExpression(ulong value)
        {
            Number = value;
        }

        #region IExpression Members

        public int Size { get { return 1; } }

        public ulong Eval(EvalContext context)
        {
            return Number;
        }

        #endregion

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
