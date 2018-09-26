using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace tracer
{
    public class ThreadTraceResultBuilder
    {
        public BlockingCollection<MethodTraceResultBuilder> MethodTraceResultBuilders;
        private Stack<MethodTraceResultBuilder> _processingMethodTraceResultBuilders;

        public ThreadTraceResultBuilder()
        {
            MethodTraceResultBuilders = new BlockingCollection<MethodTraceResultBuilder>();
            _processingMethodTraceResultBuilders = new Stack<MethodTraceResultBuilder>();
        }

        public void StartMethodTracing(MethodBase methodBase)
        {
            var methodName = methodBase.Name;
            var className = methodBase.DeclaringType.ToString();
            var methodTraceResultBuilder = new MethodTraceResultBuilder(methodName, className);
            
            _processingMethodTraceResultBuilders.Push(methodTraceResultBuilder);
            methodTraceResultBuilder.StartTracing();
        }

        public void StopMethodTracing()
        {
            var methodTraceResultBuilder = _processingMethodTraceResultBuilders.Pop();
            
            methodTraceResultBuilder.StopTracing();
            TopMethodTraceResultBuilderCollection.Add(methodTraceResultBuilder);
        }

        private BlockingCollection<MethodTraceResultBuilder> TopMethodTraceResultBuilderCollection =>
            _processingMethodTraceResultBuilders.Count > 0
                ? _processingMethodTraceResultBuilders.Peek().MethodTraceResultBuilders
                : MethodTraceResultBuilders;
    }
}