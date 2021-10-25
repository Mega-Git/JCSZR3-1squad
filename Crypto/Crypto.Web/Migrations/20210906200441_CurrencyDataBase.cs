using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypto.Web.Migrations
{
    public partial class CurrencyDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "Prices",
                table: "Currency");

            migrationBuilder.RenameColumn(
                name: "Timestamps",
                table: "Currency",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Timestamp",
                columns: table => new
                {
                    TimestampId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PriceId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    PriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    TimestampId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.PriceId);
                    table.ForeignKey(
                        name: "FK_Price_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Price_Timestamp_TimestampId",
                        column: x => x.TimestampId,
                        principalTable: "Timestamp",
                        principalColumn: "TimestampId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Price_CurrencyId",
                table: "Price",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Price_TimestampId",
                table: "Price",
                column: "TimestampId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timestamp_CurrencyId",
                table: "Timestamp",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Currency",
                newName: "Timestamps");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Currency",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prices",
                table: "Currency",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
