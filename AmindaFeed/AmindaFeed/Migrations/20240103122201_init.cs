using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmindaFeed.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmindaProductDb",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    PurchasePrice = table.Column<double>(type: "double precision", nullable: false),
                    SalePrice = table.Column<double>(type: "double precision", nullable: false),
                    IsOutOfStock = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmindaProductDb", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmindaProductDb");
        }
    }
}
