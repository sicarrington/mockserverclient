using System.Collections;
using System.Linq;
using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestHeaderValuesExpectationBuilderTests;

public class WithSchemaValueTests
{
    private readonly IRequestHeaderValuesExpectationBuilder _sut = RequestHeaderValuesExpectationBuilder.Build();

    [Fact]
    public void WhenValueIsUuidSchemaValue_ThenValueIsMappedCorrectly()
    {
        _sut.WithValue(SchemaValue.Uuid());

        var result = _sut.Create().ToArray();
        
        Assert.Single((IEnumerable)result);
        Assert.Contains(result, x =>
            ((dynamic)x).schema is SchemaValue { Type: "string", Pattern: null, Format: "uuid" });
    }
    
    [Fact]
    public void WhenValueIsIntegerSchemaValue_ThenValueIsMappedCorrectly()
    {
        _sut.WithValue(SchemaValue.Integer());

        var result = _sut.Create().ToArray();
        
        Assert.Single(result);
        Assert.Contains(result, x =>
            ((dynamic)x).schema is SchemaValue { Type: "integer", Pattern: null, Format: null });
    }
    
    [Fact]
    public void WhenValueIsStringWithFormatSchemaValue_ThenValueIsMappedCorrectly()
    {
        _sut.WithValue(SchemaValue.StringWithFormat("uuid"));
        
        var result = _sut.Create().ToArray();
        
        Assert.Single(result);
        Assert.Contains(result, x =>
            ((dynamic)x).schema is SchemaValue { Type: "string", Pattern: null, Format: "uuid" });
    }
    
    [Fact]
    public void WhenValueIsStringWithPatternSchemaValue_ThenValueIsMappedCorrectly()
    {
        var stringPattern = "^.*gzip.*$";
        _sut.WithValue(SchemaValue.StringWithPattern(stringPattern));

        var result = _sut.Create().ToArray();
        
        Assert.Single(result);
        Assert.Contains(result, x =>
            ((dynamic)x).schema is SchemaValue { Type: "string" } schemaValue &&
            schemaValue.Pattern == stringPattern && 
            schemaValue.Format == null);
    }

    [Fact]
    public void WhenMultipleSchemaValuesAreIncluded_ThenAllValuesAreMappedCorrectly()
    {
        var expectedSimpleString = "ASimpleStringValue";
        var stringPattern = "^.*gzip.*$";
        _sut.WithValue(SchemaValue.StringWithPattern(stringPattern));
        _sut.WithValue(SchemaValue.Integer());
        _sut.WithValue(expectedSimpleString);

        var result = _sut.Create().ToArray();

        Assert.Contains(result, x =>
            ((dynamic)x).schema is SchemaValue { Type: "string" } schemaValue &&
            schemaValue.Pattern == stringPattern && 
            schemaValue.Format == null);
        Assert.Contains(result, x =>
            ((dynamic)x).schema is SchemaValue { Type: "integer", Pattern: null, Format: null });
        Assert.Contains(expectedSimpleString, result);
    }
}