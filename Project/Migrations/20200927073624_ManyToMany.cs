using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_user_Userid",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_roles_Userid",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "roles");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Userid = table.Column<int>(nullable: false),
                    Roleid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.Userid, x.Roleid });
                    table.ForeignKey(
                        name: "FK_UserRole_roles_Roleid",
                        column: x => x.Roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_user_Userid",
                        column: x => x.Userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Roleid",
                table: "UserRole",
                column: "Roleid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_Userid",
                table: "roles",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_user_Userid",
                table: "roles",
                column: "Userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
