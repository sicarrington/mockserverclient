using System.Collections.Generic;
using System.Linq;
using MockServer.Client.Net.Builders;
using Moq;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.QueryStringExpectationBuilderTests;

public class AddParameterTests
{
    [Fact]
    public void GivenAddParameter_ThenBuilderCreateIsCalled()
    {
        var queryStringParameterExpectationBuilder = new Mock<IQueryStringParameterExpectationBuilder>();
        queryStringParameterExpectationBuilder.Setup(x => x.Create())
            .Returns(new KeyValuePair<string, IEnumerable<object>>("KeyOne", new[] { "ValueOne" }));
        
        var sut = QueryStringExpectationBuilder.Build().WithParameter(queryStringParameterExpectationBuilder.Object);

        queryStringParameterExpectationBuilder.Verify(x => x.Create(), Times.Once);
    }
    
    [Fact]
    public void GivenAddParameter_ThenBuilderCreateOutputIsAddedToQueryString()
    {
        var queryStringParameterExpectationBuilder = new Mock<IQueryStringParameterExpectationBuilder>();
        queryStringParameterExpectationBuilder.Setup(x => x.Create())
            .Returns(new KeyValuePair<string, IEnumerable<object>>("KeyOne", new[] { "ValueOne" }));
        
        var sut = QueryStringExpectationBuilder.Build().WithParameter(queryStringParameterExpectationBuilder.Object);

        var result = sut.Create();
        Assert.Contains(result, pair => pair.Key == "KeyOne" && (string)pair.Value.First() == "ValueOne");
    }
}