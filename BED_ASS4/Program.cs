using BED_ASS4.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services.AddSingleton<MongoService>(s => new MongoService(builder.Configuration.GetConnectionString("Localhost")));
builder.Services.AddSingleton<CardService>();
// builder.Services.AddSingleton<DecksService>();
builder.Services.AddControllers();


builder.Services.AddControllers();

var app = builder.Build();





using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var cardService = services.GetRequiredService<CardService>();
    if (!await cardService.IsSeeded())
    {
        cardService.SeedCards(new[] { "cards.json", "metadata.json" });
    }
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
