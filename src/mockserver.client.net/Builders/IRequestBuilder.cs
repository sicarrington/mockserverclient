using System;
using System.Collections.Generic;
using System.Net.Http;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IRequestBuilder
    {
        IRequestBuilder WithPath(string path);
        IRequestBuilder WithMethod(HttpMethod method);
        IRequestBuilder WithBody(string requestBody);
        IRequestBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers);
        IRequestBuilder WithHeaders(Func<IRequestHeadersExpectationBuilder, IRequestHeadersExpectationBuilder> requestHeadersBuilder);
        IRequestBuilder WithCookies(IDictionary<string, string> cookies);
        IRequestBuilder WithQueryStringParameters(
            Func<IQueryStringExpectationBuilder, IQueryStringExpectationBuilder> queryStringExpectationBuilder);
        HttpRequest Create();
    }
}