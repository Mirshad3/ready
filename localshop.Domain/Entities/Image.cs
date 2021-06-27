using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public string Path { get; set; }

        // FK_ProductTable
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
