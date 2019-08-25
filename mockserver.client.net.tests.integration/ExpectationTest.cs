using System;
using Xunit;
using System.Net.Http;
using MockServer.Client.Net;
using MockServer.Client.Net.Models;

namespace mockserver.client.net.tests.integration
{
    public class UnitTest1
    {
        [Fact]
        public async void GivenExpectationIsSet_ThenMockServerRespondsToExpectation()
        {
            var expectedReponseBody = "This is the response body";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://mockserverhost:1080/");
                var mockServerClient = new MockServer.Client.Net.MockServerClient(
                    httpClient
                );

                await mockServerClient.SetExpectations(new Expectation
                {
                    HttpRequest = new HttpRequest
                    {
                        Path = "/hello",
                        Method = "GET",
                        Secure = false,
                        KeepAlive = true,
                        Body = "hellooooooo"

                    },
                    HttpResponse = new HttpResponse
                    {
                        Delay = new Delay
                        {
                            TimeUnit = "SECONDS",
                            Value = 0
                        },
                        StatusCode = 200,
                        Body = expectedReponseBody
                    }
                });


                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/hello");
                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                Assert.Equal(expectedReponseBody, responseContent);

            }
        }
    }
}
