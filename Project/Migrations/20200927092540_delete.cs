using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_roles_Roleid",
                table: "user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_Userid",
                table: "user_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_role",
                table: "user_role");

            migrationBuilder.RenameTable(
                name: "user_role",
                newName: "UserRole");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_Roleid",
                table: "UserRole",
                newName: "IX_UserRole_Roleid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "Userid", "Roleid" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_roles_Roleid",
                table: "UserRole",
                column: "Roleid",
                principalTable: "roles",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_roles_Roleid",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_user_Userid",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "user_role");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_Roleid",
                table: "user_role",
                newName: "IX_user_role_Roleid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_role",
                table: "user_role",
                columns: new[] { "Userid", "Roleid" });

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_roles_Roleid",
                table: "user_role",
                column: "Roleid",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_user_Userid",
                table: "user_role",
                column: "Userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
