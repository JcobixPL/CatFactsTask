using CatFacts.Api.Clients;

namespace CatFacts.Api.Services;

public class CatFactService : ICatFactService
{
    private readonly ICatFactClient _catFactClient;
    private readonly IFactFileWriter _factFileWriter;

    public CatFactService(
        ICatFactClient catFactClient,
        IFactFileWriter factFileWriter)
    {
        _catFactClient = catFactClient;
        _factFileWriter = factFileWriter;
    }

    public async Task<Models.CatFact> GetAndSaveFactAsync(CancellationToken cancellationToken = default)
    {
        var fact = await _catFactClient.GetFactAsync(cancellationToken);

        await _factFileWriter.AppendAsync(
            fact,
            cancellationToken);

        return fact;
    }
}
