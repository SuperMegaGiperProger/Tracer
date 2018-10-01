using System;
using System.Threading;
using tracer;

namespace example
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ITracer tracer = new Tracer();
            ISerializer serializer = new JsonSerializer(tracer);
            ISerializerOutput resultOutput = new ConsoleSerializerOutput(serializer);
            ISerializerOutput resultOutput2 = new FileSerializerOutput(serializer, "trace_result");
            var thread1 = new Thread(() =>
            {
                trace(tracer);
                trace(tracer);
                trace(tracer);
            });
            thread1.Start();
            var thread2 = new Thread(() =>
            {
                trace(tracer);
                trace(tracer);
            });
            thread2.Start();
            
            thread1.Join();
            thread2.Join();
            
            resultOutput.Write();
            resultOutput2.Write();
        }

        private static void trace(ITracer tracer)
        {
            var time = Convert.ToInt32(Console.ReadLine());
            new Sleeper(tracer).sleep(time);
        } 
    }
}