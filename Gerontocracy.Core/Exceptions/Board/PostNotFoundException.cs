using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Gerontocracy.Core.Exceptions.Board
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException()
        {
        }

        public PostNotFoundException(string message) : base(message)
        {
        }

        public PostNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PostNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
