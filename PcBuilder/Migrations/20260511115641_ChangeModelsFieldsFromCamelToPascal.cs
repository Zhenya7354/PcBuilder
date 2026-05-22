using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Migrations;

/// <inheritdoc />
public partial class ChangeModelsFieldsFromCamelToPascal : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Admin_AspNetUsers_userId",
            table: "Admin");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_AspNetUsers_userId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_CpuCooler_cpuCoolerId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Cpu_cpuId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Gpu_gpuId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_HardDrive_hardDriveId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Motherboard_motherboardId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_PcCase_caseId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_PcMonitor_monitorId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Psu_psuId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Ram_ramId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_RegularUser_AspNetUsers_userId",
            table: "RegularUser");

        migrationBuilder.RenameColumn(
            name: "userId",
            table: "RegularUser",
            newName: "UserId");

        migrationBuilder.RenameColumn(
            name: "prefferedCurrency",
            table: "RegularUser",
            newName: "PrefferedCurrency");

        migrationBuilder.RenameColumn(
            name: "postalCode",
            table: "RegularUser",
            newName: "PostalCode");

        migrationBuilder.RenameColumn(
            name: "buildsCreated",
            table: "RegularUser",
            newName: "BuildsCreated");

        migrationBuilder.RenameColumn(
            name: "id",
            table: "RegularUser",
            newName: "Id");

        migrationBuilder.RenameIndex(
            name: "IX_RegularUser_userId",
            table: "RegularUser",
            newName: "IX_RegularUser_UserId");

        migrationBuilder.RenameColumn(
            name: "userId",
            table: "Build",
            newName: "UserId");

        migrationBuilder.RenameColumn(
            name: "ramId",
            table: "Build",
            newName: "RamId");

        migrationBuilder.RenameColumn(
            name: "psuId",
            table: "Build",
            newName: "PsuId");

        migrationBuilder.RenameColumn(
            name: "name",
            table: "Build",
            newName: "Name");

        migrationBuilder.RenameColumn(
            name: "motherboardId",
            table: "Build",
            newName: "MotherboardId");

        migrationBuilder.RenameColumn(
            name: "monitorId",
            table: "Build",
            newName: "MonitorId");

        migrationBuilder.RenameColumn(
            name: "hardDriveId",
            table: "Build",
            newName: "HardDriveId");

        migrationBuilder.RenameColumn(
            name: "gpuId",
            table: "Build",
            newName: "GpuId");

        migrationBuilder.RenameColumn(
            name: "cpuId",
            table: "Build",
            newName: "CpuId");

        migrationBuilder.RenameColumn(
            name: "cpuCoolerId",
            table: "Build",
            newName: "CpuCoolerId");

        migrationBuilder.RenameColumn(
            name: "caseId",
            table: "Build",
            newName: "CaseId");

        migrationBuilder.RenameColumn(
            name: "id",
            table: "Build",
            newName: "Id");

        migrationBuilder.RenameIndex(
            name: "IX_Build_userId",
            table: "Build",
            newName: "IX_Build_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_ramId",
            table: "Build",
            newName: "IX_Build_RamId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_psuId",
            table: "Build",
            newName: "IX_Build_PsuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_motherboardId",
            table: "Build",
            newName: "IX_Build_MotherboardId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_monitorId",
            table: "Build",
            newName: "IX_Build_MonitorId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_hardDriveId",
            table: "Build",
            newName: "IX_Build_HardDriveId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_gpuId",
            table: "Build",
            newName: "IX_Build_GpuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_cpuId",
            table: "Build",
            newName: "IX_Build_CpuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_cpuCoolerId",
            table: "Build",
            newName: "IX_Build_CpuCoolerId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_caseId",
            table: "Build",
            newName: "IX_Build_CaseId");

        migrationBuilder.RenameColumn(
            name: "userId",
            table: "Admin",
            newName: "UserId");

        migrationBuilder.RenameColumn(
            name: "id",
            table: "Admin",
            newName: "Id");

        migrationBuilder.RenameIndex(
            name: "IX_Admin_userId",
            table: "Admin",
            newName: "IX_Admin_UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_Admin_AspNetUsers_UserId",
            table: "Admin",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_AspNetUsers_UserId",
            table: "Build",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Build_CpuCooler_CpuCoolerId",
            table: "Build",
            column: "CpuCoolerId",
            principalTable: "CpuCooler",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Cpu_CpuId",
            table: "Build",
            column: "CpuId",
            principalTable: "Cpu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Gpu_GpuId",
            table: "Build",
            column: "GpuId",
            principalTable: "Gpu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_HardDrive_HardDriveId",
            table: "Build",
            column: "HardDriveId",
            principalTable: "HardDrive",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Motherboard_MotherboardId",
            table: "Build",
            column: "MotherboardId",
            principalTable: "Motherboard",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_PcCase_CaseId",
            table: "Build",
            column: "CaseId",
            principalTable: "PcCase",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_PcMonitor_MonitorId",
            table: "Build",
            column: "MonitorId",
            principalTable: "PcMonitor",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Psu_PsuId",
            table: "Build",
            column: "PsuId",
            principalTable: "Psu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Ram_RamId",
            table: "Build",
            column: "RamId",
            principalTable: "Ram",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_RegularUser_AspNetUsers_UserId",
            table: "RegularUser",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Admin_AspNetUsers_UserId",
            table: "Admin");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_AspNetUsers_UserId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_CpuCooler_CpuCoolerId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Cpu_CpuId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Gpu_GpuId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_HardDrive_HardDriveId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Motherboard_MotherboardId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_PcCase_CaseId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_PcMonitor_MonitorId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Psu_PsuId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_Build_Ram_RamId",
            table: "Build");

        migrationBuilder.DropForeignKey(
            name: "FK_RegularUser_AspNetUsers_UserId",
            table: "RegularUser");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "RegularUser",
            newName: "userId");

        migrationBuilder.RenameColumn(
            name: "PrefferedCurrency",
            table: "RegularUser",
            newName: "prefferedCurrency");

        migrationBuilder.RenameColumn(
            name: "PostalCode",
            table: "RegularUser",
            newName: "postalCode");

        migrationBuilder.RenameColumn(
            name: "BuildsCreated",
            table: "RegularUser",
            newName: "buildsCreated");

        migrationBuilder.RenameColumn(
            name: "Id",
            table: "RegularUser",
            newName: "id");

        migrationBuilder.RenameIndex(
            name: "IX_RegularUser_UserId",
            table: "RegularUser",
            newName: "IX_RegularUser_userId");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "Build",
            newName: "userId");

        migrationBuilder.RenameColumn(
            name: "RamId",
            table: "Build",
            newName: "ramId");

        migrationBuilder.RenameColumn(
            name: "PsuId",
            table: "Build",
            newName: "psuId");

        migrationBuilder.RenameColumn(
            name: "Name",
            table: "Build",
            newName: "name");

        migrationBuilder.RenameColumn(
            name: "MotherboardId",
            table: "Build",
            newName: "motherboardId");

        migrationBuilder.RenameColumn(
            name: "MonitorId",
            table: "Build",
            newName: "monitorId");

        migrationBuilder.RenameColumn(
            name: "HardDriveId",
            table: "Build",
            newName: "hardDriveId");

        migrationBuilder.RenameColumn(
            name: "GpuId",
            table: "Build",
            newName: "gpuId");

        migrationBuilder.RenameColumn(
            name: "CpuId",
            table: "Build",
            newName: "cpuId");

        migrationBuilder.RenameColumn(
            name: "CpuCoolerId",
            table: "Build",
            newName: "cpuCoolerId");

        migrationBuilder.RenameColumn(
            name: "CaseId",
            table: "Build",
            newName: "caseId");

        migrationBuilder.RenameColumn(
            name: "Id",
            table: "Build",
            newName: "id");

        migrationBuilder.RenameIndex(
            name: "IX_Build_UserId",
            table: "Build",
            newName: "IX_Build_userId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_RamId",
            table: "Build",
            newName: "IX_Build_ramId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_PsuId",
            table: "Build",
            newName: "IX_Build_psuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_MotherboardId",
            table: "Build",
            newName: "IX_Build_motherboardId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_MonitorId",
            table: "Build",
            newName: "IX_Build_monitorId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_HardDriveId",
            table: "Build",
            newName: "IX_Build_hardDriveId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_GpuId",
            table: "Build",
            newName: "IX_Build_gpuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_CpuId",
            table: "Build",
            newName: "IX_Build_cpuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_CpuCoolerId",
            table: "Build",
            newName: "IX_Build_cpuCoolerId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_CaseId",
            table: "Build",
            newName: "IX_Build_caseId");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "Admin",
            newName: "userId");

        migrationBuilder.RenameColumn(
            name: "Id",
            table: "Admin",
            newName: "id");

        migrationBuilder.RenameIndex(
            name: "IX_Admin_UserId",
            table: "Admin",
            newName: "IX_Admin_userId");

        migrationBuilder.AddForeignKey(
            name: "FK_Admin_AspNetUsers_userId",
            table: "Admin",
            column: "userId",
            principalTable: "AspNetUsers",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_AspNetUsers_userId",
            table: "Build",
            column: "userId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Build_CpuCooler_cpuCoolerId",
            table: "Build",
            column: "cpuCoolerId",
            principalTable: "CpuCooler",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Cpu_cpuId",
            table: "Build",
            column: "cpuId",
            principalTable: "Cpu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Gpu_gpuId",
            table: "Build",
            column: "gpuId",
            principalTable: "Gpu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_HardDrive_hardDriveId",
            table: "Build",
            column: "hardDriveId",
            principalTable: "HardDrive",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Motherboard_motherboardId",
            table: "Build",
            column: "motherboardId",
            principalTable: "Motherboard",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_PcCase_caseId",
            table: "Build",
            column: "caseId",
            principalTable: "PcCase",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_PcMonitor_monitorId",
            table: "Build",
            column: "monitorId",
            principalTable: "PcMonitor",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Psu_psuId",
            table: "Build",
            column: "psuId",
            principalTable: "Psu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_Build_Ram_ramId",
            table: "Build",
            column: "ramId",
            principalTable: "Ram",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_RegularUser_AspNetUsers_userId",
            table: "RegularUser",
            column: "userId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
