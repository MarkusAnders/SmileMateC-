using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileMate.Migrations
{
    /// <inheritdoc />
    public partial class ReceptionMedHistoryList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_MedicalHistories_MedicalHistoryId",
                table: "Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Receptions_MedicalHistoryId",
                table: "Receptions");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId",
                table: "Receptions");

            migrationBuilder.AddColumn<long>(
                name: "ReceptionId",
                table: "MedicalHistories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_ReceptionId",
                table: "MedicalHistories",
                column: "ReceptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistories_Receptions_ReceptionId",
                table: "MedicalHistories",
                column: "ReceptionId",
                principalTable: "Receptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistories_Receptions_ReceptionId",
                table: "MedicalHistories");

            migrationBuilder.DropIndex(
                name: "IX_MedicalHistories_ReceptionId",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "ReceptionId",
                table: "MedicalHistories");

            migrationBuilder.AddColumn<long>(
                name: "MedicalHistoryId",
                table: "Receptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_MedicalHistoryId",
                table: "Receptions",
                column: "MedicalHistoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_MedicalHistories_MedicalHistoryId",
                table: "Receptions",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id");
        }
    }
}
