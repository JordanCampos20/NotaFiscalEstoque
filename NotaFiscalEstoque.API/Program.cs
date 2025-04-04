using NotaFiscalEstoque.CrossCutting;
using System.Text.Json.Serialization;
using System.Text.Json;
using NotaFiscalEstoque.API.Interfaces;
using NotaFiscalEstoque.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureAPI();

builder.Services.AddSingleton<IEstoqueService, EstoqueService>();
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
builder.Services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularCors",
        corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

var consumer = app.Services.GetRequiredService<KafkaConsumerService>();

_ = Task.Run(consumer.ConsumirNotas);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
