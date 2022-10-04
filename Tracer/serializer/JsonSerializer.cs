using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TracerLib
{
    public class JsonSerializer : ISerializer
    {
        public void SaveTraceResult(TextWriter textWriter, ResultOfTrace traceResult)
        {
            var jtokens = from threadTracerResult in traceResult.dThreadTracerResults.Values
                          select SaveThreads(threadTracerResult);
            JObject traceResultJSON = new JObject
            {
                { "thread", new JArray(jtokens) }
            };

            using (var jsonWriter = new JsonTextWriter(textWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                traceResultJSON.WriteTo(jsonWriter);
            }
        }

        public void SaveResultToFileAndConsole(ResultOfTrace tracerResult)
        {
            string pathToSave = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\User\\Desktop\\Ycheba_5sem\\SPP\\Laba1\\result.json");

            FileStream fileStream = new FileStream(pathToSave, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            SaveTraceResult(streamWriter, tracerResult);
            SaveTraceResult(Console.Out, tracerResult);
        }

        private JToken SaveThreads(ResultOfThreadTracer threadTracerResult)
        {
            var lFirstLvlMethods = from methodTracerResult in threadTracerResult.lHighLeverFunctionsTracerResult
                                   select SaveMethods(methodTracerResult);
            return new JObject
            {
                { "id", threadTracerResult.Id },
                { "time", threadTracerResult.Time.Milliseconds + "ms"},
                { "methods", new JArray(lFirstLvlMethods) }
            };
        }

        private JToken SaveMethods(ResultOfFunctionTracer methodTracerResult)
        {
            var savedTracedMetod = new JObject
            {
                { "name", methodTracerResult.MethodName },
                { "class", methodTracerResult.ClassName },
                { "time", methodTracerResult.Time.Milliseconds + "ms" }
            };

            if (methodTracerResult.lChildrenMethodTracerResults.Any())
                savedTracedMetod.Add("methods", new JArray(from mt in methodTracerResult.lChildrenMethodTracerResults
                                                           select SaveMethods(mt)));
            return savedTracedMetod;
        }
    }
}
