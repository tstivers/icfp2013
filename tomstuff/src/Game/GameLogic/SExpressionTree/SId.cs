using System;

namespace GameClient.SExpressionTree
{
    public class SId : SExpression
    {
        public string Id { get; set; }

        public SId(string id)
        {
            Id = id;
        }

        protected bool Equals(SId other)
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
            return Equals((SId) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return String.Format("{0}", Id);
        }

        public override int Size { get { return 1; } }

        public override ulong Eval(EvalContext context)
        {
            return context[this].Eval(context);
        }
    }
}
