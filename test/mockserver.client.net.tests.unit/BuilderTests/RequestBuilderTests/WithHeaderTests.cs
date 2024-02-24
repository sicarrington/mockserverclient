using System;
using System.Collections.Generic;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests;

public class WithHeaderTests
{
    private readonly RequestBuilder _requestBuilder;
        
    public WithHeaderTests()
    {
        _requestBuilder = new RequestBuilder();
    }
    
    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsNull_ThenExceptionIsThrown()
    {
        Assert.Throws<ArgumentNullException>(() => { new RequestBuilder().WithHeaders(null); });
    }

    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsValid_ThenValueIsMappedCorrectly()
    {
        var expectedHeaders = new Dictionary<string, IEnumerable<string>> { {"HeaderOne", new[] { "ValueOne" } }};
        var result = new RequestBuilder().WithHeaders(expectedHeaders);
        Assert.Equal(expectedHeaders, result.Create().Headers);
    }
}