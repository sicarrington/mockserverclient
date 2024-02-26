using System;
using System.Net.Http;
using System.Threading.Tasks;
using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using Xunit;

namespace MockServer.Client.Net.Tests.Integration;

public class QueryStringTests
{
    [Fact]
    public async Task GivenASimpleStringMatchQueryString_ThenRequestIsMatched()
    {
        var path = $"/querystringtest{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path)
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("ParameterOne")
                        .WithValue("ParamterOneValue"))
                )
            )
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}?ParameterOne=ParamterOneValue");
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GivenAUuidSchemaValueMatch_ThenRequestIsMatched()
    {
        var path = $"/querystringtest{Guid.NewGuid()}";
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path)
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("Test2ParameterOne")
                        .WithValue(SchemaValue.Uuid()))
                )
            )
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}?Test2ParameterOne={Guid.NewGuid()}");
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GivenAnIntegerSchemaValueMatch_ThenRequestIsMatched()
    {
        var path = $"/querystringtest{Guid.NewGuid()}";
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path)
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("Test3ParameterOne")
                        .WithValue(SchemaValue.Integer()))
                )
            )
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}?Test3ParameterOne={new Random().Next()}");
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GivenAStringPatternSchemaValueMatch_ThenRequestIsMatched()
    {
        var path = $"/querystringtest{Guid.NewGuid()}";
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path)
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("Test4ParameterOne")
                        .WithValue(SchemaValue.StringWithPattern("^S_[0-9]+$")))
                )
            )
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}?Test4ParameterOne=S_{new Random().Next()}");
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GivenMultipleQueryStrings_ThenRequestIsMatched()
    {
        var path = $"/querystringtest{Guid.NewGuid()}";
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path)
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("Test5ParameterOne")
                        .WithValue(SchemaValue.StringWithPattern("^T_[0-9]+$"))
                        )
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("Test5ParameterTwo")
                        .WithValue(SchemaValue.Uuid()))
                )
            )
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}?Test5ParameterOne=T_{new Random().Next()}&Test5ParameterTwo={Guid.NewGuid()}");
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task
        GivenParameterIsSpecified_WhenParameterIsConfiguredAsExclusive_ThenRequestIncludingThatParameterIsNotMatched()
    {
        var path = $"/querystringtest{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path)
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("!Test6ParameterOne")
                        .WithValue(SchemaValue.Integer()))
                )
            )
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}?Test6ParameterOne={new Random().Next()}");
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task
        GivenParameterIsSpecified_WhenParameterIsConfiguredAsOptional_ThenRequestWithoutThatParameterIsMatched()
    {
        var path = $"/querystringtest{Guid.NewGuid()}";;

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path)
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("?Test7ParameterOne")
                        .WithValue(SchemaValue.Integer()))
                )
            )
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}");
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task
        GivenParameterIsSpecified_WhenParameterIsConfiguredAsDifferentTypeFromRequest_ThenRequestIsNotMatched()
    {
        var path = $"/querystringtest{Guid.NewGuid()}";;

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path)
                .WithQueryStringParameters(queryStringExpectationBuilder => queryStringExpectationBuilder
                    .WithParameter(queryStringParameterExpectationBuilder => 
                        queryStringParameterExpectationBuilder
                        .WithName("?Test8ParameterOne")
                        .WithValue(SchemaValue.Integer()))
                )
            )
            .Respond(responseBuilder => responseBuilder.WithStatusCode(200));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{path}?Test8ParameterOne={Guid.NewGuid()}");
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
}