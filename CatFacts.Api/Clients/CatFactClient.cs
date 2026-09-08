using CatFacts.Api.Models;

namespace CatFacts.Api.Clients;

public class CatFactClient : ICatFactClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CatFactClient> _logger;

    public CatFactClient(HttpClient httpClient, ILogger<CatFactClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<CatFact> GetFactAsync(
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Requesting a cat fact from the Cat Fact API.");

        var catFact = await _httpClient.GetFromJsonAsync<CatFact>(
            "fact",
            cancellationToken);

        if (catFact is null)
        {
            _logger.LogWarning("Cat Fact API returned an empty response.");
            throw new InvalidOperationException("Cat Fact API returned an empty response.");
        }

        _logger.LogInformation(
            "Received a cat fact from the Cat Fact API with length: {Length}",
            catFact.Length);

        return catFact;
    }
}
