﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RAS.Services.ProductAPI.DbContexts;

#nullable disable

namespace RAS.Services.ProductAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RAS.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Brand = "Gibson",
                            Category = "Elektro Gitar",
                            Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://www.do-re.com.tr/gibson-les-paul-standard-50s-elektro-gitar-gold-top-45530a2d6cd5dcd3f4e455ed47d5190a-e27c52b8db784a60a3b9470d7feaa944-max-pp.jpg",
                            Model = "Les Paul",
                            Price = 50000
                        },
                        new
                        {
                            ProductId = 2,
                            Brand = "Fender",
                            Category = "Elektro Gitar",
                            Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://www.do-re.com.tr/sx-stratocaster-elektro-gitar-3-tone-sunburst-5671a48d4ec447a809ccbc638df5b662-9e13dd93ea6bb10a149615558cc7768c-max-pp.jpg",
                            Model = "Stratocaster",
                            Price = 45000
                        },
                        new
                        {
                            ProductId = 3,
                            Brand = "Fender",
                            Category = "Elektro Gitar",
                            Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://www.do-re.com.tr/sx-telecaster-elektro-gitar-butter-scotch-blonde-36c2e45a3f6bd6c23bd69ecb21cc8210-f0e6dfa3b4bd6fca9d235a74ef762f8b-max-pp.jpg",
                            Model = "Telecaster",
                            Price = 40000
                        },
                        new
                        {
                            ProductId = 4,
                            Brand = "Gibson",
                            Category = "Elektro Gitar",
                            Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://www.do-re.com.tr/gibson-sg-standard-61-elektro-gitar-vintage-cherry-881db135534c212a112ec693d8398b83-2dea22980d3f490f0c77b774fbf8bc56-max-pp.jpg",
                            Model = "SG",
                            Price = 35000
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
