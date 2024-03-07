using MockServer.Client.Net.Builders;
using System;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests
{
    public class WithBodyTests
    {
        private readonly IRequestBuilder _sut;
        
        public WithBodyTests()
        {
            _sut = RequestBuilder.Build();
        }
        [Fact]
        public void GivenWithBodyIsCalled_WhenBodyPassedIsNull_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { _sut.WithBody(null); });
        }
        [Fact]
        public void GivenWithBodyIsCalled_WhenBodyPassedIsEmpty_ThenMethodIsSuccessful()
        {
            var result = _sut.WithBody("");
            Assert.NotNull(result);
        }
        [Fact]
        public void GivenWithBodyIsCalled_WhenValidBodyIsPassed_ThenRequestBuilderReturnedWithBody()
        {
            var testBody = "{ \"This\":\"is\",\"the\":\"body\"}";
            var result = _sut.WithBody(testBody);

            Assert.NotNull(result);
            Assert.Equal(testBody, result.Create().Body);
        }
    }
}