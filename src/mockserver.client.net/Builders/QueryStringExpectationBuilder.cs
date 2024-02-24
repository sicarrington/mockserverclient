using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public sealed class QueryStringExpectationBuilder
    {
        private readonly IDictionary<string, IEnumerable<string>> _queryStringExpectation =
            new Dictionary<string, IEnumerable<string>>();

        private QueryStringExpectationBuilder AddParameter(
            QueryStringParameterExpectationBuilder queryStringParameterExpectationBuilder)
        {
            var parameter = queryStringParameterExpectationBuilder.Create();
            _queryStringExpectation.Add(parameter.Key, parameter.Value);
            return this;
        }

        public IDictionary<string, IEnumerable<string>> Create()
        {
            return _queryStringExpectation;
        }
    }
}