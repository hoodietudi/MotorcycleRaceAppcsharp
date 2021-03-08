using System;

namespace MotorcycleContest.model.validators
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string msg) : base(msg) {}
    }
}