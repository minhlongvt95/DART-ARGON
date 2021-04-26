using Microsoft.Extensions.Logging;

namespace Dna
{
    /// <summary>
    ///
    /// <summary>
    public static class FileLoggerExstensions
    {
        /// <summary>
        /// Adds a new file logger to specific path
        /// </summary>
        /// <param name="builder">The log builder to as to</param>
        /// <param name="path">The path where to write to</param>
        /// <returns></returns>
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder,string path, FileLoggerConfiguration configuration = null)
        {
            // Create default configuration if not provided
            if (configuration == null)
                configuration = new FileLoggerConfiguration();


            // Add file log provider to builder
            builder.AddProvider(new FileLoggerProvider(path, configuration));

            return builder;
        }
    }
}
// have fun :)
