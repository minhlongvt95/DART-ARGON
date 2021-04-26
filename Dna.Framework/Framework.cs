using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Dna
{
    /// <summary>
    /// The main entry point into the Dna Framework library
    /// </summary>
    public static class Framework
    {
        #region Protected Members

        /// <summary>
        /// The Dependency Injection service provider
        /// </summary>
        private static IServiceProvider ServiceProvider;

        #endregion

        #region Public Properties

        /// <summary>
        /// The Dependency Injection service provider
        /// </summary>
        public static IServiceProvider Provider => ServiceProvider;

        /// <summary>
        /// Gets the configuration for the framework envenroment
        /// </summary>
        public static IConfiguration Configuration => Provider.GetService<IConfiguration>();

        /// <summary>
        /// Get the default logger for the framework
        /// </summary>
        public static ILogger Logger => Provider.GetService<ILogger>();

        /// <summary>
        /// Gets the framework environment of this class
        /// </summary>
        public static FrameworkEnvironment Environment => Provider.GetService<FrameworkEnvironment>();
        #endregion

        #region Public Methods

        /// <summary>
        /// Should be called at the very start of your application to configure and setup
        /// </summary>
        /// <param name="configure">the action to add custom configuration to the configuration builder</param>
        /// <param name="injection">the action to inject services into the service collection</param>
        public static void Startup(Action<IConfigurationBuilder> configure = null,Action<IServiceCollection, IConfiguration> injection = null)
        {
            #region Initialize

            // Create a new list of dependencies
            var services = new ServiceCollection();

            #endregion

            #region Environment

            // Create environment details
            var environment = new FrameworkEnvironment();

            // Inject into services
            services.AddSingleton(environment);

            #endregion

            #region Configuration

            // TOTO: Add connfiguration from file json

            // Create our configuration source
            var configurationBuilder = new ConfigurationBuilder()
                // Add environment variable.
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory())
                // Add app setting json file.
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.Configuration}.json", optional: true, reloadOnChange: true);

            // Let custom configuration happen
            configure?.Invoke(configurationBuilder);

            // Inject Configuration into services
            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(configuration);

            #endregion

            #region Logging

            // Add logging as default
            services.AddLogging(options =>
            {
                // Setup loggers from configuration
                options.AddConfiguration(configuration.GetSection("Logging"));

                // Add console logger
                options.AddConsole();

                // Add debug logger
                options.AddDebug();

                // TODO: Add file logger
                options.AddFile("log.txt");
                //options.AddFile("log2.txt");
            });

            // Add default logger
            services.AddDefaultLogger();

            #endregion

            // Allow custom service injection
            injection?.Invoke(services, configuration);

            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();

            // Log the start up complate
            Logger.LogCriticalSource($"Dna Framework started {environment.Configuration}...");
        }

        #endregion
    }
}
