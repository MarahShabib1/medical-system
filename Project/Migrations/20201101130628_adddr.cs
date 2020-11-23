using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class adddr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Doctorid",
                table: "records",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_records_Doctorid",
                table: "records",
                column: "Doctorid");

            migrationBuilder.AddForeignKey(
                name: "FK_records_doctor_Doctorid",
                table: "records",
                column: "Doctorid",
                principalTable: "doctor",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_records_doctor_Doctorid",
                table: "records");

            migrationBuilder.DropIndex(
                name: "IX_records_Doctorid",
                table: "records");

            migrationBuilder.DropColumn(
                name: "Doctorid",
                table: "records");
        }
    }
}
