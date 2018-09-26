using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace tracer
{
    public class JsonSerializer : ISerializer
    {
        private readonly ITracer _tracer;

        public JsonSerializer(ITracer tracer) => _tracer = tracer;

        public string Serialize()
        {
            var ms = new MemoryStream();
            new DataContractJsonSerializer(typeof(TraceResult), Settings).WriteObject(ms, _tracer.GetTraceResult());
            return Encoding.ASCII.GetString(ms.ToArray());
        }

        private static DataContractJsonSerializerSettings Settings => new DataContractJsonSerializerSettings
            {EmitTypeInformation = EmitTypeInformation.Never};
    }
}