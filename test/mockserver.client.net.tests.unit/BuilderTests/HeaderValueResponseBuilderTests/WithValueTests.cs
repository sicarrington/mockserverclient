using System;
using System.Linq;
using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.HeaderValueResponseBuilderTests;

public class WithValueTests
{
    [Fact]
    public void GivenHeaderValueIsSet_ThenValueIsRecorded()
    {
        var expectedHeaderValue = "HeaderValueOne";

        var headerValueBuilder = HeaderValueResponseBuilder.Build(new HttpRequest());
        headerValueBuilder.WithValue(expectedHeaderValue);

        var result = headerValueBuilder.Create();

        Assert.Equal(expectedHeaderValue, result.First().ToString());
    }
    
    [Fact]
    public void GivenMultipleHeaderValuesAreSet_ThenValuesAreRecorded()
    {
        var expectedHeaderValueOne = "HeaderValueOne";
        var expectedHeaderValueTwo = "HeaderValueTwo";
        var expectedHeaderValueThree = "HeaderValueThree";

        var headerValueBuilder = HeaderValueResponseBuilder.Build(new HttpRequest());
        headerValueBuilder.WithValue(expectedHeaderValueOne);
        headerValueBuilder.WithValue(expectedHeaderValueTwo);
        headerValueBuilder.WithValue(expectedHeaderValueThree);

        var result = headerValueBuilder.Create().ToArray();

        Assert.Contains(result, o => o.ToString() == expectedHeaderValueOne);
        Assert.Contains(result, o => o.ToString() == expectedHeaderValueTwo);
        Assert.Contains(result, o => o.ToString() == expectedHeaderValueThree);
    }
}