using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileMate.Migrations
{
    /// <inheritdoc />
    public partial class DoctorReceptionsList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Receptions_Doctor_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Doctor_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Doctor_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "DoctorId",
                table: "Receptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_DoctorId",
                table: "Receptions",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_AspNetUsers_DoctorId",
                table: "Receptions",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_AspNetUsers_DoctorId",
                table: "Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Receptions_DoctorId",
                table: "Receptions");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Receptions");

            migrationBuilder.AddColumn<long>(
                name: "Doctor_ReceptionId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Doctor_ReceptionId",
                table: "AspNetUsers",
                column: "Doctor_ReceptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Receptions_Doctor_ReceptionId",
                table: "AspNetUsers",
                column: "Doctor_ReceptionId",
                principalTable: "Receptions",
                principalColumn: "Id");
        }
    }
}
