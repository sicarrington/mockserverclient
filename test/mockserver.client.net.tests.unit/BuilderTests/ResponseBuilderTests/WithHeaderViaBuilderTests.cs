using System.Linq;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.ResponseBuilderTests;

public class WithHeaderViaBuilderTests
{
    [Fact]
    public void WhenWithHeadersIsNotCalled_ThenHeadersAreNotAddedToResponse()
    {
        var sut = ResponseBuilder.Build(RequestBuilder.Build().Create());

        var response = sut.Create();
        
        Assert.Null(response.Headers);
    }
    
    [Fact]
    public void WhenHeadersFunctionIsSpecified_ThenHeadersAreAddedToCreatedResponse()
    {
        var headerName = "HeaderOne";
        var headerValue = "HeaderOneValue";
        var sut = ResponseBuilder.Build(RequestBuilder.Build().Create());

        sut.WithHeaders(headersResponseBuilder => headersResponseBuilder.WithHeader(headerResponseBuilder =>
            headerResponseBuilder.WithName(headerName)
                .WithValues(headerValuesBuilder => headerValuesBuilder.WithValue(headerValue))));

        var response = sut.Create();

        Assert.Contains(response.Headers, pair => pair.Key == headerName && pair.Value.First().ToString() == headerValue);
    }
}