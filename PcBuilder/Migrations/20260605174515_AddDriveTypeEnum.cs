using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddDriveTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSsd",
                table: "HardDrive");

            migrationBuilder.AddColumn<string>(
                name: "PcDriveType",
                table: "HardDrive",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PcDriveType",
                table: "HardDrive");

            migrationBuilder.AddColumn<bool>(
                name: "IsSsd",
                table: "HardDrive",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
