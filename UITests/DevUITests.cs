using System;
using NUnit.Framework;
using TestFramework;
using DriverFramework;
using System.Net;
using System.Text.RegularExpressions;


namespace UiTests
{
    [TestFixture]
    public class DevUiTests
    {


        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [SetUp]
        public void startDriver()
        {
            Driver.Initialize(Driver.Browser.Headless_Chrome, TestContext.CurrentContext);
        }

        [TestCase(TestName = "Homepage Health Check")]
        [Category("Staging")]
        public void HomepagePing()
        {
            Driver.GoTo(Data.StagingSite);
            Homepage.CheckHomepage();
        }

        /// <summary>
        /// 
        /// </summary>
        [TearDown]
        public void CleanUp()
        {
            Reporting.CleanUpTest(TestContext.CurrentContext);
        }
    }
}
