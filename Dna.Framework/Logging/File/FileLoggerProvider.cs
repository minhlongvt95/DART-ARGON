using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Dna
{
    /// <summary>
    /// Cung cấp khả năng truy cập vào tệp
    /// <summary>
    public class FileLoggerProvider : ILoggerProvider
    {
        #region Protected Members

        /// <summary>
        /// The path to log to
        /// </summary>
        public string mFilePath;

        /// <summary>
        /// The conficgura
        /// </summary>
        protected readonly FileLoggerConfiguration mConfiguration;

        protected readonly ConcurrentDictionary<string, FileLogger> mLoggers = new ConcurrentDictionary<string, FileLogger>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="path">The path to log to</param>
        /// <param name="configuration">The configuration to use</param>
        public FileLoggerProvider(string path, FileLoggerConfiguration configuration)
        {
            mFilePath = path;
            mConfiguration = configuration;
        }

        #endregion

        #region ILoggerProvider Implementation

        /// <summary>
        /// Create a file logger based on the category name
        /// </summary>
        /// <param name="categoryName">The category name of this loggers</param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            // Get or create the logger for this category
            return mLoggers.GetOrAdd(categoryName, name => new FileLogger(name, mFilePath, mConfiguration));
        }

        public void Dispose()
        {

        }

        #endregion

    }
}
// have fun :)
