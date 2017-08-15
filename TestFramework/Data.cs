using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework
{
    public class Data
    {

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
