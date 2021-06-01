using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesApp.Interfaces
{
    public interface IPointOfSaleTerminal
    {
        Task<decimal> SetPricing();
        Task<decimal> CalculateTotal();
    }
}
