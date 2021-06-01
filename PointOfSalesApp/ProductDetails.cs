using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesApp
{
    public class ProductDetails
    {
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int? GroupQuantity { get; set; }
        public decimal? GroupPrice { get; set; }
    }
}
