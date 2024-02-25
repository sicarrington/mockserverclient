using System.Collections.Generic;
using MockServer.Client.Net.Builders;
using Moq;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests;

public class WithQueryStringParametersTests
{
    [Fact]
    public void GivenWithQueryString_ThenQueryStringIsBuiltCorrectly()
    {
        var queryStringParameters = new Dictionary<string, IEnumerable<object>>
        {
            { "ParameterOne", new[] { "ParameterOneValueA", "ParameterOneValueB" } },
            { "ParameterTwo", new[] { "ParameterTwoValueA" } }
        };
        var queryStringExpectationBuilder = new Mock<IQueryStringExpectationBuilder>();
        queryStringExpectationBuilder.Setup(x => x.Create())
            .Returns(queryStringParameters);

        var result = RequestBuilder.Build().WithQueryStringParameters(queryStringExpectationBuilder.Object);
        Assert.Equal(queryStringParameters, result.Create().QueryStringParameters);
    }
}