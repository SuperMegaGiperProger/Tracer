using System;
using tracer;

namespace example
{
    public class Sleeper
    {
        private ITracer _tracer;
        public Sleeper(ITracer tracer) => _tracer = tracer;
        
        public void sleep(int millisecondsTimeout)
        {
            _tracer.StartTrace();
            System.Threading.Thread.Sleep(millisecondsTimeout / 2);
            new Sleeper2(_tracer).sleep2(millisecondsTimeout - millisecondsTimeout / 2);
            _tracer.StopTrace();
        }
    }
}