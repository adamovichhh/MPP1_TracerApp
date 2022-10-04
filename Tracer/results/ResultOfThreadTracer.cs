using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class ResultOfThreadTracer
    {
        public List<ResultOfFunctionTracer> lHighLeverFunctionsTracerResult;
        public int Id { get; private set; }
        public TimeSpan Time { get; private set; }

        public static ResultOfThreadTracer GetTraceResult(ThreadTracer threadTracer)
        {
            ResultOfThreadTracer threadTracerResult = new ResultOfThreadTracer();
            threadTracerResult.lHighLeverFunctionsTracerResult = new List<ResultOfFunctionTracer>();
            threadTracerResult.Id = threadTracer.Id;
            threadTracerResult.Time = threadTracer.ThreadTime;

            foreach (var firstLvlMethodTracer in threadTracer.lHighLevelFunctions)
            {
                threadTracerResult.lHighLeverFunctionsTracerResult.Add(ResultOfFunctionTracer.GetTraceResult(firstLvlMethodTracer));
            }

            return threadTracerResult;
        }
    }
}
