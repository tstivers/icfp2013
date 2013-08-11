using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Exceptions
{
    public class ProblemExpiredException : Exception
    {
        public ProblemExpiredException()
        {
        }

        public ProblemExpiredException(string message) : base(message)
        {
        }

        public ProblemExpiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProblemExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
