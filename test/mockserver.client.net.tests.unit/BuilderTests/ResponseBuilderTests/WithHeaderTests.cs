using System.Collections.Generic;
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
        var headerValueOne = "HeaderOneValueOne";
        var headerValueTwo = "HeaderOneValueTwo";
        var sut = ResponseBuilder.Build(RequestBuilder.Build().Create());

        sut.WithHeaders(new Dictionary<string, IEnumerable<string>>
        {
            { headerName, new[] { headerValueOne, headerValueTwo } }
        });

        var response = sut.Create();

        Assert.Contains(response.Headers,
            pair => pair.Key == headerName && pair.Value.Select(x => x.ToString()).Contains(headerValueOne) &&
                    pair.Value.Select(x => x.ToString()).Contains(headerValueTwo));
    }
}