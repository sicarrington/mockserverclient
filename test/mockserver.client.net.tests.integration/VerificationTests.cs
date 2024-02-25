using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using System;
using System.Net.Http;
using Xunit;


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

                var expectedRequest = RequestBuilder.Build()
                        .WithMethod(HttpMethod.Post)
                        .WithPath(path)
                        .WithBody(requestBody);
                var mockServerClient = new MockServerClient(httpClient);
                mockServerClient.When(expectedRequest)
                    .Respond(builder => builder.WithStatusCode(200));
                
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/bodytest123");
                httpRequestMessage.Content = new StringContent(requestBody, System.Text.Encoding.UTF8);
                var response = await httpClient.SendAsync(httpRequestMessage);

                var result = await mockServerClient.Verify(expectedRequest.Create(), VerificationTimes.Once);
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

                var expectedRequest = RequestBuilder.Build()
                        .WithMethod(HttpMethod.Post)
                        .WithPath(path)
                        .WithBody(requestBody);
                var mockServerClient = new MockServerClient(httpClient);
                mockServerClient.When(expectedRequest)
                .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/bodytestfourfivesix");
                httpRequestMessage.Content = new StringContent(requestBody, System.Text.Encoding.UTF8);
                var response = await httpClient.SendAsync(httpRequestMessage);

                var result = await mockServerClient.Verify(expectedRequest.Create(), new VerificationTimes(1, 1));
                Assert.False(result);
            }
        }
    }
}