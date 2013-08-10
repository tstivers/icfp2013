using GameClient.SExpressionTree.GeneratedCode;
using OMetaSharp;

namespace GameClient.SExpressionTree
{
    public class SProgramParser
    {
        public static ProgramExpression Parse(string sexpr)
        {
            return Grammars.ParseWith<SExpressionParser>(sexpr, x => x.SProgram).As<ProgramExpression>();
        }
    }
}
