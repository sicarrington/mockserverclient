using Xunit;
using MockServer.Client.Net.Builders;
using System;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests
{
    public class WithPathTests
    {
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsNull_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { RequestBuilder.Request().WithPath(null); });
        }
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsEmpty_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { RequestBuilder.Request().WithPath(null); });
        }
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsNotEmpty_ThenPathIsSetAsExpected()
        {
            var expectedPath = "this/is/a/path";
            var result = RequestBuilder.Request().WithPath(expectedPath);
            Assert.Equal(expectedPath, result.Create().Path);
        }
    }
}