namespace GameClient.SExpressionTree
{
    public interface IExpression
    {
        int Size { get; }
        ulong Eval(EvalContext context);
    }
}
