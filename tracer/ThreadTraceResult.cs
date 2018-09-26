using System.Linq;
using System.Runtime.Serialization;

namespace tracer
{
    [KnownType(typeof(ThreadTraceResult))]
    [DataContract]
    public class ThreadTraceResult
    {
        [DataMember(Name = "id")] public readonly int Id;
        [DataMember(Name = "methods")] public readonly MethodTraceResult[] MethodTraceResults;

        [DataMember(Name = "time")]
        public string TimeView
        {
            get => Time + "ms";
            protected set { }
        }

        public long Time => MethodTraceResults.ToList().Sum((methodTraceResult) => methodTraceResult.Time);

        public ThreadTraceResult(int id, ThreadTraceResultBuilder builder)
        {
            Id = id;
            MethodTraceResults = GetMethodTraceResults(builder);
        }

        private static MethodTraceResult[] GetMethodTraceResults(ThreadTraceResultBuilder builder)
        {
            return builder.MethodTraceResultBuilders
                .Select((methodTraceResultBuilder) => new MethodTraceResult(methodTraceResultBuilder)).ToArray();
        }
    }
}