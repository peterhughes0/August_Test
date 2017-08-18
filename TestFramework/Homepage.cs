using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;
using DriverFramework;

namespace TestFramework
{
    public class Homepage : Driver
    {

        public static void CheckHomepage()
        {

            WebDriverWait waitUntilPageConditions = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {
                waitUntilPageConditions.Until((d) => {return !Driver.ContainsText("If you are the owner of this website, please contact your hosting provider:"); });
            }

            catch(Exception)
            {
                throw new Exception(string.Format("Failed to Reach Grow Observatory Homepage"));
            }
        }
    }
}
