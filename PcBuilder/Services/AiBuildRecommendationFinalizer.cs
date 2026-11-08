using PcBuilder.Enums;
using PcBuilder.Models;

namespace PcBuilder.Services;

/// <summary>
/// Currency fallback and recommendation status rules for AI builds.
/// Currency informational notes do not by themselves produce PartiallyCompleted.
/// </summary>
public static class AiBuildRecommendationFinalizer
{
    public const string CurrencyNotProvidedNote = "Currency was not provided. USD was used.";
    public const string CurrencyNotRecognizedNote = "Currency was not recognized. USD was used instead.";

    public static Currency ResolveRequestedCurrency(string? currency, ICollection<string> notes)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            notes.Add(CurrencyNotProvidedNote);
            return Currency.USD;
        }

        if (Enum.TryParse<Currency>(currency, ignoreCase: true, out var parsed))
        {
            return parsed;
        }

        notes.Add(CurrencyNotRecognizedNote);
        return Currency.USD;
    }

    /// <summary>
    /// Sets Completed vs PartiallyCompleted from completeness notes only.
    /// Does not change Failed. Currency fallback notes are ignored for status.
    /// </summary>
    public static void FinalizeStatus(BuildRecommendationResult result)
    {
        if (result.Status == BuildRecommendationStatus.Failed)
        {
            return;
        }

        var hasCompletenessNotes = result.Notes.Any(IsCompletenessNote);
        result.Status = hasCompletenessNotes
            ? BuildRecommendationStatus.PartiallyCompleted
            : BuildRecommendationStatus.Completed;
    }

    public static bool IsCurrencyInformationalNote(string note) =>
        note is CurrencyNotProvidedNote or CurrencyNotRecognizedNote;

    public static bool IsCompletenessNote(string note) => !IsCurrencyInformationalNote(note);
}
