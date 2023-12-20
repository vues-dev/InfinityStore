using System;
using Microsoft.AspNetCore.Mvc.Testing;

namespace InfinityStoreAdmin.Api.IntegrationTests
{
    public class SutFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public SutFactory()
        {
        }
    }
}

