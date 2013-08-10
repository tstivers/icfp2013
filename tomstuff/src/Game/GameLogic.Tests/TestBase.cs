using System.Diagnostics;
using log4net.Appender;
using log4net.Config;
using NUnit.Framework;

namespace GameLogic.Tests
{
    [TestFixture]
    public class TestBase
    {
        [TestFixtureSetUp]
        public void InitLog4Net()
        {
            Debug.Print("------------================== initializing logging ======================-------------");
            BasicConfigurator.Configure(new TraceAppender());
        }
    }
}
