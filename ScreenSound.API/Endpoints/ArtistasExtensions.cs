using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndPointsArtistas(this WebApplication app)
        {
            #region Endpoint Artistas
            app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
            {
                var artistas = EntityListToResponseList(dal.Listar());

                return Results.Ok(artistas);
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
            {
                var recuperarArtista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (recuperarArtista is null)
                {
                    return Results.NotFound();
                }

                ArtistaResponse artistaResponse = EntityToResponse(recuperarArtista);

                return Results.Ok(artistaResponse);
            });


            app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var artista = new Artista(artistaRequest.nome, artistaRequest.bio);
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

            app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) => {
                var artistaAtualizar = dal.RecuperarPor(a => a.Id == artistaRequestEdit.id);

                if (artistaAtualizar is null)
                {
                    return Results.NotFound();
                }

                artistaAtualizar.Nome = artistaRequestEdit.nome;
                artistaAtualizar.Bio = artistaRequestEdit.bio;
                artistaAtualizar.FotoPerfil = artistaRequestEdit.fotoPerfil;

                dal.Atualizar(artistaAtualizar);

                return Results.Ok();
            });
            #endregion
        }

        private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
        {
            return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
        }

        private static ArtistaResponse EntityToResponse(Artista artista)
        {
            return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
        }
    }
}
