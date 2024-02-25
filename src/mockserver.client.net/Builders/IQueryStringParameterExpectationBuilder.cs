using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IQueryStringParameterExpectationBuilder
    {
        IQueryStringParameterExpectationBuilder WithName(string exactNameOrRegex);
        IQueryStringParameterExpectationBuilder WithValue(SchemaValue schemaValue);
        IQueryStringParameterExpectationBuilder WithValue(string exactValueOrRegex);
        KeyValuePair<string, IEnumerable<object>> Create();
    }
}