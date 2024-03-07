using System;
using System.Collections.Generic;
using System.Net.Http;
using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
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

        using var httpClient = new HttpClient();
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

    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenExpectationIsIntegerSchemaValue_ThenValidRequestIsMatched()
    {
        var path = $"/headertest-{Guid.NewGuid()}";
        var requestBody = "This is the request body";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        var expectedIntegerValue = new Random().Next();

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithPath(path)
                .WithMethod(HttpMethod.Post)
                .WithHeaders(headersResponseBuilder => headersResponseBuilder.WithHeader(
                    headerResponseBuilder => headerResponseBuilder
                        .WithName("HeaderTwo")
                        .WithValues(
                        headerValueResponseBuilder => headerValueResponseBuilder.WithValue(SchemaValue.Integer())))))
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, path);
        httpRequestMessage.Content = new StringContent(requestBody, System.Text.Encoding.UTF8);
        httpRequestMessage.Headers.Add("HeaderTwo", new[] { expectedIntegerValue.ToString() });
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenExpectationIsIntegerSchemaValue_ThenInvalidRequestIsNotMatched()
    {
        var path = $"/headertest-{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");
        
        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithPath(path)
                .WithMethod(HttpMethod.Get)
                .WithHeaders(headersResponseBuilder => headersResponseBuilder.WithHeader(
                    headerResponseBuilder => headerResponseBuilder
                        .WithName("HeaderTwo")
                        .WithValues(
                            headerValueResponseBuilder => headerValueResponseBuilder.WithValue(SchemaValue.Integer())))))
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        httpRequestMessage.Headers.Add("HeaderTwo", new[] { "NotAnInteger" });
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenExpectationIsOptionalIntegerSchemaValue_ThenRequestWithoutHeaderIsMatched()
    {
        var path = $"/headertest-{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");
        
        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithPath(path)
                .WithMethod(HttpMethod.Get)
                .WithHeaders(headersResponseBuilder => headersResponseBuilder
                    .WithHeader(
                        headerResponseBuilder => headerResponseBuilder
                            .WithName("?HeaderTwo")
                            .WithValues(
                                headerValueResponseBuilder => headerValueResponseBuilder.WithValue(SchemaValue.Integer())))
                    .WithHeader(
                        headerResponseBuilder => headerResponseBuilder
                            .WithName("?HeaderThree")
                            .WithValues(
                                headerValueResponseBuilder => headerValueResponseBuilder.WithValue(SchemaValue.Integer())))
                ))
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        httpRequestMessage.Headers.Add("HeaderThree", new[] { "1" });
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenExpectationIsExclusiveHeader_ThenRequestWithHeaderIsNotMatched()
    {
        var path = $"/headertest-{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");
        
        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithPath(path)
                .WithMethod(HttpMethod.Get)
                .WithHeaders(headersResponseBuilder => headersResponseBuilder
                    .WithHeader(
                        headerResponseBuilder => headerResponseBuilder
                            .WithName("!HeaderTwo")
                            .WithValues(
                                headerValueResponseBuilder => headerValueResponseBuilder.WithValue(SchemaValue.Integer())))
                ))
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        httpRequestMessage.Headers.Add("HeaderTwo", new[] { "1" });
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenExpectationIsRegexMatch_ThenRequestWithHeaderIsMatched()
    {
        var path = $"/headertest-{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");
        
        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithPath(path)
                .WithMethod(HttpMethod.Get)
                .WithHeaders(headersResponseBuilder => headersResponseBuilder
                    .WithHeader(
                        headerResponseBuilder => headerResponseBuilder
                            .WithName("Header.*")
                            .WithValues(
                                headerValueResponseBuilder => headerValueResponseBuilder.WithValue(@"[A-Z0-9\\-]+")))
                ))
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        httpRequestMessage.Headers.Add("HeaderTwo", new[] { "1" });
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenExpectationIsRegexMatchUsingSchemaValue_ThenRequestWithHeaderIsMatched()
    {
        var path = $"/headertest-{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");
        
        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithPath(path)
                .WithMethod(HttpMethod.Get)
                .WithHeaders(headersResponseBuilder => headersResponseBuilder
                    .WithHeader(
                        headerResponseBuilder => headerResponseBuilder
                            .WithName("Header.*")
                            .WithValues(
                                headerValueResponseBuilder => headerValueResponseBuilder.WithValue(SchemaValue.StringWithPattern("[A-Z0-9\\-]+"))))
                ))
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        httpRequestMessage.Headers.Add($"Header{Guid.NewGuid()}", new[] { "1" });
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}