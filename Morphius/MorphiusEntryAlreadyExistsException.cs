using System;
using System.Net;
using System.Runtime.Serialization;

namespace Morphius
{
    class MorphiusEntryAlreadyExistsException : Exception
    {
        public MorphiusEntryAlreadyExistsException(Type exceptionType, HttpStatusCode existingCode, HttpStatusCode newCode)
            : this(GenerateErrorMessage(exceptionType, existingCode, newCode))
        {
        }

        public MorphiusEntryAlreadyExistsException(string message) : base(message)
        {
        }

        public MorphiusEntryAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MorphiusEntryAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string GenerateErrorMessage(Type exceptionType, HttpStatusCode existingCode, HttpStatusCode newCode)
        {
            return $"Any exceptions of type '{exceptionType.FullName}' is already registered with code '{existingCode}' and can therefore not registered to return '{newCode}'";
        }
    }
}
