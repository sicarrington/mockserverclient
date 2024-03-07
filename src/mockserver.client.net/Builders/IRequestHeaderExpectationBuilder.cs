using System;
using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public interface IRequestHeaderExpectationBuilder
    {
        IRequestHeaderExpectationBuilder WithName(string name);
        IRequestHeaderExpectationBuilder WithValues(Func<IRequestHeaderValuesExpectationBuilder, IRequestHeaderValuesExpectationBuilder> requestHeaderValueBuilder = null);
        KeyValuePair<string, IEnumerable<object>> Create();
    }
}