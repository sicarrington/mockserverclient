using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.QueryStringParameterExpectationBuilderTests;

public class WithSchemaValueTests
{
    private QueryStringParameterExpectationBuilder sut;

    public WithSchemaValueTests()
    {
        sut = new QueryStringParameterExpectationBuilder()
            .WithName("AParameterName");
    }
    
    [Fact]
    public void WhenValueIsUuidSchemaValue_ThenValueIsMappedCorrectly()
    {
        sut.WithValue(SchemaValue.Uuid());

        var expectedValueObject = new
        {
            schema = new
            {
                type = "string",
                format = "uuid"
            }
        };

        var expectedValue = JsonSerializer.Serialize(expectedValueObject,
            new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        
        Assert.Equal(expectedValue, sut.Create().Value.First());
    }
    
    [Fact]
    public void WhenValueIsIntegerSchemaValue_ThenValueIsMappedCorrectly()
    {
        sut.WithValue(SchemaValue.Integer());

        var expectedValueObject = new
        {
            schema = new
            {
                type = "integer",
            }
        };

        var expectedValue = JsonSerializer.Serialize(expectedValueObject,
            new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        
        Assert.Equal(expectedValue, sut.Create().Value.First());
    }
    
    
    [Fact]
    public void WhenValueIsStringWiothFormatSchemaValue_ThenValueIsMappedCorrectly()
    {
        sut.WithValue(SchemaValue.StringWithFormat("uuid"));

        var expectedValueObject = new
        {
            schema = new
            {
                type = "string",
                format = "uuid"
            }
        };

        var expectedValue = JsonSerializer.Serialize(expectedValueObject,
            new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        
        Assert.Equal(expectedValue, sut.Create().Value.First());
    }
    
    [Fact]
    public void WhenValueIsStringWithPatternSchemaValue_ThenValueIsMappedCorrectly()
    {
        var stringPattern = "^.*gzip.*$";
        sut.WithValue(SchemaValue.StringWithPattern(stringPattern));

        var expectedValueObject = new
        {
            schema = new
            {
                type = "string",
                pattern = stringPattern
            }
        };

        var expectedValue = JsonSerializer.Serialize(expectedValueObject,
            new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        
        Assert.Equal(expectedValue, sut.Create().Value.First());
    }

    [Fact]
    public void WhenMultipleSchemaValuesAreIncluded_ThenAllValuesAreMappedCorrectly()
    {
        var expectedSimpleString = "ASimpleStringValue";
        var stringPattern = "^.*gzip.*$";
        sut.WithValue(SchemaValue.StringWithPattern(stringPattern));
        sut.WithValue(SchemaValue.Integer());
        sut.WithValue(expectedSimpleString);

        var expectedValueOneObject = new
        {
            schema = new
            {
                type = "string",
                pattern = stringPattern
            }
        };
        var expectedValueOne = JsonSerializer.Serialize(expectedValueOneObject,
            new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
        
        var expectedValueTwoObject = new
        {
            schema = new
            {
                type = "integer",
            }
        };
        var expectedValueTwo = JsonSerializer.Serialize(expectedValueTwoObject,
            new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        var result = sut.Create();
        
        Assert.True(result.Value.Contains(expectedValueOne));
        Assert.True(result.Value.Contains(expectedValueTwo));
        Assert.True(result.Value.Contains(expectedSimpleString));
    }
}