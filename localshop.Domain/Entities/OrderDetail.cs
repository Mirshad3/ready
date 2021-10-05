using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string Name { get; set; }

        public string Sku { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }
        public int DetuctionPersontage { get; set; }

        public decimal Detuction { get; set; }
        public int Tex { get; set; }
        public decimal ReturnTotal { get; set; }
    }
}
