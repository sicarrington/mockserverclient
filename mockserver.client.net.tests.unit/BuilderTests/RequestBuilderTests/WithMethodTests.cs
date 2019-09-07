using System.Net.Http;
using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests
{
    public class WithMethodTests
    {
        RequestBuilder _requestBuilder;
        public WithMethodTests()
        {
            _requestBuilder = RequestBuilder.Request();
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodDeleteIsPassed_MethodIsMappedAsExpected()
        {
            _requestBuilder.WithMethod(HttpMethod.Delete);

            var result = _requestBuilder.Create();
            Assert.Equal("DELETE", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodGetIsPassed_MethodIsMappedAsExpected()
        {
            _requestBuilder.WithMethod(HttpMethod.Get);

            var result = _requestBuilder.Create();
            Assert.Equal("GET", result.Method);
        }

        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodHeadIsPassed_MethodIsMappedAsExpected()
        {
            _requestBuilder.WithMethod(HttpMethod.Head);

            var result = _requestBuilder.Create();
            Assert.Equal("HEAD", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodOptionsIsPassed_MethodIsMappedAsExpected()
        {
            _requestBuilder.WithMethod(HttpMethod.Options);

            var result = _requestBuilder.Create();
            Assert.Equal("OPTIONS", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodPatchIsPassed_MethodIsMappedAsExpected()
        {
            _requestBuilder.WithMethod(HttpMethod.Patch);

            var result = _requestBuilder.Create();
            Assert.Equal("PATCH", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodPostIsPassed_MethodIsMappedAsExpected()
        {
            _requestBuilder.WithMethod(HttpMethod.Post);

            var result = _requestBuilder.Create();
            Assert.Equal("POST", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodPutIsPassed_MethodIsMappedAsExpected()
        {
            _requestBuilder.WithMethod(HttpMethod.Put);

            var result = _requestBuilder.Create();
            Assert.Equal("PUT", result.Method);
        }
        [Fact]
        public void GivenWithMethodIsCalled_WhenMethodTraceIsPassed_MethodIsMappedAsExpected()
        {
            _requestBuilder.WithMethod(HttpMethod.Trace);

            var result = _requestBuilder.Create();
            Assert.Equal("TRACE", result.Method);
        }
    }
}