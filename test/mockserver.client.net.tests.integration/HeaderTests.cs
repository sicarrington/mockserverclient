using System;
using System.Collections.Generic;
using System.Net.Http;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Integration;

public class HeaderTests
{
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenRequestIsMadeWithMatchingHeader_ThenRequestIsMatched()
    {
        var path = "/headertest";
        var requestBody = "This is the request body";

        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("http://localhost:1080/");

            new MockServerClient(httpClient)
                .When(RequestBuilder.Build()
                    .WithPath(path)
                    .WithMethod(HttpMethod.Post)
                    .WithHeaders(new Dictionary<string, IEnumerable<string>>{{"HeaderOne", new[]{"ValueOne"}}}))
                .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, path);
            httpRequestMessage.Content = new StringContent(requestBody, System.Text.Encoding.UTF8);
            httpRequestMessage.Headers.Add("HeaderOne", new []{"ValueOne"});
            var response = await httpClient.SendAsync(httpRequestMessage);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
    
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenRequestIsMadeWithNonMatchingHeader_ThenRequestIsNotMatched()
    {
        var path = "/headertestfail";
        var requestBody = "This is the request body";

        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("http://localhost:1080/");

            new MockServerClient(httpClient)
                .When(RequestBuilder.Build()
                    .WithPath(path)
                    .WithMethod(HttpMethod.Post)
                    .WithHeaders(new Dictionary<string, IEnumerable<string>>{{"HeaderOne", new[]{"ValueOne"}}}))
                .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, path);
            httpRequestMessage.Content = new StringContent(requestBody, System.Text.Encoding.UTF8);
            httpRequestMessage.Headers.Add("HeaderTwo", new []{"ValueTwo"});
            var response = await httpClient.SendAsync(httpRequestMessage);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}