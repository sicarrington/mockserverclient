using MockServer.Client.Net.Builders;
using System;
using System.Net.Http;
using Xunit;


namespace MockServer.Client.Net.Tests.Integration
{
    public class PathTests
    {
        [Fact]
        public async void GivenExpectationSetOnPath_WhenRequestIsMadeForMatchingPath_ThenMatchIsMade()
        {
            var path = "/pathtest";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:1080/");

                var mockServerClient = new MockServerClient(httpClient)
                    .When(RequestBuilder.Build()
                        .WithMethod(HttpMethod.Get)
                        .WithPath(path))
                    .Respond(ResponseBuilder.Build().WithStatusCode(200));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
                var response = await httpClient.SendAsync(httpRequestMessage);

                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async void GivenExpectationIsSetOnPath_WhenRequestIsMadeForNonMatchingPath_ThenMatchIsNotMade()
        {
            var path = "/pathtestnotmatching";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:1080/");

                // var mockServerClient = new MockServerClient(httpClient)
                //     .When(RequestBuilder.Request()
                //         .WithMethod(HttpMethod.Get)
                //         .WithPath(path))
                //     .Respond(ResponseBuilder.Respond().WithStatusCode(200));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
                var response = await httpClient.SendAsync(httpRequestMessage);

                Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}