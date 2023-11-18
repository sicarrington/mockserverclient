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
        Mock<MockServerClient> _mockServerClient;
        RequestBuilder _requestBuilder;
        ExpectationBuilder _expectationBuilder;
        public RespondTests()
        {
            _mockServerClient = new Mock<MockServerClient>(new HttpClient());
            _requestBuilder = new RequestBuilder();

            _expectationBuilder = ExpectationBuilder.When(_mockServerClient.Object, _requestBuilder);
        }
        [Fact]
        public void GivenRespond_WhenResponsePassedIsNull_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _expectationBuilder.Respond(null);
            });
        }
        [Fact]
        public void GivenRespond_WhenResponseIsPassed_ThenExpectationIsBuiltAsExpected()
        {
            var responseBuilder = ResponseBuilder.Respond();
            var result = _expectationBuilder.Respond(responseBuilder);

            Assert.Equal(responseBuilder.Create(), result.HttpResponse);
            Assert.Equal(_requestBuilder.Create(), result.HttpRequest);
        }
        [Fact]
        public void GivenRespond_WhenResponseIsPassed_ThenExpectationIsSetAgainstMockServerClient()
        {
            _mockServerClient.Setup(x => x.SetExpectations(It.IsAny<Expectation>())).Returns(Task.CompletedTask);
            var responseBuilder = ResponseBuilder.Respond();
            var result = _expectationBuilder.Respond(responseBuilder);

            _mockServerClient.Verify(x => x.SetExpectations(
                It.Is<Expectation>(y =>
                y.HttpRequest == _requestBuilder.Create() &&
                y.HttpResponse == responseBuilder.Create())
            ), Moq.Times.Once());
        }
    }
}