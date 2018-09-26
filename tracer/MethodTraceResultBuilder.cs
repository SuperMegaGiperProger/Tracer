using System.Collections.Concurrent;
using System.Diagnostics;

namespace tracer
{
    public class MethodTraceResultBuilder
    {
        private Stopwatch _stopwatch;
        public string Name, ClassName;
        public long Time;
        public BlockingCollection<MethodTraceResultBuilder> MethodTraceResultBuilders;

        public MethodTraceResultBuilder(string name = null, string className = null)
        {
            Name = name;
            ClassName = className;
            MethodTraceResultBuilders = new BlockingCollection<MethodTraceResultBuilder>();
        }

        public void StartTracing()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void StopTracing()
        {
            _stopwatch.Stop();
            Time = _stopwatch.ElapsedMilliseconds;
        }
    }
}