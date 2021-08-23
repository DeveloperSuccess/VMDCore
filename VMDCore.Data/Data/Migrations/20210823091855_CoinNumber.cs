using Microsoft.EntityFrameworkCore.Migrations;

namespace VMDCore.Data.Data.Migrations
{
    public partial class CoinNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberCoins",
                table: "Coins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberCoins",
                table: "Coins");
        }
    }
}
