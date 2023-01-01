using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Work_Tasks.Exceptions
{
    public class ExistingTaskException : Exception
    {
        public ExistingTaskException()
        {
        }

        public ExistingTaskException(string message) : base(message)
        {
        }

        public ExistingTaskException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExistingTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
