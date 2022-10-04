using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TracerLib;


namespace Laba1
{
    class Program
    {
        static Tracer tracer;

        static void Main(string[] args)
        {
            tracer = new Tracer();

            Method_1();
            Method_2();

            ResultOfTrace tracerResult = tracer.GetTraceResult();

            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.SaveResultToFileAndConsole(tracerResult);
            Console.WriteLine();
            XmlSerializer xmlSerializer = new XmlSerializer();
            xmlSerializer.SaveResultToFileAndConsole(tracerResult);

        }

        #region Test1
        static void Method_1()
        {
            tracer.StartTrace();
            ChildrenMethod_1();
            ChildrenMethod_2();
            tracer.StopTrace();
        }

        static void ChildrenMethod_1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        static void ChildrenMethod_2()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            ChildrenChildrenMethod_1();
            tracer.StopTrace();
        }
        static void ChildrenChildrenMethod_1()
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            tracer.StopTrace();
        }
        #endregion

        #region Test2
        static void Method_2()
        {
            tracer.StartTrace();
            ChildrenMethod_3();
            tracer.StopTrace();
        }

        static void ChildrenMethod_3()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            ChildrenChildrenMethod_2();
            tracer.StopTrace();
        }
        static void ChildrenChildrenMethod_2()
        {
            tracer.StartTrace();
            Thread.Sleep(150);
            ChildrenChildrenChildrenMethod_1();
            tracer.StopTrace();
        }
        static void ChildrenChildrenChildrenMethod_1()
        {
            tracer.StartTrace();
            Thread.Sleep(250);
            tracer.StopTrace();
        }
        #endregion

    }
}
