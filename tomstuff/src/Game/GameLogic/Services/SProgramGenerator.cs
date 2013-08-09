using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameClient.SExpressionTree;
using log4net;

namespace GameClient.Services
{
    public class SProgramGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string[][] opPool;
        public int Size { get; set; }
        public string[] Operators { get; set; }

        private List<SExpression> _expressionPool = new List<SExpression>();

        public SProgramGenerator(int size, string[] operators)
        {
            Size = size;
            Operators = operators;

            opPool = new string[6][];
            opPool[1] = new[] {"0", "1", "x"};
            opPool[2] = new[] {"not", "shl1", "shr1", "shr4", "shr16"}.Intersect(Operators).ToArray();
            opPool[3] = new[] {"and", "or", "xor", "plus"}.Intersect(Operators).ToArray();
            opPool[4] = new[] {"if0"}.Intersect(Operators).ToArray();
            opPool[5] = new[] {"fold"}.Intersect(Operators).ToArray();
        }

        public SProgram[] GeneratePrograms()
        {
            Log.InfoFormat("Generating all programs of size {0} using operators {1}", Size, String.Join(",", Operators.Select(x => "\"" + x + "\"")));
            throw new NotImplementedException();
        }

        private SExpression GenerateExpression(int size)
        {
            throw new NotImplementedException();
        }
    }
}
