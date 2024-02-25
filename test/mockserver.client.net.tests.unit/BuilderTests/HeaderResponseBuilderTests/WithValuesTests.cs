using System;
using System.Linq;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.HeaderResponseBuilderTests;

public class WithValuesTests
{
    [Fact]
    public void GivenHeaderValueHasNotBeenSet_WhenCreateIsCalled_ThenErrorIsThrown()
    {
        var expectedHeaderName = "HeaderOneName";
        var headerResponseBuilder = HeaderResponseBuilder.Build(RequestBuilder.Build().Create());

        headerResponseBuilder.WithName(expectedHeaderName);

        Assert.Throws<InvalidOperationException>(() => headerResponseBuilder.Create());
    }
    
    [Fact]
    public void GivenHeaderNameHasNotBeenSet_WhenCreateIsCalled_ThenErrorIsThrown()
    {
        var expectedHeaderValue = "TestValue";
        var headerResponseBuilder = HeaderResponseBuilder.Build(RequestBuilder.Build().Create());

        headerResponseBuilder
            .WithValues(builder => builder.WithValue(expectedHeaderValue));

        Assert.Throws<InvalidOperationException>(() => headerResponseBuilder.Create());
    }

    [Fact]
    public void GivenHeaderValueFunctionIsSpecified_WhenHeaderValuesAreConfigured_ThenHeaderValuesAreAddedAtCreation()
    {
        var expectedHeaderName = "HeaderOneName";
        var expectedHeaderValue = "TestValue";
        var headerResponseBuilder = HeaderResponseBuilder.Build(RequestBuilder.Build().Create());

        headerResponseBuilder.WithName(expectedHeaderName)
            .WithValues(builder => builder.WithValue(expectedHeaderValue));

        var header = headerResponseBuilder.Create();
        
        Assert.Equal(expectedHeaderName, header.Key);
        Assert.Equal(expectedHeaderValue, header.Value.First());
    }

    [Fact]
    public void GivenHeaderValueFunctionIsSpecified_WhenHeaderFuncIsConfigured_ThenHeaderValuesAreAddedAtCreate()
    {
        var expectedHeaderName = "HeaderOneName";
        var expectedHeaderValue = "TestValue";
        var headerResponseBuilder = HeaderResponseBuilder.Build(RequestBuilder.Build().Create());

        headerResponseBuilder.WithName(expectedHeaderName)
            .WithValues(builder => builder.WithValue(request => expectedHeaderValue));

        var header = headerResponseBuilder.Create();
        
        Assert.Equal(expectedHeaderName, header.Key);
        Assert.Equal(expectedHeaderValue, header.Value.First());
    }
}