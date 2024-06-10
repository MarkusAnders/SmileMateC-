using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileMate.Migrations
{
    /// <inheritdoc />
    public partial class ReceptionListClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Receptions_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReceptionId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "Receptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptions_ClientId",
                table: "Receptions",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptions_AspNetUsers_ClientId",
                table: "Receptions",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptions_AspNetUsers_ClientId",
                table: "Receptions");

            migrationBuilder.DropIndex(
                name: "IX_Receptions_ClientId",
                table: "Receptions");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Receptions");

            migrationBuilder.AddColumn<long>(
                name: "ReceptionId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReceptionId",
                table: "AspNetUsers",
                column: "ReceptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Receptions_ReceptionId",
                table: "AspNetUsers",
                column: "ReceptionId",
                principalTable: "Receptions",
                principalColumn: "Id");
        }
    }
}
