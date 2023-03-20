using System;
using Health;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace HealthTests.ApiRequests
{
    public class Requests
	{
        private HttpClient client = new HttpClient();
        private string baseUrl = "https://localhost:7777/api";

        public async Task<HttpResponseMessage> PostRecord(string path, TestHealthRecord testHealthRecord)
        {
            JObject testHealthRecordJO = JObject.FromObject(testHealthRecord);

            // remove the "Id" field before sending since it is not a valid input object
            testHealthRecordJO["Id"].Parent.Remove();

            string json = JsonConvert.SerializeObject(testHealthRecordJO, Formatting.Indented);
            Console.WriteLine(json);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync($"{baseUrl}{path}", httpContent);
        }

        public async Task<HttpResponseMessage> GetRecord(string path)
        { 
        
            return await client.GetAsync($"{baseUrl}{path}");
        }

    }
}

