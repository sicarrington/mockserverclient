using System;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IExpectationBuilder
    {
        Expectation Respond(Func<IResponseBuilder, IResponseBuilder> responseBuilder = null);
    }
}