using CdbCalculator.Application.Calculators;
using CdbCalculator.Application.Calculators.Interfaces;
using CdbCalculator.Application.Services;
using CdbCalculator.Application.Services.Interfaces;
using Serilog;


Console.WriteLine("Iniciando...");


var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
ConfigureLogging(builder);

// Adicionando serviços ao contêiner
ConfigureServices(builder);

// Criando a aplicação
var app = builder.Build();

// Configuração do pipeline de requisições
ConfigurePipeline(app);

// Rodando a aplicação
await app.RunAsync();

// Métodos auxiliares
void ConfigureLogging(WebApplicationBuilder builder)
{
    // Configuração do Serilog
    Log.Logger = new LoggerConfiguration()
        .CreateLogger();

    // Usando Serilog para logging
    builder.Host.UseSerilog();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    // Adicionando controladores
    builder.Services.AddControllers();

    // Registro dos serviços
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
    // Configuração do pipeline de requisições
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}