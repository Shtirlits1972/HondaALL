using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace HondaALL.Models
{
    public class RestWork
    {
        public static string GetContent(string url, Method method  = Method.GET)
        {
            string content = string.Empty;

            try
            {
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                content = response.Content;
            }
            catch(Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }

            return content;
        }
    }
}
