using System.Runtime.Serialization;

namespace tracer
{
    public class TraceResult
    {
        public readonly string MethodName;
        public readonly string ClassName;
        public readonly long TimeSpent;
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