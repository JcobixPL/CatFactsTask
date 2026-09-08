using CatFacts.Api.Models;

namespace CatFacts.Api.Services;

public interface ICatFactService
{
    Task<CatFact> GetAndSaveFactAsync(CancellationToken cancellationToken = default);
}
