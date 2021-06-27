using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class Wishlist
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
