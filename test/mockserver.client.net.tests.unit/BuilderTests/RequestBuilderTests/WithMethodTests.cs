using MockServer.Client.Net.Builders;
using System.Net.Http;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests
{
    public class WithMethodTests
    {
        private readonly IRequestBuilder _sut = RequestBuilder.Build();

        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodDeleteIsPassed_MethodIsMappedAsExpected()
        {
            _sut.WithMethod(HttpMethod.Delete);

            var result = _sut.Create();
            Assert.Equal("DELETE", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodGetIsPassed_MethodIsMappedAsExpected()
        {
            _sut.WithMethod(HttpMethod.Get);

            var result = _sut.Create();
            Assert.Equal("GET", result.Method);
        }

        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodHeadIsPassed_MethodIsMappedAsExpected()
        {
            _sut.WithMethod(HttpMethod.Head);

            var result = _sut.Create();
            Assert.Equal("HEAD", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodOptionsIsPassed_MethodIsMappedAsExpected()
        {
            _sut.WithMethod(HttpMethod.Options);

            var result = _sut.Create();
            Assert.Equal("OPTIONS", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodPatchIsPassed_MethodIsMappedAsExpected()
        {
            _sut.WithMethod(HttpMethod.Patch);

            var result = _sut.Create();
            Assert.Equal("PATCH", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodPostIsPassed_MethodIsMappedAsExpected()
        {
            _sut.WithMethod(HttpMethod.Post);

            var result = _sut.Create();
            Assert.Equal("POST", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodPutIsPassed_MethodIsMappedAsExpected()
        {
            _sut.WithMethod(HttpMethod.Put);

            var result = _sut.Create();
            Assert.Equal("PUT", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodTraceIsPassed_MethodIsMappedAsExpected()
        {
            _sut.WithMethod(HttpMethod.Trace);

            var result = _sut.Create();
            Assert.Equal("TRACE", result.Method);
        }
    }
}