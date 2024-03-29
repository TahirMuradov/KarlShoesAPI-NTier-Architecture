using KarlShoes.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Concrete.SQLServer
{
    public class AppDBContext:IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = KarlFashionApiAppDb; Trusted_Connection = True; MultipleActiveResultSets = True; TrustServerCertificate = True;");
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLanguage> CategoryLanguages { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentMethodLanguage> PaymentMethodsLaunge { get; set; }

        public DbSet<Item> SoldProducts { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductLanguage> ProductLanguages { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<SubCategory> subCategories { get; set; }
        public DbSet<SubCategoryLaunguage> subCategoryLaunguages { get; set; }

        public DbSet<Size> Sizes { get; set; }


        public DbSet<ProductSize> ProductSizes { get; set; }

        public DbSet<ShippingMethods> ShippingMethods { get; set; }
        public DbSet<ShippingLanguage> ShippingLaunguages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");

            builder.Entity<IdentityRole>().ToTable("Roles");
        

            builder.Entity<Product>()
                .HasMany(x => x.ProductCategories)
                .WithOne(x=>x.Product)
                .HasForeignKey(x=>x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<CategoryProduct>()
               .HasOne(cp => cp.Category)
               .WithMany(p => p.CategoryProducts)
               .HasForeignKey(cp => cp.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasMany(x=>x.CategoryLanguages)
                .WithOne(x=>x.Category)
                .HasForeignKey(x=>x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<SubCategoryLaunguage>()
                .HasOne(x => x.SubCategory)
                .WithMany(x=>x.subCategoryLaunguages)
                .HasForeignKey(x=>x.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(cp => cp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);







        }

    }
}
