using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public interface IQueryStringExpectationBuilder
    {
        IQueryStringExpectationBuilder WithParameter(
            IQueryStringParameterExpectationBuilder queryStringParameterExpectationBuilder);
        IDictionary<string, IEnumerable<object>> Create();
    }
}