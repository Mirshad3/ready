using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localshop.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            // Product tags
            Tags = new HashSet<Tag>();

            Price = 0;
            Quantity = 0;
            IsFeatured = false;
            IsActive = true;
        }

        public string Id { get; set; }

        // Unique
        public string Sku { get; set; }

        // Unique
        public string MetaTitle { get; set; }

        public string Name { get; set; }

        public string ShortDesciption { get; set; }

        public string LongDescription { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public DateTime? EndDiscountDate { get; set; }

        public int Quantity { get; set; }
        public int? Weight { get; set; }
        public int? Width { get; set; }
        public int? Hight { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        // Product specifications (Detail)
        public virtual ProductSpecification ProductSpecification { get; set; }

        // Product images
        public ICollection<Image> Images { get; set; }

        // FK_Status
        // Product status (In stock, Out of stock, ...)
        public string StatusId { get; set; }
        public Status Status { get; set; }

        // FK_CategoryTable
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        // Product tags
        public virtual ICollection<Tag> Tags { get; set; }
        public string UserId { get; set; }
        // Product orders
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        // Product whishlists
        public virtual ICollection<Wishlist> Whishlists { get; set; }

        // Product reviews
        public virtual ICollection<Review> Reviews { get; set; }

        // FK_Brand

        // Backlog, Backorder,...
    }
}
