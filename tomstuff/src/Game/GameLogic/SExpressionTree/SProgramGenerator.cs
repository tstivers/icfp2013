using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;

namespace GameClient.SExpressionTree
{
    public class SProgramGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string OPNAME = "y";

        private readonly ConcurrentDictionary<int, List<IExpression>> _expressionCache =
            new ConcurrentDictionary<int, List<IExpression>>();

        private readonly ConcurrentDictionary<int, List<If0Expression>> _if0ExpressionCache =
            new ConcurrentDictionary<int, List<If0Expression>>();

        private readonly ConcurrentDictionary<Tuple<int, string>, List<Op1Expression>> _op1ExpressionCache =
            new ConcurrentDictionary<Tuple<int, String>, List<Op1Expression>>();

        private readonly ConcurrentDictionary<Tuple<int, String>, List<Op2Expression>> _op2ExpressionCache =
            new ConcurrentDictionary<Tuple<int, String>, List<Op2Expression>>();

        private readonly string[][] opPool;
        public int Size { get; set; }
        public string[] Operators { get; set; }

        public SProgramGenerator(int size, string[] operators)
        {
            Size = size;
            Operators = operators.Where(x => x != "bonus").ToArray();

            opPool = new string[6][];
            opPool[1] = new[] {"0", "1", OPNAME};
            opPool[2] = new[] {"not", "shl1", "shr1", "shr4", "shr16"}.Intersect(Operators).ToArray();
            opPool[3] = new[] {"and", "or", "xor", "plus"}.Intersect(Operators).ToArray();
            opPool[4] = new[] {"if0"}.Intersect(Operators).ToArray();
            opPool[5] = new[] {"fold"}.Intersect(Operators).ToArray();
        }

        public List<ProgramExpression> GeneratePrograms()
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
            }).Select(x => new ProgramExpression(new IdExpression(OPNAME), x)).ToList();

            Log.InfoFormat("Generated {0} programs in {1:c}", results.Count(), sw.Elapsed);

            return results;
        }

        private List<IExpression> GenerateExpressions(int size)
        {
            List<IExpression> cached;
            if (_expressionCache.TryGetValue(size, out cached))
                return cached;

            if (_expressionCache.ContainsKey(size))
                return _expressionCache[size];

            var expressions = new List<IExpression>();

            if (size == 1)
            {
                return new List<IExpression>
                {
                    new IdExpression(OPNAME),
                    new NumberExpression(0),
                    new NumberExpression(1)
                };
            }

            if (size >= 2)
            {
                //foreach (var op1 in opPool[2])
                Parallel.ForEach(opPool[2], op1 =>
                {
                    var op1s = GenerateOp1Expressions(op1, size - 1);
                    lock (expressions)
                    {
                        expressions.AddRange(op1s);
                    }
                });
            }

            if (size >= 3)
            {
                Parallel.ForEach(opPool[3], op2 =>
                {
                    var op2s = GenerateOp2Expressions(op2, size - 1);
                    lock (expressions)
                    {
                        expressions.AddRange(op2s);
                    }
                });
            }

            if (size >= 4)
            {
                foreach (var if0 in opPool[4])
                    expressions.AddRange(GenerateIf0Expressions(size - 1));
            }

            _expressionCache[size] = expressions;
            return expressions;
        }

        private List<Op1Expression> GenerateOp1Expressions(string op, int size)
        {
            List<Op1Expression> cached;
            if (_op1ExpressionCache.TryGetValue(new Tuple<int, string>(size, op), out cached))
                return cached;

            var results = GenerateExpressions(size).Select(x => new Op1Expression(op, x)).ToList();
            _op1ExpressionCache[new Tuple<int, string>(size, op)] = results;

            return results;
        }

        private List<Op2Expression> GenerateOp2Expressions(string op, int size)
        {
            List<Op2Expression> cached;
            if (_op2ExpressionCache.TryGetValue(new Tuple<int, string>(size, op), out cached))
                return cached;

            var e1 = new List<IExpression>();
            var results = new List<Op2Expression>();

            for (var i = 1; i < size; i++)
                e1.AddRange(GenerateExpressions(i));

            foreach (var exp in e1)
                results.AddRange(GenerateExpressions(size - exp.Size).Select(x => new Op2Expression(op, exp, x)));

            _op2ExpressionCache[new Tuple<int, string>(size, op)] = results;

            return results;
        }

        private List<If0Expression> GenerateIf0Expressions(int size)
        {
            List<If0Expression> cached;
            if (_if0ExpressionCache.TryGetValue(size, out cached))
                return cached;

            var e1 = new List<IExpression>();
            var e2 = new List<Tuple<IExpression, IExpression>>();
            var o = new List<If0Expression>();

            for (var i = 1; i < size - 1; i++)
                e1.AddRange(GenerateExpressions(i));

            foreach (var exp in e1)
            {
                for (var i = 1; size - exp.Size - i > 0; i++)
                {
                    e2.AddRange(
                        GenerateExpressions(size - exp.Size - i)
                            .Select(x => new Tuple<IExpression, IExpression>(exp, x)));
                }
            }

            foreach (var exp in e2)
            {
                o.AddRange(
                    GenerateExpressions(size - exp.Item1.Size - exp.Item2.Size)
                        .Select(x => new If0Expression(exp.Item1, exp.Item2, x)));
            }

            _if0ExpressionCache[size] = o;

            return o;
        }
    }
}
