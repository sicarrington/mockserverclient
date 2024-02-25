using MockServer.Client.Net.Models;
using System;
using System.Threading.Tasks;

namespace MockServer.Client.Net.Builders
{
    public interface IExpectationBuilder
    {
        Expectation Respond(IResponseBuilder responseBuilder);
    }

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
        public Expectation Respond(IResponseBuilder responseBuilder)
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
