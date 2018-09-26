using System.Linq;
using System.Runtime.Serialization;

namespace tracer
{
    [KnownType(typeof(MethodTraceResult))]
    [DataContract]
    public class MethodTraceResult
    {
        [DataMember(Name = "name")]
        public readonly string Name;
        
        [DataMember(Name = "class")]
        public readonly string ClassName;

        [DataMember(Name = "time")]
        public string TimeView
        {
            get => Time + "ms";
            protected set {}
        }

        public readonly long Time;
        
        [DataMember(Name = "methods")]
        public readonly MethodTraceResult[] MethodTraceResults;

        public MethodTraceResult(MethodTraceResultBuilder builder)
        {
            Name = builder.Name;
            ClassName = builder.ClassName;
            Time = builder.Time;
            MethodTraceResults = GetMethodTraceResults(builder);
        }

        private static MethodTraceResult[] GetMethodTraceResults(MethodTraceResultBuilder builder)
        {
            return builder.MethodTraceResultBuilders
                .Select((methodTraceResultBuilder) => new MethodTraceResult(methodTraceResultBuilder)).ToArray();
        }
    }
}