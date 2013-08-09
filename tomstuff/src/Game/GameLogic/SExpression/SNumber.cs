namespace SExpression
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

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
