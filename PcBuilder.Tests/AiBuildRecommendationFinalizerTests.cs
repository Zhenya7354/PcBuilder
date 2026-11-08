using PcBuilder.Enums;
using PcBuilder.Models;
using PcBuilder.Services;

namespace PcBuilder.Tests;

public class AiBuildRecommendationFinalizerTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ResolveRequestedCurrency_Should_DefaultToUsd_AndNote_WhenCurrencyMissing(string? currency)
    {
        var notes = new List<string>();

        var resolved = AiBuildRecommendationFinalizer.ResolveRequestedCurrency(currency, notes);

        Assert.Equal(Currency.USD, resolved);
        Assert.Equal(
            [AiBuildRecommendationFinalizer.CurrencyNotProvidedNote],
            notes);
    }

    [Theory]
    [InlineData("USD", Currency.USD)]
    [InlineData("eur", Currency.EUR)]
    [InlineData("Gbp", Currency.GBP)]
    public void ResolveRequestedCurrency_Should_ParseValidCurrency_WithoutNotes(string currency, Currency expected)
    {
        var notes = new List<string>();

        var resolved = AiBuildRecommendationFinalizer.ResolveRequestedCurrency(currency, notes);

        Assert.Equal(expected, resolved);
        Assert.Empty(notes);
    }

    [Fact]
    public void ResolveRequestedCurrency_Should_DefaultToUsd_AndNote_WhenCurrencyUnrecognized()
    {
        var notes = new List<string>();

        var resolved = AiBuildRecommendationFinalizer.ResolveRequestedCurrency("BLAH", notes);

        Assert.Equal(Currency.USD, resolved);
        Assert.Equal(
            [AiBuildRecommendationFinalizer.CurrencyNotRecognizedNote],
            notes);
    }

    [Fact]
    public void FinalizeStatus_Should_SetCompleted_WhenOnlyCurrencyNotProvidedNote()
    {
        var result = new BuildRecommendationResult
        {
            Notes = [AiBuildRecommendationFinalizer.CurrencyNotProvidedNote]
        };

        AiBuildRecommendationFinalizer.FinalizeStatus(result);

        Assert.Equal(BuildRecommendationStatus.Completed, result.Status);
    }

    [Fact]
    public void FinalizeStatus_Should_SetCompleted_WhenOnlyCurrencyNotRecognizedNote()
    {
        var result = new BuildRecommendationResult
        {
            Notes = [AiBuildRecommendationFinalizer.CurrencyNotRecognizedNote]
        };

        AiBuildRecommendationFinalizer.FinalizeStatus(result);

        Assert.Equal(BuildRecommendationStatus.Completed, result.Status);
    }

    [Fact]
    public void FinalizeStatus_Should_SetCompleted_WhenNoNotes()
    {
        var result = new BuildRecommendationResult();

        AiBuildRecommendationFinalizer.FinalizeStatus(result);

        Assert.Equal(BuildRecommendationStatus.Completed, result.Status);
    }

    [Fact]
    public void FinalizeStatus_Should_SetPartiallyCompleted_WhenOptionalPartMissing()
    {
        var result = new BuildRecommendationResult
        {
            Notes =
            [
                AiBuildRecommendationFinalizer.CurrencyNotProvidedNote,
                "No suitable hard drive found within budget; build is missing storage."
            ]
        };

        AiBuildRecommendationFinalizer.FinalizeStatus(result);

        Assert.Equal(BuildRecommendationStatus.PartiallyCompleted, result.Status);
    }

    [Fact]
    public void FinalizeStatus_Should_PreserveFailed()
    {
        var result = new BuildRecommendationResult
        {
            Status = BuildRecommendationStatus.Failed,
            Notes =
            [
                AiBuildRecommendationFinalizer.CurrencyNotProvidedNote,
                "No Cpu found matching budget/priority target."
            ]
        };

        AiBuildRecommendationFinalizer.FinalizeStatus(result);

        Assert.Equal(BuildRecommendationStatus.Failed, result.Status);
    }
}
