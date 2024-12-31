using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace APIAccessLayer.Helper
{
    public class APIClient<input>
    {
        private string Baseurl;
        private HttpClient client;

        public APIClient()
        {
            Baseurl = Convert.ToString(ConfigurationManager.AppSettings["APIUrl"]);
            client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(30);
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(WebConfigurationManager.AppSettings["APIKey"]));
        }

        public Task<string> Post(string url,input postData)
        {
             HttpResponseMessage response = client.PostAsync(url,
                 new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json")).Result;
            return response.Content.ReadAsStringAsync();
        }

        public Task<string> Get(string url)
        {
            HttpResponseMessage response = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync();
        }

        //public Task<string> Put(string url, input putData)
        //{
        //    HttpResponseMessage response = client.PutAsync(url,
        //        new StringContent(JsonConvert.SerializeObject(putData), Encoding.UTF8, "application/json")).Result;
        //    return response.Content.ReadAsStringAsync();
        //}
    }
}
