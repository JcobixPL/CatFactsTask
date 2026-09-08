using CatFacts.Api.Models;
using System.Text.Json;

namespace CatFacts.Api.Services;

public class FactFileWriter : IFactFileWriter
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly ILogger<FactFileWriter> _logger;

    public FactFileWriter(IConfiguration configuration, IWebHostEnvironment environment, ILogger<FactFileWriter> logger)
    {
        _logger = logger;
            
        var path = configuration["FileStorage:Path"]
            ?? throw new InvalidOperationException(
                "FileStorage path is missing.");

        _filePath = Path.Combine(environment.ContentRootPath, path);

        var directory = Path.GetDirectoryName(_filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public async Task AppendAsync(CatFact catFact, CancellationToken cancellationToken = default)
    {
        var line = JsonSerializer.Serialize(catFact);

        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            await File.AppendAllTextAsync(
                _filePath,
                line + Environment.NewLine,
                cancellationToken);

            _logger.LogInformation(
                "Appended cat fact to file: {FilePath}",
                _filePath);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
