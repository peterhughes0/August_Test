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
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.Threading;

namespace TestFramework
{
    public class Reporting : Driver
    {
        static string imageFilePath;
        static string imageFileName;

        public static void InitialiseValues()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
        }


        public static void CleanUpTest(TestContext currentTest)
        {
            
            var outcome = currentTest.Result.Outcome.ToString();

            var name = TestContext.CurrentContext.Test.Name;

            if (!outcome.Contains("Failed"))
            {
                //Telegram_API.Send_Message(null, "Everything is good!!!");
                Driver.Close();
            }

            else
            {
                
                string blobUrl = string.Empty;
                try
                {
                    TakeScreenshot(TestContext.CurrentContext.Test.MethodName);
                    Driver.Close();

                    var cs = System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"];
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(cs);

                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference("images");

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(imageFileName);

                    // Create or overwrite the "myblob" blob with contents from a local file.
                    using (var fileStream = System.IO.File.OpenRead(imageFilePath + "\\" + imageFileName))
                    {
                        blockBlob.UploadFromStream(fileStream);
                        blockBlob.Properties.ContentType = "image/jpeg";
                        blockBlob.SetProperties();
                        blobUrl = blockBlob.StorageUri.PrimaryUri.OriginalString;
                    }

                }
                catch (NullReferenceException)
                { }
                catch (WebDriverException)
                { }

                Telegram_API.Send_Message(BuilNotification(name, currentTest.Result.Message, blobUrl), "-238095289", "HTML");

            }
           
        }

        public static string BuilNotification(string testName, string errorMessage, string imageUrl = "")
        {
            imageUrl = string.Format("<a href='{0}'>Image</a>", imageUrl);

            var message = string.Format("<b>Error!</b>{2}{2}<b>Test:  </b>{0}{2}{2}<b>Details:  </b>{1}{2}{2}{3}", testName, errorMessage, Environment.NewLine, imageUrl);

            return message;
        }


        public static void TakeScreenshot(string details)
        {
            Driver.DesktopResize();
            var date = DateTime.Today.ToShortDateString().ToString(new CultureInfo("en-GB"));
            details = details.Replace("\"", "-");
            details = details.Replace("\\", "-");
            date = date.Replace("/", "_");
            var time = DateTime.Now.ToString("HH:mm");
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

            sw.WriteLine("Failed at Page:");
            sw.WriteLine();
            sw.Write(driver.Url.ToString());

            sw.WriteLine();
            sw.WriteLine();


            sw.Close();

            imageFileName = string.Format("{0}_{1}_{2}.jpg", details, date, time);
            imageFileName.Replace(" ", "");

            imageFilePath = resultsDirectory;

            //WebdriverExtensions.TakeScreenshot(driver, "" + details + ".png", System.Drawing.Imaging.ImageFormat.Png);
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            Driver.ScrollToBottomOfPageUsingJS();
            ss.SaveAsFile(string.Format("{0}", imageFileName), ScreenshotImageFormat.Jpeg);
        }
    }
}
