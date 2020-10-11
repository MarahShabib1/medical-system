using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class phonenum1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "phonenumber",
                table: "user1",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phonenumber",
                table: "user1",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
