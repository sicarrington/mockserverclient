using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MockServer.Client.Net.Models;
using Newtonsoft.Json;

namespace MockServer.Client.Net
{
    public class MockServerClient
    {
        private readonly HttpClient _httpClient;
        public MockServerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SetExpectations(Expectation expectations)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, "/expectation");
            httpRequestMessage.Content = new StringContent(
                JsonConvert.SerializeObject(expectations,
                    Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }),
                Encoding.UTF8,
                "application/json"
            );

            await _httpClient.SendAsync(httpRequestMessage);
        }
    }
}