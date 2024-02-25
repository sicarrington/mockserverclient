using System;
using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IHeadersResponseBuilder
    {
        IHeadersResponseBuilder WithHeader(Func<IHeaderResponseBuilder, IHeaderResponseBuilder> headerResponseBuilder);
        IDictionary<string, IEnumerable<string>> Create();
    }
    
    public sealed class HeadersResponseBuilder : IHeadersResponseBuilder
    {
        private readonly HttpRequest _httpRequest;
        private readonly IDictionary<string, IEnumerable<string>> _headers;

        private HeadersResponseBuilder(HttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
            _headers = new Dictionary<string, IEnumerable<string>>();
        }

        public static IHeadersResponseBuilder Build(HttpRequest httpRequest)
        {
            return new HeadersResponseBuilder(httpRequest);
        }

        public IHeadersResponseBuilder WithHeader(Func<IHeaderResponseBuilder,IHeaderResponseBuilder> headerResponseBuilder)
        {
            if (headerResponseBuilder == null)
            {
                throw new ArgumentNullException(nameof(headerResponseBuilder));
            }
            _headers.Add(headerResponseBuilder(HeaderResponseBuilder.Build(_httpRequest)).Create());
            return this;
        }

        public IDictionary<string, IEnumerable<string>> Create()
        {
            return _headers;
        }
    }
}