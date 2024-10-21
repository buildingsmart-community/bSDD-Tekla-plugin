using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using static System.IO.File;

namespace Bsdd.Core.Log
{

    public static class LogHelper
    {
        private static Logger log;

        /// <summary>
        /// Adds a log target using the specified configuration.
        /// Archives the log file if it exceeds a certain size.
        /// </summary>
        /// <param name="configuration">The log configuration settings.</param>
        public static void AddLogTarget(BsddLogConfiguration configuration)
        {
            try
            {
                EnsureLogDirectoryExists(configuration.LogFilePath);
                ArchiveLogFileIfNeeded(configuration.LogFilePath, 10000);

                configuration.OpenOnButton = true;

                var logConfiguration = LogManager.Configuration ?? new LoggingConfiguration();
                logConfiguration.RemoveTarget(configuration.LogName); // Ensure no duplicate targets

                var target = CreateFileTarget(configuration);
                logConfiguration.AddTarget(target);

                LogLevel level = ParseLogLevel(configuration.MinLevel);
                var loggingRule = new LoggingRule(configuration.NameFilter, level, target);
                logConfiguration.LoggingRules.Add(loggingRule);

                LogManager.Configuration = logConfiguration;
                log = LogManager.GetCurrentClassLogger();
                log.Debug("Using programmatic configuration for logging.");
            }
            catch (Exception ex)
            {
                log?.Error(ex, "Failed to add log target.");
            }
        }

        /// <summary>
        /// Removes the specified log target by name.
        /// </summary>
        /// <param name="name">The name of the log target to remove.</param>
        public static void RemoveLogTarget(string name)
        {
            var logConfiguration = LogManager.Configuration;
            logConfiguration?.RemoveTarget(name);
        }

        /// <summary>
        /// Adds a default temporary log target.
        /// </summary>
        /// <param name="name">Log target name.</param>
        /// <param name="logFilePath">Log file path.</param>
        public static void AddDefaultTempLogTarget(string name, string logFilePath)
        {
            var defaultConfiguration = new BsddLogConfiguration
            {
                OpenOnStartUp = false,
                LogName = name,
                LogFilePath = logFilePath,
                LogLayout = "${date:format=yyyy-MM-dd hh\\:mm\\:ss} | ${level} | ${logger} | ${message} ${exception}",
                NameFilter = "*",
                MinLevel = "Trace"
            };

            AddLogTarget(defaultConfiguration);
        }

        /// <summary>
        /// Adds a temporary log target for error reporting.
        /// </summary>
        /// <param name="name">Log target name.</param>
        /// <param name="logFilePath">Log file path.</param>
        public static void AddErrorReportTempLogTarget(string name, string logFilePath)
        {
            var errorReportConfiguration = new BsddLogConfiguration
            {
                OpenOnStartUp = false,
                LogName = name,
                LogFilePath = logFilePath,
                LogLayout = "${level} | ${message} ${exception}",
                NameFilter = "*",
                MinLevel = "Warn"
            };

            AddLogTarget(errorReportConfiguration);
        }

        /// <summary>
        /// Ensures the directory for the log file exists.
        /// </summary>
        /// <param name="logFilePath">The log file path.</param>
        private static void EnsureLogDirectoryExists(string logFilePath)
        {
            var directory = Path.GetDirectoryName(logFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Archives the log file if it exceeds a specified number of lines.
        /// </summary>
        /// <param name="logFilePath">The log file path.</param>
        /// <param name="maxLines">The maximum number of lines allowed in the log file.</param>
        private static void ArchiveLogFileIfNeeded(string logFilePath, int maxLines)
        {
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Dispose(); // Ensure the file exists
                return;
            }

            var lines = File.ReadAllLines(logFilePath).Length;
            if (lines > maxLines)
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                var archiveFileName = Path.GetFileNameWithoutExtension(logFilePath) + "_" + timestamp + ".log";
                var archiveFilePath = Path.Combine(Path.GetDirectoryName(logFilePath), archiveFileName);

                File.Copy(logFilePath, archiveFilePath);
                File.Delete(logFilePath);
                File.Create(logFilePath).Dispose(); // Recreate the log file
            }
        }

        /// <summary>
        /// Creates a new file target for logging based on the provided configuration.
        /// </summary>
        /// <param name="configuration">The log configuration.</param>
        /// <returns>A FileTarget instance.</returns>
        private static FileTarget CreateFileTarget(BsddLogConfiguration configuration)
        {
            return new FileTarget(configuration.LogName)
            {
                FileName = configuration.LogFilePath,
                Layout = configuration.LogLayout
            };
        }

