using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLib;

namespace TracerTests
{
    [TestClass]
    public class TracerTests
    {
        public Tracer tracer = new Tracer();

        #region UnitTest1
        void TestFunction_1()
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        void TestFunction_2()
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }
        [TestMethod]
        public void UnitTest1()
        {
            // check accordance of methods names

            TestFunction_1();
            TestFunction_2();

            ResultOfTrace tracerResult = tracer.GetTraceResult();
            ResultOfThreadTracer[] threadTracersResults = new ResultOfThreadTracer[1];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual("TestFunction_1", threadTracersResults[0].lHighLeverFunctionsTracerResult[0].MethodName);
            Assert.AreEqual("TestFunction_2", threadTracersResults[0].lHighLeverFunctionsTracerResult[1].MethodName);
        }
        #endregion

        #region UnitTest2
        
        void TestFunction_3()
        {
            tracer.StartTrace();
            ChildrenOfTestFunction_1();
            tracer.StopTrace();
        }

        void ChildrenOfTestFunction_1()
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        [TestMethod]
       public void UnitTest2()
        {
            // check accordance of children method name
            TestFunction_3();

            ResultOfTrace tracerResult = tracer.GetTraceResult();
            ResultOfThreadTracer[] threadTracersResults = new ResultOfThreadTracer[1];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual("ChildrenOfTestFunction_1", threadTracersResults[0].lHighLeverFunctionsTracerResult[0].lChildrenMethodTracerResults[0].MethodName);
            
        }
        #endregion

        #region UnitTest3
        void TestFunction_4()
        {
            tracer.StartTrace();
            tracer.StartTrace();
        }

        void ThreeThreads()
        {
          
            Thread thread1 = new Thread(TestFunction_4);
            thread1.Start();

            Thread thread2 = new Thread(TestFunction_4);
            thread2.Start();

            Thread thread3 = new Thread(TestFunction_4);
            thread3.Start();

            thread3.Join();
            thread2.Join();
            thread1.Join();

        }

        [TestMethod]
        public void UnitTest3()
        {
            //check counts of Threads

            ThreeThreads();

            ResultOfTrace tracerResult = tracer.GetTraceResult();
            ResultOfThreadTracer[] threadTracersResults = new ResultOfThreadTracer[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(3, threadTracersResults.Length);

        }
        #endregion

        #region UnitTest4
        void TestFunction_5()
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }
        [TestMethod]
        public void UnitTest4()
        {
            // check accordance of class name
            
            TestFunction_5();

            ResultOfTrace tracerResult = tracer.GetTraceResult();
            ResultOfThreadTracer[] threadTracersResults = new ResultOfThreadTracer[1];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual("TracerTests", threadTracersResults[0].lHighLeverFunctionsTracerResult[0].ClassName);
        }
        #endregion
    }
}
