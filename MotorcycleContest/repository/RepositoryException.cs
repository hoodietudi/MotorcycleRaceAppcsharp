using System;

namespace MotorcycleContest.repository
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string msg) : base(msg) { }
    }
}