using MockServer.Client.Net.Builders;
using Xunit;

namespace MockServer.Client.Net.Tests.Unit.BuilderTests.ResponseBuilderTests
{
    public class WithStatusCodeTests
    {
        [Theory]
        [InlineData(404)]
        [InlineData(200)]
        public void GivenWithStatusCode_WhenStatusCodeIsPassed_ThenStatusCodeIsSetAgainstResponseBuilt(int expectedStatusCode)
        {
            var responseBuilder = new ResponseBuilder().WithStatusCode(expectedStatusCode);

            Assert.Equal(expectedStatusCode, responseBuilder.Create().StatusCode);
        }
    }
}