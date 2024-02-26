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

        var result = RequestBuilder.Build().WithQueryStringParameters(queryStringExpectationBuilder =>
            queryStringExpectationBuilder.WithParameter(parameterBuilder =>
                    parameterBuilder.WithName("ParameterOne")
                        .WithValue("ParameterOneValueA")
                        .WithValue("ParameterOneValueB"))
                .WithParameter(parameterBuilder =>
                    parameterBuilder.WithName("ParameterTwo")
                        .WithValue("ParameterTwoValueA")));
        Assert.Equal(queryStringParameters, result.Create().QueryStringParameters);
    }
}