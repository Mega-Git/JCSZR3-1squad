using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypto.Web.Migrations
{
    public partial class CurrencyDB_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_Timestamp_TimestampId",
                table: "Price");

            migrationBuilder.DropTable(
                name: "Timestamp");

            migrationBuilder.DropIndex(
                name: "IX_Price_TimestampId",
                table: "Price");

            migrationBuilder.DropColumn(
                name: "TimestampId",
                table: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Timestamp",
                table: "Price",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Price");

            migrationBuilder.AddColumn<int>(
                name: "TimestampId",
                table: "Price",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Timestamp",
                columns: table => new
                {
                    TimestampId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timestamp", x => x.TimestampId);
                    table.ForeignKey(
                        name: "FK_Timestamp_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Price_TimestampId",
                table: "Price",
                column: "TimestampId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timestamp_CurrencyId",
                table: "Timestamp",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Timestamp_TimestampId",
                table: "Price",
                column: "TimestampId",
                principalTable: "Timestamp",
                principalColumn: "TimestampId");
        }
    }
}
