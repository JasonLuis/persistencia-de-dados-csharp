using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Integration.Test;

public class Artista_GET : IClassFixture<ScreenSoundWebApplicationFactory>
{
    private readonly ScreenSoundWebApplicationFactory app;

    public Artista_GET(ScreenSoundWebApplicationFactory app)
    {
        this.app = app;
    }

    [Fact]
    public async Task Recupera_Artitsta_Por_Nome()
    {
        //Arrange
        var artistaExistente = app.Context.Artistas.FirstOrDefault();
        if (artistaExistente is null)
        {
            artistaExistente = new Artista("Cantor Santos", "Bora cantar");

            app.Context.Add(artistaExistente);
            app.Context.SaveChanges();
        }

        using var client = await app.GetClientWithAccessTokenAsync();
        
        //Act
        var response = await client.GetFromJsonAsync<Artista>($"/artistas/{artistaExistente.Nome}");

        //Assert
        Assert.NotNull(response);
        Assert.Equal(artistaExistente.Id, response.Id);
        Assert.Equal(artistaExistente.Nome, response.Nome);

    }
}
