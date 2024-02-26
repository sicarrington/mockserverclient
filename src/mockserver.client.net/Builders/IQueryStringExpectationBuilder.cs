using System;
using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public interface IQueryStringExpectationBuilder
    {
        IQueryStringExpectationBuilder WithParameter(
            Func<IQueryStringParameterExpectationBuilder, IQueryStringParameterExpectationBuilder>
                queryStringParameterExpectationBuilder);
        IDictionary<string, IEnumerable<object>> Create();
    }
}