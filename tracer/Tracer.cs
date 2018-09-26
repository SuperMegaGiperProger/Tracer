using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace tracer
{
    public class Tracer : ITracer
    {
        private Stopwatch _stopwatch;
        private readonly TraceResultBuilder _traceResultBuilder;
        private static int CurrThreadId => Thread.CurrentThread.ManagedThreadId;

        public Tracer()
        {
            _traceResultBuilder = new TraceResultBuilder();
        }

        public void StartTrace()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        private MethodTraceResultBuilder GetMethodTraceResultBuilder(MethodBase methodInfo)
        {
            var methodName = methodInfo.Name;
            var className = methodInfo.DeclaringType.ToString();
            MethodTraceResultBuilder methodThreadResultBuilder = new MethodTraceResultBuilder(methodName, className);
            ThreadTraceResultBuilder.MethodTraceResultBuilders.Add(methodThreadResultBuilder);
            return methodThreadResultBuilder;
        }

        private ThreadTraceResultBuilder ThreadTraceResultBuilder =>
            _traceResultBuilder.ThreadTraceResultBuilders.GetOrAdd(CurrThreadId, new ThreadTraceResultBuilder());

        public void StopTrace()
        {
            _stopwatch.Stop();
            GetMethodTraceResultBuilder(new StackTrace().GetFrame(1).GetMethod()).Time = _stopwatch.ElapsedMilliseconds;
        }

        public TraceResult GetTraceResult()
        {
            return _traceResultBuilder.TraceResult;
        }
    }
}