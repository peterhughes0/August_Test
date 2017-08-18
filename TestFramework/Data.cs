using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TestFramework
{
    public class Data
    {
        public static string StagingSite = "http://staging.growobservatory.org";

        public static HttpClient httpClient = new HttpClient();

        public static string replaceLinesWithSpace(string text)
        {
            text = text.Replace("\t", " ");
            text = text.Replace("\r\n", " ");

            return text;
        }
        

        public static string GetSafeFileName(string testName)
        {
            string[] nameSplit = testName.Split('(');
            return nameSplit[0];
        }


        public static string siteUrl { get; set; }
    }
}
