using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmileMate.Migrations
{
    /// <inheritdoc />
    public partial class MedHistoryPhotoCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "MedicalHistories");

            migrationBuilder.AddColumn<List<string>>(
                name: "PhotoCollection",
                table: "MedicalHistories",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoCollection",
                table: "MedicalHistories");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "MedicalHistories",
                type: "text",
                nullable: true);
        }
    }
}
