using System;

namespace log4net
{
    public class LogManager
    {
        public LogManager()
        {
        }

        public static ILog GetLogger(Type type)
        {
            return new DumyLog();
        }
    }

    public interface ILog
    {
        bool IsDebugEnabled { get; set; }

        void Error(object data);

        void Warn(object data);

        void Warn(string context, Exception ex);

        void Error(string context, Exception ex);

        void Debug(string context, Exception ex);

        void WarnFormat(string format, params object[] paramters);

        void DebugFormat(string format, params object[] parameters);

        void Debug(string v);

        void InfoFormat(string format, params object[] parameters);

        void Info(string v);

        void ErrorFormat(string format, params object[] parameters);
    }

    internal class DumyLog : ILog
    {
        public bool IsDebugEnabled { get; set; }

        public void Debug(string v)
        {
        }

        public void Debug(string context, Exception ex)
        {
        }

        public void DebugFormat(string format, params object[] parameters)
        {
        }

        public void Error(object data)
        {
        }

        public void Error(string context, Exception ex)
        {
        }

        public void ErrorFormat(string format, params object[] parameters)
        {
        }

        public void Info(string v)
        {
        }

        public void InfoFormat(string format, params object[] parameters)
        {
        }

        public void Warn(object data)
        {
        }

        public void Warn(string context, Exception ex)
        {
        }

        public void WarnFormat(string format, params object[] paramters)
        {
        }
    }
}
