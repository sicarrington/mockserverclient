namespace MockServer.Client.Net.Models
{
    public class ChainExpectation
    {
        private readonly MockServerClient _mockServerClient;
        private readonly Expectation _expectation;

        public ChainExpectation(MockServerClient mockServerClient, Expectation expectation)
        {
            _mockServerClient = mockServerClient;
            _expectation = expectation;
        }
    }
}