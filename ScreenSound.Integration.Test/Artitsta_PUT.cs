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

public class Artitsta_PUT: IClassFixture<ScreenSoundWebApplicationFactory>
{
    private readonly ScreenSoundWebApplicationFactory app;

    public Artitsta_PUT(ScreenSoundWebApplicationFactory app)
    {
        this.app = app;
    }

    [Fact] 
    public async Task Atualiza_Artista_Por_Id() { 
        //Arrange
        var artistaExistente = app.Context.Artistas.FirstOrDefault();
        if (artistaExistente is null)
        {
            artistaExistente = new Artista("Cantor Santos", "Bora cantar");

            app.Context.Add(artistaExistente);
            app.Context.SaveChanges();
        }

        using var client = await app.GetClientWithAccessTokenAsync();

        artistaExistente.Nome = "Cantor Santos -  Atualizado!";
        artistaExistente.Bio = "Bora cantar - Atualizado!";

        //Act
        var response = await client.PutAsJsonAsync($"/artistas/Artistas", new ArtistaRequestEdit(artistaExistente.Id, artistaExistente.Nome, artistaExistente.Bio));

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
