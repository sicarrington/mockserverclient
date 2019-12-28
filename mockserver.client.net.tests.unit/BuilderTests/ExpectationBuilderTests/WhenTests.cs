using MockServer.Client.Net.Builders;
using System;
using System.Net.Http;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.ExpectationBuilderTests
{
    public class WhenTests
    {
        [Fact]
        public void GivenWhenIsCalled_WhenMockServerClientPassedIsNull_ThenExceptionIsThrown()
        {
            var requestBuilder = RequestBuilder.Request();
            Assert.Throws<ArgumentNullException>(() =>
            {
                ExpectationBuilder.When(null, requestBuilder);
            });
        }
        [Fact]
        public void GivenWhenIsCalled_WhenRequestBuilderPassedIsNull_ThenExcptionIsThrown()
        {
            using (var httpClient = new HttpClient())
            {
                var mockServerClient = new MockServerClient(httpClient);
                Assert.Throws<ArgumentNullException>(() =>
                {
                    ExpectationBuilder.When(mockServerClient, null);
                });
            }
        }
        [Fact]
        public void GivenWhenIsCalled_WhenParametersAreValid_ThenNewBuilderIsReturnedWithRequestPersisted()
        {
            using (var httpClient = new HttpClient())
            {
                var mockServerClient = new MockServerClient(httpClient);
                var requestBuilder = RequestBuilder.Request();

                var result = ExpectationBuilder.When(mockServerClient, requestBuilder);

                Assert.Equal(requestBuilder, result.RequestBuilder);

            }
        }
    }
}