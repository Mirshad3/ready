using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class Category
    {
        public string Id { get; set; }

        public string Name { get; set; }

        // FK_ParentCategory
        public string ParentId { get; set; }
        public Category Parent { get; set; }

        // List product
        public ICollection<Product> Products { get; set; }
    }
}
