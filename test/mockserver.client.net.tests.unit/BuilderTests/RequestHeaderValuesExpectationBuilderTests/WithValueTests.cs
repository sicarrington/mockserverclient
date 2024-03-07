using System.Linq;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestHeaderValuesExpectationBuilderTests;

public class WithValueTests
{
    [Fact]
    public void GivenHeaderValueIsSet_ThenValueIsRecorded()
    {
        var expectedHeaderValue = "HeaderValueOne";

        var sut = RequestHeaderValuesExpectationBuilder.Build();
        sut.WithValue(expectedHeaderValue);

        var result = sut.Create();

        Assert.Equal(expectedHeaderValue, result.First().ToString());
    }
    
    [Fact]
    public void GivenMultipleHeaderValuesAreSet_ThenValuesAreRecorded()
    {
        var expectedHeaderValueOne = "HeaderValueOne";
        var expectedHeaderValueTwo = "HeaderValueTwo";
        var expectedHeaderValueThree = "HeaderValueThree";

        var sut = RequestHeaderValuesExpectationBuilder.Build();
        sut.WithValue(expectedHeaderValueOne);
        sut.WithValue(expectedHeaderValueTwo);
        sut.WithValue(expectedHeaderValueThree);

        var result = sut.Create().ToArray();

        Assert.Contains(result, o => o.ToString() == expectedHeaderValueOne);
        Assert.Contains(result, o => o.ToString() == expectedHeaderValueTwo);
        Assert.Contains(result, o => o.ToString() == expectedHeaderValueThree);
    }
}