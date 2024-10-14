using ScreenSound.API.Requests;
using System.Net;
using System.Net.Http.Json;

namespace ScreenSound.Integration.Test
{
    public class Artista_POST: IClassFixture<ScreenSoundWebApplicationFactory>
    {

        private readonly ScreenSoundWebApplicationFactory app;

        public Artista_POST(ScreenSoundWebApplicationFactory app)
        {
            this.app = app;
        }

        [Fact]
        public async Task Cadastra_Artista()
        {
            //Arrange
            using var client = await app.GetClientWithAccessTokenAsync();

            var artista = new ArtistaRequest("Cantor Santos", "Bora cantar", "");

            //Act
            var response = await client.PostAsJsonAsync("/artistas", artista);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}