using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public sealed class ResponseBuilder : IResponseBuilder
    {
        private readonly HttpResponse _httpResponse;
        
        private ResponseBuilder(HttpResponse response)
        {
            _httpResponse = response;
        }

        private ResponseBuilder()
        {
            _httpResponse = new HttpResponse();
        }

        public static IResponseBuilder Build()
        {
            return new ResponseBuilder();
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