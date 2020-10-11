using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class medList : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_medicine_company_Companyid",
                table: "medicine",
                column: "Companyid",
                principalTable: "company",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
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
