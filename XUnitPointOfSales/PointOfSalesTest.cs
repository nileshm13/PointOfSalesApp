using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PointOfSalesApp;
using PointOfSalesApp.Controllers;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

/*https://localhost:5001/pointsales/ABCD */


namespace XUnitPointOfSales
{
    public class PointOfSalesTest
    {
        private readonly ILogger<PointOfSalesTerminalController> _logger;

        [Fact]
        public async Task GetTotalCostsStatusABCDABA()
        {
            using (var client = new MaterAPIClientProvider().Client)
            {
                string expectedResult = "13.25";
                var response = await client.GetAsync("pointsales/ABCDABA");
                var xyz = JsonConvert.DeserializeObject <SalesTerminal>( await response.Content.ReadAsStringAsync());
                Assert.Equal(expectedResult,xyz.TotalPrice.ToString("0.00"));                
            }          
        }

        [Fact]
        public async Task GetTotalCostsStatusCCCCCCC()
        {
            using (var client = new MaterAPIClientProvider().Client)
            {                
                string expectedResult = "6.00";
                var response = await client.GetAsync("pointsales/CCCCCCC");
                var xyz = JsonConvert.DeserializeObject<SalesTerminal>(await response.Content.ReadAsStringAsync());
                Assert.Equal(expectedResult,xyz.TotalPrice.ToString("0.00"));
            }
        }

        [Fact]
        public async Task GetTotalCostsStatusABCD()
        {
            using (var client = new MaterAPIClientProvider().Client)
            {
                string expectedResult = "7.25";
                var response = await client.GetAsync("pointsales/ABCD");
                var xyz = JsonConvert.DeserializeObject<SalesTerminal>(await response.Content.ReadAsStringAsync());
                Assert.Equal(expectedResult,xyz.TotalPrice.ToString("0.00"));
            }
        }        
    }
}
