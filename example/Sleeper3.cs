using System;
using tracer;

namespace example
{
    public class Sleeper3
    {
        private ITracer _tracer;
        public Sleeper3(ITracer tracer) => _tracer = tracer;
        
        public void sleep3(int millisecondsTimeout)
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(millisecondsTimeout);
            _tracer.StopTrace();
        }
    }
}