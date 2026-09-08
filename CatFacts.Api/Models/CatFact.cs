using System.Text.Json.Serialization;

namespace CatFacts.Api.Models;

public sealed record CatFact(
    [property: JsonPropertyName("fact")] string Fact,
    [property: JsonPropertyName("length")] int Length
);
