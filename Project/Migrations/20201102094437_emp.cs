using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class emp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_employee_Employeeid",
                table: "records");

            migrationBuilder.AlterColumn<int>(
                name: "Employeeid",
                table: "records",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_records_employee_Employeeid",
                table: "records",
                column: "Employeeid",
                principalTable: "employee",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_employee_Employeeid",
                table: "records");

            migrationBuilder.AlterColumn<int>(
                name: "Employeeid",
                table: "records",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_records_employee_Employeeid",
                table: "records",
                column: "Employeeid",
                principalTable: "employee",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
