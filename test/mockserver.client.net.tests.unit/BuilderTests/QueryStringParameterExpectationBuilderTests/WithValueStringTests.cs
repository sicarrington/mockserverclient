using System.Linq;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.QueryStringParameterExpectationBuilderTests;

public class WithValueStringTests
{
    private IQueryStringParameterExpectationBuilder sut;

    public WithValueStringTests()
    {
        sut = QueryStringParameterExpectationBuilder.Build()
            .WithName("AParameterName");
    }
    
    [Fact]
    public void WhenValueIsSimpleString_ThenValueIsMappedCorrectly()
    {
        var expectedValue = "ThisIsAValue";
        sut.WithValue(expectedValue);
        
        Assert.Equal(expectedValue, sut.Create().Value.First());
    }
    
    [Fact]
    public void WhenValueIsRegex_ThenValueIsMappedCorrectly()
    {
        var expectedValue = "^[A-Z0-9-]+$";
        sut.WithValue(expectedValue);
        
        Assert.Equal(expectedValue, sut.Create().Value.First());
    }

    [Fact]
    public void WhenMultipleValuesAreSpecified_ThenAllValuesAreMappedCorrectly()
    {
        var expectedValueOne = "^[A-Z0-9-]+$";
        var expectedValueTwo = "ExpectedValue123";
        var expectedValueThree = "[A-z]{0,10}";
        var expectedValueFour = "ExpectedValue456";

        sut.WithValue(expectedValueOne);
        sut.WithValue(expectedValueTwo);
        sut.WithValue(expectedValueThree);
        sut.WithValue(expectedValueFour);

        var result = sut.Create();
        
        Assert.Equal(4, result.Value.Count());
        Assert.Contains(expectedValueOne, result.Value);
        Assert.Contains(expectedValueTwo, result.Value);
        Assert.Contains(expectedValueThree, result.Value);
        Assert.Contains(expectedValueFour, result.Value);
    }
}