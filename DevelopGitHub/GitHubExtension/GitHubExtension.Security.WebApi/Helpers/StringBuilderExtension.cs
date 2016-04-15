using System;
using System.Text;
using System.Web.Http.Tracing;

namespace GitHubExtension.Security.WebApi.Helpers
{
    public static class StringBuilderExtension
    {
        public static StringBuilder CheckRecordMessage(this StringBuilder message, TraceRecord record)
        {
            if (!string.IsNullOrWhiteSpace(record.Message))
            {
                message.Append(string.Empty).Append(record.Message + Environment.NewLine);
            }

            return message;
        }

        public static StringBuilder CheckRecordRequest(this StringBuilder message, TraceRecord record)
        {
            if (record.Request != null)
            {
                if (record.Request.Method != null)
                {
                    message.Append("Method: " + record.Request.Method + Environment.NewLine);
                }

                if (record.Request.RequestUri != null)
                {
                    message.Append(string.Empty).Append("URL: " + record.Request.RequestUri + Environment.NewLine);
                }
            }

            return message;
        }

        public static StringBuilder CheckRecordCategory(this StringBuilder message, TraceRecord record)
        {
            if (!string.IsNullOrWhiteSpace(record.Category))
            {
                message.Append(string.Empty).Append(record.Category);
            }

            return message;
        }

        public static StringBuilder CheckRecordOperator(this StringBuilder message, TraceRecord record)
        {
            if (!string.IsNullOrWhiteSpace(record.Operator))
            {
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);
            }

            return message;
        }
    }
}