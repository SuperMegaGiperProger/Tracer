namespace tracer
{
    public class TraceResultBuilder
    {
        public string methodName, className;
        public long elapsedMilliseconds;
        public int threadId;

        public TraceResult TraceResult => new TraceResult(methodName, className, elapsedMilliseconds, threadId);
    }
}