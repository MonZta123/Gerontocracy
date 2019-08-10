using System;
using System.Collections.Generic;
using System.Net;

namespace Morphius
{
    public class MorphiusOptions
    {
        private readonly Dictionary<Type, HttpStatusCode> _errors = new Dictionary<Type, HttpStatusCode>();

        public MorphiusOptions AddException<TException>(HttpStatusCode code)
            where TException : Exception
        {
            Type exceptionType = typeof(TException);

            if (_errors.TryGetValue(exceptionType, out HttpStatusCode existingCode))
            {
                throw new MorphiusEntryAlreadyExistsException(exceptionType, existingCode, code);
            }

            _errors[exceptionType] = code;
            return this;
        }

        public HttpStatusCode GetErrorOrDefault<T>(T e)
        {
            return _errors.TryGetValue(e.GetType(), out HttpStatusCode value)
                ? value
                : HttpStatusCode.InternalServerError;
        }
    }
}
