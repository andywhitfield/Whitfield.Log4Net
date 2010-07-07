using System.Linq;
using System.Text;
using System.Web;
using Whitfield.Log4Net.Properties;

namespace Whitfield.Log4Net
{
    public class Log4NetDisplayHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var rows = new StringBuilder();
            foreach (var logEventInfo in InMemoryBuffer.Instance.LogEvents.Reverse())
                rows.AppendFormat(@"
                <tr>
			        <td>{0}</td>
			        <td>{1}</td>
			        <td>{2}</td>
			        <td>{3}</td>
		        </tr>",
                logEventInfo.Timestamp.ToString("u"), logEventInfo.Level,
                logEventInfo.Class, logEventInfo.Message);

            context.Response.Write(Resources.Template.Replace("<!--LOGGINGROWS-->", rows.ToString()));
        }
    }
}
