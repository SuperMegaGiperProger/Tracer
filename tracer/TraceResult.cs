using System.Linq;
using System.Runtime.Serialization;

namespace tracer
{
    [KnownType(typeof(TraceResult))]
    [DataContract(Name = "root")]
    public class TraceResult
    {
        [DataMember(Name = "treads")]
        public readonly ThreadTraceResult[] ThreadTraceResults;

        public TraceResult(TraceResultBuilder builder)
        {
            ThreadTraceResults = GetThreadTraceResults(builder);
        }

        private static ThreadTraceResult[] GetThreadTraceResults(TraceResultBuilder builder)
        {
            return builder.ThreadTraceResultBuilders.Select((threadIdResultPair) =>
                new ThreadTraceResult(threadIdResultPair.Key, threadIdResultPair.Value)).ToArray();
        }
    }
}