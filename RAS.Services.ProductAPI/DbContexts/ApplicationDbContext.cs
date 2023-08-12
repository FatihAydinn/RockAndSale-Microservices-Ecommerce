using Microsoft.EntityFrameworkCore;
using RAS.Services.ProductAPI.Models;

namespace RAS.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                GuitarModel = "Les Paul",
                Price = 50000,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "gibson-les-paul-standard-50s-elektro-gitar-gold.jpg",
                Brand = "Gibson",
                Category = "Elektro Gitar"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                GuitarModel = "Stratocaster",
                Price = 45000,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "sx-stratocaster-elektro-gitar-3-tone-sunburst.jpg",
                Brand = "Fender",
                Category = "Elektro Gitar"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                GuitarModel = "Telecaster",
                Price = 40000,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "sx-telecaster-elektro-gitar-butter-scotch-blonde.jpg",
                Brand = "Fender",
                Category = "Elektro Gitar"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                GuitarModel = "SG",
                Price = 35000,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "gibson-sg-standard-61-elektro-gitar-vintage-cherry-.jpg",
                Brand = "Gibson",
                Category = "Elektro Gitar"
            });
        }
    }
}
