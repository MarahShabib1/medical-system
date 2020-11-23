using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class pres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_prescription_Prescriptionid",
                table: "records");

            migrationBuilder.AlterColumn<int>(
                name: "Prescriptionid",
                table: "records",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_records_prescription_Prescriptionid",
                table: "records",
                column: "Prescriptionid",
                principalTable: "prescription",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_prescription_Prescriptionid",
                table: "records");

            migrationBuilder.AlterColumn<int>(
                name: "Prescriptionid",
                table: "records",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_records_prescription_Prescriptionid",
                table: "records",
                column: "Prescriptionid",
                principalTable: "prescription",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
