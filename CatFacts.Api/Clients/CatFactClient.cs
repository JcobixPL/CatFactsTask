using CatFacts.Api.Models;

namespace CatFacts.Api.Clients;

public class CatFactClient : ICatFactClient
{
    private readonly HttpClient _httpClient;

    public CatFactClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CatFact> GetFactAsync(
        CancellationToken cancellationToken)
    {
        var catFact = await _httpClient.GetFromJsonAsync<CatFact>(
            "fact",
            cancellationToken);

        return catFact
            ?? throw new InvalidOperationException(
                "Cat Fact API returned an empty response.");
    }
}
