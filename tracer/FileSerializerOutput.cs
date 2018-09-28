using System;

namespace tracer
{
    public class FileSerializerOutput : ISerializerOutput
    {
        public ISerializer Serializer;
        public string FileName;

        public FileSerializerOutput(ISerializer serializer, string fileName)
        {
            Serializer = serializer;
            FileName = fileName;
        }

        public void Write()
        {
            var directory = Environment.CurrentDirectory;
            var fileName = $"{FileName}.{Serializer.FileType()}";
            var filePath = $"{directory}/{fileName}";
            
            System.IO.File.WriteAllText(filePath, Serializer.Serialize());
        }
    }
}