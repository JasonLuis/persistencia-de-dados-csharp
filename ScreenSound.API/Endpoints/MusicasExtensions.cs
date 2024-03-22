using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointMusicas(this WebApplication app)
        {
            #region Endpoint Músicas
            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                var musicas = EntityListToResponseList(dal.Listar());


                return Results.Ok(musicas);
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(m => m.Nome == nome);

                if (musica is null)
                {
                    return Results.NoContent();
                }

                var musicaResponse = EntityToResponse(musica);

                return Results.Ok(musicaResponse);
            });

            app.MapGet("/Musicas/Artista/{id}", ([FromServices] DAL<Musica> dal, int id) =>
            {
                var musicas = dal.ListarPor(m => m.ArtistaId == id);

                if (musicas is null)
                {
                    return Results.NoContent();
                }

                var musicasResponse = EntityListToResponseList(musicas);


                return Results.Ok(musicasResponse);
            });

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal,
                [FromServices] DAL<Genero> dalGenero,
                [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.Nome)
                {
                    ArtistaId = musicaRequest.ArtistaId,
                    AnoLancamento = musicaRequest.AnoLancamento,
                    Generos = musicaRequest.Generos is not null ?
                    GeneroRequestConverter(musicaRequest.Generos, dalGenero) :
                    new List<Genero>()
                };
                dal.Adicionar(musica);
                return Results.Ok();
            });

            app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromServices] DAL<Genero> dalGenero, [FromBody] MusicaRequestEdit musicaRequestEdit) =>
            {
                var musicaAtualizar = dal.RecuperarPor(m => m.Id == musicaRequestEdit.Id);

                if (musicaAtualizar is null)
                {
                    return Results.NotFound();
                }

                musicaAtualizar.Nome = musicaRequestEdit.Nome;
                musicaAtualizar.AnoLancamento = musicaRequestEdit.AnoLancamento;
                musicaAtualizar.ArtistaId = musicaRequestEdit.ArtistaId;
                musicaAtualizar.Generos = musicaRequestEdit.Generos is not null ?
                GeneroRequestConverter(musicaRequestEdit.Generos, dalGenero) : new List<Genero>();

                dal.Atualizar(musicaAtualizar);
                return Results.Ok();
            });
            #endregion

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
            {
                var musica = dal.RecuperarPor(m => m.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                dal.Remover(musica);
                return Results.Ok();
            });
        }

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
        {
            var listaDeGeneros = new List<Genero>();
            foreach (var item in generos)
            {
                var entity = RequestToEntity(item);
                var genero = dalGenero.RecuperarPor(g => g.Nome.ToUpper().Equals(item.Nome.ToUpper()));
                if (genero is not null)
                {
                    listaDeGeneros.Add(genero);
                }
                else
                {
                    listaDeGeneros.Add(entity);
                }
            }

            return listaDeGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            if (musica.Generos is not null)
            {
                var generos = EntityListToResponseListGeneros(musica.Generos);
                return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista?.Nome, musica.AnoLancamento, generos);
            }

            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista?.Nome, musica.AnoLancamento);
        }

        private static ICollection<GeneroResponse> EntityListToResponseListGeneros(IEnumerable<Genero> generoList)
        {
            return generoList.Select(g => EntityToResponseGenero(g)).ToList();
        }

        private static GeneroResponse EntityToResponseGenero(Genero genero)
        {
            return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
        }
    }
}
