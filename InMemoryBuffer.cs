using System.Collections.Generic;
using System.Threading;
using log4net.Core;

namespace Whitfield.Log4Net
{
    public class InMemoryBuffer
    {
        public static readonly InMemoryBuffer Instance = new InMemoryBuffer();
        private static readonly int MaxBufferSize = 500;

        private readonly object bufferLock = new object();
        private readonly LinkedList<LogEventInfo> logBuffer = new LinkedList<LogEventInfo>();

        private InMemoryBuffer() { }

        public void AsyncAppendEvent(LoggingEvent loggingEvent)
        {
            var timestamp = loggingEvent.TimeStamp;
            var loggingLevel = loggingEvent.Level;
            var name = loggingEvent.LoggerName;
            var message = loggingEvent.RenderedMessage;
            ThreadPool.QueueUserWorkItem(x =>
            {
                lock (this.bufferLock)
                {
                    this.logBuffer.AddLast(new LogEventInfo(timestamp, loggingLevel.DisplayName, name, message));
                    while (this.logBuffer.Count > InMemoryBuffer.MaxBufferSize) this.logBuffer.RemoveFirst();
                }
            });
        }

        public IList<LogEventInfo> LogEvents
        {
            get
            {
                var bufferCopy = new List<LogEventInfo>(InMemoryBuffer.MaxBufferSize);
                lock (this.bufferLock)
                {
                    bufferCopy.AddRange(this.logBuffer);
                }
                bufferCopy.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp));
                return bufferCopy;
            }
        }
    }
}
