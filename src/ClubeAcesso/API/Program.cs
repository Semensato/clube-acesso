using Application.Interfaces;
using Application.Services;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ClubeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// DI
builder.Services.AddScoped<ISocioRepository, SocioRepository>();
builder.Services.AddScoped<IPlanoAcessoRepository, PlanoAcessoRepository>();
builder.Services.AddScoped<IAreaClubeRepository, AreaClubeRepository>();
builder.Services.AddScoped<ITentativaAcessoRepository, TentativaAcessoRepository>();

builder.Services.AddScoped<ISocioService, SocioService>();
builder.Services.AddScoped<IAreaClubeService, AreaClubeService>();
builder.Services.AddScoped<IPlanoAcessoService, PlanoAcessoService>();
builder.Services.AddScoped<ITentativaAcessoService, TentativaAcessoService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClubeDbContext>();
    db.Database.Migrate();
}

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
