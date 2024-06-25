using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenAPI.Core.Migrations
{
    public partial class AddPaymentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");
        }
    }
}
