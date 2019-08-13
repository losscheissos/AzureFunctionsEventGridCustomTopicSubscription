using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Producer.EventGrid
{
    public class EventGridSender
    {
        private readonly Lazy<HttpClient> Client;

        private readonly Lazy<string> topicEndpoint;

        public EventGridSender(Func<string> topicEndpoint, Func<string> topicKey)
        {
            this.topicEndpoint = new Lazy<string>(topicEndpoint);
            this.Client = new Lazy<HttpClient>(CreateHttpClient(topicKey));
        }

        private HttpClient CreateHttpClient(Func<string> topicKey)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("aeg-sas-key", topicKey());
            return httpClient;
        }

        public Task SendSingleEventToTopic(object singleEvent)
        {
            return SendEventsToTopic(new object[] { singleEvent });
        }


        public async Task SendEventsToTopic(object[] events)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(events);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await Client.Value.PostAsync(topicEndpoint.Value, content);
            result.EnsureSuccessStatusCode();
        }
    }
}
