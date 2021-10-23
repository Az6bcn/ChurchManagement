using System;
using System.Collections.Generic;

namespace Application.RequestValidators
{
    public sealed class RequestValidationException : Exception
    {
        public RequestValidationException() : base()
        {
        }

        public RequestValidationException(string message) : base(message)
        {
        }

        public RequestValidationException(string message,
                                          IDictionary<string, object> validationErrors) : base(message)
        {
            foreach (var err in validationErrors)
            {
                Data.Add(err.Key, err.Value);
            }
        }

        public RequestValidationException(string message,
                                          Exception ex) : base(message, ex)
        {
        }
    }
}