using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeCrud.Data.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "james@gmail.com", "james", "miller" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 2, "stewart@gmail.com", "stewart", "coffey" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { 3, "david@gmail.com", "david", "fooster" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
