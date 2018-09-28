using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace tracer
{
    public class XmlSerializer : ISerializer
    {
        private readonly ITracer _tracer;

        public XmlSerializer(ITracer tracer) => _tracer = tracer;

        public string Serialize()
        {
            var ms = new MemoryStream();
            new DataContractSerializer(typeof(TraceResult)).WriteObject(ms, _tracer.GetTraceResult());
            return Encoding.ASCII.GetString(ms.ToArray());
        }

        public string FileType()
        {
            return "xml";
        }
    }
}