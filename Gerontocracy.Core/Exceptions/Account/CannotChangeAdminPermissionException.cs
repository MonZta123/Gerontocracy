using System;
using System.Runtime.Serialization;

namespace Gerontocracy.Core.Exceptions.Account
{
    public class CannotChangeAdminPermissionException : Exception
    {
        public CannotChangeAdminPermissionException()
        {
        }

        public CannotChangeAdminPermissionException(string message) : base(message)
        {
        }

        public CannotChangeAdminPermissionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotChangeAdminPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
