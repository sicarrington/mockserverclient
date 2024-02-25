using System;
using System.Linq;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.HeadersResponseBuilderTests;

public class WithHeaderTests
{
    private readonly IHeadersResponseBuilder _headersResponseBuilder;

    public WithHeaderTests()
    {
        _headersResponseBuilder = HeadersResponseBuilder.Build(RequestBuilder.Build().Create());
    }
    
    [Fact]
    public void GivenHeaderIsAdded_WhenBuilderPassedIsNull_ThenException()
    {
        Assert.Throws<ArgumentNullException>(() => _headersResponseBuilder.WithHeader(null));
    }
    
    [Fact]
    public void GivenHeaderIsAdded_ThenHeaderIsBuiltOnCreate()
    {
        var headerName = "HeaderOneName";
        var headerValue = "HeaderOneValue";
        
        _headersResponseBuilder.WithHeader(builder =>
            builder.WithName(headerName).WithValues(valueBuilder => valueBuilder.WithValue(headerValue)));

        var headers = _headersResponseBuilder.Create();

        Assert.Contains(headers, pair => pair.Key == headerName && pair.Value.First() == headerValue);
    }

    [Fact]
    public void GivenMultipleHeadersAreAdded_ThenAllHeadersAreBuiltOnCreate()
    {
        var headerOneName = "HeaderOneName";
        var headerOneValue = "HeaderOneValue";
        var headerTwoName = "HeaderTwoName";
        var headerTwoValue = "HeaderTwoValue";
        
        _headersResponseBuilder.WithHeader(builder =>
            builder.WithName(headerOneName).WithValues(valueBuilder => valueBuilder.WithValue(headerOneValue)));
        _headersResponseBuilder.WithHeader(builder =>
            builder.WithName(headerTwoName).WithValues(valueBuilder => valueBuilder.WithValue(request => headerTwoValue)));

        var headers = _headersResponseBuilder.Create();

        Assert.Contains(headers, pair => pair.Key == headerOneName && pair.Value.First() == headerOneValue);
        Assert.Contains(headers, pair => pair.Key == headerTwoName && pair.Value.First() == headerTwoValue);
    }

    [Fact]
    public void GivenHeaderIsAdded_WhenHeaderValueIsDerivedFromRequest_ThenHeaderIsBuiltCorrectlyOnCreate()
    {
        var headerOneName = "HeaderOneName";
        var requestPath = "ThisIsTheRequestPath";

        var request = RequestBuilder.Build().WithPath(requestPath).Create();
        var headersResponseBuilder = HeadersResponseBuilder.Build(request);
        headersResponseBuilder.WithHeader(builder => builder
            .WithName(headerOneName)
            .WithValues(headerValueResponseBuilder =>
            headerValueResponseBuilder.WithValue(request => request.Path)));

        var headers = headersResponseBuilder.Create();

        Assert.Contains(headers, pair => pair.Key == headerOneName && pair.Value.First() == requestPath);
    }
}