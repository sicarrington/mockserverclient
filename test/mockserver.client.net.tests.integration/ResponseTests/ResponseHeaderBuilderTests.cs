using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Integration.ResponseTests;

public class ResponseHeaderBuilderTests
{
    [Fact]
    public async Task GivenResponseHeaderWithSimpleValueIsSpecified_ThenResponseHeadersAreMappedCorrectly()
    {
        var headerOneName = "HeaderOneName";
        var headerOneValue = "HeaderOneValue";
        var path = $"/headerrepsonsetest{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path))
            .Respond(
                responseBuilder => responseBuilder
                    .WithStatusCode(200)
                    .WithHeaders(headersResponseBuilder => headersResponseBuilder
                        .WithHeader(headerResponseBuilder => headerResponseBuilder
                            .WithName(headerOneName)
                            .WithValues(headerValueResponseBuilder => headerValueResponseBuilder
                                .WithValue(headerOneValue))))
            );

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(response.Headers, pair => pair.Key == headerOneName && pair.Value.First() == headerOneValue);
    }
    
    [Fact]
    public async Task GivenResponseHeaderWithRequestDerivedValueIsSpecified_ThenResponseHeadersAreMappedCorrectly()
    {
        var headerOneName = "HeaderOneName";
        var path = $"/headerrepsonsetest{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path))
            .Respond(
                responseBuilder => responseBuilder
                    .WithStatusCode(200)
                    .WithHeaders(headersResponseBuilder => headersResponseBuilder
                        .WithHeader(headerResponseBuilder => headerResponseBuilder
                            .WithName(headerOneName)
                            .WithValues(headerValueResponseBuilder => headerValueResponseBuilder
                                .WithValue(request => request.Path))))
            );

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(response.Headers, pair => pair.Key == headerOneName && pair.Value.First() == path);
    }
    
    [Fact]
    public async Task GivenMultipleResponseHeadersAreSpecified_ThenResponseHeadersAreMappedCorrectly()
    {
        var headerOneName = "HeaderOneName";
        var headerOneValueOne = "HeaderOneValue";
        var headerOneValueTwo = "HeaderOneTwo";
        var headerTwoName = "HeaderTwoName";
        var headerTwoValueOne = "HeaderTwoValueOne";
        var headerThreeName = "HeaderThreeName";

        var path = $"/headerrepsonsetest{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path))
            .Respond(
                responseBuilder => responseBuilder
                    .WithStatusCode(200)
                    .WithHeaders(headersResponseBuilder => headersResponseBuilder
                        .WithHeader(headerResponseBuilder => headerResponseBuilder
                            .WithName(headerOneName)
                            .WithValues(headerValueResponseBuilder => headerValueResponseBuilder
                                .WithValue(headerOneValueOne)
                                .WithValue(headerOneValueTwo)))
                        .WithHeader(headerResponseBuilder => headerResponseBuilder
                            .WithName(headerTwoName)
                            .WithValues(headerValueResponseBuilder => headerValueResponseBuilder
                                .WithValue(headerTwoValueOne)
                                .WithValue(request => request.Path)))
                        .WithHeader(headerResponseBuilder => headerResponseBuilder
                            .WithName(headerThreeName)
                            .WithValues(headerValueResponseBuilder => headerValueResponseBuilder
                                .WithValue(request => request.Path))))
            );

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(response.Headers, pair => pair.Key == headerOneName && pair.Value.Contains(headerOneValueOne) && pair.Value.Contains(headerOneValueTwo));
        Assert.Contains(response.Headers, pair => pair.Key == headerTwoName && pair.Value.Contains(headerTwoValueOne) && pair.Value.Contains(path));
        Assert.Contains(response.Headers, pair => pair.Key == headerThreeName && pair.Value.First() == path);
    }
}