using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using PointOfSalesApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace XUnitPointOfSales
{
    public class MaterAPIClientProvider : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; private set; }

        public MaterAPIClientProvider()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                )
                .UseStartup<Startup>();

            server = new TestServer(builder);
            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}
