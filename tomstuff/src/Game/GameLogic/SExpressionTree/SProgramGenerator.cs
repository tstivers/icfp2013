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
        private const string OpName = "y";

        private static readonly List<IExpression> Numbers = new List<IExpression>
        {
            new NumberExpression(0),
            new NumberExpression(1)
        };

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ConcurrentDictionary<Tuple<int, List<IdExpression>>, List<IExpression>> _expressionCache =
            new ConcurrentDictionary<Tuple<int, List<IdExpression>>, List<IExpression>>();

        private readonly ConcurrentDictionary<Tuple<int, List<IdExpression>>, List<If0Expression>> _if0ExpressionCache =
            new ConcurrentDictionary<Tuple<int, List<IdExpression>>, List<If0Expression>>();

        private readonly ConcurrentDictionary<Tuple<int, List<IdExpression>, string>, List<Op1Expression>> _op1ExpressionCache =
            new ConcurrentDictionary<Tuple<int, List<IdExpression>, string>, List<Op1Expression>>();

        private readonly ConcurrentDictionary<Tuple<int, List<IdExpression>, String>, List<Op2Expression>> _op2ExpressionCache =
            new ConcurrentDictionary<Tuple<int, List<IdExpression>, String>, List<Op2Expression>>();

        private readonly string[][] _opPool;
        public int Size { get; set; }
        public string[] Operators { get; set; }

        public SProgramGenerator(int size, string[] operators)
        {
            Size = size;
            Operators = operators.Where(x => x != "bonus").ToArray();

            _opPool = new string[6][];
            _opPool[1] = new[] {"0", "1"};
            _opPool[2] = new[] {"not", "shl1", "shr1", "shr4", "shr16"}.Intersect(Operators).ToArray();
            _opPool[3] = new[] {"and", "or", "xor", "plus"}.Intersect(Operators).ToArray();
            _opPool[4] = new[] {"if0"}.Intersect(Operators).ToArray();
            _opPool[5] = new[] {"fold"}.Intersect(Operators).ToArray();
        }

        public List<ProgramExpression> GeneratePrograms()
        {
            Log.InfoFormat("Generating all programs of size {0} using operators {1}", Size,
                String.Join(",", Operators.Select(x => "\"" + x + "\"")));

            var vars = new List<IdExpression> {new IdExpression(OpName)};

            var sw = new Stopwatch();
            sw.Start();

            var results = GenerateExpressions(Size - 1, vars).Where(x =>
            {
                var p = x.ToString();
                return Operators.All(p.Contains) && p.Contains(OpName);
            }).Select(x => new ProgramExpression(new IdExpression(OpName), x)).ToList();

            Log.InfoFormat("Generated {0} programs in {1:c}", results.Count(), sw.Elapsed);

            return results;
        }

        private List<IExpression> GenerateExpressions(int size, List<IdExpression> vars)
        {
            List<IExpression> cached;
            if (_expressionCache.TryGetValue(new Tuple<int, List<IdExpression>>(size, vars), out cached))
                return cached;          

            var expressions = new List<IExpression>();

            if (size == 1)
                return Numbers.Concat(vars).ToList();

            if (size >= 2)
            {                
                Parallel.ForEach(_opPool[2], op1 =>
                {
                    var ops = GenerateOp1Expressions(op1, size - 1, vars);
                    lock (expressions)
                    {
                        expressions.AddRange(ops);
                    }
                });
            }

            if (size >= 3)
            {
                Parallel.ForEach(_opPool[3], op2 =>
                {
                    var ops = GenerateOp2Expressions(op2, size - 1, vars);
                    lock (expressions)
                    {
                        expressions.AddRange(ops);
                    }
                });
            }

            if (size >= 4)
            {
                if(_opPool[4].Length != 0)
                    expressions.AddRange(GenerateIf0Expressions(size - 1, vars));
            }

            if (size >= 5)
            {
            }

            _expressionCache[new Tuple<int, List<IdExpression>>(size, vars)] = expressions;
            return expressions;
        }

        private List<Op1Expression> GenerateOp1Expressions(string op, int size, List<IdExpression> vars)
        {
            List<Op1Expression> cached;
            if (_op1ExpressionCache.TryGetValue(new Tuple<int, List<IdExpression>, string>(size, vars, op), out cached))
                return cached;

            var results = GenerateExpressions(size, vars).Select(x => new Op1Expression(op, x)).ToList();
            _op1ExpressionCache[new Tuple<int, List<IdExpression>, string>(size, vars, op)] = results;

            return results;
        }

        private List<Op2Expression> GenerateOp2Expressions(string op, int size, List<IdExpression> vars)
        {
            List<Op2Expression> cached;
            if (_op2ExpressionCache.TryGetValue(new Tuple<int, List<IdExpression>, string>(size, vars, op), out cached))
                return cached;

            var e1 = new List<IExpression>();
            var results = new List<Op2Expression>();

            Parallel.For(1, size - 1, i =>
            {
                var expressions = GenerateExpressions(i, vars);
                lock (e1)
                    e1.AddRange(expressions);
            });

            //for (var i = 1; i < size; i++)
            //    e1.AddRange(GenerateExpressions(i, vars));

            foreach (var exp in e1)
                results.AddRange(GenerateExpressions(size - exp.Size, vars).Select(x => new Op2Expression(op, exp, x)));

            _op2ExpressionCache[new Tuple<int, List<IdExpression>, string>(size, vars, op)] = results;

            return results;
        }

        private List<If0Expression> GenerateIf0Expressions(int size, List<IdExpression> vars)
        {
            List<If0Expression> cached;
            if (_if0ExpressionCache.TryGetValue(new Tuple<int, List<IdExpression>>(size, vars), out cached))
                return cached;

            var e1 = new List<IExpression>();
            var e2 = new List<Tuple<IExpression, IExpression>>();
            var o = new List<If0Expression>();

            Parallel.For(1, size - 1, i =>
            {
                var expressions = GenerateExpressions(i, vars);
                lock (e1)
                    e1.AddRange(expressions);
            });

            //for (var i = 1; i < size - 1; i++)
            //    e1.AddRange(GenerateExpressions(i, vars));

            foreach (var exp in e1)
            {
                for (var i = 1; size - exp.Size - i > 0; i++)
                {
                    e2.AddRange(
                        GenerateExpressions(size - exp.Size - i, vars)
                            .Select(x => new Tuple<IExpression, IExpression>(exp, x)));
                }
            }

            foreach (var exp in e2)
            {
                o.AddRange(
                    GenerateExpressions(size - exp.Item1.Size - exp.Item2.Size, vars)
                        .Select(x => new If0Expression(exp.Item1, exp.Item2, x)));
            }

            _if0ExpressionCache[new Tuple<int, List<IdExpression>>(size, vars)] = o;

            return o;
        }

        private List<FoldExpression> GenerateFoldExpressions(int size, List<IdExpression> vars)
        {         
            var e0 = new List<IExpression>();
            var e1 = new List<Tuple<IExpression, IExpression>>();
            var expressions = new List<FoldExpression>();

            for (var i = 1; i < size - 1; i++)
                e0.AddRange(GenerateExpressions(i, vars));

            foreach (var exp in e0)
            {
                for (var i = 1; size - exp.Size - i > 0; i++)
                {
                    e1.AddRange(
                        GenerateExpressions(size - exp.Size - i, vars)
                            .Select(x => new Tuple<IExpression, IExpression>(exp, x)));
                }
            }

            //foreach (var exp in e1)
            //{
            //    expressions.AddRange(
            //        GenerateExpressions(size - exp.Item1.Size - exp.Item2.Size, vars)
            //            .Select(x => new If0Expression(exp.Item1, exp.Item2, x)));
            //}


            return expressions;
        }
    }
}
