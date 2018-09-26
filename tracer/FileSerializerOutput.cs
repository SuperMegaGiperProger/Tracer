using System;

namespace tracer
{
    public class FileSerializerOutput : ISerializerOutput
    {
        public ISerializer Serializer;

        public FileSerializerOutput(ISerializer serializer) => Serializer = serializer;

        public void Write()
        {
            var directory = Environment.CurrentDirectory;
            var fileName = $"trace_result.{Serializer.FileType()}";
            var filePath = $"{directory}/{fileName}";
            
            System.IO.File.WriteAllText(filePath, Serializer.Serialize());
        }
    }
}