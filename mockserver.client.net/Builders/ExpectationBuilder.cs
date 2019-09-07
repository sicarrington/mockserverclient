using MockServer.Client.Net.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MockServer.Client.Net.Builders
{
    public class ExpectationBuilder
    {
        private readonly MockServerClient _mockServerClient;
        private RequestBuilder _requestBuilder;
        internal RequestBuilder RequestBuilder
        {
            get
            {
                return _requestBuilder;
            }
        }

        private ExpectationBuilder(MockServerClient mockServerClient, RequestBuilder httpRequest)
        {
            _mockServerClient = mockServerClient;
            _requestBuilder = httpRequest;
        }
        internal static ExpectationBuilder When(MockServerClient mockServerClient, RequestBuilder requestBuilder)
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


            new Task(() =>
            {
                _mockServerClient.SetExpectations(expectation).RunSynchronously();
            }).RunSynchronously();
            return expectation;
        }
    }
}
