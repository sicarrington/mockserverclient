using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("MockServer.Client.Net.Tests.Unit")]

public class poops
{

}

namespace MockServer.Client.Net
{
    public class MockServerClient
    {
        private readonly HttpClient _httpClient;
        public MockServerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task SetExpectations(Expectation expectations)
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

            var result = await _httpClient.SendAsync(httpRequestMessage);
        }
        public ExpectationBuilder When(RequestBuilder requestBuilder)
        {
            return ExpectationBuilder.When(this, requestBuilder);
        }
        public async Task<bool> Verify(HttpRequest request, VerificationTimes verificationTimes)
        {
            var verification = new Verification(request, verificationTimes);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, "/verify");
            httpRequestMessage.Content = new StringContent(
                JsonConvert.SerializeObject(verification,
                    Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            }),
                Encoding.UTF8,
                "application/json"
            );

            var result = await _httpClient.SendAsync(httpRequestMessage);
            return result.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}