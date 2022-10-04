using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class ThreadTracer
    {
        public int Id { get; private set; }
        public List<FunctionsTracer> lHighLevelFunctions { get; private set; }
        private Stack<FunctionsTracer> sAllFunctionTracers;
        public TimeSpan ThreadTime { get; private set; }

        public ThreadTracer(int id)
        {
            Id = id;
            lHighLevelFunctions = new List<FunctionsTracer>();
            sAllFunctionTracers = new Stack<FunctionsTracer>();
            ThreadTime = new TimeSpan();
        }

        public void StartTrace()
        {
            FunctionsTracer methodTracer = new FunctionsTracer();

            if (sAllFunctionTracers.Count > 0)
            {
                FunctionsTracer lastUnstoppedMethodTracer = sAllFunctionTracers.Peek();
                lastUnstoppedMethodTracer.lChildrenMethodTracers.Add(methodTracer);
            }

            sAllFunctionTracers.Push(methodTracer);
            methodTracer.StartTrace();
        }

        public void StopTrace()
        {
            FunctionsTracer lastUnstoppedMethodTracer = sAllFunctionTracers.Pop();
            lastUnstoppedMethodTracer.StopTrace();
            if (!sAllFunctionTracers.Any())
            {
                lHighLevelFunctions.Add(lastUnstoppedMethodTracer);
                ThreadTime += lastUnstoppedMethodTracer.Time;
            }

        }
    }
}
