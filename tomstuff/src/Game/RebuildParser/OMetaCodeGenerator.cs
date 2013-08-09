using System.IO;
using OMetaSharp;

namespace SExpression.Utils.RebuildParser
{
    public class OMetaCodeGenerator
    {
        public static void RebuildParser()
        {
            string contents = File.ReadAllText(@"..\..\..\GameLogic\SExpression\SExpression.ometacs");
            string result =
                Grammars.ParseGrammarThenOptimizeThenTranslate<OMetaParser, OMetaOptimizer, OMetaTranslator>(contents,
                    p => p.Grammar, o => o.OptimizeGrammar, t => t.Trans);

            File.WriteAllText(@"..\..\..\GameLogic\SExpression\GeneratedCode\SExpressionParser.cs", result);
        }
    }
}
