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
        private const string OpName = "y";

        private static readonly IdExpression YExpression = new IdExpression("y");
        private static readonly IdExpression F1Expression = new IdExpression("f1");
        private static readonly IdExpression F2Expression = new IdExpression("f2");

        private static readonly List<IExpression> NoFoldIds = new List<IExpression>
        {
            new NumberExpression(0),
            new NumberExpression(1),
            YExpression
        };

        private static readonly List<IExpression> FoldIds =
            new[] {F1Expression, F2Expression}.Concat(NoFoldIds).ToList();

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ConcurrentDictionary<int, List<IExpression>> _foldCache =
            new ConcurrentDictionary<int, List<IExpression>>();

        private readonly ConcurrentDictionary<int, List<IExpression>> _noFoldCache =
            new ConcurrentDictionary<int, List<IExpression>>();

        private readonly string[][] _opPool;
        public int Size { get; set; }
        public string[] Operators { get; set; }

        public SProgramGenerator(int size, string[] operators)
        {
            Size = size;
            Operators = operators.Where(x => x != "bonus").Select(x => x.Replace("tfold", "fold")).ToArray();

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

            _noFoldCache[1] = NoFoldIds;
            _foldCache[1] = FoldIds;

            var id = new IdExpression(OpName);

            var sw = new Stopwatch();
            sw.Start();

            for (var i = 2; i < Size; i++)
                GenerateExpressions(i, false);

            var results = _noFoldCache[Size - 1].Where(expression =>
            {
                var expressionText = expression.ToString();
                return Operators.All(expressionText.Contains) && expressionText.Contains(OpName);
            }).Select(e0 => new ProgramExpression(id, e0)).ToList();

            Log.InfoFormat("Generated {0} programs in {1:c}", results.Count(), sw.Elapsed);

            return results;
        }

        private List<IExpression> GenerateExpressions(int size, bool inFold)
        {
            if (size == 0)
                throw new ArgumentException("Attempted to generate size 0 expressions");

            List<IExpression> expressions;

            if (!inFold && _noFoldCache.TryGetValue(size, out expressions))
                return expressions;

            if (inFold && _foldCache.TryGetValue(size, out expressions))
                return expressions;

            expressions = new List<IExpression>();

            //Log.DebugFormat("Generating expressions of size {0}", size);

            if (size >= 2)
            {
                foreach (var opCode in _opPool[2])
                    expressions.AddRange(GenerateOp1Expressions(opCode, size, inFold));
            }

            if (size >= 3)
            {
                foreach (var opCode in _opPool[3])
                    expressions.AddRange(GenerateOp2Expressions(opCode, size, inFold));
            }

            if (size >= 4)
            {
                if (_opPool[4].Length != 0)
                    expressions.AddRange(GenerateIf0Expressions(size, inFold));
            }

            if (size >= 5 && !inFold)
            {
                if (_opPool[5].Length != 0)
                    expressions.AddRange(GenerateFoldExpressions(size));
            }

            if (inFold)
                _foldCache[size] = expressions;
            else
                _noFoldCache[size] = expressions;

            //Debug.Assert(expressions.All(op => op.Size == size));

            return expressions;
        }

        private List<Op1Expression> GenerateOp1Expressions(string opCode, int size, bool inFold)
        {
            //Log.DebugFormat("Generating op1 expressions of size {0}", size);
            var ops = GenerateExpressions(size - 1, inFold).Select(e1 => new Op1Expression(opCode, e1)).ToList();

            //Debug.Assert(ops.All(op => op.Size == size));

            return ops;
        }

        private List<Op2Expression> GenerateOp2Expressions(string opCode, int size, bool inFold)
        {
            //Log.DebugFormat("Generating op2 expressions of size {0}", size);
            var e1List = new List<IExpression>();
            var ops = new List<Op2Expression>();
            var maxExpSize = size - 2;
            var totalExpSize = size - 1;

            for (var i = 1; i <= maxExpSize; i++)
                e1List.AddRange(GenerateExpressions(i, inFold));

            foreach (var e1 in e1List)
            {
                ops.AddRange(
                    GenerateExpressions(totalExpSize - e1.Size, inFold).Select(e2 => new Op2Expression(opCode, e1, e2)));
            }

            //Debug.Assert(ops.All(op => op.Size == size));

            return ops;
        }

        private List<If0Expression> GenerateIf0Expressions(int size, bool inFold)
        {
            //Log.DebugFormat("Generating if0 expressions of size {0}", size);
            var e1List = new List<IExpression>();
            var e2List = new List<Tuple<IExpression, IExpression>>();
            var ops = new List<If0Expression>();
            var maxExpSize = size - 3;
            var totalExpSize = size - 1;

            for (var i = 1; i <= maxExpSize; i++)
                e1List.AddRange(GenerateExpressions(i, inFold));

            foreach (var e1 in e1List)
            {
                for (var i = 1; i <= maxExpSize - e1.Size - 1; i++)
                {
                    e2List.AddRange(
                        GenerateExpressions(i, inFold).Select(e2 => new Tuple<IExpression, IExpression>(e1, e2)));
                }
            }

            foreach (var e1e2 in e2List)
            {
                ops.AddRange(
                    GenerateExpressions(totalExpSize - e1e2.Item1.Size - e1e2.Item2.Size, inFold)
                        .Select(e3 => new If0Expression(e1e2.Item1, e1e2.Item2, e3)));
            }

            //Debug.Assert(ops.All(op => op.Size == size));

            return ops;
        }

        private List<FoldExpression> GenerateFoldExpressions(int size)
        {
            var e0List = new List<IExpression>();
            var e0e1List = new List<Tuple<IExpression, IExpression>>();
            var ops = new List<FoldExpression>();
            var maxExpSize = size - 4;
            var totalExpSize = size - 2;

            for (var i = 1; i <= maxExpSize; i++)
                e0List.AddRange(GenerateExpressions(i, false));

            foreach (var e0 in e0List)
            {
                for (var i = 1; i <= maxExpSize - e0.Size - 1; i++)
                {
                    e0e1List.AddRange(
                        GenerateExpressions(i, false).Select(e1 => new Tuple<IExpression, IExpression>(e0, e1)));
                }
            }

            foreach (var e0e1 in e0e1List)
            {
                ops.AddRange(
                    GenerateExpressions(totalExpSize - e0e1.Item1.Size - e0e1.Item2.Size, true)
                        .Select(e2 => new FoldExpression(e0e1.Item1, e0e1.Item2, F1Expression, F2Expression, e2)));
            }

            //Debug.Assert(ops.All(op => op.Size == size));

            return ops;
        }
    }
}
