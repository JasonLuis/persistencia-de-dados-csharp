using ScreenSound.Shared.Dados.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Integration.Test;

public class ScreenSound_AuthTest
{


    [Fact]
    public async Task Post_Efetua_Autenticacao_Com_Sucesso()
    {
        //Arrange
        var app = new ScreenSoundWebApplicationFactory();

        var email = "jason@gmail.com";
        var password = "Senha@123";

        using var client = app.CreateClient();

        //Act 
        var response = await client.PostAsJsonAsync("auth/login", new { email, password });

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
