using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IResponseBuilder
    {
        // IResponseBuilder Build();
        HttpResponse Create();
        IResponseBuilder WithStatusCode(int statusCode);
    }
    
    public sealed class ResponseBuilder : IResponseBuilder
    {
        private readonly HttpResponse _httpResponse;
        
        private ResponseBuilder(HttpResponse response)
        {
            _httpResponse = response;
        }

        public ResponseBuilder()
        {
            _httpResponse = new HttpResponse();
        }

        public HttpResponse Create()
        {
            return _httpResponse;
        }

        public IResponseBuilder WithStatusCode(int statusCode)
        {
            _httpResponse.StatusCode = statusCode;
            return this;
        }
    }
}