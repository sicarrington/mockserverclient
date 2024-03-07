using System;
using System.Collections.Generic;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests;

public class WithCookiesTests
{
    private readonly IRequestBuilder _sut = RequestBuilder.Build();

    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsNull_ThenExceptionIsThrown()
    {
        Assert.Throws<ArgumentNullException>(() => { _sut.WithCookies(null); });
    }

    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsValid_ThenValueIsMappedCorrectly()
    {
        var expectedCookies = new Dictionary<string, string>
            { { "CookieOne", "ValueOne" }, { "CookieTwo", "ValueTwo" } };
        var result = _sut.WithCookies(expectedCookies);
        Assert.Equal(expectedCookies, result.Create().Cookies);
    }
}