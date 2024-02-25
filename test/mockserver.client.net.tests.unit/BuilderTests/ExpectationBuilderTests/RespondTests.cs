using MockServer.Client.Net.Builders;
using MockServer.Client.Net.Models;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.ExpectationBuilderTests
{
    public class RespondTests
    {
        private readonly Mock<IMockServerClient> _mockServerClient;
        private readonly IRequestBuilder _requestBuilder;
        private readonly ExpectationBuilder _expectationBuilder;
        
        public RespondTests()
        {
            _mockServerClient = new Mock<IMockServerClient>();
            _requestBuilder = RequestBuilder.Build();

            _expectationBuilder = ExpectationBuilder.When(_mockServerClient.Object, _requestBuilder);
        }
        
        [Fact]
        public void GivenRespond_WhenResponseIsPassed_ThenExpectationIsBuiltAsExpected()
        {
            var responseBuilder = ResponseBuilder.Build(RequestBuilder.Build().Create());
            var result = _expectationBuilder.Respond();
        
            Assert.Equal(responseBuilder.Create(), result.HttpResponse);
            Assert.Equal(_requestBuilder.Create(), result.HttpRequest);
        }
        
        [Fact]
        public void GivenRespond_WhenResponseIsPassed_ThenExpectationIsSetAgainstMockServerClient()
        {
            _mockServerClient.Setup(x => x.SetExpectations(It.IsAny<Expectation>())).Returns(Task.CompletedTask);
            var responseBuilder = ResponseBuilder.Build(RequestBuilder.Build().Create());
            var result = _expectationBuilder.Respond(builder => responseBuilder);
        
            _mockServerClient.Verify(x => x.SetExpectations(
                It.Is<Expectation>(y =>
                y.HttpRequest == _requestBuilder.Create() &&
                y.HttpResponse == responseBuilder.Create())
            ), Moq.Times.Once());
        }
    }
}