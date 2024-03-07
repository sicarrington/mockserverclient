using System;
using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IResponseBuilder
    {
        HttpResponse Create();
        IResponseBuilder WithStatusCode(int statusCode);
        IResponseBuilder WithHeaders(Func<IHeadersResponseBuilder, IHeadersResponseBuilder> headersResponseBuilder = null);
        IResponseBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers);
    }
}