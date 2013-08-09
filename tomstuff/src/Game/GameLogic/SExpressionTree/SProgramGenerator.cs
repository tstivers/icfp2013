using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;

namespace GameClient.SExpressionTree
{
    public class SProgramGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string OPNAME = "y";
        private readonly string[][] opPool;
        public int Size { get; set; }
        public string[] Operators { get; set; }

        public SProgramGenerator(int size, string[] operators)
        {
            Size = size;
            Operators = operators;

            opPool = new string[6][];
            opPool[1] = new[] {"0", "1", OPNAME};
            opPool[2] = new[] {"not", "shl1", "shr1", "shr4", "shr16"}.Intersect(Operators).ToArray();
            opPool[3] = new[] {"and", "or", "xor", "plus"}.Intersect(Operators).ToArray();
            opPool[4] = new[] {"if0"}.Intersect(Operators).ToArray();
            opPool[5] = new[] {"fold"}.Intersect(Operators).ToArray();
        }

        public IEnumerable<SProgram> GeneratePrograms()
        {
            Log.InfoFormat("Generating all programs of size {0} using operators {1}", Size,
                String.Join(",", Operators.Select(x => "\"" + x + "\"")));
            return GenerateExpressions(Size - 1).Where(x =>
            {
                var p = x.ToString();
                foreach (var op in Operators)
                {
                    if (!p.Contains(op))
                        return false;
                }

                if (!p.Contains(OPNAME))
                    return false;

                return true;
            }).Select(x => new SProgram(new SIdExpression(OPNAME), x));
        }

        private IEnumerable<SExpression> GenerateExpressions(int size)
        {
            var expressions = new List<SExpression>();

            if (size == 1)
                return new SExpression[] {new SIdExpression(OPNAME), new SNumber(0), new SNumber(1)};

            if (size >= 2)
            {
                foreach (var op1 in opPool[2])
                    expressions.AddRange(GenerateOp1Expressions(op1, size - 1));
            }

            if (size >= 3)
            {
                foreach (var op2 in opPool[3])
                    expressions.AddRange(GenerateOp2Expressions(op2, size - 1));
            }

            if (size >= 4)
            {
                foreach (var if0 in opPool[4])
                    expressions.AddRange(GenerateIf0Expressions(size - 1));
            }

            return expressions;
        }

        private IEnumerable<SOp1Expression> GenerateOp1Expressions(string op, int size)
        {
            return GenerateExpressions(size).Select(x => new SOp1Expression(op, x));
        }

        private IEnumerable<SOp2Expression> GenerateOp2Expressions(string op, int size)
        {
            var e1 = new List<SExpression>();
            var o = new List<SOp2Expression>();

            for (var i = 1; i < size; i++)
                e1.AddRange(GenerateExpressions(i));

            foreach (var exp in e1)
                o.AddRange(GenerateExpressions(size - exp.Size).Select(x => new SOp2Expression(op, exp, x)));

            return o;
        }

        private IEnumerable<SIf0Expression> GenerateIf0Expressions(int size)
        {
            var e1 = new List<SExpression>();
            var e2 = new List<Tuple<SExpression, SExpression>>();
            var o = new List<SIf0Expression>();

            for (var i = 1; i < size; i++)
                e1.AddRange(GenerateExpressions(i));

            foreach (var exp in e1)
            {
                e2.AddRange(GenerateExpressions(size - exp.Size)
                    .Select(x => new Tuple<SExpression, SExpression>(exp, x)));
            }

            foreach (var exp in e2)
            {
                o.AddRange(
                    GenerateExpressions(size - exp.Item1.Size - exp.Item2.Size)
                        .Select(x => new SIf0Expression(exp.Item1, exp.Item2, x)));
            }

            return o;
        }
    }
}
