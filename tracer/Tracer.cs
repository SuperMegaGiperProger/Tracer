using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace tracer
{
    public class Tracer : ITracer
    {
        private readonly TraceResultBuilder _traceResultBuilder;
        private static int CurrThreadId => Thread.CurrentThread.ManagedThreadId;

        public Tracer()
        {
            _traceResultBuilder = new TraceResultBuilder();
        }

        public void StartTrace()
        {
            ThreadTraceResultBuilder.StartMethodTracing(new StackTrace().GetFrame(1).GetMethod());
        }

        private ThreadTraceResultBuilder ThreadTraceResultBuilder =>
            _traceResultBuilder.ThreadTraceResultBuilders.GetOrAdd(CurrThreadId, new ThreadTraceResultBuilder());

        public void StopTrace()
        {
            ThreadTraceResultBuilder.StopMethodTracing();
        }

        public TraceResult GetTraceResult()
        {
            return _traceResultBuilder.TraceResult;
        }
    }
}