using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public class ResponseBuilder
    {
        private readonly HttpResponse _httpResponse;
        private ResponseBuilder(HttpResponse response)
        {
            _httpResponse = response;
        }
        public static ResponseBuilder Respond()
        {
            return new ResponseBuilder(new HttpResponse());
        }
        public HttpResponse Create()
        {
            return _httpResponse;
        }
        public ResponseBuilder WithStatusCode(int statusCode)
        {
            _httpResponse.StatusCode = statusCode;
            return this;
        }
    }
}