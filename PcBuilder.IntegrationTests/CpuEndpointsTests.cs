using FluentAssertions;
using PcBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.KeyPerFile;

namespace PcBuilder.IntegrationTests;

public class CpuEndpointsTests : BaseIntegrationTest
{
    public CpuEndpointsTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAllCpusAsync_Should_ReturnOk()
    {
        // Arrange


        // Act

        var response = await HttpClient.GetAsync("/cpus");
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var result = await response.Content.ReadFromJsonAsync<List<CpuEntity>>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        result.Should().NotBeNullOrEmpty();
        result.Count.Should().Be(2);
        result[0].Name.Should().Be("Ryzen 7 9800");
        result[0].Socket.Should().Be(Enums.PcSocketType.AM5);
        result[0].Price.Should().Be(449.99m);
    }
    [Fact]
    public async Task GetCpuByIdAsync_Should_ReturnOk()
    {
        // Arrange


        // Act

        var response = await HttpClient.GetAsync("/cpus/1");
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var result = await response.Content.ReadFromJsonAsync<CpuEntity>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Ryzen 7 9800");
        result.Socket.Should().Be(Enums.PcSocketType.AM5); 
        result.Price.Should().Be(449.99m);

    }
[Fact]
    public async Task GetCpuByIdAsync_Should_ThrowKeyNotFoundException_WhenPassedInvalidId()
    {
        // Arrange
        int cpuId = 999;

        // Act

        var response = await HttpClient.GetAsync($"/cpus/{cpuId}");
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        problem.Title.Should().Be("Resource not found");
        problem.Detail.Should().Contain("Cpu not found");
    }
[Fact]
    public async Task AddCpuAsync_Should_ReturnOK()
    {
        // Arrange
        var cpuTest = new CpuEntity()
        {
            BrandId = 1,
            Name = "Test Cpu",
            Socket = Enums.PcSocketType.AM5,
            Price = 100.00m
        };
        var serializedCpu = new StringContent(JsonSerializer.Serialize(cpuTest),
            Encoding.UTF8, "application/json");
        // Act

        var response = await HttpClient.PostAsync("/cpus", serializedCpu);
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var result = await response.Content.ReadFromJsonAsync<CpuEntity>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        result.Name.Should().Be("Test Cpu");
        result.Socket.Should().Be(Enums.PcSocketType.AM5);
        result.Price.Should().Be(100.00m);
    }
[Fact]
    public async Task AddCpuAsync_Should_ThrowNotFoundException_WhenBrandInCpuDoesNotExists()
    {
        // Arrange
        var cpu = new CpuEntity()
        {
            Name = "CpuTest",
            BrandId = 999 
        };
        var serializedCpu = new StringContent(JsonSerializer.Serialize(cpu),
            Encoding.UTF8, "application/json");
        // Act

        var response = await HttpClient.PostAsync("/cpus", serializedCpu);
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        problem.Title.Should().Be("Resource not found");
        problem.Detail.Should().Contain("Brand not found");
        
    }
[Fact]
    public async Task UpdateCpuAsync_Should_ReturnOk()
    {
        // Arrange
        int cpuId = 1;
        var cpu = new CpuEntity()
        {
            Name = "Updated Cpu",
            BrandId = 1
        };
        var serializedCpu = new StringContent(JsonSerializer.Serialize(cpu),
            Encoding.UTF8, "application/json");
        // Act

        var response = await HttpClient.PutAsync($"/cpus/{cpuId}", serializedCpu);
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var result = await response.Content.ReadFromJsonAsync<CpuEntity>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        result.Name.Should().Be("Updated Cpu");
    }
[Fact]
    public async Task UpdateCpuAsync_Should_ThrowNotFoundException_WhenInvalidIdPassed()
    {
        // Arrange
        int cpuId = 999;
        var cpu = new CpuEntity()
        {
            Name = "Updated Cpu",
            BrandId = 1
        };
        var serializedCpu = new StringContent(JsonSerializer.Serialize(cpu),
            Encoding.UTF8, "application/json");
        // Act

        var response = await HttpClient.PutAsync($"/cpus/{cpuId}", serializedCpu);
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        problem.Title.Should().Be("Resource not found");
        problem.Detail.Should().Contain("Cpu not found");
    }
[Fact]
    public async Task UpdateCpuAsync_Should_ThrowNotFoundException_WhenBrandDoesNotExists()
    {
        // Arrange
        int cpuId = 1;
        var cpu = new CpuEntity()
        {
            Name = "Updated Cpu",
            BrandId = 999
        };
        var serializedCpu = new StringContent(JsonSerializer.Serialize(cpu),
            Encoding.UTF8, "application/json");
        // Act

        var response = await HttpClient.PutAsync($"/cpus/{cpuId}", serializedCpu);
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        problem.Title.Should().Be("Resource not found");
        problem.Detail.Should().Contain("Brand with the specified ID does not exist.");
    }

    [Fact]
    public async Task DeleteCpuAsync_Should_ReturnOk()
    {
        // Arrange
        int cpuId = 2;
        // Act

        var response = await HttpClient.DeleteAsync($"/cpus/{cpuId}");
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var result = await response.Content.ReadFromJsonAsync<string>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        result.Should().Contain($"Cpu with id {cpuId} deleted successfully");
    }
    [Fact]
    public async Task DeleteCpuAsync_Should_ThrowNotFoundException_WhenInvalidIdPassed()
    {
        // Arrange
        int cpuId = 999;
        // Act

        var response = await HttpClient.DeleteAsync($"/cpus/{cpuId}");
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        jsonOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        problem.Title.Should().Be("Resource not found");
        problem.Detail.Should().Contain("Cpu not found");
    }
}
