﻿using Microsoft.EntityFrameworkCore;
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
                Model = "Les Paul",
                Price = 50000,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://www.do-re.com.tr/gibson-les-paul-standard-50s-elektro-gitar-gold-top-45530a2d6cd5dcd3f4e455ed47d5190a-e27c52b8db784a60a3b9470d7feaa944-max-pp.jpg",
                Brand = "Gibson",
                Category = "Elektro Gitar"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Model = "Stratocaster",
                Price = 45000,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://www.do-re.com.tr/sx-stratocaster-elektro-gitar-3-tone-sunburst-5671a48d4ec447a809ccbc638df5b662-9e13dd93ea6bb10a149615558cc7768c-max-pp.jpg",
                Brand = "Fender",
                Category = "Elektro Gitar"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Model = "Telecaster",
                Price = 40000,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://www.do-re.com.tr/sx-telecaster-elektro-gitar-butter-scotch-blonde-36c2e45a3f6bd6c23bd69ecb21cc8210-f0e6dfa3b4bd6fca9d235a74ef762f8b-max-pp.jpg",
                Brand = "Fender",
                Category = "Elektro Gitar"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Model = "SG",
                Price = 35000,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://www.do-re.com.tr/gibson-sg-standard-61-elektro-gitar-vintage-cherry-881db135534c212a112ec693d8398b83-2dea22980d3f490f0c77b774fbf8bc56-max-pp.jpg",
                Brand = "Gibson",
                Category = "Elektro Gitar"
            });
        }
    }
}
