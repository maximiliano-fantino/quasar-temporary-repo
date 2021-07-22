using ApiQF.Interfaces;
using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace ApiQF.Domain
{
    public class Logger : ILogger
    {
        private readonly ILog logger;

        private readonly string _msgFormat;

        public Logger()
        {
            ILoggerRepository loggerRepository = LogManager.CreateRepository("TestNetCore");
            XmlConfigurator.Configure(loggerRepository, new FileInfo("log4net.config"));
            logger = LogManager.GetLogger(loggerRepository.Name, typeof(Program));
            _msgFormat = "[{0}, {1}]::{2}";
        }

        public Logger(string _loggerName) : this()
        {
            logger = LogManager.GetLogger(_loggerName);
        }

        public void Error(Exception ex, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "")
        {
            logger.Error(String.Format("Error Type: {0}", ex.GetType().FullName));
            logger.Error(String.Format("Error Message: {0}", ex));
        }

        public void Debug(string message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "")
        {
            var msg = string.Format(_msgFormat, GetClassName(filePath), memberName, message);
            logger.Debug(msg);
        }

        public void Debug(Exception ex, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "")
        {
            logger.Debug(String.Format("Error Type: {0}", ex.GetType().FullName));
            logger.Debug(String.Format("Error Message: {0}", ex));
        }

        public void Error(string message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "")
        {
            var msg = string.Format(_msgFormat, GetClassName(filePath), memberName, message);
            logger.Error(msg);
        }

        public void Trace(string message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "")
        {
            var msg = string.Format(_msgFormat, GetClassName(filePath), memberName, message);
            logger.Info(msg);
        }

        public void Warn(string message, [CallerFilePathAttribute] string filePath = "", [CallerMemberName] string memberName = "")
        {
            var msg = string.Format(_msgFormat, GetClassName(filePath), memberName, message);
            logger.Warn(msg);
        }

        private string GetClassName(string filePath)
        {
            var className = string.Empty;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return className;
            }
            var s = filePath.LastIndexOf('\\') + 1;
            className = filePath.Substring(s, filePath.Length - s);
            return className;
        }

        public void Debug(StringBuilder message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "")
        {
            var msg = string.Format(_msgFormat, GetClassName(filePath), memberName, message.ToString());
            logger.Debug(msg);
        }
    }
}