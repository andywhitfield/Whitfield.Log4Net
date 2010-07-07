using System;

namespace Whitfield.Log4Net
{
    public class LogEventInfo
    {
        public readonly DateTime Timestamp;
        public readonly string Level;
        public readonly string Class;
        public readonly string Message;

        public LogEventInfo(DateTime timestamp, string level, string className, string message)
        {
            this.Timestamp = timestamp;
            this.Level = level;
            this.Class = className;
            this.Message = message;
        }
    }
}
