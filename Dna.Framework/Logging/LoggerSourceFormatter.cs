using System;
using System.IO;

namespace Dna
{
    /// <summary>
    /// Format a message when the callers source infomation is provided first in the arguments
    /// </summary>
    public static class LoggerSourceFormatter
    {
        /// <summary>
        /// Formats the message including the source infomation pulled out of the state
        /// </summary>
        /// <param name="state">The state infomation about the log</param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string Format(object[] state, Exception exception)
        {
            // Get the values from the state
            var origin = (string)state[0];
            var filePath = (string)state[1];
            var lineNumber = (int)state[2];
            var message = state[3];

            //Get the exception message
            var exceptionMessage = exception?.ToString();

            // New line if we have an exception
            if (exception != null)
                exceptionMessage += System.Environment.NewLine + exception;

            return $"{message} [{Path.GetFileName(filePath)} > {origin}() > Line {lineNumber}]";
        }
    }
}
// have fun :)
