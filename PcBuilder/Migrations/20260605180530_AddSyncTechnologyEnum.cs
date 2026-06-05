using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddSyncTechnologyEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFreeSync",
                table: "PcMonitor");

            migrationBuilder.DropColumn(
                name: "HasFreeSyncPremium",
                table: "PcMonitor");

            migrationBuilder.DropColumn(
                name: "HasGSync",
                table: "PcMonitor");

            migrationBuilder.AddColumn<int[]>(
                name: "SyncTechnologies",
                table: "PcMonitor",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SyncTechnologies",
                table: "PcMonitor");

            migrationBuilder.AddColumn<bool>(
                name: "HasFreeSync",
                table: "PcMonitor",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasFreeSyncPremium",
                table: "PcMonitor",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasGSync",
                table: "PcMonitor",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
