namespace GameClient.SExpression
{
    public class SNumber : SExpression
    {
        public ulong Number { get; set; }

        public SNumber(string value)
        {
            ulong num;
            ulong.TryParse(value, out num);
            Number = num;
        }

        public SNumber(ulong value)
        {
            Number = value;
        }

        public override string ToString()
        {
            return Number.ToString();
        }

        public override ulong Eval(EvalContext context)
        {
            return Number;
        }
    }
}
