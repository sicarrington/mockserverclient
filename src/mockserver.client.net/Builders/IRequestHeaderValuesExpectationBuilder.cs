using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IRequestHeaderValuesExpectationBuilder
    {
        IRequestHeaderValuesExpectationBuilder WithValue(string value);
        IRequestHeaderValuesExpectationBuilder WithValue(SchemaValue schemaValue);
        IEnumerable<object> Create();
    }
}