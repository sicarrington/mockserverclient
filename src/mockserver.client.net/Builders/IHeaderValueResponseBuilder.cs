using System;
using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IHeaderValueResponseBuilder
    {
        IHeaderValueResponseBuilder WithValue(Func<HttpRequest, string> function);
        IHeaderValueResponseBuilder WithValue(string value);
        IHeaderValueResponseBuilder WithValue(SchemaValue schemaValue);
        IEnumerable<object> Create();
    }
}