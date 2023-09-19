using Shops.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Inject(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseCors("DefaultPolicy");

await app.RunAsync();