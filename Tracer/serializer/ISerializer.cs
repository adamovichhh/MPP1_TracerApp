using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    interface ISerializer
    {
        void SaveTraceResult(TextWriter textWriter, ResultOfTrace traceResult);
        void SaveResultToFileAndConsole(ResultOfTrace tracerResult);
    }
}
