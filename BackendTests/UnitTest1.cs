using System;
using System.Net.Http;
using System.Threading.Tasks;
using FlexmodulBackendV2;
using FlexmodulBackendV2.Contracts.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BackendTests
{
    public class UnitTest1
    {
        private readonly HttpClient _client;

        public UnitTest1()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            await _client.GetAsync("api/values/1");
        }
    }
}
