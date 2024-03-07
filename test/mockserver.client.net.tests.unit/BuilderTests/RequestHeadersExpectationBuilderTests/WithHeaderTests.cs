using System;
using System.Linq;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestHeadersExpectationBuilderTests;

public class WithHeaderTests
{
    private readonly IRequestHeadersExpectationBuilder _sut = RequestHeadersExpectationBuilder.Build();

    [Fact]
    public void GivenHeaderIsAdded_WhenBuilderPassedIsNull_ThenException()
    {
        Assert.Throws<ArgumentNullException>(() => _sut.WithHeader(null));
    }
    
    [Fact]
    public void GivenHeaderIsAdded_ThenHeaderIsBuiltOnCreate()
    {
        var headerName = "HeaderOneName";
        var headerValue = "HeaderOneValue";
        
        _sut.WithHeader(builder =>
            builder.WithName(headerName).WithValues(valueBuilder => valueBuilder.WithValue(headerValue)));

        var headers = _sut.Create();

        Assert.Contains(headers, pair => pair.Key == headerName && pair.Value.First().ToString() == headerValue);
    }

    [Fact]
    public void GivenMultipleHeadersAreAdded_ThenAllHeadersAreBuiltOnCreate()
    {
        var headerOneName = "HeaderOneName";
        var headerOneValue = "HeaderOneValue";
        var headerTwoName = "HeaderTwoName";
        var headerTwoValue = "HeaderTwoValue";
        
        _sut.WithHeader(builder =>
            builder.WithName(headerOneName).WithValues(valueBuilder => valueBuilder.WithValue(headerOneValue)));
        _sut.WithHeader(builder =>
            builder.WithName(headerTwoName).WithValues(valueBuilder => valueBuilder.WithValue(headerTwoValue)));

        var headers = _sut.Create();

        Assert.Contains(headers, pair => pair.Key == headerOneName && pair.Value.First().ToString() == headerOneValue);
        Assert.Contains(headers, pair => pair.Key == headerTwoName && pair.Value.First().ToString() == headerTwoValue);
    }
}