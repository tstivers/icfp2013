using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly ConcurrentDictionary<int, List<SExpression>> _expressionCache = new ConcurrentDictionary<int, List<SExpression>>();
        private readonly ConcurrentDictionary<Tuple<int, string>, List<SOp1Expression>> _op1ExpressionCache = new ConcurrentDictionary<Tuple<int, String>, List<SOp1Expression>>();
        private readonly ConcurrentDictionary<Tuple<int, String>, List<SOp2Expression>> _op2ExpressionCache = new ConcurrentDictionary<Tuple<int, String>, List<SOp2Expression>>();
        private readonly ConcurrentDictionary<int, List<SIf0Expression>> _if0ExpressionCache = new ConcurrentDictionary<int, List<SIf0Expression>>();

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

            var sw = new Stopwatch();
            sw.Start();

            var results = GenerateExpressions(Size - 1).Where(x =>
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

            sw.Stop();

            Log.InfoFormat("Generated {0} programs in {1:c}", results.Count(), sw.Elapsed);

            return results;
        }

        private IEnumerable<SExpression> GenerateExpressions(int size)
        {
            List<SExpression> cached;
            if (_expressionCache.TryGetValue(size, out cached))
                return cached;

            if (_expressionCache.ContainsKey(size))
                return _expressionCache[size];

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

            _expressionCache[size] = expressions;
            return expressions;
        }

        private IEnumerable<SOp1Expression> GenerateOp1Expressions(string op, int size)
        {
            List<SOp1Expression> cached;
            if (_op1ExpressionCache.TryGetValue(new Tuple<int, string>(size, op), out cached))
                return cached;

            if (_op1ExpressionCache.ContainsKey(new Tuple<int, string>(size, op)))
                return _op1ExpressionCache[new Tuple<int, string>(size, op)];


            var results = GenerateExpressions(size).Select(x => new SOp1Expression(op, x));
            _op1ExpressionCache[new Tuple<int, string>(size, op)] = results.ToList();

            return results;
        }

        private IEnumerable<SOp2Expression> GenerateOp2Expressions(string op, int size)
        {
            List<SOp2Expression> cached;
            if (_op2ExpressionCache.TryGetValue(new Tuple<int, string>(size, op), out cached))
                return cached;

            var e1 = new List<SExpression>();
            var results = new List<SOp2Expression>();

            for (var i = 1; i < size; i++)
                e1.AddRange(GenerateExpressions(i));

            foreach (var exp in e1)
                results.AddRange(GenerateExpressions(size - exp.Size).Select(x => new SOp2Expression(op, exp, x)));

            _op2ExpressionCache[new Tuple<int, string>(size, op)] = results.ToList();

            return results;
        }

        private IEnumerable<SIf0Expression> GenerateIf0Expressions(int size)
        {
            var e1 = new List<SExpression>();
            var e2 = new List<Tuple<SExpression, SExpression>>();
            var o = new List<SIf0Expression>();

            for (var i = 1; i < size - 1; i++)
                e1.AddRange(GenerateExpressions(i));

            foreach (var exp in e1)
            {
                for (var i = 1; size - exp.Size - i > 0; i++)
                    e2.AddRange(GenerateExpressions(size - exp.Size - i)
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

