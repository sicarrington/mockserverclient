using System;
using System.Net.Http;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public class RequestBuilder
    {
        private readonly HttpRequest _httpRequest;
        private RequestBuilder(HttpRequest request)
        {
            _httpRequest = request;
        }
        public static RequestBuilder Request()
        {
            return new RequestBuilder(new HttpRequest());
        }
        public virtual RequestBuilder WithPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            _httpRequest.Path = path;
            return this;
        }
        public virtual RequestBuilder WithMethod(HttpMethod method)
        {
            _httpRequest.Method = method.Method;
            return this;
        }
        public virtual HttpRequest Create()
        {
            return _httpRequest;
        }
    }
}