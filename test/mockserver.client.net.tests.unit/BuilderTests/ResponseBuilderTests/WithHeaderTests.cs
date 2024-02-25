using System.Linq;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.ResponseBuilderTests;

public class WithHeaderTests
{
    [Fact]
    public void WhenWithHeadersIsNotCalled_ThenHeadersAreNotAddedToResponse()
    {
        var responseBuilder = ResponseBuilder.Build(RequestBuilder.Build().Create());

        var response = responseBuilder.Create();
        
        Assert.Null(response.Headers);
    }
    
    [Fact]
    public void WhenHeadersFunctionIsSpecified_ThenHeadersAreAddedToCreatedResponse()
    {
        var headerName = "HeaderOne";
        var headerValue = "HeaderOneValue";
        var responseBuilder = ResponseBuilder.Build(RequestBuilder.Build().Create());

        responseBuilder.WithHeaders(headersResponseBuilder => headersResponseBuilder.WithHeader(headerResponseBuilder =>
            headerResponseBuilder.WithName(headerName)
                .WithValues(headerValuesBuilder => headerValuesBuilder.WithValue(headerValue))));

        var response = responseBuilder.Create();

        Assert.Contains(response.Headers, pair => pair.Key == headerName && pair.Value.First() == headerValue);
    }
}