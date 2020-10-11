using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class DeleteMed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medicine_company_companyid",
                table: "medicine");

            migrationBuilder.RenameColumn(
                name: "companyid",
                table: "medicine",
                newName: "Companyid");

            migrationBuilder.RenameIndex(
                name: "IX_medicine_companyid",
                table: "medicine",
                newName: "IX_medicine_Companyid");

            migrationBuilder.AlterColumn<string>(
                name: "pwd",
                table: "user1",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Companyid",
                table: "medicine",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_medicine_company_Companyid",
                table: "medicine",
                column: "Companyid",
                principalTable: "company",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medicine_company_Companyid",
                table: "medicine");

            migrationBuilder.RenameColumn(
                name: "Companyid",
                table: "medicine",
                newName: "companyid");

            migrationBuilder.RenameIndex(
                name: "IX_medicine_Companyid",
                table: "medicine",
                newName: "IX_medicine_companyid");

            migrationBuilder.AlterColumn<string>(
                name: "pwd",
                table: "user1",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "companyid",
                table: "medicine",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_medicine_company_companyid",
                table: "medicine",
                column: "companyid",
                principalTable: "company",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
