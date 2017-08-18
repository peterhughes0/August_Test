using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;

namespace TestFramework
{
    public static class Telegram_API
    {

        public static void Send_Message(string message, string chat_id = "-238095289", string parse_mode = "HTML")
        {

            var urlBuilder = new UriBuilder(string.Format("https://api.telegram.org/bot285631342:AAHk9uxE8F7MW1P1scVJLqt139_gViIzOxE/sendMessage?chat_id={0}&text={1}&parse_mode={2}", chat_id, message, parse_mode));

            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            urlBuilder.Query = query.ToString();
            urlBuilder.Port = -1;
            var url = urlBuilder.ToString();

            HttpResponseMessage rm = null;

            try
            {
                rm = Data.httpClient.GetAsync(url).Result;
            }

            catch (AggregateException ae)
            {
               // throw new Exception(string.Format("User Enquiry Endpoint failed to respond {0}. Endpoint: {1}. Status Code {2}", ae.InnerException, url, rm.StatusCode));
            }

            catch (Exception e)
            {
               // throw new Exception(string.Format("User Enquiry Endpoint failed to respond {0}. Endpoint: {1}. Status Code {2}", e.InnerException, url, rm.StatusCode));
            }

            if (rm.StatusCode != HttpStatusCode.OK)
            {
               // throw new Exception(string.Format("Error on User Enquiry Endpoint. Endpoint: {0}. Status Code {1}", url, rm.StatusCode));
            }

        }

    }
}
