using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PointOfSalesApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesApp.Controllers
{
    //[Route("api/[controller]")]
    [Route("pointsales")]
    [ApiController]
    public class PointOfSalesTerminalController : ControllerBase
    {
        private readonly ILogger<PointOfSalesTerminalController> _logger;

        public PointOfSalesTerminalController(ILogger<PointOfSalesTerminalController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get Total Costs based on Scanned Items 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [HttpGet("{items}")]
        public ActionResult GetTotalCosts(string items)
        {           
            decimal grandTotal;
            string message, orderedItems;
            orderedItems = items.ToUpper();
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            SalesTerminal sales = new SalesTerminal();
            try
            {
                terminal.SetPricing();
                message = terminal.Scan(ref orderedItems);
                grandTotal = terminal.CalculateTotal();

                sales = new SalesTerminal
                {
                    CartItems = orderedItems,
                    TotalPrice = grandTotal,
                    Message = message
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Exeption caught in program {0}", e);
            }
            return Ok(sales);
        }

        /// <summary>
        /// Add new Product in to the system
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNewProducts(ProductDetails products)
        {
            try
            {
                PointOfSaleTerminal terminal = new PointOfSaleTerminal();
                List<ProductDetails> lstProductDetails = new List<ProductDetails>();
                lstProductDetails = terminal.AddItems(products);
                return Ok(lstProductDetails);
            }
            catch(Exception e) 
            {
                Console.WriteLine("Excetion caught in program {0}", e);
                return StatusCode(500);
            }
        }
    }
}
