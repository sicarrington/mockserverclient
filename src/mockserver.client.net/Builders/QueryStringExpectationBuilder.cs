using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public interface IQueryStringExpectationBuilder
    {
        IQueryStringExpectationBuilder WithParameter(
            IQueryStringParameterExpectationBuilder queryStringParameterExpectationBuilder);
        IDictionary<string, IEnumerable<object>> Create();
    }
    
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
            IQueryStringParameterExpectationBuilder queryStringParameterExpectationBuilder)
        {
            var parameter = queryStringParameterExpectationBuilder.Create();
            _queryStringExpectation.Add(parameter.Key, parameter.Value);
            return this;
        }

        public IDictionary<string, IEnumerable<object>> Create()
        {
            return _queryStringExpectation;
        }
    }
}