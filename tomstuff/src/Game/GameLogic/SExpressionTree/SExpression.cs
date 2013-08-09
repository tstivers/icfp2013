namespace GameClient.SExpressionTree
{
    public abstract class SExpression
    {
        public abstract int Size { get; }
        public abstract ulong Eval(EvalContext context);
    }
}
