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
            var sut = RequestBuilder.Build();
            Assert.Throws<ArgumentNullException>(() =>
            {
                ExpectationBuilder.When(null, sut);
            });
        }
        [Fact]
        public void GivenWhenIsCalled_WhenRequestBuilderPassedIsNull_ThenExcptionIsThrown()
        {
            using var httpClient = new HttpClient();
            var mockServerClient = new MockServerClient(httpClient);
            Assert.Throws<ArgumentNullException>(() =>
            {
                ExpectationBuilder.When(mockServerClient, null);
            });
        }
        [Fact]
        public void GivenWhenIsCalled_WhenParametersAreValid_ThenNewBuilderIsReturnedWithRequestPersisted()
        {
            using var httpClient = new HttpClient();
            var mockServerClient = new MockServerClient(httpClient);
            var sut = RequestBuilder.Build();

            var result = ExpectationBuilder.When(mockServerClient, sut);

            Assert.Equal(sut, ((ExpectationBuilder)result).RequestBuilder);
        }
    }
}