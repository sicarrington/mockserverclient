using MockServer.Client.Net.Models;
using System;
using System.Net.Http;

namespace MockServer.Client.Net.Builders
{
    public interface IRequestBuilder
    {
        IRequestBuilder WithPath(string path);
        IRequestBuilder WithMethod(HttpMethod method);
        IRequestBuilder WithBody(string requestBody);
        
        HttpRequest Create();
    }
    
    public sealed class RequestBuilder : IRequestBuilder
    {
        private readonly HttpRequest _httpRequest;
        private RequestBuilder(HttpRequest request)
        {
            _httpRequest = request;
        }

        public static IRequestBuilder Build()
        {
            return new RequestBuilder(new HttpRequest());
        }

        public IRequestBuilder WithPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            _httpRequest.Path = path;
            return this;
        }
        public IRequestBuilder WithMethod(HttpMethod method)
        {
            _httpRequest.Method = method.Method;
            return this;
        }
        public IRequestBuilder WithBody(string requestBody)
        {
            if (requestBody == null)
            {
                throw new ArgumentNullException(nameof(requestBody));
            }
            _httpRequest.Body = requestBody;
            return this;
        }
        
        public HttpRequest Create()
        {
            return _httpRequest;
        }
    }
}