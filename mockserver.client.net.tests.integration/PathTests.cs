using System;
using Xunit;
using System.Net.Http;
using MockServer.Client.Net;
using MockServer.Client.Net.Models;
using MockServer.Client.Net.Builders;
using System.Net.Http;


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
                    .When(RequestBuilder.Request()
                        .WithMethod(HttpMethod.Get)
                        .WithPath(path))
                    .Respond(ResponseBuilder.Respond().WithStatusCode(200));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
                var response = await httpClient.SendAsync(httpRequestMessage);

                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async void GivenExpectationIsSetOnPath_WhenRequestIsMadeForNonMatchingPath_ThenMatchIsNotMade()
        {
            var path = "/pathtest";

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