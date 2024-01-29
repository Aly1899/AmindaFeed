using AmindaFeed.Data;
using AmindaFeed.Repository;
using AmindaFeed.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
// Add services to the container.
var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .Build();

IConfiguration configuration = configurationBuilder.Build();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddScoped<IMatterhornAdapter, MatterhornAdapter>();
builder.Services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresSQL")));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AmindaAllowOrigins",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200");
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AmindaAllowOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
