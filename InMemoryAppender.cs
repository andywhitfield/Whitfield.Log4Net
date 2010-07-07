using log4net.Appender;
using log4net.Core;

namespace Whitfield.Log4Net
{
    public class InMemoryAppender : IAppender
    {
        public InMemoryAppender() { this.Name = "InMemoryAppender"; }

        public string Name { get; set; }

        public void Close() { }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            InMemoryBuffer.Instance.AsyncAppendEvent(loggingEvent);
        }
    }
}
