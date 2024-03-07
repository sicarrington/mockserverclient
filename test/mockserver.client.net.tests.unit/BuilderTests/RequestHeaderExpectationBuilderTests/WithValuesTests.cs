using System;
using System.Linq;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestHeaderExpectationBuilderTests;

public class WithValuesTests
{
    [Fact]
    public void GivenHeaderValueHasNotBeenSet_WhenCreateIsCalled_ThenErrorIsThrown()
    {
        var expectedHeaderName = "HeaderOneName";
        var sut = RequestHeaderExpectationBuilder.Build();

        sut.WithName(expectedHeaderName);

        Assert.Throws<InvalidOperationException>(() => sut.Create());
    }
    
    [Fact]
    public void GivenHeaderNameHasNotBeenSet_WhenCreateIsCalled_ThenErrorIsThrown()
    {
        var expectedHeaderValue = "TestValue";
        var sut = RequestHeaderExpectationBuilder.Build();

        sut.WithValues(builder => builder.WithValue(expectedHeaderValue));

        Assert.Throws<InvalidOperationException>(() => sut.Create());
    }

    [Fact]
    public void GivenHeaderValueFunctionIsSpecified_WhenHeaderValuesAreConfigured_ThenHeaderValuesAreAddedAtCreation()
    {
        var expectedHeaderName = "HeaderOneName";
        var expectedHeaderValue = "TestValue";
        var sut = RequestHeaderExpectationBuilder.Build();

        sut.WithName(expectedHeaderName)
            .WithValues(builder => builder.WithValue(expectedHeaderValue));

        var header = sut.Create();
        
        Assert.Equal(expectedHeaderName, header.Key);
        Assert.Equal(expectedHeaderValue, header.Value.First());
    }
}