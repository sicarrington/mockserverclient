using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, "/expectation")
            {
                Content = new StringContent(
                JsonSerializer.Serialize(expectations, JsonSerializerOptionsContants.Default),
                Encoding.UTF8,
                "application/json"
            )
            };

            _ = await _httpClient.SendAsync(httpRequestMessage);
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
                JsonSerializer.Serialize(verification, JsonSerializerOptionsContants.Default),
                Encoding.UTF8,
                "application/json"
            );

            var result = await _httpClient.SendAsync(httpRequestMessage);
            return result.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}