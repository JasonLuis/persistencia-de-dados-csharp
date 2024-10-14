using ScreenSound.API.Requests;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Integration.Test;

public class Musica_GET : IClassFixture<ScreenSoundWebApplicationFactory>
{
    private readonly ScreenSoundWebApplicationFactory app;

    public Musica_GET(ScreenSoundWebApplicationFactory app)
    {
        this.app = app;
    }

    [Fact]
    public async Task Atualiza_Musica_Por_Id()
    {
        //Arrange
        var musicaExistente = app.Context.Musicas.FirstOrDefault();
        if (musicaExistente is null)
        {
            var artistaExistente = app.Context.Artistas.FirstOrDefault();
            if (artistaExistente is null)
            {
                artistaExistente = new Artista("Cantor Santos", "Bora cantar");
                app.Context.Add(artistaExistente);
                List<Genero> generos = new List<Genero>();
                generos.Add(new Genero() { 
                    Nome = "Rock",
                    Descricao = "Rock é um termo abrangente que define um gênero musical de música popular que se desenvolveu durante e após a década de 1950."
                });

                musicaExistente = new Musica()
                {
                   Nome = "Musica Teste",
                   ArtistaId = artistaExistente.Id,
                   AnoLancamento = 2024,
                   Generos = generos                
                };
                app.Context.Add(musicaExistente);
                app.Context.SaveChanges();
   
            }

        }

       using var client = await app.GetClientWithAccessTokenAsync();

        //Act
        var response = await client.GetFromJsonAsync<Musica>($"/musicas/{musicaExistente!.Nome}");

        //Assert
        Assert.NotNull(response);
        Assert.Equal(musicaExistente.Id, response.Id);
        Assert.Equal(musicaExistente.Nome, response.Nome);
    }
}
