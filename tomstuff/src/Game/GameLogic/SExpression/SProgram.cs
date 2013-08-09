namespace GameClient.SExpression
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

        public ulong[] Eval(ulong[] input)
        {
            var output = new ulong[input.Length];
            for (int i = 0; i < input.Length; i++)
                output[i] = Eval(input[i]);

            return output;
        }

        private ulong Eval(ulong input)
        {
            var context = new EvalContext();

            context[Id] = new SNumber(input);

            return Expression.Eval(context);
        }
    }
}
