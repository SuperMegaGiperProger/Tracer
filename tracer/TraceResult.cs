using System.Runtime.Serialization;

namespace tracer
{
    [KnownType(typeof(TraceResult))]
    [DataContract]
    public class TraceResult
    {
        [DataMember(Name = "name")]
        public readonly string MethodName;
        [DataMember(Name = "class")]
        public readonly string ClassName;
        [DataMember(Name = "time")]
        public readonly long TimeSpent;
        [DataMember(Name = "thread_id")]
        public readonly int ThreadId;

        public TraceResult(string methodName, string className, long timeSpent, int threadId)
        {
            MethodName = methodName;
            ClassName = className;
            TimeSpent = timeSpent;
            ThreadId = threadId;
        }
    }
}