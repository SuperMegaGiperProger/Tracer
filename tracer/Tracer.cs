using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace tracer
{
    public class Tracer : ITracer
    {
        private Stopwatch stopwatch;
        private TraceResultBuilder traceResultBuilder;
        
        public Tracer() {}
        
        public void StartTrace()
        {
            traceResultBuilder = new TraceResultBuilder();
            getParentMethodInfo();
            stopwatch = Stopwatch.StartNew();
        }

        private void getParentMethodInfo()
        {
            MethodBase methodInfo = new StackTrace().GetFrame(2).GetMethod();
            traceResultBuilder.methodName = methodInfo.Name;
            traceResultBuilder.className = methodInfo.DeclaringType.ToString();
            traceResultBuilder.threadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            traceResultBuilder.elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        }

        public TraceResult GetTraceResult()
        {
            return traceResultBuilder.TraceResult;
        }
    }
}