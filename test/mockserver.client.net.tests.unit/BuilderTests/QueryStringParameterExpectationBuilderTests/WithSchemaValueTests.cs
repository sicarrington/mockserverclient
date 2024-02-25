using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.QueryStringParameterExpectationBuilderTests;

public class WithSchemaValueTests
{
    private IQueryStringParameterExpectationBuilder sut;

    public WithSchemaValueTests()
    {
        sut = QueryStringParameterExpectationBuilder.Build()
            .WithName("AParameterName");
    }
    
    [Fact]
    public void WhenValueIsUuidSchemaValue_ThenValueIsMappedCorrectly()
    {
        sut.WithValue(SchemaValue.Uuid());

        var result = sut.Create();
        
        Assert.Single(result.Value);
        Assert.Contains(result.Value, x =>
            ((dynamic)x).schema is SchemaValue schemaValue && 
            schemaValue.Type == "string" &&
            schemaValue.Pattern == null && 
            schemaValue.Format == "uuid");
    }
    
    [Fact]
    public void WhenValueIsIntegerSchemaValue_ThenValueIsMappedCorrectly()
    {
        sut.WithValue(SchemaValue.Integer());

        var result = sut.Create();
        
        Assert.Single(result.Value);
        Assert.Contains(result.Value, x =>
            ((dynamic)x).schema is SchemaValue schemaValue && 
            schemaValue.Type == "integer" &&
            schemaValue.Pattern == null && 
            schemaValue.Format == null);
    }
    
    
    [Fact]
    public void WhenValueIsStringWiothFormatSchemaValue_ThenValueIsMappedCorrectly()
    {
        sut.WithValue(SchemaValue.StringWithFormat("uuid"));
        
        var result = sut.Create();
        
        Assert.Single(result.Value);
        Assert.Contains(result.Value, x =>
            ((dynamic)x).schema is SchemaValue schemaValue && 
            schemaValue.Type == "string" &&
            schemaValue.Pattern == null && 
            schemaValue.Format == "uuid");
    }
    
    [Fact]
    public void WhenValueIsStringWithPatternSchemaValue_ThenValueIsMappedCorrectly()
    {
        var stringPattern = "^.*gzip.*$";
        sut.WithValue(SchemaValue.StringWithPattern(stringPattern));

        var result = sut.Create();
        
        Assert.Single(result.Value);
        Assert.Contains(result.Value, x =>
            ((dynamic)x).schema is SchemaValue schemaValue && 
            schemaValue.Type == "string" &&
            schemaValue.Pattern == stringPattern && 
            schemaValue.Format == null);
    }

    [Fact]
    public void WhenMultipleSchemaValuesAreIncluded_ThenAllValuesAreMappedCorrectly()
    {
        var expectedSimpleString = "ASimpleStringValue";
        var stringPattern = "^.*gzip.*$";
        sut.WithValue(SchemaValue.StringWithPattern(stringPattern));
        sut.WithValue(SchemaValue.Integer());
        sut.WithValue(expectedSimpleString);

        var result = sut.Create();

        Assert.Contains(result.Value, x =>
            ((dynamic)x).schema is SchemaValue schemaValue && 
            schemaValue.Type == "string" &&
            schemaValue.Pattern == stringPattern && 
            schemaValue.Format == null);
        Assert.Contains(result.Value, x =>
            ((dynamic)x).schema is SchemaValue schemaValue && 
            schemaValue.Type == "integer" &&
            schemaValue.Pattern == null && 
            schemaValue.Format == null);
        Assert.Contains(expectedSimpleString, result.Value);
    }
}