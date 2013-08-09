namespace SExpression
{
    public class SProgram
    {
        public SId Id { get; set; }
        public SExpression Expression { get; set; }

        public SProgram(SId id, SExpression expression)
        {
            Id = id;
            Expression = expression;
        }
    }
}
