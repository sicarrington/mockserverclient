using MockServer.Client.Net.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace mockserver.client.net
{
    public class ExpectationBuilder
    {
        private readonly Expectation _expectation;
        public ExpectationBuilder()
        {
            _expectation = new Expectation();
        }
        public ExpectationBuilder WithRequest()
        {

        }
    }
}
