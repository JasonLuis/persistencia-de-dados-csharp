using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();


/*Esse builder.Services.Configure() acima configura essa referência cíclica que vai acontecer no momento da conversão, da desserialização do objeto retornado, para o JSON.*/
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", ([FromServices] DAL < Artista > dal, string nome) =>
{
    var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista is null)
    {
       return Results.NotFound();
    }

    return Results.Ok(artista);
});


app.MapPost("/Artistas", ([FromServices] DAL < Artista > dal, [FromBody] Artista artista) =>
{
    dal.Adicionar(artista);
    return Results.Ok(artista);
});

app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
{
    var artista = dal.RecuperarPor(a => a.Id == id);

    if (artista is null)
    {
        return Results.NotFound();
    }

    dal.Remover(artista);
    return Results.NoContent();
});

app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) => {
    var artistaAtualizar = dal.RecuperarPor(a => a.Id == artista.Id);

    if (artistaAtualizar is null)
    {
        return Results.NotFound();
    }

    artistaAtualizar.Nome = artista.Nome;
    artistaAtualizar.Bio  = artista.Bio;
    artistaAtualizar.FotoPerfil = artista.FotoPerfil;

    dal.Atualizar(artistaAtualizar);

    return Results.Ok();
});

/* Músicas */

app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
{
    var musica = dal.RecuperarPor(m => m.Nome == nome);

    if (musica is null)
    {
        return  Results.NoContent();
    }

    return Results.Ok(musica);
});

app.MapPost("/Musicas", ([FromServices] DAL<Musica>dal, [FromBody] Musica musica) =>
{
    dal.Adicionar(musica);
    return Results.Ok();
});

app.MapPut("/Musicas", ([FromServices] DAL < Musica > dal, [FromBody] Musica musica) =>
{
    var musicaAtualizar = dal.RecuperarPor(m => m.Id == musica.Id);

    if (musicaAtualizar is null)
    {
        return Results.NotFound();
    }

    musicaAtualizar.Nome = musica.Nome;
    musicaAtualizar.AnoLancamento = musica.AnoLancamento;

    dal.Atualizar(musicaAtualizar);
    return Results.Ok();
});
app.Run(); 