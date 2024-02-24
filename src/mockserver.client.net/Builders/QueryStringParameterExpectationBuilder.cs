using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public class QueryStringParameterExpectationBuilder
    {
        public string Name { get; private set; }
        
        public IList<object> Values { get; private set; } = new List<object>();
        
        public QueryStringParameterExpectationBuilder WithName(string exactNameOrRegex)
        {
            Name = exactNameOrRegex;
            return this;
        }

        public QueryStringParameterExpectationBuilder WithValue(SchemaValue schemaValue)
        {
            Values.Add(schemaValue);
            return this;
        }
        
        public QueryStringParameterExpectationBuilder WithValue(string exactValueOrRegex)
        {
            Values.Add(exactValueOrRegex);
            return this;
        }

        public KeyValuePair<string, IEnumerable<string>> Create()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new InvalidOperationException($"{nameof(Name)} is not set");
            }

            var values = new List<string>();
            foreach (var value in Values)
            {
                if (value is string s)
                {
                    values.Add(s);
                }
                else if (value is SchemaValue schemaValue)
                {
                    values.Add(JsonSerializer.Serialize(new { schema = schemaValue },
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                        }));
                }
            }

            return new KeyValuePair<string, IEnumerable<string>>(Name, values);
        }
    }
}