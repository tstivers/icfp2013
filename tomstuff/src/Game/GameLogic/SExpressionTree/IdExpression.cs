using System;

namespace GameClient.SExpressionTree
{
    public class IdExpression : IExpression
    {
        public string Id { get; set; }

        public IdExpression(string id)
        {
            Id = id;
        }

        #region IExpression Members

        public int Size { get { return 1; } }

        public ulong Eval(EvalContext context)
        {
            IExpression exp;
            if (!context.TryGetValue(this, out exp))
                throw new ArgumentException(String.Format("Id {0} was not bound in the current context.", Id));

            return exp.Eval(context);
        }

        #endregion

        protected bool Equals(IdExpression other)
        {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((IdExpression) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return String.Format("{0}", Id);
        }
    }
}
