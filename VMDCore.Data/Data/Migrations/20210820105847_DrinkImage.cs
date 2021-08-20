using Microsoft.EntityFrameworkCore.Migrations;

namespace VMDCore.Data.Data.Migrations
{
    public partial class DrinkImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesCount",
                table: "Drinks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImagesCount",
                table: "Drinks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
