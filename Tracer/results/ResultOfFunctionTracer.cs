using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;

namespace TracerLib
{
    public class ResultOfFunctionTracer
    {
        public string ClassName { get; private set; }
        public string MethodName { get; private set; }
        public TimeSpan Time { get; private set; }
        public List<ResultOfFunctionTracer> lChildrenMethodTracerResults;

        public static ResultOfFunctionTracer GetTraceResult(FunctionsTracer methodTracer)
        {
            ResultOfFunctionTracer methodTracerResult = new ResultOfFunctionTracer();
            methodTracerResult.ClassName = methodTracer.ClassName;
            methodTracerResult.MethodName = methodTracer.MethodName;
            methodTracerResult.Time = methodTracer.Time;
            methodTracerResult.lChildrenMethodTracerResults = new List<ResultOfFunctionTracer>();

            foreach (var innerMethodTracer in methodTracer.lChildrenMethodTracers)
            {
                methodTracerResult.lChildrenMethodTracerResults.Add(ResultOfFunctionTracer.GetTraceResult(innerMethodTracer));
            }

            return methodTracerResult;
        }
    }
}
