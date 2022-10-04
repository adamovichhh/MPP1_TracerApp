using System.Collections.Concurrent;
using System.Collections.Generic;


namespace TracerLib
{
    public class ResultOfTrace
    {
        public IDictionary<int, ResultOfThreadTracer> dThreadTracerResults { get; private set; }

        public ResultOfTrace(ConcurrentDictionary<int, ThreadTracer> cdThreadTracers)
        {
            dThreadTracerResults = new Dictionary<int, ResultOfThreadTracer>();
            foreach (var threadTracer in cdThreadTracers)
            {
                dThreadTracerResults[threadTracer.Key] = ResultOfThreadTracer.GetTraceResult(threadTracer.Value);
            }
        }
    }
}
