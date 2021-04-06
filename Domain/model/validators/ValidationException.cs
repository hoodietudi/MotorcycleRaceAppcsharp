using System;

namespace Domain.model.validators
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string msg) : base(msg) {}
    }
}