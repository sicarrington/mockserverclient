using MockServer.Client.Net.Builders;
using System;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests
{
    public class WithBodyTests
    {
        private readonly RequestBuilder _requestBuilder;
        
        public WithBodyTests()
        {
            _requestBuilder = new RequestBuilder();
        }
        [Fact]
        public void GivenWithBodyIsCalled_WhenBodyPassedIsNull_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { _requestBuilder.WithBody(null); });
        }
        [Fact]
        public void GivenWithBodyIsCalled_WhenBodyPassedIsEmpty_ThenMethodIsSuccessful()
        {
            var result = _requestBuilder.WithBody("");
            Assert.NotNull(result);
        }
        [Fact]
        public void GivenWithBodyIsCalled_WhenValidBodyIsPassed_ThenRequestBuilderReturnedWithBody()
        {
            var testBody = "{ \"This\":\"is\",\"the\":\"body\"}";
            var result = _requestBuilder.WithBody(testBody);

            Assert.NotNull(result);
            Assert.Equal(testBody, result.Create().Body);
        }
    }
}