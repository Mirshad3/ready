using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class Tag
    {
        public Tag()
        {
            // Product tags
            Products = new HashSet<Product>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        // Product tags
        public virtual ICollection<Product> Products { get; set; }
    }
}
