using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RAS.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuitarModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Brand", "Category", "Description", "GuitarModel", "ImageUrl", "Price" },
                values: new object[,]
                {
                    { 1, "Gibson", "Elektro Gitar", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "Les Paul", "https://www.do-re.com.tr/gibson-les-paul-standard-50s-elektro-gitar-gold-top-45530a2d6cd5dcd3f4e455ed47d5190a-e27c52b8db784a60a3b9470d7feaa944-max-pp.jpg", 50000 },
                    { 2, "Fender", "Elektro Gitar", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "Stratocaster", "https://www.do-re.com.tr/sx-stratocaster-elektro-gitar-3-tone-sunburst-5671a48d4ec447a809ccbc638df5b662-9e13dd93ea6bb10a149615558cc7768c-max-pp.jpg", 45000 },
                    { 3, "Fender", "Elektro Gitar", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "Telecaster", "https://www.do-re.com.tr/sx-telecaster-elektro-gitar-butter-scotch-blonde-36c2e45a3f6bd6c23bd69ecb21cc8210-f0e6dfa3b4bd6fca9d235a74ef762f8b-max-pp.jpg", 40000 },
                    { 4, "Gibson", "Elektro Gitar", "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.", "SG", "https://www.do-re.com.tr/gibson-sg-standard-61-elektro-gitar-vintage-cherry-881db135534c212a112ec693d8398b83-2dea22980d3f490f0c77b774fbf8bc56-max-pp.jpg", 35000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
