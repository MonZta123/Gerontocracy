using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Party
{
    public class PoliticianNotFoundException : Exception
    {
        public PoliticianNotFoundException()
        {
        }

        public PoliticianNotFoundException(string message) : base(message)
        {
        }

        public PoliticianNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PoliticianNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
