using System;

namespace Persistance
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string msg) : base(msg) { }
    }
}