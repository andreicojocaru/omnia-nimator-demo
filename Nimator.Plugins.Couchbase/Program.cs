using System;
using System.IO;
using System.Threading;
using log4net;

namespace Nimator.Plugins.Couchbase
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger("Nimator");
        private static readonly string ConfigFilename = "Nimator.Couchbase.config.json";

        private const int CheckIntervalInSecs = 15;

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionLogger;

            Logger.Info("Creating Nimator.");

            var nimator = CreateNimator();

            Logger.Info($"Nimator created. Starting timer for cycle every {CheckIntervalInSecs} seconds.");

            using (new Timer(_ => nimator.TickSafe(Logger), null, 0, CheckIntervalInSecs * 1000))
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }

            Logger.Info("Shutting down.");
        }

        private static INimator CreateNimator()
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), ConfigFilename);

            var json = File.ReadAllText(configPath);
            return Nimator.FromSettings(Logger, json);
        }

        private static void UnhandledExceptionLogger(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            if (eventArgs.ExceptionObject is Exception exc)
            {
                Logger.Fatal("Unhandled exception occurred.", exc);
            }
            else
            {
                Logger.Fatal("Fatal problem without Excption occurred.");
                Logger.Fatal(eventArgs.ExceptionObject);
            }
        }
    }
}
