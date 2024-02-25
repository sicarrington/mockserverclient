using MockServer.Client.Net.Models;
using System;
using System.Threading.Tasks;

namespace MockServer.Client.Net.Builders
{
    public sealed  class ExpectationBuilder : IExpectationBuilder
    {
        private readonly IMockServerClient _mockServerClient;
        private readonly IRequestBuilder _requestBuilder;
        internal IRequestBuilder RequestBuilder => _requestBuilder;

        private ExpectationBuilder(IMockServerClient mockServerClient, IRequestBuilder httpRequest)
        {
            _mockServerClient = mockServerClient;
            _requestBuilder = httpRequest;
        }
        internal static IExpectationBuilder When(IMockServerClient mockServerClient, IRequestBuilder requestBuilder)
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

        public Expectation Respond(Func<IResponseBuilder, IResponseBuilder> responseBuilder = null)
        {
            var httpRequest = _requestBuilder.Create();

            var response = responseBuilder == null
                ? ResponseBuilder.Build(httpRequest).Create()
                : responseBuilder(ResponseBuilder.Build(httpRequest)).Create();
            
            var expectation = new Expectation
            {
                HttpRequest = httpRequest,
                HttpResponse = response
            };

            var task = Task.Run(async () => { await _mockServerClient.SetExpectations(expectation); });
            task.Wait();

            return expectation;
        }
    }
}
