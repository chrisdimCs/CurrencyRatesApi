using RatesInterfaces;
using RatesApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using RatesApi.Services.Helper;
using RatesDataCommand.Interfaces;
using RatesDataCommand.Repositories;
using Microsoft.Extensions.Configuration;
using RatesData.Data;
using RatesDataCommand.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<RatesDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IConvertUrlHelper, ConvertUrlHelper>();
builder.Services.AddScoped<IConvertRatesRepository, ConvertRatesRepository>();
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
//builder.Services.AddDbContext<RatesDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
