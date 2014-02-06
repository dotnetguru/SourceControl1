using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Tracing;
using System.Xml;

namespace HelloWebApi3.Models
{
    public class WebApiTracer : ITraceWriter
    {
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                TraceRecord rec = new TraceRecord(request, category, level);
                traceAction(rec);
                WriteXmlElement(rec);
            }
        }
        private void WriteXmlElement(TraceRecord rec)
        {
            using (Stream xmlFile = new FileStream(@"C:\path\log.xml", FileMode.Append))
            {
                using (XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartElement("trace");

                    writer.WriteElementString("timestamp", rec.Timestamp.ToString());
                    writer.WriteElementString("operation", rec.Operation);
                    writer.WriteElementString("user", rec.Operator);
                    if (!String.IsNullOrWhiteSpace(rec.Message))
                    {
                        writer.WriteStartElement("message");
                        writer.WriteCData(rec.Message);
                        writer.WriteEndElement();
                    }
                    writer.WriteElementString("category", rec.Category);
                    writer.WriteEndElement();
                    writer.WriteString(Environment.NewLine);
                    writer.Flush();
                }
            }
        }

    }
}