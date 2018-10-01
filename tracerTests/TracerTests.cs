using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Castle.Core;
using tracer;
using Xunit;
using Moq;
using NSubstitute;

namespace tracerTests
{
    public class TracerTests
    {
        private ITracer _tracer;
        private int time1, time2, time3, thread1Id, thread2Id;
        
        [Fact]
        public void CalculatesTraceResult()
        {
            _tracer = new Tracer();
            time1 = new Random().Next(50);
            time2 = new Random().Next(50);
            time3 = new Random().Next(50);
            
            var thread1 = new Thread(() =>
            {
                tracingMethod1(time1);
                tracingMethod2(time2);
            });
            thread1.Start();
            var thread2 = new Thread(() =>
            {
                tracingMethod2(time3);
            });
            thread2.Start();

            thread1Id = thread1.ManagedThreadId;
            thread2Id = thread2.ManagedThreadId;

            thread1.Join();
            thread2.Join();

            var traceResult = _tracer.GetTraceResult();

            Assert.IsType<TraceResult>(traceResult);
            assertThreads(traceResult);
        }

        private void assertThreads(TraceResult traceResult)
        {
            var threadResult1 = traceResult.ThreadTraceResults[0];
            var threadResult2 = traceResult.ThreadTraceResults[1];
            Assert.Equal(thread1Id, threadResult1.Id);
            Assert.Equal(thread2Id, threadResult2.Id);
            Assert.True(threadResult1.Time >= time1 + time2);
            Assert.True(threadResult2.Time >= time3);
            Assert.True(threadResult1.MethodTraceResults[0].Time >= time1);
            Assert.Equal("tracingMethod1", threadResult1.MethodTraceResults[0].Name);
            Assert.Equal("tracerTests.TracerTests", threadResult1.MethodTraceResults[0].ClassName);
            Assert.True(threadResult1.MethodTraceResults[0].MethodTraceResults[0].Time >= time1 - time1/2);
            Assert.Equal("tracingMethod2", threadResult1.MethodTraceResults[0].MethodTraceResults[0].Name);
            Assert.Equal("tracerTests.TracerTests", threadResult1.MethodTraceResults[0].MethodTraceResults[0].ClassName);
            Assert.True(threadResult2.MethodTraceResults[0].Time >= time3);
            Assert.Equal("tracingMethod2", threadResult2.MethodTraceResults[0].Name);
            Assert.Equal("tracerTests.TracerTests", threadResult2.MethodTraceResults[0].ClassName);
        }

        private void tracingMethod1(int time)
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(time / 2);
            tracingMethod2(time - time / 2);
            _tracer.StopTrace();
        }

        private void tracingMethod2(int time)
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(time);
            _tracer.StopTrace();
        }
    }
}