using System;
using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public sealed class HeaderResponseBuilder : IHeaderResponseBuilder
    {
        private readonly HttpRequest _httpRequest;
        private string _name;
        private Func<IHeaderValueResponseBuilder, IHeaderValueResponseBuilder> _headerValueResponseBuilder;
        
        private HeaderResponseBuilder(HttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public static IHeaderResponseBuilder Build(HttpRequest httpRequest)
        {
            return new HeaderResponseBuilder(httpRequest);
        }

        public IHeaderResponseBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public IHeaderResponseBuilder WithValues(Func<IHeaderValueResponseBuilder, IHeaderValueResponseBuilder> headerValueResponseBuilder = null)
        {
            _headerValueResponseBuilder = headerValueResponseBuilder;
            return this;
        }

        public KeyValuePair<string, IEnumerable<object>> Create()
        {
            if (_headerValueResponseBuilder == null)
            {
                throw new InvalidOperationException("No Values specified for header");
            }
            if (string.IsNullOrWhiteSpace(_name))
            {
                throw new InvalidOperationException("No Name specified for header");
            }
            
            return new KeyValuePair<string, IEnumerable<object>>(_name,
                _headerValueResponseBuilder(HeaderValueResponseBuilder.Build(_httpRequest)).Create());
        }
    }
}