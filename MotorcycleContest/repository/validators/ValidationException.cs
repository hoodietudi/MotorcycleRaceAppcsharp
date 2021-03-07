using System;

namespace MotorcycleContest.repository.validators
{
    public class ValidationException : Exception
    {
        public ValidationException(string msg) : base(msg) {}
    }
}