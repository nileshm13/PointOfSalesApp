using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesApp.Services
{
    public class PointOfSaleTerminal
    {
        private List<ProductDetails> productsInfo = new List<ProductDetails>();    //Maintains Default Prodcut Details
        private Dictionary<string, int> cartItems = new Dictionary<string, int>(); //Maintains ProductCode and their Total Count

        public void SetPricing()
        {
            List<ProductDetails> lstProducts = new List<ProductDetails>
            {  new ProductDetails { ProductCode = "A", UnitPrice = 1.25M,GroupQuantity=3,GroupPrice = 3M },
               new ProductDetails { ProductCode = "B", UnitPrice = 4.25M,GroupQuantity=null,GroupPrice = null },
               new ProductDetails { ProductCode = "C", UnitPrice = 1M,GroupQuantity=6,GroupPrice = 5 },
               new ProductDetails { ProductCode = "D", UnitPrice = 0.75M,GroupQuantity=null,GroupPrice = null }
            };
            productsInfo.AddRange(lstProducts);
        }

        public List<ProductDetails> AddItems(ProductDetails newProduct)
        {
            SetPricing();
            if(String.IsNullOrEmpty(newProduct.ProductCode) || newProduct.UnitPrice <= 0.0M )
            {
                Console.WriteLine("Please enter valid  Product Code / Unit Price");
                return productsInfo;
            }

            if (!productsInfo.Exists(x => x.ProductCode == newProduct.ProductCode))
            {
                productsInfo.Add(new ProductDetails
                {
                    ProductCode = newProduct.ProductCode,
                    UnitPrice = newProduct.UnitPrice,
                    GroupPrice = newProduct.GroupPrice,
                    GroupQuantity = newProduct.GroupQuantity
                });
            }
            else
            {
                Console.WriteLine("Product with specified Product Code already exists");
            }

            return productsInfo;
        }
        
        public string Scan(ref string items)
        {
            string invalidItems = "";
            int invalidCount = 0;
            string message = String.Empty;
            foreach (var c in items)
            {
                if (productsInfo.Exists(x => x.ProductCode == c.ToString()))
                {
                    if (cartItems.ContainsKey(c.ToString()))
                    {
                        cartItems[c.ToString()]++;
                    }
                    else
                    {
                        cartItems.Add(c.ToString(), 1);
                    }
                }
                else
                {
                    invalidItems = invalidItems + c.ToString() + ",";
                    invalidCount++;
                    items = items.Replace(c.ToString(), String.Empty);
                }
            }            
            if (invalidCount > 0)
            {
                invalidItems = invalidItems.Remove(invalidItems.Length - 1);
                message = String.Format("Following are the invalid items {0} prices of which has not been included in total price", invalidItems);
            }
            else
            {
                message = "Success";
            }

            return message;
        }

        public decimal CalculateTotal()
        {
            decimal total = CalculateFinal();
            return total;
        }
                
        private decimal CalculateFinal()
        {
            decimal grandTotal = 0.0M;
            //Formula(for items having Dicount on multiple items): Total  = ItemCount % GroupQty * UnitPrice + ItemCount / GroupQty * GroupPrice 
            //Formula(for items not having Dicount on multiple items): Total  = ItemCount * UnitPrice
            foreach (KeyValuePair<string, int> item in cartItems)
            {
                var prod = productsInfo.Where(x => x.ProductCode == item.Key).FirstOrDefault();
                if (prod.GroupQuantity == null)
                {
                    grandTotal += prod.UnitPrice * item.Value;
                }
                else
                {
                    grandTotal += (item.Value / prod.GroupQuantity.Value * prod.GroupPrice.Value) + (item.Value % prod.GroupQuantity.Value * prod.UnitPrice);
                }
            }
            return grandTotal;
        }
    }
}
