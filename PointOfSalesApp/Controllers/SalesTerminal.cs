using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesApp.Controllers
{
    public class SalesTerminal
    {
        public string CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public string Message { get; set; }
    }
}
