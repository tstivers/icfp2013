using System;
using System.Reflection;
using log4net;

namespace GameClient.SExpressionTree
{
    public class FoldExpression : IExpression
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IExpression _e0;
        private readonly IExpression _e1;
        private readonly IExpression _e2;
        private readonly IdExpression _x;
        private readonly IdExpression _y;

        public FoldExpression(IExpression e0, IExpression e1, IdExpression x, IdExpression y, IExpression e2)
        {
            _e0 = e0;
            _e1 = e1;
            _x = x;
            _y = y;
            _e2 = e2;
        }

        #region IExpression Members

        public int Size { get { return 2 + _e0.Size + _e1.Size + _e2.Size; } }

        public ulong Eval(EvalContext context)
        {
            var y = _e1;
            var c = new EvalContext(context);
            foreach (var b in BitConverter.GetBytes(_e0.Eval(context)))
            {
                var x = new NumberExpression(b);
                //Debug.Print("evaluating {0} where {{{1}: {2}, {3}: {4}}}", _e2, _x, x, _y, y);                
                c[_x] = x;
                c[_y] = y;
                y = new NumberExpression(_e2.Eval(c));
            }

            return y.Eval(context);
        }

        #endregion

        public override string ToString()
        {
            return String.Format("(fold {0} {1} (lambda ({2} {3}) {4}))", _e0, _e1, _x, _y, _e2);
        }
    }
}
