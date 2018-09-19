using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace tracer
{
    public class JsonSerializer : ISerializer
    {
        private readonly object _obj;

        public JsonSerializer(object obj)
        {
            _obj = obj;
        }

        public string Serialize()
        {
            var ms = new MemoryStream();
            new DataContractJsonSerializer(typeof(object), Settings).WriteObject(ms, _obj);
            return Encoding.ASCII.GetString(ms.ToArray());
        }

        private static DataContractJsonSerializerSettings Settings => new DataContractJsonSerializerSettings
            {EmitTypeInformation = EmitTypeInformation.Never};
    }
}