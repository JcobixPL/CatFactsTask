using CatFacts.Api.Clients;
using CatFacts.Api.Exceptions;
using CatFacts.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<ICatFactClient, CatFactClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CatFactApi:BaseUrl"]);
    client.Timeout = TimeSpan.FromSeconds(builder.Configuration.GetValue<int>("CatFactApi:TimeoutSeconds"));
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
