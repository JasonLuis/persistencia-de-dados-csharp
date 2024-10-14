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

public class Musica_POST:IClassFixture<ScreenSoundWebApplicationFactory>
{
    private readonly ScreenSoundWebApplicationFactory app;

    public Musica_POST(ScreenSoundWebApplicationFactory app)
    {
        this.app = app;
    }

    [Fact]
    public async Task Cadastra_Musica()
    {
        //Arrange
        using var client = await app.GetClientWithAccessTokenAsync();
        var artistaExistente = app.Context.Artistas.FirstOrDefault();
        if (artistaExistente is null)
        {
            artistaExistente = new Artista("Teste Musica Artista", "Teste Musica Artista");

            app.Context.Add(artistaExistente);
            app.Context.SaveChanges();
        }

        var musica = new MusicaRequest
        (
           "Musica Teste",
           artistaExistente.Id,
           2024,
           new List<GeneroRequest>() { 
                new GeneroRequest("Rock", "Rock é um termo abrangente que define um gênero musical de música popular que se desenvolveu durante e após a década de 1950.")
           }
        );

        //Act
        var response = await client.PostAsJsonAsync("/musicas", musica);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
