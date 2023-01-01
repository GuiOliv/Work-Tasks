using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Work_Tasks.Exceptions
{
    public class NullStatusException : Exception
    {
        public NullStatusException()
        {
        }

        public NullStatusException(string message) : base(message)
        {
        }

        public NullStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
