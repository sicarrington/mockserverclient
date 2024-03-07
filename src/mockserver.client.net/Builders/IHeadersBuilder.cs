using System;
using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public interface IHeadersBuilder<out THeadersBuilder, THeaderBuilder>
    {
        THeadersBuilder WithHeader(Func<THeaderBuilder, THeaderBuilder> headerResponseBuilder);
        IDictionary<string, IEnumerable<object>> Create();
    }
}