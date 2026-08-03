using NSubstitute;
using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace PcBuilder.Tests;

public class CompatibilityCheckServiceTests
{
    private readonly ICompatibilityCheckRepository _compatibilityCheckRepositoryMock;
    private readonly CompatibilityCheckService _compatibilityCheckService;

    public CompatibilityCheckServiceTests()
    {
        _compatibilityCheckRepositoryMock = Substitute.For<ICompatibilityCheckRepository>();
        _compatibilityCheckService = new CompatibilityCheckService(_compatibilityCheckRepositoryMock);
    }

    [Fact]
    public async Task CheckCpuToMotherboardCompatibilityAsync_Should_ReturnIssues_WhenSocketsIncompatible()
    {
        // Arrange
        
        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 149
                
            });
        
        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity 
            {
                Socket = Enums.PcSocketType.AM4,
                Chipset = "X570",
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 150
                
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuToMotherboardCompatibilityAsync(1,2,default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(CpuEntity.Socket) &&
        issue.Severity == Enums.CompatibilitySeverity.Error &&
        issue.Message.Contains("socket"));
    }
    [Fact]
    public async Task CheckCpuToMotherboardCompatibilityAsync_Should_ReturnIssues_WhenChipsetsIncompatible()
    {
        // Arrange
        
        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 149
                
            });
        
        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                Chipset = "",
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 150
                
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuToMotherboardCompatibilityAsync(1,2,default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(MotherboardEntity.Chipset) &&
        issue.Severity == Enums.CompatibilitySeverity.Error &&
        issue.Message.Contains("chipset"));
    }
    [Fact]
    public async Task CheckCpuToMotherboardCompatibilityAsync_Should_ReturnIssues_WhenMemoryTypesIncompatible()
    {
        // Arrange
        
        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 149
                
            });
        
        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                Chipset = "X570",
                MemoryType = Enums.MemoryType.DDR4,
                MaxMemorySpeedMhz = 150
                
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuToMotherboardCompatibilityAsync(1,2,default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(MotherboardEntity.MemoryType) &&
        issue.Severity == Enums.CompatibilitySeverity.Error &&
        issue.Message.Contains("memory type"));
    }
    [Fact]
    public async Task CheckCpuToMotherboardCompatibilityAsync_Should_ReturnIssues_WhenMemoryMaxSpeedIncompatible()
    {
        // Arrange
        
        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 160
                
            });
        
        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                Chipset = "X570",
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 150
                
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuToMotherboardCompatibilityAsync(1,2,default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(MotherboardEntity.MaxMemorySpeedMhz) &&
        issue.Severity == Enums.CompatibilitySeverity.Warning &&
        issue.Message.Contains("memory speed"));
    }
    [Fact]
    public async Task CheckCpuToMotherboardCompatibilityAsync_Should_ReturnSuccess()
    {
        // Arrange
        
        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 149
                
            });
        
        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                Chipset = "X570",
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 150
                
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuToMotherboardCompatibilityAsync(1,2,default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);

    }
    [Fact]
    public async Task CheckCpuCoolerToCpuCompatibilityAsync_Should_ReturnListOfIssues_WhenSocketsIncompatible()
    {
        // Arrange
        
        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 149,
                TdpWatts = 105
            });
        
        _compatibilityCheckRepositoryMock.GetCpuCoolerByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuCoolerEntity 
            {
                SocketsSupported = new List<Enums.PcSocketType> { Enums.PcSocketType.AM4 },
                MaxTdpWatts = 110
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuCoolerToCpuCompatibilityAsync(1,2,default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(CpuEntity.Socket) &&
        issue.Severity == Enums.CompatibilitySeverity.Error &&
        issue.Message.Contains("socket"));
    }
    [Fact]
    public async Task CheckCpuCoolerToCpuCompatibilityAsync_Should_ReturnListOfIssues_WhenTdpWattsIncompatible()
    {
        // Arrange
        
        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 149,
                TdpWatts = 105
            });
        
        _compatibilityCheckRepositoryMock.GetCpuCoolerByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuCoolerEntity 
            {
                SocketsSupported = new List<Enums.PcSocketType> { Enums.PcSocketType.AM5 },
                MaxTdpWatts = 95
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuCoolerToCpuCompatibilityAsync(1,2,default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(CpuEntity.TdpWatts) &&
        issue.Severity == Enums.CompatibilitySeverity.Warning);
    }
    [Fact]
    public async Task CheckCpuCoolerToCpuCompatibilityAsync_Should_ReturnSuccess()
    {
        // Arrange
        
        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity 
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 149,
                TdpWatts = 105
            });
        
        _compatibilityCheckRepositoryMock.GetCpuCoolerByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuCoolerEntity 
            {
                SocketsSupported = new List<Enums.PcSocketType> { Enums.PcSocketType.AM5 },
                MaxTdpWatts = 110
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuCoolerToCpuCompatibilityAsync(1,2,default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);
    }

    [Fact]
    public async Task CheckCpuToRamCompatibilityAsync_Should_ReturnWarning_WhenMaxMemorySpeedIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1599,
                TdpWatts = 105,
                MemoryChannels = 2
            });

        _compatibilityCheckRepositoryMock.GetRamByIdAsync(Arg.Any<int>(), default)
            .Returns(new RamEntity
            {
                SpeedMhz = 1600,
                KitCount = 2
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuToRamCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(CpuEntity.MaxMemorySpeedMhz) &&
        issue.Severity == Enums.CompatibilitySeverity.Warning &&
        issue.Message.Contains("memory speed"));
    }
    [Fact]
    public async Task CheckCpuToRamCompatibilityAsync_Should_ReturnWarning_WhenMemoryChanelsIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1601,
                TdpWatts = 105,
                MemoryChannels = 3
            });

        _compatibilityCheckRepositoryMock.GetRamByIdAsync(Arg.Any<int>(), default)
            .Returns(new RamEntity
            {
                SpeedMhz = 1600,
                KitCount = 2
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuToRamCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(CpuEntity.MemoryChannels) &&
        issue.Severity == Enums.CompatibilitySeverity.Warning &&
        issue.Message.Contains("memory channels"));
    }
    [Fact]
    public async Task CheckCpuToRamCompatibilityAsync_Should_ReturnSuccess()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetCpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuEntity
            {
                Socket = Enums.PcSocketType.AM5,
                ChipsetsSupported = new List<string> { "X570" },
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1601,
                TdpWatts = 105,
                MemoryChannels = 2
            });

        _compatibilityCheckRepositoryMock.GetRamByIdAsync(Arg.Any<int>(), default)
            .Returns(new RamEntity
            {
                SpeedMhz = 1600,
                KitCount = 2
            });
        // Act

        var result = await _compatibilityCheckService.CheckCpuToRamCompatibilityAsync(1, 2, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);
    }

    [Fact]
    public async Task CheckRamToMotherboardCompatibilityAsync_Should_ReturnError_WhenMemoryTypeIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity
            {
                Socket = Enums.PcSocketType.AM5,
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1601,
                MemorySlots = 4
            });

        _compatibilityCheckRepositoryMock.GetRamByIdAsync(Arg.Any<int>(), default)
            .Returns(new RamEntity
            {
                MemoryType = Enums.MemoryType.DDR4,
                SpeedMhz = 1600,
                KitCount = 2
            });
        // Act

        var result = await _compatibilityCheckService.CheckRamToMotherboardCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(MotherboardEntity.MemoryType) &&
        issue.Severity == Enums.CompatibilitySeverity.Error &&
        issue.Message.Contains("memory type"));
    }
    [Fact]
    public async Task CheckRamToMotherboardCompatibilityAsync_Should_ReturnWarning_WhenMemorySpeedIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity
            {
                Socket = Enums.PcSocketType.AM5,
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1599,
                MemorySlots = 4
            });

        _compatibilityCheckRepositoryMock.GetRamByIdAsync(Arg.Any<int>(), default)
            .Returns(new RamEntity
            {
                MemoryType = Enums.MemoryType.DDR5,
                SpeedMhz = 1600,
                KitCount = 2
            });
        // Act

        var result = await _compatibilityCheckService.CheckRamToMotherboardCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(RamEntity.SpeedMhz) &&
        issue.Severity == Enums.CompatibilitySeverity.Warning);
    }
    [Fact]
    public async Task CheckRamToMotherboardCompatibilityAsync_Should_ReturnWarning_WhenMemorySlotsIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity
            {
                Socket = Enums.PcSocketType.AM5,
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1599,
                MemorySlots = 1
            });

        _compatibilityCheckRepositoryMock.GetRamByIdAsync(Arg.Any<int>(), default)
            .Returns(new RamEntity
            {
                MemoryType = Enums.MemoryType.DDR5,
                SpeedMhz = 1600,
                KitCount = 2
            });
        // Act

        var result = await _compatibilityCheckService.CheckRamToMotherboardCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(RamEntity.KitCount) &&
        issue.Severity == Enums.CompatibilitySeverity.Warning);
    }
    [Fact]
    public async Task CheckRamToMotherboardCompatibilityAsync_Should_ReturnWarning_WhenMaxMemoryIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity
            {
                Socket = Enums.PcSocketType.AM5,
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1599,
                MemorySlots = 2,
                MaxMemoryGb = 8
            });

        _compatibilityCheckRepositoryMock.GetRamByIdAsync(Arg.Any<int>(), default)
            .Returns(new RamEntity
            {
                MemoryType = Enums.MemoryType.DDR5,
                SpeedMhz = 1600,
                KitCount = 2,
                CapacityGb = 8
            });
        // Act

        var result = await _compatibilityCheckService.CheckRamToMotherboardCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(RamEntity.CapacityGb) &&
        issue.Severity == Enums.CompatibilitySeverity.Warning);
    }
    [Fact]
    public async Task CheckRamToMotherboardCompatibilityAsync_Should_Success()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity
            {
                Socket = Enums.PcSocketType.AM5,
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1601,
                MemorySlots = 2,
                MaxMemoryGb = 32
            });

        _compatibilityCheckRepositoryMock.GetRamByIdAsync(Arg.Any<int>(), default)
            .Returns(new RamEntity
            {
                MemoryType = Enums.MemoryType.DDR5,
                SpeedMhz = 1600,
                KitCount = 2,
                CapacityGb = 8
            });
        // Act

        var result = await _compatibilityCheckService.CheckRamToMotherboardCompatibilityAsync(1, 2, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);
    }
    [Fact]
    public async Task CheckCaseToMotherboardCompatibilityAsync_Should_ReturnError_WhenFormFactorIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity
            {
                Socket = Enums.PcSocketType.AM5,
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1599,
                MemorySlots = 2,
                MaxMemoryGb = 8,
                FormFactor = FormFactor.MiniITX
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX }
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToMotherboardCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(MotherboardEntity.FormFactor) &&
        issue.Severity == Enums.CompatibilitySeverity.Error);
    }
    [Fact]
    public async Task CheckCaseToMotherboardCompatibilityAsync_Should_ReturnSuccess()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetMotherboardByIdAsync(Arg.Any<int>(), default)
            .Returns(new MotherboardEntity
            {
                Socket = Enums.PcSocketType.AM5,
                MemoryType = Enums.MemoryType.DDR5,
                MaxMemorySpeedMhz = 1599,
                MemorySlots = 2,
                MaxMemoryGb = 8,
                FormFactor = FormFactor.EATX
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX }
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToMotherboardCompatibilityAsync(1, 2, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);
    }
    [Fact]
    public async Task CheckCaseToCpuCoolerCompatibilityAsync_Should_ReturnError_WhenHeightIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetCpuCoolerByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuCoolerEntity
            {
                HeightMm = 160
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX },
                MaxCpuCoolerHeightMm = 150
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToCpuCoolerCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(CpuCoolerEntity.HeightMm) &&
        issue.Severity == Enums.CompatibilitySeverity.Error);
    }
    [Fact]
    public async Task CheckCaseToCpuCoolerCompatibilityAsync_Should_ReturnError_WhenRadiatorSizeForLiquidCoolingIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetCpuCoolerByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuCoolerEntity
            {
                HeightMm = 160,
                CoolerType = CoolerType.Liquid,
                RadiatorSizeMm = 360
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX },
                CaseHeightMm = 150,
                RadiatorSupportMm = new List<string> { "240", "280" }
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToCpuCoolerCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(CpuCoolerEntity.RadiatorSizeMm) &&
        issue.Severity == Enums.CompatibilitySeverity.Error);
    }
    [Fact]
    public async Task CheckCaseToCpuCoolerCompatibilityAsync_Should_ReturnSuccess()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetCpuCoolerByIdAsync(Arg.Any<int>(), default)
            .Returns(new CpuCoolerEntity
            {
                HeightMm = 160,
                CoolerType = CoolerType.Liquid,
                RadiatorSizeMm = 280
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX },
                CaseHeightMm = 170,
                RadiatorSupportMm = new List<string> { "240", "280" },
                MaxCpuCoolerHeightMm = 170
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToCpuCoolerCompatibilityAsync(1, 2, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);
    }
    [Fact]
    public async Task CheckCaseToGpuCompatibilityAsync_Should_ReturnError_WhenGpuLengthIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetGpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new GpuEntity
            {
                CardLengthMm = 350
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX },
                CaseHeightMm = 150,
                RadiatorSupportMm = new List<string> { "240", "280" },
                MaxGpuLengthMm = 300
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToGpuCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(GpuEntity.CardLengthMm) &&
        issue.Severity == Enums.CompatibilitySeverity.Error);
    }
    [Fact]
    public async Task CheckCaseToGpuCompatibilityAsync_Should_ReturnSuccess()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetGpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new GpuEntity
            {
                CardLengthMm = 350
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX },
                CaseHeightMm = 150,
                RadiatorSupportMm = new List<string> { "240", "280" },
                MaxGpuLengthMm = 370
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToGpuCompatibilityAsync(1, 2, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);
    }
    [Fact]
    public async Task CheckCaseToPsuCompatibilityAsync_Should_ReturnError_WhenPsuLengthIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetPsuByIdAsync(Arg.Any<int>(), default)
            .Returns(new PsuEntity
            {
                LengthMm = 350
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX },
                CaseHeightMm = 150,
                RadiatorSupportMm = new List<string> { "240", "280" },
                MaxGpuLengthMm = 300,
                MaxPsuLengthMm = 300
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToPsuCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(PsuEntity.LengthMm) &&
        issue.Severity == Enums.CompatibilitySeverity.Error);
    }
    [Fact]
    public async Task CheckCaseToPsuCompatibilityAsync_Should_ReturnSuccess()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetPsuByIdAsync(Arg.Any<int>(), default)
            .Returns(new PsuEntity
            {
                LengthMm = 350
            });

        _compatibilityCheckRepositoryMock.GetCaseByIdAsync(Arg.Any<int>(), default)
            .Returns(new PcCaseEntity
            {
                SupportedFormFactors = new List<FormFactor> { FormFactor.EATX },
                CaseHeightMm = 150,
                RadiatorSupportMm = new List<string> { "240", "280" },
                MaxGpuLengthMm = 300,
                MaxPsuLengthMm = 370
            });
        // Act

        var result = await _compatibilityCheckService.CheckCaseToPsuCompatibilityAsync(1, 2, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);
    }
    [Fact]
    public async Task CheckPsuToGpuCompatibilityAsync_Should_ReturnError_WhenPsuWattageIncompatible()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetPsuByIdAsync(Arg.Any<int>(), default)
            .Returns(new PsuEntity
            {
                LengthMm = 350,
                Wattage = 600
            });

        _compatibilityCheckRepositoryMock.GetGpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new GpuEntity
            {
                RecommendedPsuWattage = 750
            });
        // Act

        var result = await _compatibilityCheckService.CheckPsuToGpuCompatibilityAsync(1, 2, default);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Issues);
        Assert.Contains(result.Issues, issue =>
        issue.Field == nameof(PsuEntity.Wattage) &&
        issue.Severity == CompatibilitySeverity.Error);
    }
    [Fact]
    public async Task CheckPsuToGpuCompatibilityAsync_Should_ReturnSuccess()
    {
        // Arrange

        _compatibilityCheckRepositoryMock.GetPsuByIdAsync(Arg.Any<int>(), default)
            .Returns(new PsuEntity
            {
                LengthMm = 350,
                Wattage = 600
            });

        _compatibilityCheckRepositoryMock.GetGpuByIdAsync(Arg.Any<int>(), default)
            .Returns(new GpuEntity
            {
                RecommendedPsuWattage = 500
            });
        // Act

        var result = await _compatibilityCheckService.CheckPsuToGpuCompatibilityAsync(1, 2, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Issues);
    }

}
