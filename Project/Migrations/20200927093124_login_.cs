using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class login_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Login",
                table: "user",
                newName: "_Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Login",
                table: "user",
                newName: "Login");
        }
    }
}
