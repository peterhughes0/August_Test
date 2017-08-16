using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverFramework;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;
using System.Globalization;

namespace TestFramework
{
    public class Reporting : Driver
    {

        public static void InitialiseValues()
        {
            
        }


        public static void CleanUpTest(TestContext currentTest)
        {
            
            var outcome = currentTest.Result.Outcome.ToString();

            if (!outcome.Contains("Failed"))
            {
                Telegram_API.Send_Message(null, "Everything is good!!!");
                Driver.Close();
            }

            else
            {
                Telegram_API.Send_Message(null, "Something didn't work");
                TakeScreenshot(Data.GetSafeFileName(TestContext.CurrentContext.Test.Name));  
            }
        }


        public static void TakeScreenshot(string details)
        {
            var date = DateTime.Today.ToShortDateString().ToString(new CultureInfo("en-GB"));
            details = details.Replace("\"", "-");
            details = details.Replace("\\", "-");
            date = date.Replace("/", "_");
            var time = DateTime.Now.ToShortTimeString();
            time = time.Replace(":", "-");
            var Location = rootLocation;
            System.IO.Directory.SetCurrentDirectory(Location);

            var resultsDirectory = Location + "\\TestResults\\" + date + "\\" + details + time;

            resultsDirectory = resultsDirectory.Replace("\"", "-");

            if (!System.IO.Directory.Exists(resultsDirectory))
            {
                System.IO.Directory.CreateDirectory(resultsDirectory);
            }

            System.IO.Directory.SetCurrentDirectory(resultsDirectory);


            var sw = new System.IO.StreamWriter("Details.txt");

           

            sw.Close();
            //WebdriverExtensions.TakeScreenshot(driver, "" + details + ".png", System.Drawing.Imaging.ImageFormat.Png);
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            Driver.ScrollToBottomOfPageUsingJS();
            ss.SaveAsFile("" + details + ".png", ScreenshotImageFormat.Png);
            Screenshot sb = ((ITakesScreenshot)driver).GetScreenshot();
            sb.SaveAsFile("" + details + "_Bottom.png", ScreenshotImageFormat.Png);
        }
    }
}
