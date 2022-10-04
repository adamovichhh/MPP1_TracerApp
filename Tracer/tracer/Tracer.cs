using System.Collections.Concurrent;
using System.Threading;


namespace TracerLib
{
    public class Tracer : ITracer
    {
        ResultOfTrace tracerResult { get; set; }
        private ConcurrentDictionary<int, ThreadTracer> cdThreadTracers;
        static private object locker = new object();

        public Tracer()
        {
            cdThreadTracers = new ConcurrentDictionary<int, ThreadTracer>();
        }

        public void StartTrace()
        {
            ThreadTracer curThreadTracer = CheckThread(Thread.CurrentThread.ManagedThreadId);
            curThreadTracer.StartTrace();
        }

        public void StopTrace()
        {
            ThreadTracer currThreadTracer = CheckThread(Thread.CurrentThread.ManagedThreadId);
            currThreadTracer.StopTrace();
        }

        public ResultOfTrace GetTraceResult()
        {
            tracerResult = new ResultOfTrace(cdThreadTracers);
            return tracerResult;
        }

        private ThreadTracer CheckThread(int tracerId)
        {
            lock (locker)
            {
                if (!cdThreadTracers.TryGetValue(tracerId, out ThreadTracer threadTracer))
                {
                    threadTracer = new ThreadTracer(tracerId);
                    cdThreadTracers[tracerId] = threadTracer;
                }
                return threadTracer;
            }
        }
    }
}
