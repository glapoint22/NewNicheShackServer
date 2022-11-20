using Manager.Api;
using Manager.Application;
using Manager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ApiServices();
builder.Services.ApplicationServices(builder.Configuration);
builder.Services.InfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
