using System;
using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public sealed class QueryStringExpectationBuilder : IQueryStringExpectationBuilder
    {
        private readonly IDictionary<string, IEnumerable<object>> _queryStringExpectation =
            new Dictionary<string, IEnumerable<object>>();

        private QueryStringExpectationBuilder()
        {
            
        }

        public static IQueryStringExpectationBuilder Build()
        {
            return new QueryStringExpectationBuilder();
        }

        public IQueryStringExpectationBuilder WithParameter(
            Func<IQueryStringParameterExpectationBuilder, IQueryStringParameterExpectationBuilder>
                queryStringParameterExpectationBuilder)
        {
            var parameter = queryStringParameterExpectationBuilder(
                    QueryStringParameterExpectationBuilder.Build())
                .Create();
            _queryStringExpectation.Add(parameter.Key, parameter.Value);
            return this;
        }

        public IDictionary<string, IEnumerable<object>> Create()
        {
            return _queryStringExpectation;
        }
    }
}