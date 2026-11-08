using NSubstitute;
using PcBuilder.Entities;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Tests;

public class BuildServiceTests
{
    private const string UserId = "user-1";

    private readonly IBuildRepository _buildRepository;
    private readonly ICompatibilityCheckService _compatibilityCheckService;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly BuildService _sut;

    public BuildServiceTests()
    {
        _buildRepository = Substitute.For<IBuildRepository>();
        _compatibilityCheckService = Substitute.For<ICompatibilityCheckService>();
        _userContextAccessor = Substitute.For<IUserContextAccessor>();
        _userContextAccessor.GetUserContext().Returns(new UserContextResponse { UserId = UserId });
        _sut = new BuildService(_buildRepository, _compatibilityCheckService, _userContextAccessor);
    }

    [Fact]
    public async Task UpdateBuildAsync_Should_ClearComponentSlots_WhenRequestContainsNullIds()
    {
        var existing = new BuildEntity
        {
            Id = 1,
            Name = "Gaming Rig",
            UserId = UserId,
            CpuId = 10,
            GpuId = 20,
            RamId = 30,
            MotherboardId = 40
        };

        var request = new BuildRequest
        {
            Name = "Gaming Rig",
            CpuId = 10,
            GpuId = null,
            RamId = 30,
            MotherboardId = null
        };

        var reloaded = new BuildEntity
        {
            Id = 1,
            Name = "Gaming Rig",
            UserId = UserId,
            CpuId = 10,
            GpuId = null,
            RamId = 30,
            MotherboardId = null
        };

        _buildRepository.GetByIdForUpdateAsync(1, UserId, Arg.Any<CancellationToken>())
            .Returns(existing);
        _buildRepository.GetByIdAsync(1, UserId, Arg.Any<CancellationToken>())
            .Returns(reloaded);

        var result = await _sut.UpdateBuildAsync(1, request, CancellationToken.None);

        Assert.Null(existing.GpuId);
        Assert.Null(existing.MotherboardId);
        Assert.Equal(10, existing.CpuId);
        Assert.Equal(30, existing.RamId);
        await _buildRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        Assert.Same(reloaded, result);
    }

    [Fact]
    public async Task UpdateBuildAsync_Should_ReplaceAllSlots_FromRequestBody()
    {
        var existing = new BuildEntity
        {
            Id = 2,
            Name = "Old Name",
            UserId = UserId,
            CpuId = 1,
            GpuId = 2
        };

        var request = new BuildRequest
        {
            Name = null,
            CpuId = 99,
            GpuId = null,
            RamId = 5
        };

        _buildRepository.GetByIdForUpdateAsync(2, UserId, Arg.Any<CancellationToken>())
            .Returns(existing);
        _buildRepository.GetByIdAsync(2, UserId, Arg.Any<CancellationToken>())
            .Returns(existing);

        await _sut.UpdateBuildAsync(2, request, CancellationToken.None);

        Assert.Equal("Old Name", existing.Name);
        Assert.Equal(99, existing.CpuId);
        Assert.Null(existing.GpuId);
        Assert.Equal(5, existing.RamId);
        Assert.Null(existing.CpuCoolerId);
        Assert.Null(existing.HardDriveId);
        Assert.Null(existing.MotherboardId);
        Assert.Null(existing.PsuId);
        Assert.Null(existing.CaseId);
        Assert.Null(existing.MonitorId);
    }

    [Fact]
    public async Task RunCompatibilityChecksForBuildUpdateAsync_Should_UseRequestSlots_NotMergeExisting()
    {
        var existing = new BuildEntity
        {
            Id = 3,
            UserId = UserId,
            CpuId = 1,
            MotherboardId = 2
        };

        var request = new BuildRequest
        {
            CpuId = 1,
            MotherboardId = null
        };

        _buildRepository.GetByIdAsync(3, UserId, Arg.Any<CancellationToken>())
            .Returns(existing);

        _compatibilityCheckService
            .CheckCpuToMotherboardCompatibilityAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(CompatibilityCheckResponse.Success());

        var result = await _sut.RunCompatibilityChecksForBuildUpdateAsync(3, request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        await _compatibilityCheckService.DidNotReceive()
            .CheckCpuToMotherboardCompatibilityAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>());
    }
}
