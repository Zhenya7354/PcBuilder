using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyAndColorSchemeEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasRgb",
                table: "Ram");

            migrationBuilder.DropColumn(
                name: "HasRgb",
                table: "Gpu");

            migrationBuilder.DropColumn(
                name: "HasRgb",
                table: "CpuCooler");

            migrationBuilder.RenameColumn(
                name: "PriceUsd",
                table: "Ram",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceUsd",
                table: "Psu",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceUsd",
                table: "PcMonitor",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceUsd",
                table: "PcCase",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceUsd",
                table: "Motherboard",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceUsd",
                table: "HardDrive",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceUsd",
                table: "CpuCooler",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceUsd",
                table: "Cpu",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "ColorScheme",
                table: "Ram",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Ram",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Psu",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "PcMonitor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorScheme",
                table: "PcCase",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "PcCase",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Motherboard",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "HardDrive",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColorScheme",
                table: "Gpu",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Gpu",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorScheme",
                table: "CpuCooler",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "CpuCooler",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Cpu",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorScheme",
                table: "Ram");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Ram");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Psu");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "PcMonitor");

            migrationBuilder.DropColumn(
                name: "ColorScheme",
                table: "PcCase");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "PcCase");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Motherboard");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "HardDrive");

            migrationBuilder.DropColumn(
                name: "ColorScheme",
                table: "Gpu");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Gpu");

            migrationBuilder.DropColumn(
                name: "ColorScheme",
                table: "CpuCooler");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "CpuCooler");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Cpu");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Ram",
                newName: "PriceUsd");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Psu",
                newName: "PriceUsd");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PcMonitor",
                newName: "PriceUsd");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PcCase",
                newName: "PriceUsd");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Motherboard",
                newName: "PriceUsd");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "HardDrive",
                newName: "PriceUsd");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "CpuCooler",
                newName: "PriceUsd");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Cpu",
                newName: "PriceUsd");

            migrationBuilder.AddColumn<bool>(
                name: "HasRgb",
                table: "Ram",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRgb",
                table: "Gpu",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRgb",
                table: "CpuCooler",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
