using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Exceptions
{
    public class AlreadySolvedException : Exception
    {
        public AlreadySolvedException()
        {
        }

        public AlreadySolvedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AlreadySolvedException(string message) : base(message)
        {
        }

        protected AlreadySolvedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
