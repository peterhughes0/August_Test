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
                var titleElement = driver.FindElement(By.ClassName("hero__title"));
                var titleSpan = titleElement.FindElement(By.TagName("span"));
                
                waitUntilPageConditions.Until((d) => { return titleSpan.Text == "Discussions"; });
            }

            catch(Exception)
            {
                throw new Exception(string.Format("Failed to Reach Grow Observatory Homepage - Url:{0}", driver.Url));
            }
        }

        public static void CheckKnowledgebaseHomepage()
        {
            WebDriverWait waitUntilPageConditions = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            try
            {

                waitUntilPageConditions.Until((d) => { return Driver.ContainsText("How can we help?", 3); });
            }

            catch (Exception)
            {
                throw new Exception(string.Format("Failed to Reach Grow Observatory Knowledgebase Homepage - Url:{0}", driver.Url));
            }
        }
    }
}
