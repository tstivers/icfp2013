using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Exceptions
{
    class GonnaRunOutOfMemoryException : OutOfMemoryException
    {
        public GonnaRunOutOfMemoryException()
        {
        }

        public GonnaRunOutOfMemoryException(string message) : base(message)
        {
        }

        public GonnaRunOutOfMemoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GonnaRunOutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
