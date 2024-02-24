using System;
using System.Collections.Generic;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests;

public class WithCookiesTests
{
    private readonly RequestBuilder _requestBuilder;

    public WithCookiesTests()
    {
        _requestBuilder = new RequestBuilder();
    }

    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsNull_ThenExceptionIsThrown()
    {
        Assert.Throws<ArgumentNullException>(() => { new RequestBuilder().WithCookies(null); });
    }

    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsValid_ThenValueIsMappedCorrectly()
    {
        var expectedCookies = new Dictionary<string, string>
            { { "CookieOne", "ValueOne" }, { "CookieTwo", "ValueTwo" } };
        var result = new RequestBuilder().WithCookies(expectedCookies);
        Assert.Equal(expectedCookies, result.Create().Cookies);
    }
}