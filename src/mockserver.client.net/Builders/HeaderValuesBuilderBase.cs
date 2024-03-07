using System;
using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public abstract class HeaderValuesBuilderBase
    {
        protected readonly IList<object> _values;

        protected HeaderValuesBuilderBase(IList<object> values)
        {
            _values = values;
        }
        
        protected void WithValue(string value)
        {
            _values.Add(value);
        }

        protected void WithValue(SchemaValue schemaValue)
        {
            _values.Add(schemaValue);
        }
    }
}