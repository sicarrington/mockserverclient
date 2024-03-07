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
    
    public sealed class RequestHeaderValuesExpectationBuilder : HeaderValuesBuilderBase, IRequestHeaderValuesExpectationBuilder
    {
        private RequestHeaderValuesExpectationBuilder() : base(new List<object>())
        {
        }

        public static IRequestHeaderValuesExpectationBuilder Build()
        {
            return new RequestHeaderValuesExpectationBuilder();
        }

        public new IRequestHeaderValuesExpectationBuilder WithValue(string value)
        {
            base.WithValue(value);
            return this;
        }

        public new IRequestHeaderValuesExpectationBuilder WithValue(SchemaValue schemaValue)
        {
            base.WithValue(schemaValue);
            return this;
        }

        public IEnumerable<object> Create()
        {
            var finalValues = new List<object>();
            foreach (var value in _values)
            {
                switch (value)
                {
                    case string s:
                        finalValues.Add(s);
                        break;
                    case SchemaValue schemaValue:
                        finalValues.Add(new { schema = schemaValue });
                        break;
                }
            }

            return finalValues;
        }
    }
}