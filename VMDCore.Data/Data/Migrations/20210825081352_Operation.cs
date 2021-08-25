using Microsoft.EntityFrameworkCore.Migrations;

namespace VMDCore.Data.Data.Migrations
{
    public partial class Operation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationId);
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "OperationId", "Balance", "Message" },
                values: new object[] { 1, 0, "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}
