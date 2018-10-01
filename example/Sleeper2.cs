using System;
using tracer;

namespace example
{
    public class Sleeper2
    {
        private ITracer _tracer;
        public Sleeper2(ITracer tracer) => _tracer = tracer;
        
        public void sleep2(int millisecondsTimeout)
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(millisecondsTimeout / 2);
            new Sleeper3(_tracer).sleep3(millisecondsTimeout - millisecondsTimeout / 2);
            _tracer.StopTrace();
        }
    }
}