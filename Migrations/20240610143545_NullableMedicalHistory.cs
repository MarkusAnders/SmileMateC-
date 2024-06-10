using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileMate.Migrations
{
    /// <inheritdoc />
    public partial class NullableMedicalHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_MedicalHistories_MedicalHistoryId",
                table: "Receptions");

            migrationBuilder.AlterColumn<long>(
                name: "MedicalHistoryId",
                table: "Receptions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_MedicalHistories_MedicalHistoryId",
                table: "Receptions",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_MedicalHistories_MedicalHistoryId",
                table: "Receptions");

            migrationBuilder.AlterColumn<long>(
                name: "MedicalHistoryId",
                table: "Receptions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_MedicalHistories_MedicalHistoryId",
                table: "Receptions",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
