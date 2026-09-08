using CatFacts.Api.Models;

namespace CatFacts.Api.Services;

public interface IFactFileWriter
{
    Task AppendAsync(CatFact catFact, CancellationToken cancellationToken = default);
}
