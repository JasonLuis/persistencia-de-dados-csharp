using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Endpoints;
using ScreenSound.Banco;
using ScreenSound.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Após criar o uma CONFIGURAÇÂO DE APP no Azure DevOps e adicionar o a biblioteca AppConfiguration nos pacotes do projeto, adicionar essa linha de código
builder.Host.ConfigureAppConfiguration(config =>
{
    var settings = config.Build();
    // A string adicionada nesse campo é encontrada em COnfiguração de Aplicativos - NOME-DO-RECURSO - Chave de Acesso
    // Copiar Cadeia de conexão
    config.AddAzureAppConfiguration("Endpoint=https://screensound-configurationjc.azconfig.io;Id=GGcL;Secret=AJJTelBdbfTTbmBVS+BCcs7uMSvPOJuRDoYv46NE4Og=");
});

builder.Services.AddDbContext<ScreenSoundContext>((options) =>
{
    options
        .UseSqlServer(builder.Configuration
            ["ConnectionStrings:ScreenSoundDB"])
        .UseLazyLoadingProxies();
});
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion


/*Esse builder.Services.Configure() acima configura essa refer�ncia c�clica que vai acontecer no momento da convers�o, da desserializa��o do objeto retornado, para o JSON.*/
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddCors();
var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

// informa a aplica��o web que exibir� arquivos estaticos;
app.UseStaticFiles();

app.AddEndPointsArtistas();
app.AddEndPointMusicas();
app.AddEndPointGeneros();

#region Swagger
app.UseSwagger();
app.UseSwaggerUI();
#endregion
app.Run(); 