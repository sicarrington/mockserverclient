using MockServer.Client.Net.Models;
using System;
using System.Threading.Tasks;

namespace MockServer.Client.Net.Builders
{
    public class ExpectationBuilder
    {
        private readonly IMockServerClient _mockServerClient;
        private readonly RequestBuilder _requestBuilder;
        internal RequestBuilder RequestBuilder => _requestBuilder;

        private ExpectationBuilder(IMockServerClient mockServerClient, RequestBuilder httpRequest)
        {
            _mockServerClient = mockServerClient;
            _requestBuilder = httpRequest;
        }
        internal static ExpectationBuilder When(IMockServerClient mockServerClient, RequestBuilder requestBuilder)
        {
            if (mockServerClient == null)
            {
                throw new ArgumentNullException(nameof(mockServerClient));
            }
            if (requestBuilder == null)
            {
                throw new ArgumentNullException(nameof(requestBuilder));
            }
            var expectationBuilder = new ExpectationBuilder(mockServerClient, requestBuilder);
            return expectationBuilder;
        }
        public Expectation Respond(ResponseBuilder responseBuilder)
        {
            if (responseBuilder == null)
            {
                throw new ArgumentNullException(nameof(responseBuilder));
            }

            var expectation = new Expectation
            {
                HttpRequest = _requestBuilder.Create(),
                HttpResponse = responseBuilder.Create()
            };

            var task = Task.Run(async () => { await _mockServerClient.SetExpectations(expectation); });
            task.Wait();

            return expectation;
        }
    }
}
