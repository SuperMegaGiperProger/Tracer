using System.Collections.Concurrent;

namespace tracer
{
    public class TraceResultBuilder
    {
        public ConcurrentDictionary<int, ThreadTraceResultBuilder> ThreadTraceResultBuilders;

        public TraceResult TraceResult => new TraceResult(this);

        public TraceResultBuilder()
        {
            ThreadTraceResultBuilders = new ConcurrentDictionary<int, ThreadTraceResultBuilder>();
        }
    }
}