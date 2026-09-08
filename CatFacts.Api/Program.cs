using CatFacts.Api.Clients;
using CatFacts.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<ICatFactClient, CatFactClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CatFactApi:BaseUrl"]);
    client.Timeout = TimeSpan.FromSeconds(builder.Configuration.GetValue<int>("CatFactApi:TimeoutSeconds"));
});

builder.Services.AddSingleton<IFactFileWriter, FactFileWriter>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
