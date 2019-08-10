using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Party
{
    public class PartyNotFoundException : Exception
    {
        public PartyNotFoundException()
        {
        }

        public PartyNotFoundException(string message) : base(message)
        {
        }

        public PartyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PartyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
