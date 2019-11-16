using System;
using Xunit;
using System.Net.Http;
using MockServer.Client.Net;
using MockServer.Client.Net.Models;
using MockServer.Client.Net.Builders;
using System.Net.Http;


namespace MockServer.Client.Net.Tests.Integration
{
    public class VerificationTests
    {
        [Fact]
        public async void GivenMatchingExpectationIsExecuted_WhenVerificationIsAttempted_ThenVerificationSuceeds()
        {
            var path = "/bodytest123";
            var requestBody = "This is the request body 123";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:1080/");

                var expectedRequest = RequestBuilder.Request()
                        .WithMethod(HttpMethod.Post)
                        .WithPath(path)
                        .WithBody(requestBody);
                var mockServerClient = new MockServerClient(httpClient);
                mockServerClient.When(expectedRequest)
                .Respond(ResponseBuilder.Respond().WithStatusCode(200));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/bodytest123");
                httpRequestMessage.Content = new StringContent(requestBody, System.Text.Encoding.UTF8);
                var response = await httpClient.SendAsync(httpRequestMessage);

                var result = await mockServerClient.Verify(expectedRequest.Create(), new VerificationTimes(1, 1));
                Assert.True(result);
            }
        }
        [Fact]
        public async void GivenRequestDoesNotMatchVerification_WhenVerificationIsAttempted_ThenVerificationSuceeds()
        {
            var path = "/bodytest456";
            var requestBody = "This is the request body 456";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:1080/");

                var expectedRequest = RequestBuilder.Request()
                        .WithMethod(HttpMethod.Post)
                        .WithPath(path)
                        .WithBody(requestBody);
                var mockServerClient = new MockServerClient(httpClient);
                mockServerClient.When(expectedRequest)
                .Respond(ResponseBuilder.Respond().WithStatusCode(200));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/bodytestfourfivesix");
                httpRequestMessage.Content = new StringContent(requestBody, System.Text.Encoding.UTF8);
                var response = await httpClient.SendAsync(httpRequestMessage);

                var result = await mockServerClient.Verify(expectedRequest.Create(), new VerificationTimes(1, 1));
                Assert.False(result);
            }
        }
    }
}