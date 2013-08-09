namespace GameClient.SExpression
{
    public abstract class SExpression
    {
        public abstract ulong Eval(EvalContext context);
    }
}
