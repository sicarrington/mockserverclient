using MockServer.Client.Net.Builders;
using System;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests
{
    public class WithPathTests
    {
        private readonly IRequestBuilder _sut = RequestBuilder.Build();
        
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsNull_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { _sut.WithPath(null); });
        }
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsEmpty_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _sut.WithPath(null);
            });
        }
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsNotEmpty_ThenPathIsSetAsExpected()
        {
            var expectedPath = "this/is/a/path";
            var result = _sut.WithPath(expectedPath);
            Assert.Equal(expectedPath, result.Create().Path);
        }
    }
}