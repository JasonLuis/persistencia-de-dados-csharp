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
            var groupBuilder = app.MapGroup("artistas")
                .RequireAuthorization()
                .WithTags("Artistas");

            #region Endpoint Artistas
            groupBuilder.MapGet("", ([FromServices] DAL<Artista> dal) =>
            {
                var artistas = EntityListToResponseList(dal.Listar());

                return Results.Ok(artistas);
            });

            groupBuilder.MapGet("{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
            {
                var recuperarArtista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (recuperarArtista is null)
                {
                    return Results.NotFound();
                }

                ArtistaResponse artistaResponse = EntityToResponse(recuperarArtista);

                return Results.Ok(artistaResponse);
            });


            groupBuilder.MapPost("", async ([FromServices] IHostEnvironment env,[FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
            {
                string imagemArtista = "";
                if (artistaRequest.fotoPerfil is not null)
                {
                    var nome = artistaRequest.nome.Trim();
                    imagemArtista = DateTime.Now.ToString("ddMMyyyyhhss") + "." + nome + ".jpeg";

                    var path = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", imagemArtista);

                    using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequest.fotoPerfil!));
                    using FileStream fs = new(path, FileMode.Create);
                    await ms.CopyToAsync(fs);
                }
                

                var artista = new Artista(artistaRequest.nome, artistaRequest.bio) { FotoPerfil =  $"/FotosPerfil/{imagemArtista}"};
                dal.Adicionar(artista);
                return Results.Ok(artista);
            });

            groupBuilder.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
            {
                var artista = dal.RecuperarPor(a => a.Id == id);

                if (artista is null)
                {
                    return Results.NotFound();
                }

                dal.Remover(artista);
                return Results.NoContent();
            });

            groupBuilder.MapPut("/Artistas", async ([FromServices] IHostEnvironment env, [FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) => {
                var artistaAtualizar = dal.RecuperarPor(a => a.Id == artistaRequestEdit.id);

                if (artistaAtualizar is null)
                {
                    return Results.NotFound();
                }

                if (artistaRequestEdit.fotoPerfil is not null)
                {
                    var nome = artistaRequestEdit.nome.Trim();
                    var imagemArtista = DateTime.Now.ToString("ddMMyyyyhhss") + "." + nome + ".jpeg";

                    var path = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", imagemArtista);

                    using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequestEdit.fotoPerfil!));
                    using FileStream fs = new(path, FileMode.Create);
                    await ms.CopyToAsync(fs);
                    artistaAtualizar.FotoPerfil = $"/FotosPerfil/{imagemArtista}";
                }
                

                artistaAtualizar.Nome = artistaRequestEdit.nome;
                artistaAtualizar.Bio = artistaRequestEdit.bio;

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
