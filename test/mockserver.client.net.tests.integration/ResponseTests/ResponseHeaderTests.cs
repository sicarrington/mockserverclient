using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Integration.ResponseTests;

public class ResponseHeaderTests
{
    [Fact]
    public async Task GivenResponseHeadersAreExplicitlySpecified_ThenResponseHeadersAreMappedCorrectly()
    {
        var headerOneName = "HeaderOneName";
        var headerOneValue = "HeaderOneValue";
        var path = $"/headerrepsonsetest{Guid.NewGuid()}";

        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:1080/");

        new MockServerClient(httpClient)
            .When(RequestBuilder.Build()
                .WithMethod(HttpMethod.Get)
                .WithPath(path))
            .Respond(
                responseBuilder => responseBuilder
                    .WithStatusCode(200)
                    .WithHeaders(new Dictionary<string, IEnumerable<string>>
                        { { headerOneName, new[] { headerOneValue } } }));

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, path);
        var response = await httpClient.SendAsync(httpRequestMessage);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(response.Headers, pair => pair.Key == headerOneName && pair.Value.First() == headerOneValue);
    }
}