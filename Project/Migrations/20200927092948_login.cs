using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "user",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Login",
                table: "user",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
