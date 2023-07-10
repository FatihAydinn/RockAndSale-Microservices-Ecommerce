using System.Collections.Generic;
using RAS.Services.BagAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RAS.Services.BagAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<BagHeader> BagHeaders { get; set; }
        public DbSet<BagDetails> BagDetails { get; set; }
    }
}
