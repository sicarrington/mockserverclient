using MockServer.Client.Net.Builders;
using System;
using System.Net.Http;
using Xunit;


namespace MockServer.Client.Net.Tests.Integration
{
    public class BodyTests
    {
        [Fact]
        public async void GivenExpectationIsSetOnBody_WhenRequestIsMadeWithMatchingBody_ThenRequestIsMatched()
        {
            var path = "/bodytest";
            var requestBody = "This is the request body";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:1080/");

                var mockServerClient = new MockServerClient(httpClient)
                    .When(RequestBuilder.Request()
                        .WithMethod(HttpMethod.Post)
                        .WithPath(path)
                        .WithBody(requestBody))
                    .Respond(ResponseBuilder.Respond().WithStatusCode(200));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, path);
                httpRequestMessage.Content = new StringContent(requestBody, System.Text.Encoding.UTF8);
                var response = await httpClient.SendAsync(httpRequestMessage);

                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async void GivenExpectationIsSetOnBody_WhenRequestIsMadeWithNonMatchingBody_ThenRequestIsNotMatched()
        {
            var path = "/bodytest";
            var requestBody = "This is the request body";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:1080/");

                var mockServerClient = new MockServerClient(httpClient)
                    .When(RequestBuilder.Request()
                        .WithMethod(HttpMethod.Post)
                        .WithPath(path)
                        .WithBody(requestBody))
                    .Respond(ResponseBuilder.Respond().WithStatusCode(200));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, path);
                httpRequestMessage.Content = new StringContent("This body does not match", System.Text.Encoding.UTF8);
                var response = await httpClient.SendAsync(httpRequestMessage);

                Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}