        /// <summary>
        /// Parses the logging level from a string.
        /// </summary>
        /// <param name="minLevel">The logging level as a string.</param>
        /// <returns>The corresponding LogLevel.</returns>
        private static LogLevel ParseLogLevel(string minLevel)
        {
            LogLevel level;
            switch (minLevel)
            {
                case "Debug":
                    level = LogLevel.Debug;
                    break;
                case "Trace":
                    level = LogLevel.Trace;
                    break;
                case "Info":
                    level = LogLevel.Info;
                    break;
                case "Warn":
                    level = LogLevel.Warn;
                    break;
                case "Error":
                    level = LogLevel.Error;
                    break;
                default:
                    level = LogLevel.Trace;
                    break;
            }
            return level;
        }
    }

    //Gerrit van Lohuizen: Deprecated
    //public static class LogHandler
    //{
    //    private static Logger log;

    //    public static void AddLogTarget(NLogBasedLogConfiguration configuration)
    //    {
    //        try
    //        {
    //            var fileInfo = new FileInfo(configuration.LogFilePath);
    //            if (fileInfo.DirectoryName != null)
    //                Directory.CreateDirectory(fileInfo.DirectoryName);


    //            if (!Exists(configuration.LogFilePath))
    //                Create(configuration.LogFilePath);

    //            if (ReadAllLines(configuration.LogFilePath).ToList().Count() >
    //                10000) //Archive file with timestamp, delete old file
    //            {
    //                var date = DateTime.Now.ToString().Replace('/', '-');
    //                date = date.Replace(':', '_');
    //                var dirPath = Path.GetDirectoryName(configuration.LogFilePath);
    //                var fileName = Path.GetFileName(configuration.LogFilePath);
    //                fileName = fileName.Replace(".log", date + ".log");
    //                Copy(configuration.LogFilePath, Path.Combine(dirPath, fileName));
    //                Delete(configuration.LogFilePath);
    //                Create(configuration.LogFilePath);
    //            }
    //        }
    //        catch
    //        {
    //            //Fail silently
    //        }

    //        configuration.OpenOnButton = true;

    //        var logConfiguration = LogManager.Configuration ?? new LoggingConfiguration();

    //        logConfiguration.RemoveTarget(configuration.LogName);
    //        var target = new FileTarget(configuration.LogName)
    //        {
    //            FileName = configuration.LogFilePath
    //        };

    //        logConfiguration.AddTarget(target);

    //        LogLevel level;
    //        switch (configuration.MinLevel)
    //        {
    //            case "Debug":
    //                level = LogLevel.Debug;
    //                break;
    //            case "Trace":
    //                level = LogLevel.Trace;
    //                break;
    //            case "Info":
    //                level = LogLevel.Info;
    //                break;
    //            case "Warn":
    //                level = LogLevel.Warn;
    //                break;
    //            case "Error":
    //                level = LogLevel.Error;
    //                break;
    //            default:
    //                level = LogLevel.Trace;
    //                break;
    //        }

    //        var loggingRule = new LoggingRule(configuration.NameFilter, level, target);
    //        logConfiguration.LoggingRules.Add(loggingRule);

    //        LogManager.Configuration = logConfiguration;
    //        log = LogManager.GetCurrentClassLogger();

    //        log.Debug("Using programmatic config");
    //    }

    //    public static void RemoveLogTarget(string name)
    //    {
    //        var logConfiguration = LogManager.Configuration;
    //        logConfiguration?.RemoveTarget(name);
    //    }

    //    public static void AddDefaultTempLogTarget(string name, string logFilePath)
    //    {

    //        var defaultConfiguration = new NLogBasedLogConfiguration
    //        {
    //            OpenOnStartUp = false,
    //            LogName = name,
    //            LogFilePath = logFilePath,
    //            LogLayout = "${date:format=yyyy-MM-dd hh\\:mm\\:ss} | ${level} | ${logger} | ${message} ${exception}",
    //            NameFilter = "*",
    //            MinLevel = "Trace"
    //        };

    //        AddLogTarget(defaultConfiguration);
    //    }

    //    public static void AddErrorReportTempLogTarget(string name, string logFilePath)
    //    {
    //        var errorReportConfiguration = new NLogBasedLogConfiguration
    //        {
    //            OpenOnStartUp = false,
    //            LogName = name,
    //            LogFilePath = logFilePath,
    //            LogLayout = "${level} | ${message} ${exception}",
    //            NameFilter = "*",
    //            MinLevel = "Warn"
    //        };

    //        AddLogTarget(errorReportConfiguration);
    //    }
    //}
}