using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PcBuilder.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admin_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PrefferedCurrency = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    BuildsCreated = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegularUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cpu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModelNumber = table.Column<string>(type: "text", nullable: true),
                    Socket = table.Column<string>(type: "text", nullable: false),
                    ChipsetsSupported = table.Column<List<string>>(type: "text[]", nullable: false),
                    Cores = table.Column<int>(type: "integer", nullable: false),
                    Threads = table.Column<int>(type: "integer", nullable: false),
                    BaseClockGhz = table.Column<double>(type: "double precision", nullable: false),
                    BoostClockGhz = table.Column<double>(type: "double precision", nullable: true),
                    L3CacheMb = table.Column<int>(type: "integer", nullable: true),
                    TdpWatts = table.Column<int>(type: "integer", nullable: false),
                    MemoryType = table.Column<string>(type: "text", nullable: false),
                    MaxMemoryGb = table.Column<int>(type: "integer", nullable: false),
                    MaxMemorySpeedMhz = table.Column<int>(type: "integer", nullable: false),
                    MemoryChannels = table.Column<int>(type: "integer", nullable: false),
                    IntegratedGraphics = table.Column<bool>(type: "boolean", nullable: false),
                    IgpuModel = table.Column<string>(type: "text", nullable: true),
                    PcieVersion = table.Column<string>(type: "text", nullable: true),
                    PcieLanes = table.Column<int>(type: "integer", nullable: true),
                    IncludesCooler = table.Column<bool>(type: "boolean", nullable: false),
                    LaunchedYear = table.Column<int>(type: "integer", nullable: true),
                    PriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cpu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cpu_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CpuCooler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoolerType = table.Column<string>(type: "text", nullable: false),
                    SocketsSupported = table.Column<int[]>(type: "integer[]", nullable: false),
                    RadiatorSizeMm = table.Column<int>(type: "integer", nullable: true),
                    FanCount = table.Column<int>(type: "integer", nullable: false),
                    FanSizeMm = table.Column<int>(type: "integer", nullable: false),
                    MaxTdpWatts = table.Column<int>(type: "integer", nullable: false),
                    HeightMm = table.Column<int>(type: "integer", nullable: true),
                    HasRgb = table.Column<bool>(type: "boolean", nullable: false),
                    NoiseLevelDb = table.Column<double>(type: "double precision", nullable: true),
                    PriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CpuCooler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CpuCooler_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gpu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GpuChip = table.Column<string>(type: "text", nullable: false),
                    GpuInterface = table.Column<string>(type: "text", nullable: false),
                    VramGb = table.Column<int>(type: "integer", nullable: false),
                    VramType = table.Column<string>(type: "text", nullable: false),
                    BaseClockMhz = table.Column<int>(type: "integer", nullable: true),
                    BoostClockMhz = table.Column<int>(type: "integer", nullable: false),
                    MemoryBusBits = table.Column<int>(type: "integer", nullable: false),
                    MemoryBandwithGb = table.Column<double>(type: "double precision", nullable: true),
                    TdpWatts = table.Column<int>(type: "integer", nullable: false),
                    RecommendedPsuWattage = table.Column<int>(type: "integer", nullable: false),
                    PowerConnectors = table.Column<string>(type: "text", nullable: true),
                    OutputHdmi = table.Column<int>(type: "integer", nullable: false),
                    OutputDp = table.Column<int>(type: "integer", nullable: false),
                    CardLengthMm = table.Column<int>(type: "integer", nullable: true),
                    CardSlots = table.Column<double>(type: "double precision", nullable: false),
                    HasRgb = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gpu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gpu_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HardDrive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CapacityGb = table.Column<int>(type: "integer", nullable: false),
                    DriveInterface = table.Column<string>(type: "text", nullable: false),
                    FormFactor = table.Column<string>(type: "text", nullable: false),
                    IsSsd = table.Column<bool>(type: "boolean", nullable: false),
                    ReadSpeedMbS = table.Column<int>(type: "integer", nullable: true),
                    WriteSpeedMbs = table.Column<int>(type: "integer", nullable: true),
                    Rpm = table.Column<int>(type: "integer", nullable: true),
                    CacheMb = table.Column<int>(type: "integer", nullable: true),
                    Tbw = table.Column<int>(type: "integer", nullable: true),
                    PowerWatts = table.Column<double>(type: "double precision", nullable: true),
                    PriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardDrive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardDrive_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motherboard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Socket = table.Column<string>(type: "text", nullable: false),
                    Chipset = table.Column<string>(type: "text", nullable: false),
                    FormFactor = table.Column<string>(type: "text", nullable: false),
                    MemoryType = table.Column<string>(type: "text", nullable: false),
                    MemorySlots = table.Column<int>(type: "integer", nullable: false),
                    MaxMemoryGb = table.Column<int>(type: "integer", nullable: false),
                    MaxMemorySpeedMhz = table.Column<int>(type: "integer", nullable: false),
                    PcieX16Slots = table.Column<int>(type: "integer", nullable: false),
                    PcieX1Slots = table.Column<int>(type: "integer", nullable: false),
                    M2Slots = table.Column<int>(type: "integer", nullable: false),
                    SataPorts = table.Column<int>(type: "integer", nullable: false),
                    UsbHeaders3Gen2 = table.Column<int>(type: "integer", nullable: false),
                    UsbHeaders2Gen0 = table.Column<int>(type: "integer", nullable: false),
                    HasWifi = table.Column<bool>(type: "boolean", nullable: false),
                    HasBluetooth = table.Column<bool>(type: "boolean", nullable: false),
                    LanSpeedGbps = table.Column<int>(type: "integer", nullable: false),
                    FanHeaders = table.Column<int>(type: "integer", nullable: false),
                    ArgbHeaders = table.Column<int>(type: "integer", nullable: false),
                    VrmPhases = table.Column<int>(type: "integer", nullable: true),
                    RearUsbA = table.Column<int>(type: "integer", nullable: false),
                    RearUsbC = table.Column<int>(type: "integer", nullable: false),
                    RearHdmi = table.Column<bool>(type: "boolean", nullable: false),
                    RearDisplayPort = table.Column<bool>(type: "boolean", nullable: false),
                    PriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motherboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motherboard_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcCase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupportedFormFactors = table.Column<int[]>(type: "integer[]", nullable: false),
                    MaxGpuLengthMm = table.Column<int>(type: "integer", nullable: false),
                    MaxCpuCoolerHeightMm = table.Column<int>(type: "integer", nullable: false),
                    MaxPsuLengthMm = table.Column<int>(type: "integer", nullable: false),
                    DriveBays35Inch = table.Column<int>(type: "integer", nullable: false),
                    DriveBays25Inch = table.Column<int>(type: "integer", nullable: false),
                    FrontUsbA = table.Column<int>(type: "integer", nullable: false),
                    FrontUsbC = table.Column<int>(type: "integer", nullable: false),
                    RadiatorSupportMm = table.Column<List<string>>(type: "text[]", nullable: false),
                    CaseWidthMm = table.Column<int>(type: "integer", nullable: true),
                    CaseHeightMm = table.Column<int>(type: "integer", nullable: true),
                    CaseDepthMm = table.Column<int>(type: "integer", nullable: true),
                    HasGlassPanel = table.Column<bool>(type: "boolean", nullable: false),
                    IncludedFans = table.Column<int>(type: "integer", nullable: false),
                    PriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PcCase_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcMonitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScreenSizeInch = table.Column<double>(type: "double precision", nullable: false),
                    ResolutionWidth = table.Column<int>(type: "integer", nullable: false),
                    ResolutionHeight = table.Column<int>(type: "integer", nullable: false),
                    PanelType = table.Column<string>(type: "text", nullable: false),
                    RefreshRateHz = table.Column<int>(type: "integer", nullable: false),
                    ResponseTimeMs = table.Column<double>(type: "double precision", nullable: true),
                    HdrSupport = table.Column<string>(type: "text", nullable: true),
                    BrightnessNits = table.Column<int>(type: "integer", nullable: true),
                    ContrastRatio = table.Column<string>(type: "text", nullable: true),
                    ColorGamutP3 = table.Column<int>(type: "integer", nullable: true),
                    HasGSync = table.Column<bool>(type: "boolean", nullable: false),
                    HasFreeSync = table.Column<bool>(type: "boolean", nullable: false),
                    HasFreeSyncPremium = table.Column<bool>(type: "boolean", nullable: false),
                    HdmiPorts = table.Column<int>(type: "integer", nullable: false),
                    HdmiVersion = table.Column<string>(type: "text", nullable: true),
                    DpPorts = table.Column<int>(type: "integer", nullable: false),
                    DpVersion = table.Column<string>(type: "text", nullable: true),
                    UsbCPorts = table.Column<int>(type: "integer", nullable: false),
                    HasUsbHub = table.Column<bool>(type: "boolean", nullable: false),
                    HasSpeakers = table.Column<bool>(type: "boolean", nullable: false),
                    HeightAdjustable = table.Column<bool>(type: "boolean", nullable: false),
                    VesaMount = table.Column<string>(type: "text", nullable: false),
                    PriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcMonitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PcMonitor_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Psu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Wattage = table.Column<int>(type: "integer", nullable: false),
                    Efficiency = table.Column<string>(type: "text", nullable: false),
                    Modularity = table.Column<string>(type: "text", nullable: false),
                    AtxVersion = table.Column<string>(type: "text", nullable: false),
                    Has16Pin = table.Column<bool>(type: "boolean", nullable: false),
                    EpsConnectors = table.Column<int>(type: "integer", nullable: false),
                    SataConnectors = table.Column<int>(type: "integer", nullable: false),
                    Pcie8PinConnectors = table.Column<int>(type: "integer", nullable: false),
                    FanSizeMm = table.Column<int>(type: "integer", nullable: false),
                    LengthMm = table.Column<int>(type: "integer", nullable: true),
                    PriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Psu_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemoryType = table.Column<string>(type: "text", nullable: false),
                    CapacityGb = table.Column<int>(type: "integer", nullable: false),
                    KitCount = table.Column<int>(type: "integer", nullable: false),
                    SpeedMhz = table.Column<int>(type: "integer", nullable: false),
                    CasLatency = table.Column<int>(type: "integer", nullable: true),
                    Voltage = table.Column<double>(type: "double precision", nullable: true),
                    HasRgb = table.Column<bool>(type: "boolean", nullable: false),
                    HasEcc = table.Column<bool>(type: "boolean", nullable: false),
                    HeightMm = table.Column<int>(type: "integer", nullable: true),
                    PriceUsd = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ram_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Build",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CpuId = table.Column<int>(type: "integer", nullable: true),
                    CpuCoolerId = table.Column<int>(type: "integer", nullable: true),
                    GpuId = table.Column<int>(type: "integer", nullable: true),
                    RamId = table.Column<int>(type: "integer", nullable: true),
                    HardDriveId = table.Column<int>(type: "integer", nullable: true),
                    MotherboardId = table.Column<int>(type: "integer", nullable: true),
                    PsuId = table.Column<int>(type: "integer", nullable: true),
                    CaseId = table.Column<int>(type: "integer", nullable: true),
                    MonitorId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Build", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Build_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Build_CpuCooler_CpuCoolerId",
                        column: x => x.CpuCoolerId,
                        principalTable: "CpuCooler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Build_Cpu_CpuId",
                        column: x => x.CpuId,
                        principalTable: "Cpu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Build_Gpu_GpuId",
                        column: x => x.GpuId,
                        principalTable: "Gpu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Build_HardDrive_HardDriveId",
                        column: x => x.HardDriveId,
                        principalTable: "HardDrive",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Build_Motherboard_MotherboardId",
                        column: x => x.MotherboardId,
                        principalTable: "Motherboard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Build_PcCase_CaseId",
                        column: x => x.CaseId,
                        principalTable: "PcCase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Build_PcMonitor_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "PcMonitor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Build_Psu_PsuId",
                        column: x => x.PsuId,
                        principalTable: "Psu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Build_Ram_RamId",
                        column: x => x.RamId,
                        principalTable: "Ram",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserId",
                table: "Admin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Build_CaseId",
                table: "Build",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_CpuCoolerId",
                table: "Build",
                column: "CpuCoolerId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_CpuId",
                table: "Build",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_GpuId",
                table: "Build",
                column: "GpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_HardDriveId",
                table: "Build",
                column: "HardDriveId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_MonitorId",
                table: "Build",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_MotherboardId",
                table: "Build",
                column: "MotherboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_PsuId",
                table: "Build",
                column: "PsuId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_RamId",
                table: "Build",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_UserId",
                table: "Build",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cpu_BrandId",
                table: "Cpu",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CpuCooler_BrandId",
                table: "CpuCooler",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Gpu_BrandId",
                table: "Gpu",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_HardDrive_BrandId",
                table: "HardDrive",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Motherboard_BrandId",
                table: "Motherboard",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PcCase_BrandId",
                table: "PcCase",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PcMonitor_BrandId",
                table: "PcMonitor",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Psu_BrandId",
                table: "Psu",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Ram_BrandId",
                table: "Ram",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularUser_UserId",
                table: "RegularUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Build");

            migrationBuilder.DropTable(
                name: "RegularUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CpuCooler");

            migrationBuilder.DropTable(
                name: "Cpu");

            migrationBuilder.DropTable(
                name: "Gpu");

            migrationBuilder.DropTable(
                name: "HardDrive");

            migrationBuilder.DropTable(
                name: "Motherboard");

            migrationBuilder.DropTable(
                name: "PcCase");

            migrationBuilder.DropTable(
                name: "PcMonitor");

            migrationBuilder.DropTable(
                name: "Psu");

            migrationBuilder.DropTable(
                name: "Ram");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
