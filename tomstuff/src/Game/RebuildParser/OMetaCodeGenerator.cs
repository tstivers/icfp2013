using System.IO;
using OMetaSharp;

namespace SExpression.Utils.RebuildParser
{
    public class OMetaCodeGenerator
    {
        public static void RebuildParser()
        {
            var contents = File.ReadAllText(@"..\..\..\GameLogic\SExpressionTree\SExpression.ometacs");
            var result =
                Grammars.ParseGrammarThenOptimizeThenTranslate<OMetaParser, OMetaOptimizer, OMetaTranslator>(contents,
                    p => p.Grammar, o => o.OptimizeGrammar, t => t.Trans);

            File.WriteAllText(@"..\..\..\GameLogic\SExpressionTree\GeneratedCode\SExpressionParser.cs", result);
        }
    }
}
