using System.Collections.Concurrent;

namespace tracer
{
    public class MethodTraceResultBuilder
    {
        public string Name, ClassName;
        public long Time;
        public BlockingCollection<MethodTraceResultBuilder> MethodTraceResultBuilders;

        public MethodTraceResultBuilder(string name = null, string className = null)
        {
            Name = name;
            ClassName = className;
            MethodTraceResultBuilders = new BlockingCollection<MethodTraceResultBuilder>();
        }
    }
}