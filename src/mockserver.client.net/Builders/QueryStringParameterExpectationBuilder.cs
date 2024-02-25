using System;
using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public class QueryStringParameterExpectationBuilder : IQueryStringParameterExpectationBuilder
    {
        private string Name { get; set; }

        private IList<object> Values { get; set; } = new List<object>();

        private QueryStringParameterExpectationBuilder()
        {
            
        }

        public static IQueryStringParameterExpectationBuilder Build()
        {
            return new QueryStringParameterExpectationBuilder();
        }
        
        public IQueryStringParameterExpectationBuilder WithName(string exactNameOrRegex)
        {
            Name = exactNameOrRegex;
            return this;
        }

        public IQueryStringParameterExpectationBuilder WithValue(SchemaValue schemaValue)
        {
            Values.Add(schemaValue);
            return this;
        }
        
        public IQueryStringParameterExpectationBuilder WithValue(string exactValueOrRegex)
        {
            Values.Add(exactValueOrRegex);
            return this;
        }

        public KeyValuePair<string, IEnumerable<object>> Create()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new InvalidOperationException($"{nameof(Name)} is not set");
            }

            var values = new List<object>();
            foreach (var value in Values)
            {
                if (value is string s)
                {
                    values.Add(s);
                }
                else if (value is SchemaValue schemaValue)
                {
                    values.Add(new { schema = schemaValue });
                }
            }

            return new KeyValuePair<string, IEnumerable<object>>(Name, values);
        }
    }
}