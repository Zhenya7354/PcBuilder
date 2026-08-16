using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PcBuilder.Entities;
using PcBuilder.Enums;
using PcBuilder.Models;
using PcBuilder.Repositories.Interfaces;
using PcBuilder.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PcBuilder.IntegrationTests;

public class AiBuildEndpointsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private const string Prompt = "I want gaming pc within 1500 dollars";
    private const string Uri = "/builds/ai/recommend";
    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnOk()
    {
        // Arrange
        AiBuildRequest request = new AiBuildRequest
        {
            Prompt = Prompt,
            Name = "TestGamingPc",
            AcceptAiSuggestedName = false
        };
        var serializedPrompt = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        // Act
        var response = await HttpClient.PostAsync(Uri, serializedPrompt);
        var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Build.Name.Should().Be("TestGamingPc");
        result.Notes.Count.Should().Be(0);
        result.Status.Should().Be(BuildRecommendationStatus.Completed);
        result.Build.Should().NotBeNull();
        result.Build.CpuId.Should().NotBeNull();
        result.Build.GpuId.Should().NotBeNull();
        result.Build.RamId.Should().NotBeNull();
        result.Build.HardDriveId.Should().NotBeNull();
        result.Build.PsuId.Should().NotBeNull();
        result.Build.CpuCoolerId.Should().NotBeNull();
        result.Build.CaseId.Should().NotBeNull();
        result.Build.MotherboardId.Should().NotBeNull();
        result.Build.MonitorId.Should().BeNull();
    }
    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnUnauthorized()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var testHandlerDescriptor = services.SingleOrDefault(d =>
                    d.ImplementationType == typeof(AlwaysAuthorizedAuthHandler) ||
                    (d.ServiceType == typeof(IAuthenticationHandler) && d.ImplementationType == typeof(AlwaysAuthorizedAuthHandler))
                );
                if (testHandlerDescriptor is not null) services.Remove(testHandlerDescriptor);
                services.PostConfigure<AuthenticationOptions>(opts =>
                {
                    opts.DefaultAuthenticateScheme = "NonExistingScheme";
                    opts.DefaultChallengeScheme = "NonExistingScheme";
                });
                services.AddAuthentication("NonExistingScheme")
                    .AddScheme<AuthenticationSchemeOptions, AlwaysUnauthorizedHandler>(
                        "NonExistingScheme",
                        options => { });
            });
        });
        using var client = tempFactory.CreateClient();
        AiBuildRequest request = new AiBuildRequest
        {
            Prompt = Prompt,
            Name = "TestGamingPc",
            AcceptAiSuggestedName = false
        };

        var serializedPrompt = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        // Act
        var response = await client.PostAsync(Uri, serializedPrompt);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        response.IsSuccessStatusCode.Should().BeFalse();
    }

    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnFirstCpuWhen_SuitableCpuNotFound()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(_ => { });

        using (var scope = tempFactory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
            try
            {
                db.Cpu.RemoveRange(db.Cpu);
                await db.SaveChangesAsync();

                var brand = await db.Brand.FirstAsync();

                List<CpuEntity> incompatibleCpus = new List<CpuEntity>
                {
                    new CpuEntity
                    {
                        BrandId = brand.Id,
                        Name = "FirstCpu",
                        Price = 1000.00m,
                        Socket = PcSocketType.AM4,
                        ChipsetsSupported = new List<string> { "B550", "X570" },
                        MemoryType = MemoryType.DDR4
                    },
                    new CpuEntity
                    {
                        BrandId = brand.Id,
                        Name = "SecondCpu",
                        Price = 1000.00m,

                    }
                };

                db.Cpu.AddRange(incompatibleCpus);
                await db.SaveChangesAsync();

                // Act
                var request = new AiBuildRequest
                {
                    Prompt = Prompt,
                    Name = "TestGamingPc",
                    AcceptAiSuggestedName = false
                };
                var serialized = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                using var client = tempFactory.CreateClient();
                var response = await client.PostAsync(Uri, serialized);
                var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Should().NotBeNull();
                result.Notes.Count.Should().Be(0);
                result.Status.Should().Be(BuildRecommendationStatus.Completed);
                result.Build.CpuId.Should().Be(incompatibleCpus.First().Id);
            }
            finally
            {
                var seeder = new TestDataSeeder(db);
                await seeder.SeedAsync();
            }
        }
    }

    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnFailedWhen_MotherboardIncompatible()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(_ => { });

        using (var scope = tempFactory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
            try
            {
                db.Motherboard.RemoveRange(db.Motherboard);
                await db.SaveChangesAsync();

                var brand = await db.Brand.FirstAsync();

                var incompatibleMb = new MotherboardEntity
                {
                    BrandId = brand.Id,
                    Name = "Incompatible MB",
                    Price = 100.00m,
                    Socket = Enums.PcSocketType.LGA1700,
                    Chipset = "Z790",
                    FormFactor = Enums.FormFactor.EATX,
                    MemoryType = Enums.MemoryType.DDR5,
                    MemorySlots = 4,
                    M2Slots = 1,
                    MaxMemoryGb = 128
                };

                db.Motherboard.Add(incompatibleMb);
                await db.SaveChangesAsync();

                // Act
                var request = new AiBuildRequest
                {
                    Prompt = Prompt,
                    Name = "TestGamingPc",
                    AcceptAiSuggestedName = false
                };
                var serialized = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                using var client = tempFactory.CreateClient();
                var response = await client.PostAsync(Uri, serialized);
                var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Should().NotBeNull();
                result.Status.Should().Be(BuildRecommendationStatus.Failed);
                result.Notes.Count.Should().Be(1);
                result.Notes.Should().Contain(n => n.Contains("No motherboard compatible with"));
            }
            finally
            {
                var seeder = new TestDataSeeder(db);
                await seeder.SeedAsync();
            }
        }
    }
    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnFailedWhen_RamIncompatibleWithMotherboard()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(_ => { });

        using (var scope = tempFactory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
            try
            {
                db.Ram.RemoveRange(db.Ram);
                await db.SaveChangesAsync();

                var brand = await db.Brand.FirstAsync();

                var incompatibleRam = new RamEntity
                {
                    BrandId = brand.Id,
                    Name = "Incompatible RAM",
                    Price = 100.00m,
                    MemoryType = Enums.MemoryType.Mixed,

                };

                db.Ram.Add(incompatibleRam);
                await db.SaveChangesAsync();

                // Act
                var request = new AiBuildRequest
                {
                    Prompt = Prompt,
                    Name = "TestGamingPc",
                    AcceptAiSuggestedName = false
                };
                var serialized = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                using var client = tempFactory.CreateClient();
                var response = await client.PostAsync(Uri, serialized);
                var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Should().NotBeNull();
                result.Status.Should().Be(BuildRecommendationStatus.Failed);
                result.Notes.Count.Should().Be(1);
                result.Notes.Should().Contain(n => n.Contains("No RAM compatible with"));
            }
            finally
            {
                var seeder = new TestDataSeeder(db);
                await seeder.SeedAsync();
            }
        }
    }
    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnFirstGpuWhen_SuitableGpuNotFound()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(_ => { });

        using (var scope = tempFactory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
            try
            {
                db.Gpu.RemoveRange(db.Gpu);
                await db.SaveChangesAsync();

                var brand = await db.Brand.FirstAsync();

                List<GpuEntity> incompatibleGpus = new List<GpuEntity>
                {
                    new GpuEntity
                    {
                        BrandId = brand.Id,
                        Name = "FirstGpu",
                        Price = 1000.00m,

                    },
                    new GpuEntity
                    {
                        BrandId = brand.Id,
                        Name = "SecondGpu",
                        Price = 1000.00m,

                    }
                };

                db.Gpu.AddRange(incompatibleGpus);
                await db.SaveChangesAsync();

                // Act
                var request = new AiBuildRequest
                {
                    Prompt = Prompt,
                    Name = "TestGamingPc",
                    AcceptAiSuggestedName = false
                };
                var serialized = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                using var client = tempFactory.CreateClient();
                var response = await client.PostAsync(Uri, serialized);
                var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Should().NotBeNull();
                result.Notes.Count.Should().Be(0);
                result.Status.Should().Be(BuildRecommendationStatus.Completed);
                result.Build.GpuId.Should().Be(incompatibleGpus.First().Id);
            }
            finally
            {
                var seeder = new TestDataSeeder(db);
                await seeder.SeedAsync();
            }
        }
    }
    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnFailedWhen_PsuIncompatibleWithGpu()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(_ => { });

        using (var scope = tempFactory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
            try
            {
                db.Psu.RemoveRange(db.Psu);
                await db.SaveChangesAsync();

                var brand = await db.Brand.FirstAsync();

                var incompatiblePsu = new PsuEntity
                {
                    BrandId = brand.Id,
                    Name = "Incompatible PSU",
                    Price = 100.00m,
                    Wattage = 50,

                };

                db.Psu.Add(incompatiblePsu);
                await db.SaveChangesAsync();

                // Act
                var request = new AiBuildRequest
                {
                    Prompt = Prompt,
                    Name = "TestGamingPc",
                    AcceptAiSuggestedName = false
                };
                var serialized = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                using var client = tempFactory.CreateClient();
                var response = await client.PostAsync(Uri, serialized);
                var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Should().NotBeNull();
                result.Status.Should().Be(BuildRecommendationStatus.Failed);
                result.Notes.Should().Contain(n => n.Contains("No PSU rated for"));
            }
            finally
            {
                var seeder = new TestDataSeeder(db);
                await seeder.SeedAsync();
            }
        }
    }
    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnFailedWhen_CaseIncompatibleWithGpu()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(_ => { });

        using (var scope = tempFactory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
            try
            {
                db.PcCase.RemoveRange(db.PcCase);
                await db.SaveChangesAsync();

                var brand = await db.Brand.FirstAsync();

                var incompatibleCase = new PcCaseEntity
                {
                    BrandId = brand.Id,
                    Name = "Incompatible Case",
                    Price = 100.00m,
                    MaxGpuLengthMm = 1,
                };

                db.PcCase.Add(incompatibleCase);
                await db.SaveChangesAsync();

                // Act
                var request = new AiBuildRequest
                {
                    Prompt = Prompt,
                    Name = "TestGamingPc",
                    AcceptAiSuggestedName = false
                };
                var serialized = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                using var client = tempFactory.CreateClient();
                var response = await client.PostAsync(Uri, serialized);
                var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Should().NotBeNull();
                result.Status.Should().Be(BuildRecommendationStatus.Failed);
                result.Notes.Should().Contain(n => n.Contains("No case fits the selected motherboard/GPU/PSU combination within budget."));
            }
            finally
            {
                var seeder = new TestDataSeeder(db);
                await seeder.SeedAsync();
            }
        }
    }
    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnFailedWhen_CpuCoolerIncompatibleWithCpu()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(_ => { });

        using (var scope = tempFactory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
            try
            {
                db.CpuCooler.RemoveRange(db.CpuCooler);
                await db.SaveChangesAsync();

                var brand = await db.Brand.FirstAsync();

                var incompatibleCpuCooler = new CpuCoolerEntity
                {
                    BrandId = brand.Id,
                    Name = "Incompatible CpuCooler",
                    Price = 100.00m,
                    SocketsSupported = new List<Enums.PcSocketType> { Enums.PcSocketType.LGA1200 }
                };

                db.CpuCooler.Add(incompatibleCpuCooler);
                await db.SaveChangesAsync();

                // Act
                var request = new AiBuildRequest
                {
                    Prompt = Prompt,
                    Name = "TestGamingPc",
                    AcceptAiSuggestedName = false
                };
                var serialized = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                using var client = tempFactory.CreateClient();
                var response = await client.PostAsync(Uri, serialized);
                var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Should().NotBeNull();
                result.Status.Should().Be(BuildRecommendationStatus.Failed);
                result.Notes.Should().Contain(n =>
                n.Contains("No CPU cooler fits the selected CPU/case combination within budget."));
            }
            finally
            {
                var seeder = new TestDataSeeder(db);
                await seeder.SeedAsync();
            }
        }
    }
    [Fact]
    public async Task RecommendBuildAsync_Should_ReturnFirstHardDriveWhen_SuitableDriveNotFound()
    {
        // Arrange
        await using var tempFactory = factory.WithWebHostBuilder(_ => { });

        using (var scope = tempFactory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PcDbContext>();
            try
            {
                db.HardDrive.RemoveRange(db.HardDrive);
                await db.SaveChangesAsync();

                var brand = await db.Brand.FirstAsync();

                List<HardDriveEntity> incompatibleDrives = new List<HardDriveEntity>
                {
                    new HardDriveEntity
                    {
                        BrandId = brand.Id,
                        Name = "FirstHardDrive",
                        Price = 1000.00m,
                        CapacityGb = 500
                    },
                    new HardDriveEntity
                    {
                        BrandId = brand.Id,
                        Name = "SecondHardDrive",
                        Price = 1000.00m,
                        CapacityGb = 1000
                    }
                };

                db.HardDrive.AddRange(incompatibleDrives);
                await db.SaveChangesAsync();

                // Act
                var request = new AiBuildRequest
                {
                    Prompt = Prompt,
                    Name = "TestGamingPc",
                    AcceptAiSuggestedName = false
                };
                var serialized = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var jsonOptions = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                using var client = tempFactory.CreateClient();
                var response = await client.PostAsync(Uri, serialized);
                var result = await response.Content.ReadFromJsonAsync<BuildRecommendationResult>(jsonOptions);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Should().NotBeNull();
                result.Notes.Count.Should().Be(0);
                result.Status.Should().Be(BuildRecommendationStatus.Completed);
                result.Build.HardDriveId.Should().Be(incompatibleDrives.First().Id);
            }
            finally
            {
                var seeder = new TestDataSeeder(db);
                await seeder.SeedAsync();
            }


        }
    }
}



