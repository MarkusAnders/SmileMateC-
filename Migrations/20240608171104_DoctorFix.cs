using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileMate.Migrations
{
    /// <inheritdoc />
    public partial class DoctorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Receptions_Doctor_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Receptions_Doctor_ReceptionId",
                table: "AspNetUsers",
                column: "Doctor_ReceptionId",
                principalTable: "Receptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Receptions_Doctor_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Receptions_Doctor_ReceptionId",
                table: "AspNetUsers",
                column: "Doctor_ReceptionId",
                principalTable: "Receptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
