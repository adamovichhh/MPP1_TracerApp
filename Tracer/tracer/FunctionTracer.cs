using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class FunctionsTracer
    {
        public string MethodName { get; private set; }
        public string ClassName { get; private set; }
        public TimeSpan Time { get; private set; }
        private Stopwatch StopWatch;
        public List<FunctionsTracer> lChildrenMethodTracers { get; private set; }

        public FunctionsTracer()
        {

            StackFrame tempStackFrame = new StackFrame(3);
            MethodName = tempStackFrame.GetMethod().Name;
            ClassName = tempStackFrame.GetMethod().DeclaringType.Name;

            Time = new TimeSpan();
            StopWatch = new Stopwatch();
            lChildrenMethodTracers = new List<FunctionsTracer>();
        }

        public void StartTrace()
        {
            StopWatch.Start();
        }

        public void StopTrace()
        {
            StopWatch.Stop();
            Time = StopWatch.Elapsed;
        }
    }
}
