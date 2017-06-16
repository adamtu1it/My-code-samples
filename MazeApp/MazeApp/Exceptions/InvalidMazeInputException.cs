using System;
using System.Runtime.Serialization;

namespace MazeApp.Exceptions
{
    [Serializable]
    public class InvalidMazeInputException : Exception
    {
        public InvalidMazeInputException()
        {
            // Add any type-specific logic, and supply the default message.
        }

        public InvalidMazeInputException(string message)
            : base(message)
        {
            // Add any type-specific logic.
        }

        public InvalidMazeInputException(string message, Exception innerException) :
            base(message, innerException)
        {
            // Add any type-specific logic for inner exceptions.
        }

        protected InvalidMazeInputException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
            // Implement type-specific serialization constructor logic.
        }
    }
}