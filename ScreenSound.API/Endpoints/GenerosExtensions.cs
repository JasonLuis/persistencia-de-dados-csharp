using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class GenerosExtensions
{
    public static void AddEndPointGeneros(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("generos")
                .RequireAuthorization()
                .WithTags("Generos");

        #region Endpoint Generos
        groupBuilder.MapGet("", ([FromServices] DAL<Genero> dal) =>
        {
            var generos = EntityListToReponseList(dal.Listar());
            return generos;
        });

        groupBuilder.MapGet("{nome}", ([FromServices] DAL < Genero > dal, string nome) =>
        {
            var recuperarGenero = dal.RecuperarPor(g => g.Nome.ToUpper().Equals(nome.ToUpper()));
            if (recuperarGenero is null)
            {
                return Results.NotFound();
            }

            GeneroResponse generoResponse = EntityToResponse(recuperarGenero);

            return Results.Ok(generoResponse);
        });

        groupBuilder.MapPost("", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) =>
        {
            var genero = new Genero(generoRequest.Nome) { Descricao = generoRequest.Descricao};
            dal.Adicionar(genero);

            return Results.Ok(genero);
        });


        groupBuilder.MapPut("", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequestEdit generoRequestEdit) =>
        {
            var genero = dal.RecuperarPor(g => g.Id == generoRequestEdit.Id);
            if (genero is null)
            {
                return Results.NotFound();
            }

            genero.Nome = generoRequestEdit.Nome;
            genero.Descricao = generoRequestEdit.Descricao;
            dal.Atualizar(genero);
            return Results.Ok(generoRequestEdit);
        });

        groupBuilder.MapDelete("{id}", ([FromServices] DAL<Genero> dal, int id) =>
        {
            var genero = dal.RecuperarPor(g => g.Id == id);

            if (genero is null)
            {
                return Results.NotFound();
            }

            dal.Remover(genero);

            return Results.Ok();
        });
        #endregion
    }

    private static ICollection<GeneroResponse> EntityListToReponseList(IEnumerable<Genero> listaDeGeneros)
    {
        return listaDeGeneros.Select(a => EntityToResponse(a)).ToList();
    }

    private static GeneroResponse EntityToResponse(Genero genero)
    {
        return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
    }
}

