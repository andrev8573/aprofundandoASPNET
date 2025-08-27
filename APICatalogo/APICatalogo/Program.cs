using System.Text.Json.Serialization;
using APICatalogo.Properties.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    options.JsonSerializerOptions
    .ReferenceHandler = ReferenceHandler.IgnoreCycles); // Evita ciclo na serialização JSON ao chamar o método Include()
    
    ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionando EF como serviço usando a classe criada AppDbContext, que implementa DBContext
string postgreConexao = builder.Configuration.GetConnectionString("DefaultConnection"); // Definida em appsettings.json
builder.Services.AddDbContext<AppDbContext>((sp, options) =>
    options.UseNpgsql(postgreConexao));

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
