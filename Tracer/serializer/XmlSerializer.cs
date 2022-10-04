using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;


namespace TracerLib
{
    public class XmlSerializer : ISerializer
    {
        public void SaveTraceResult(TextWriter textWriter, ResultOfTrace traceResult)
        {
            XDocument doc = new XDocument(
                new XElement("root", from threadTracerResult in traceResult.dThreadTracerResults.Values
                                     select SaveThread(threadTracerResult)
                ));

            using (XmlTextWriter xmlWriter = new XmlTextWriter(textWriter))
            {
                xmlWriter.Formatting = Formatting.Indented;
                doc.WriteTo(xmlWriter);
            }
        }

        public void SaveResultToFileAndConsole(ResultOfTrace tracerResult)
        {
            string pathToSave = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\User\\Desktop\\Ycheba_5sem\\SPP\\Laba1\\result.xml");

            FileStream fileStream = new FileStream(pathToSave, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            SaveTraceResult(streamWriter, tracerResult);
            SaveTraceResult(Console.Out, tracerResult);
        }

        private XElement SaveThread(ResultOfThreadTracer threadTracer)
        {
            return new XElement("thread",
                new XAttribute("id", threadTracer.Id),
                new XAttribute("time", threadTracer.Time.Milliseconds + "ms"),
                from methodTracerResult in threadTracer.lHighLeverFunctionsTracerResult
                select SaveMethod(methodTracerResult)
                );
        }

        private XElement SaveMethod(ResultOfFunctionTracer methodTracer)
        {
            XElement savedTracedMetod = new XElement("method",
                new XAttribute("name", methodTracer.MethodName),
                new XAttribute("time", methodTracer.Time.Milliseconds + "ms"),
                new XAttribute("class", methodTracer.ClassName));

            if (methodTracer.lChildrenMethodTracerResults.Any())
                savedTracedMetod.Add(from innerMethod in methodTracer.lChildrenMethodTracerResults
                                     select SaveMethod(innerMethod));
            return savedTracedMetod;
        }
    }
}
