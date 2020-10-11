using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctor_user_Userid",
                table: "doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_user_Userid",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_records_user_Userid",
                table: "records");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_user_Userid",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "_user");

            migrationBuilder.AddPrimaryKey(
                name: "PK__user",
                table: "_user",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_doctor__user_Userid",
                table: "doctor",
                column: "Userid",
                principalTable: "_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employee__user_Userid",
                table: "employee",
                column: "Userid",
                principalTable: "_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_records__user_Userid",
                table: "records",
                column: "Userid",
                principalTable: "_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole__user_Userid",
                table: "UserRole",
                column: "Userid",
                principalTable: "_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctor__user_Userid",
                table: "doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_employee__user_Userid",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_records__user_Userid",
                table: "records");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole__user_Userid",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK__user",
                table: "_user");

            migrationBuilder.RenameTable(
                name: "_user",
                newName: "user");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_doctor_user_Userid",
                table: "doctor",
                column: "Userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_user_Userid",
                table: "employee",
                column: "Userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_records_user_Userid",
                table: "records",
                column: "Userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_user_Userid",
                table: "UserRole",
                column: "Userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
