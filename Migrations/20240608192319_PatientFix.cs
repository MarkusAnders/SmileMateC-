using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileMate.Migrations
{
    /// <inheritdoc />
    public partial class PatientFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Receptions_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Receptions_ReceptionId",
                table: "AspNetUsers",
                column: "ReceptionId",
                principalTable: "Receptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Receptions_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Receptions_ReceptionId",
                table: "AspNetUsers",
                column: "ReceptionId",
                principalTable: "Receptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
