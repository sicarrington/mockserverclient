using MockServer.Client.Net.Builders;
using System;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.RequestBuilderTests
{
    public class WithPathTests
    {
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsNull_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { new RequestBuilder().WithPath(null); });
        }
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsEmpty_ThenExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { new RequestBuilder().WithPath(string.Empty); });
        }
        [Fact]
        public void GivenWithPath_WhenPathProvidedIsNotEmpty_ThenPathIsSetAsExpected()
        {
            var expectedPath = "this/is/a/path";
            var result = new RequestBuilder().WithPath(expectedPath);
            Assert.Equal(expectedPath, result.Create().Path);
        }
    }
}