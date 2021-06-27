using localshop.Core.DTO;
using localshop.Domain.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localshop.Models
{
    // For update cart via Json
    public class Line
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }

    public class CartLine
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public List<CartLine> LineCollection = new List<CartLine>();

        public decimal Summary
        {
            get { return LineCollection.Sum(l => GetRealPrice(l.Product) * l.Quantity); }
        }

        public int SummaryQuantity
        {
            get { return LineCollection.Sum(l => l.Quantity); }
        }

        private decimal GetRealPrice(ProductDTO product)
        {
            if (product.DiscountPrice != null)
            {
                if (product.EndDiscountDate != null)
                {
                    if (DateTime.Now <= product.EndDiscountDate.Value)
                    {
                        return product.DiscountPrice.Value;
                    }
                    else
                    {
                        return product.Price;
                    }
                }
                else
                {
                    return product.DiscountPrice.Value;
                }
            }
            else
            {
                return product.Price;
            }
        }
    }
}