using CatFacts.Api.Models;

namespace CatFacts.Api.Clients;

public interface ICatFactClient
{
    Task<CatFact> GetFactAsync(CancellationToken cancellationToken = default);
}
