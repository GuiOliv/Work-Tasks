using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Work_Tasks.Exceptions
{
    public class OngoingTaskException : Exception
    {
        public OngoingTaskException()
        {
        }

        public OngoingTaskException(string message) : base(message)
        {
        }

        public OngoingTaskException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OngoingTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
