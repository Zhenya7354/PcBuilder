using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PcBuilder.Configurations;
using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Mappers;
using PcBuilder.Models;
using PcBuilder.Repositories;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;

namespace PcBuilder.Services;

public class ComponentCatalogCache(
    IOptions<CacheOptions> options,
    IMemoryCache cache,
    ICpuRepository cpuRepository,
    IGpuRepository gpuRepository,
    IMotherboardRepository motherboardRepository,
    IRamRepository ramRepository,
    IHardDriveRepository hardDriveRepository,
    IPsuRepository psuRepository,
    ICpuCoolerRepository cpuCoolerRepository,
    IPcCaseRepository pcCaseRepository,
    IPcMonitorRepository pcMonitorRepository) : IComponentCatalogCache
{
    private readonly IOptions<CacheOptions> _options = options;
    private readonly IMemoryCache _cache = cache;

    private async Task<List<T>> GetOrLoadAsync<T>(string cacheKey, Func<CancellationToken, Task<List<T>>> loadFunction, CancellationToken cancellationToken)
    {
        var result = await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Value?.AbsoluteExpirationInMinutes ?? 0);
            entry.SlidingExpiration = TimeSpan.FromMinutes(_options.Value?.SlidingExpirationInMinutes ?? 0);
            entry.Size = 1;

            var data = await loadFunction(cancellationToken);

            return data?.ToList() ?? new List<T>();
        });

        return result ?? new List<T>();
    }
    public Task<List<CpuCoolerEntity>> GetAllCpuCoolersAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.CpuCoolersKey, cpuCoolerRepository.GetAllCpuCoolersAsync, cancellationToken);

    public Task<List<CpuEntity>> GetAllCpusAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.CpusKey, cpuRepository.GetAllCpusAsync, cancellationToken);

    public Task<List<GpuEntity>> GetAllGpusAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.GpusKey, gpuRepository.GetAllGpusAsync, cancellationToken);

    public Task<List<PcMonitorEntity>> GetAllMonitorsAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.MonitorsKey, pcMonitorRepository.GetAllMonitorsAsync, cancellationToken);

    public Task<List<MotherboardEntity>> GetAllMotherboardsAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.MotherboardsKey, motherboardRepository.GetAllMotherboardsAsync, cancellationToken);

    public Task<List<PcCaseEntity>> GetAllCasesAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.PcCasesKey, pcCaseRepository.GetAllCasesAsync, cancellationToken);

    public Task<List<PsuEntity>> GetAllPsusAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.PsusKeys, psuRepository.GetAllPsusAsync, cancellationToken);

    public Task<List<RamEntity>> GetAllRamsAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.RamsKey, ramRepository.GetAllRamAsync, cancellationToken);

    public Task<List<HardDriveEntity>> GetAllHardDrivesAsync(CancellationToken cancellationToken) =>
        GetOrLoadAsync(ComponentCacheKeys.HardDrivesKey, hardDriveRepository.GetAllHardDrivesAsync, cancellationToken);

    public void InvalidateAllCaches()
    {
        foreach(var key in ComponentCacheKeys.All)
        {
            _cache.Remove(key);
        }
    }

    public void InvalidateCache(BuildComponentType componentType)
    {
        var cacheKey = CacheKeyToBuildTypeMapper.GetCacheKeyForBuildComponentType(componentType);
        _cache.Remove(cacheKey);
    }
}
