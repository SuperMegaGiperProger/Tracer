using System.Collections.Concurrent;

namespace tracer
{
    public class ThreadTraceResultBuilder
    {
        public BlockingCollection<MethodTraceResultBuilder> MethodTraceResultBuilders;

        public ThreadTraceResultBuilder()
        {
            MethodTraceResultBuilders = new BlockingCollection<MethodTraceResultBuilder>();
        }
    }
}