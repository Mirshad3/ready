using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Core.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }

        [Required]
        public string OrderId { get; set; }

        public string ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Sku { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public int DetuctionPersontage { get; set; }

        public decimal Detuction { get; set; }
        public int Tex {  get; set; }
        public decimal ReturnTotal { get; set; }
    }
}
