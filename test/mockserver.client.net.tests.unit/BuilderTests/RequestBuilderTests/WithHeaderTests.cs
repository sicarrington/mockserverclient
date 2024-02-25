using System;
using System.Collections.Generic;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests;

public class WithHeaderTests
{
    private readonly IRequestBuilder _requestBuilder;
        
    public WithHeaderTests()
    {
        _requestBuilder = RequestBuilder.Build();
    }
    
    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsNull_ThenExceptionIsThrown()
    {
        Assert.Throws<ArgumentNullException>(() => { RequestBuilder.Build().WithHeaders(null); });
    }

    [Fact]
    public void GivenWithHeaders_WhenHeadersProvidedIsValid_ThenValueIsMappedCorrectly()
    {
        var expectedHeaders = new Dictionary<string, IEnumerable<string>> { {"HeaderOne", new[] { "ValueOne" } }};
        var result = RequestBuilder.Build().WithHeaders(expectedHeaders);
        Assert.Equal(expectedHeaders, result.Create().Headers);
    }
}