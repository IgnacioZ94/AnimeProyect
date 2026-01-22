using Domain.Core.Interfaces;
using Domain.Core.Services;
using Infrastructure.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// MongoDB Context
builder.Services.AddSingleton<MongoDbContext>();

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// You can register generic repository if needed, but usually specific ones are better for DI injection matching
// builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

// Services
builder.Services.AddScoped<IProductService, ProductService>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// HttpClient for Jikan
builder.Services.AddHttpClient<IAnimeService, JikanAnimeService>();

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
