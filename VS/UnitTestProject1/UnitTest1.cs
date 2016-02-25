using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bellatrixLog;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMessageToFile()
        {
            var aJobLog = new JobLooger(true, false, false, true, false, false);
            aJobLog.LogMessage("", true, false, false);
        }

        [TestMethod]
        public void TestMessageToConsole()
        {
            var aJobLog = new JobLooger(false, true, false, true, false, false);
            aJobLog.LogMessage("", true, false, false);
        }

        [TestMethod]
        public void TestMessageToDataBase()
        {
            var aJobLog = new JobLooger(false, false, true, true, false, false);
            aJobLog.LogMessage("", true, false, false);
        }

        [TestMethod]
        public void TestWarningToFile()
        {
            var aJobLog = new JobLooger(true, false, false, false, true, false);
            aJobLog.LogMessage("", false, true, false);
        }
        [TestMethod]
        public void TestWarningToConsole()
        {
            var aJobLog = new JobLooger(false, true, false, false, true, false);
            aJobLog.LogMessage("", false, true, false);
        }
        [TestMethod]
        public void TestWarningToDataBase()
        {
            var aJobLog = new JobLooger(false, false, true, false, true, false);
            aJobLog.LogMessage("", false, true, false);
        }
        [TestMethod]
        public void TestErrorToFile()
        {
            var aJobLog = new JobLooger(true, false, false, false, false, true);
            aJobLog.LogMessage("", false, false, true);
        }
        [TestMethod]
        public void TestErrorToConsole()
        {
            var aJobLog = new JobLooger(false, true, false, false, false, true);
            aJobLog.LogMessage("", false, false, true);
        }
        [TestMethod]
        public void TestErrorToDataBase()
        {
            var aJobLog = new JobLooger(false, false, true, false, false, true);
            aJobLog.LogMessage("", false, false, true);
        }
    }
}
