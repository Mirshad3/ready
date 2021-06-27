using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class Review
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public int Rating { get; set; }

        public string Body { get; set; }

        public bool IsApproved { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
