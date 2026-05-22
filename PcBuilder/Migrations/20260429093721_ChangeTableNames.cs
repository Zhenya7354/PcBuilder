using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Migrations;

/// <inheritdoc />
public partial class ChangeTableNames : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_admin_AspNetUsers_userId",
            table: "admin");

        migrationBuilder.DropForeignKey(
            name: "FK_build_AspNetUsers_userId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_cpu_cooler_cpuCoolerId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_cpu_cpuId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_gpu_gpuId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_hard_drive_hardDriveId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_memory_ramId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_monitor_monitorId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_motherboard_motherboardId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_pc_case_caseId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_build_psu_psuId",
            table: "build");

        migrationBuilder.DropForeignKey(
            name: "FK_regular_user_AspNetUsers_userId",
            table: "regular_user");

        migrationBuilder.DropPrimaryKey(
            name: "PK_build",
            table: "build");

        migrationBuilder.DropPrimaryKey(
            name: "PK_admin",
            table: "admin");

        migrationBuilder.DropPrimaryKey(
            name: "PK_regular_user",
            table: "regular_user");

        migrationBuilder.RenameTable(
            name: "build",
            newName: "Build");

        migrationBuilder.RenameTable(
            name: "admin",
            newName: "Admin");

        migrationBuilder.RenameTable(
            name: "regular_user",
            newName: "RegularUser");

        migrationBuilder.RenameIndex(
            name: "IX_build_userId",
            table: "Build",
            newName: "IX_Build_userId");

        migrationBuilder.RenameIndex(
            name: "IX_build_ramId",
            table: "Build",
            newName: "IX_Build_ramId");

        migrationBuilder.RenameIndex(
            name: "IX_build_psuId",
            table: "Build",
            newName: "IX_Build_psuId");

        migrationBuilder.RenameIndex(
            name: "IX_build_motherboardId",
            table: "Build",
            newName: "IX_Build_motherboardId");

        migrationBuilder.RenameIndex(
            name: "IX_build_monitorId",
            table: "Build",
            newName: "IX_Build_monitorId");

        migrationBuilder.RenameIndex(
            name: "IX_build_hardDriveId",
            table: "Build",
            newName: "IX_Build_hardDriveId");

        migrationBuilder.RenameIndex(
            name: "IX_build_gpuId",
            table: "Build",
            newName: "IX_Build_gpuId");

        migrationBuilder.RenameIndex(
            name: "IX_build_cpuId",
            table: "Build",
            newName: "IX_Build_cpuId");

        migrationBuilder.RenameIndex(
            name: "IX_build_cpuCoolerId",
            table: "Build",
            newName: "IX_Build_cpuCoolerId");

        migrationBuilder.RenameIndex(
            name: "IX_build_caseId",
            table: "Build",
            newName: "IX_Build_caseId");

        migrationBuilder.RenameIndex(
            name: "IX_admin_userId",
            table: "Admin",
            newName: "IX_Admin_userId");

        migrationBuilder.RenameIndex(
            name: "IX_regular_user_userId",
            table: "RegularUser",
            newName: "IX_RegularUser_userId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Build",
            table: "Build",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Admin",
            table: "Admin",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_RegularUser",
            table: "RegularUser",
            column: "id");

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
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

        migrationBuilder.DropPrimaryKey(
            name: "PK_Build",
            table: "Build");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Admin",
            table: "Admin");

        migrationBuilder.DropPrimaryKey(
            name: "PK_RegularUser",
            table: "RegularUser");

        migrationBuilder.RenameTable(
            name: "Build",
            newName: "build");

        migrationBuilder.RenameTable(
            name: "Admin",
            newName: "admin");

        migrationBuilder.RenameTable(
            name: "RegularUser",
            newName: "regular_user");

        migrationBuilder.RenameIndex(
            name: "IX_Build_userId",
            table: "build",
            newName: "IX_build_userId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_ramId",
            table: "build",
            newName: "IX_build_ramId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_psuId",
            table: "build",
            newName: "IX_build_psuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_motherboardId",
            table: "build",
            newName: "IX_build_motherboardId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_monitorId",
            table: "build",
            newName: "IX_build_monitorId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_hardDriveId",
            table: "build",
            newName: "IX_build_hardDriveId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_gpuId",
            table: "build",
            newName: "IX_build_gpuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_cpuId",
            table: "build",
            newName: "IX_build_cpuId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_cpuCoolerId",
            table: "build",
            newName: "IX_build_cpuCoolerId");

        migrationBuilder.RenameIndex(
            name: "IX_Build_caseId",
            table: "build",
            newName: "IX_build_caseId");

        migrationBuilder.RenameIndex(
            name: "IX_Admin_userId",
            table: "admin",
            newName: "IX_admin_userId");

        migrationBuilder.RenameIndex(
            name: "IX_RegularUser_userId",
            table: "regular_user",
            newName: "IX_regular_user_userId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_build",
            table: "build",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_admin",
            table: "admin",
            column: "id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_regular_user",
            table: "regular_user",
            column: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_admin_AspNetUsers_userId",
            table: "admin",
            column: "userId",
            principalTable: "AspNetUsers",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_AspNetUsers_userId",
            table: "build",
            column: "userId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_build_cpu_cooler_cpuCoolerId",
            table: "build",
            column: "cpuCoolerId",
            principalTable: "cpu_cooler",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_cpu_cpuId",
            table: "build",
            column: "cpuId",
            principalTable: "cpu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_gpu_gpuId",
            table: "build",
            column: "gpuId",
            principalTable: "gpu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_hard_drive_hardDriveId",
            table: "build",
            column: "hardDriveId",
            principalTable: "hard_drive",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_memory_ramId",
            table: "build",
            column: "ramId",
            principalTable: "memory",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_monitor_monitorId",
            table: "build",
            column: "monitorId",
            principalTable: "monitor",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_motherboard_motherboardId",
            table: "build",
            column: "motherboardId",
            principalTable: "motherboard",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_pc_case_caseId",
            table: "build",
            column: "caseId",
            principalTable: "pc_case",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_build_psu_psuId",
            table: "build",
            column: "psuId",
            principalTable: "psu",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "FK_regular_user_AspNetUsers_userId",
            table: "regular_user",
            column: "userId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
