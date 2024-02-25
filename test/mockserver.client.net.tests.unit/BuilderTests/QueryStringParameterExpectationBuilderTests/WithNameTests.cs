using System;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.QueryStringParameterExpectationBuilderTests;

public class WithNameTests
{
    [Fact]
    public void WhenNameIsEmpty_ThenErrorOnCreate()
    {
        var sut = QueryStringParameterExpectationBuilder.Build()
            .WithName(string.Empty);
        Assert.Throws<InvalidOperationException>(() => sut.Create());
    }

    [Fact]
    public void WhenNameIsNull_ThenErrorOnCreate()
    {
        var sut = QueryStringParameterExpectationBuilder.Build();
        Assert.Throws<InvalidOperationException>(() => sut.Create());
    }

    [Fact]
    public void WhenNameIsSetToSimpleString_ThenNameIsMappedCorrectly()
    {
        var expectedName = "TestName";
        var sut = QueryStringParameterExpectationBuilder.Build()
            .WithName(expectedName);
        
        Assert.Equal(expectedName, sut.Create().Key);
    }
    
    [Fact]
    public void WhenNameIsSetToRegexString_ThenNameIsMappedCorrectly()
    {
        var expectedName = "[A-Z0-9-]+$";
        var sut = QueryStringParameterExpectationBuilder.Build()
            .WithName(expectedName);
        
        Assert.Equal(expectedName, sut.Create().Key);
    }
}