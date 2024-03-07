using System.Collections.Generic;
using System.Linq;
using MockServer.Client.Net.Builders;
using Moq;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.QueryStringExpectationBuilderTests;

public class WithParameterTests
{
    [Fact]
    public void GivenAddParameter_ThenBuilderCreateOutputIsAddedToQueryString()
    {
        var sut = QueryStringExpectationBuilder.Build()
            .WithParameter(queryStringParameterExpectationBuilder =>
                queryStringParameterExpectationBuilder
                    .WithName("KeyOne")
                    .WithValue("ValueOne"));

        var result = sut.Create();
        
        Assert.Contains(result, pair => pair.Key == "KeyOne" && (string)pair.Value.First() == "ValueOne");
    }
}