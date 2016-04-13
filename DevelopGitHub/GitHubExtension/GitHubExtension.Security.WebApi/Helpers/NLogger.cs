using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;
using NLog;

namespace GitHubExtension.Security.WebApi.Helpers
{
    public sealed class NLogger : ITraceWriter
    {
        private static readonly Logger ClassLogger = LogManager.GetCurrentClassLogger();

        private static readonly Lazy<Dictionary<TraceLevel, Action<string>>> LoggingMap =
            new Lazy<Dictionary<TraceLevel, Action<string>>>(
                () =>
                    new Dictionary<TraceLevel, Action<string>>
                    {
                        {TraceLevel.Info, ClassLogger.Info},
                        {TraceLevel.Debug, ClassLogger.Debug},
                        {TraceLevel.Error, ClassLogger.Error},
                        {TraceLevel.Fatal, ClassLogger.Fatal},
                        {TraceLevel.Warn, ClassLogger.Warn}
                    });


        private Dictionary<TraceLevel, Action<string>> Logger
        {
            get { return LoggingMap.Value; }
        }


        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                if (traceAction != null && traceAction.Target != null)
                {
                    category = category + Environment.NewLine + "Action Parameters : " + traceAction.Target.ToJSON();
                }
                var record = new TraceRecord(request, category, level);
                if (traceAction != null) traceAction(record);
                Log(record);
            }
        }

        private void Log(TraceRecord record)
        {
            var message = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(record.Message))
                message.Append("").Append(record.Message + Environment.NewLine);

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    message.Append("Method: " + record.Request.Method + Environment.NewLine);

                if (record.Request.RequestUri != null)
                    message.Append("").Append("URL: " + record.Request.RequestUri + Environment.NewLine);

                if (record.Request.Headers != null && record.Request.Headers.Contains("Token") &&
                    record.Request.Headers.GetValues("Token") != null &&
                    record.Request.Headers.GetValues("Token").FirstOrDefault() != null)
                    message.Append("")
                        .Append("Token: " + record.Request.Headers.GetValues("Token").FirstOrDefault() +
                                Environment.NewLine);
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
                message.Append("").Append(record.Category);

            if (!string.IsNullOrWhiteSpace(record.Operator))
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);


            Logger[record.Level](Convert.ToString(message) + Environment.NewLine);
        }
    }
}
