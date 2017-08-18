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
    public class Class1 : Driver
    {

        public static void NavigateToBasicPage()
        {
            try
            {


                var googleSearchBox = driver.FindElement(By.ClassName("gfi"));

                googleSearchBox.SendKeys("Hi");

                googleSearchBox.SendKeys(Keys.Return);
            }

            catch(Exception)
            {
                throw new Exception(string.Format("Failed to manipulate search element."));
            }
        }
    }
}
