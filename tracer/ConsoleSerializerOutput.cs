using System;

namespace tracer
{
    public class ConsoleSerializerOutput : ISerializerOutput
    {
        public ISerializer Serializer;

        public ConsoleSerializerOutput(ISerializer serializer) => Serializer = serializer;

        public void Write()
        {
            Console.WriteLine(Serializer.Serialize());
        }
    }
}