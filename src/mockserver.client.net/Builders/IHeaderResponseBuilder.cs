using System;
using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public interface IHeaderResponseBuilder
    {
        IHeaderResponseBuilder WithName(string name);
        IHeaderResponseBuilder WithValues(Func<IHeaderValueResponseBuilder, IHeaderValueResponseBuilder> headerValueResponseBuilder = null);
        KeyValuePair<string, IEnumerable<object>> Create();
    }
}