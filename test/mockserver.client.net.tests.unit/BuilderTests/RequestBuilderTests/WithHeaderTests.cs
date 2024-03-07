using System;
using System.Collections.Generic;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests;

public class WithHeaderTests
{
    private readonly IRequestBuilder _sut = RequestBuilder.Build();

    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsNull_ThenExceptionIsThrown()
    {
        Assert.Throws<ArgumentNullException>(() => { _sut.WithHeaders((IDictionary<string, IEnumerable<string>>)null); });
    }

    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsValid_ThenValueIsMappedCorrectly()
    {
        var expectedHeaders = new Dictionary<string, IEnumerable<object>> { {"HeaderOne", new[] { "ValueOne" } }};
        var result = _sut.WithHeaders(new Dictionary<string, IEnumerable<string>> { {"HeaderOne", new[] { "ValueOne" } }});
        Assert.Equal(expectedHeaders, result.Create().Headers);
    }
}