using CdbCalculator.Application.Calculators;
using CdbCalculator.Application.Calculators.Interfaces;
using CdbCalculator.Application.Services;
using CdbCalculator.Application.Services.Interfaces;
using Serilog;


Console.WriteLine("Iniciando...");


var builder = WebApplication.CreateBuilder(args);

// Configura��o do Serilog
ConfigureLogging(builder);

// Adicionando servi�os ao cont�iner
ConfigureServices(builder);

// Criando a aplica��o
var app = builder.Build();

// Configura��o do pipeline de requisi��es
ConfigurePipeline(app);

// Rodando a aplica��o
await app.RunAsync();

// M�todos auxiliares
void ConfigureLogging(WebApplicationBuilder builder)
{
    // Configura��o do Serilog
    Log.Logger = new LoggerConfiguration()
        .CreateLogger();

    // Usando Serilog para logging
    builder.Host.UseSerilog();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    // Adicionando controladores
    builder.Services.AddControllers();

    // Registro dos servi�os
    builder.Services.AddScoped<ICdbYieldCalculator, CdbYieldCalculator>();
    builder.Services.AddScoped<ICdbService, CdbService>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            policy => policy.AllowAnyOrigin().
                             AllowAnyHeader().
                             AllowAnyMethod());
    });

    builder.Services.AddControllers();
}

void ConfigurePipeline(WebApplication app)
{
    app.UseCors("AllowAll");
    // Configura��o do pipeline de requisi��es
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}