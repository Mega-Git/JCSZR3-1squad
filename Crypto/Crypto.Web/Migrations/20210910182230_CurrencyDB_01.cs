using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypto.Web.Migrations
{
    public partial class CurrencyDB_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Timestamp",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
