using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Integration;

public class CookiesTest
{
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenRequestIsMadeWithMatchingHeader_ThenRequestIsMatched()
    {
        var path = "/cookietest";

        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler { CookieContainer = cookieContainer };
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(new RequestBuilder()
                .WithPath(path)
                .WithMethod(HttpMethod.Get)
                .WithCookies(new Dictionary<string, string>{{"CookieOne", "CookieValueOne"}}))
            .Respond(new ResponseBuilder().WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        cookieContainer.Add(httpClient.BaseAddress, new Cookie("CookieOne", "CookieValueOne"));
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async void GivenExpectationIsSetOnHeader_WhenRequestIsMadeWithNonMatchingHeader_ThenRequestIsNotMatched()
    {
        var path = "/cookietestfail";

        var cookieContainer = new CookieContainer();
        using var handler = new HttpClientHandler { CookieContainer = cookieContainer };
        using var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(new RequestBuilder()
                .WithPath(path)
                .WithMethod(HttpMethod.Get)
                .WithCookies(new Dictionary<string, string>{{"CookieOneFail", "CookieValueOne"}}))
            .Respond(new ResponseBuilder().WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        cookieContainer.Add(httpClient.BaseAddress, new Cookie("CookieOne", "CookieValueOne"));
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
}