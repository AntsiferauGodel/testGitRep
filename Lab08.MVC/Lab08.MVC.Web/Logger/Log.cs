using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Lab08.MVC.Web.Logger
{
    public class Log : IDisposable
    {
        public ILogger Logger { get; }
        private readonly LoggingConfiguration logConfig = new LoggingConfiguration();

        private readonly FileTarget fileTarget = new FileTarget("fileTarget1")
        {
            FileName = "Logger/log.txt",
            Layout = "${date} ${processid} ${threadid} ${level} ${logger} ${message} ${exception}"
        };

        public Log(string name)
        {
            logConfig.AddTarget(fileTarget);
            logConfig.AddRuleForAllLevels(fileTarget);
            LogManager.Configuration = logConfig;
            Logger = LogManager.GetLogger(name);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                fileTarget?.Dispose();
            }
        }
    }
}