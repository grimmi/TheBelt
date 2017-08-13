using System;

namespace TheBelt
{
    [Serializable]
    internal class MissingArgumentException : Exception
    {
        public MissingArgumentException()
        {
        }

        public MissingArgumentException(string message) : base(message)
        {
        }

        public MissingArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}