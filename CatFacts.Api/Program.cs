using CatFacts.Api.Clients;
using CatFacts.Api.Exceptions;
using CatFacts.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var catFactApiBaseUrl =
    builder.Configuration["CatFactApi:BaseUrl"]
    ?? throw new InvalidOperationException(
        "Cat Fact API base URL is missing.");

var catFactApiTimeoutSeconds =
    builder.Configuration.GetValue<int>("CatFactApi:TimeoutSeconds");

if (catFactApiTimeoutSeconds <= 0)
{
    throw new InvalidOperationException(
        "Cat Fact API timeout must be greater than 0.");
}

builder.Services.AddHttpClient<ICatFactClient, CatFactClient>(client =>
{
    client.BaseAddress = new Uri(catFactApiBaseUrl);
    client.Timeout = TimeSpan.FromSeconds(catFactApiTimeoutSeconds);
});

builder.Services.AddSingleton<IFactFileWriter, FactFileWriter>();
builder.Services.AddScoped<ICatFactService, CatFactService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
