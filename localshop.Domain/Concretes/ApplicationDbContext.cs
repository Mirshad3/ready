using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using localshop.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace localshop.Domain.Concretes
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("LocalShopConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SpecialFeatured> SpecialFeatureds { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Review> Reviews { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //------------------------------------------------------------------------
            // Products
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Sku).IsRequired().HasMaxLength(450);
            modelBuilder.Entity<Product>().HasIndex(p => p.Sku).IsUnique();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.MetaTitle).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.UserId).IsRequired();
            modelBuilder.Entity<Product>().HasOptional(p => p.ProductSpecification).WithRequired(ps => ps.Product).WillCascadeOnDelete();

            // ProductSepcifications
            modelBuilder.Entity<ProductSpecification>().HasKey(ps => ps.ProductId);

            //------------------------------------------------------------------------
            // Images
            modelBuilder.Entity<Image>().HasKey(i => i.Id);
            modelBuilder.Entity<Image>().Property(i => i.Path).IsRequired();
            modelBuilder.Entity<Image>().Property(i => i.ProductId).IsRequired();

            //------------------------------------------------------------------------
            // Categories
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();
            //------------------------------------------------------------------------
            // Cities
            modelBuilder.Entity<City>().HasKey(c => c.Id);
            modelBuilder.Entity<City>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<City>().Property(c => c.Amount).IsRequired();
            //------------------------------------------------------------------------
            // Statuses
            modelBuilder.Entity<Status>().HasKey(s => s.Id).ToTable("Statuses");
            modelBuilder.Entity<Status>().Property(s => s.Name).IsRequired();

            //------------------------------------------------------------------------
            // Tags
            modelBuilder.Entity<Tag>().HasKey(t => t.Id);
            modelBuilder.Entity<Tag>().Property(t => t.Name).IsRequired();

            //------------------------------------------------------------------------
            // ProductTags
            modelBuilder.Entity<Product>()
                        .HasMany<Tag>(p => p.Tags)
                        .WithMany(t => t.Products)
                        .Map(pt =>
                        {
                            pt.MapLeftKey("ProductId");
                            pt.MapRightKey("TagId");
                            pt.ToTable("ProductTags");
                        });

            //------------------------------------------------------------------------
            // Orders
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Order>().Property(o => o.FirstName).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.LastName).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Email).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.PhoneNumber).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Country).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.City).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.State).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Zip).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Address1).IsRequired();

            //------------------------------------------------------------------------
            // OrderDetails
            modelBuilder.Entity<OrderDetail>().HasKey(od => od.Id);
            modelBuilder.Entity<OrderDetail>().Property(od => od.OrderId).IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(od => od.Name).IsRequired();
            modelBuilder.Entity<OrderDetail>().Property(od => od.Sku).IsRequired();

            //------------------------------------------------------------------------
            // OrderStatuses
            modelBuilder.Entity<OrderStatus>().HasKey(os => os.Id).ToTable("OrderStatuses");
            modelBuilder.Entity<OrderStatus>().Property(os => os.Name).IsRequired();

            //------------------------------------------------------------------------
            // PaymentMethods
            modelBuilder.Entity<PaymentMethod>().HasKey(pm => pm.Id);
            modelBuilder.Entity<PaymentMethod>().Property(pm => pm.Name).IsRequired();

            //------------------------------------------------------------------------
            // Contacts
            modelBuilder.Entity<Contact>().HasKey(c => c.Id);
            modelBuilder.Entity<Contact>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.Email).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.Subject).IsRequired();
            modelBuilder.Entity<Contact>().Property(c => c.Message).IsRequired();

            //------------------------------------------------------------------------
            // SpecialFeatureds
            modelBuilder.Entity<SpecialFeatured>().HasKey(sf => sf.Id);
            modelBuilder.Entity<SpecialFeatured>().Property(sf => sf.Title).IsRequired();
            modelBuilder.Entity<SpecialFeatured>().Property(sf => sf.Description).IsRequired();
            modelBuilder.Entity<SpecialFeatured>().Property(sf => sf.Link).IsRequired();
            modelBuilder.Entity<SpecialFeatured>().Property(sf => sf.BackgroundImage).IsRequired();
            modelBuilder.Entity<SpecialFeatured>().Property(sf => sf.ProductImage).IsRequired();

            //------------------------------------------------------------------------
            // Banners
            modelBuilder.Entity<Banner>().HasKey(b => b.Id);
            modelBuilder.Entity<Banner>().Property(b => b.Image).IsRequired();
            modelBuilder.Entity<Banner>().Property(b => b.Link).IsRequired();

            //------------------------------------------------------------------------
            // Whishlists
            modelBuilder.Entity<Wishlist>().HasKey(w => new { w.UserId, w.ProductId });

            //------------------------------------------------------------------------
            // Reviews
            modelBuilder.Entity<Review>().HasKey(r => new { r.UserId, r.ProductId });
            modelBuilder.Entity<Review>().Property(r => r.Body).IsRequired();
            modelBuilder.Entity<Review>().Property(r => r.Rating).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
