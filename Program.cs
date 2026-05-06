using Microsoft.EntityFrameworkCore;
using MormorDB.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MormorDbContext>(options =>
 {
     options.UseSqlite(builder.Configuration.GetConnectionString("sqlite"));
 });

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MormorDbContext>();
    await SeedData.Initialize(context);
}

app.MapControllers();
app.Run();