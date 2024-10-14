using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Integration.Test;

public class ScreenSoundWebApplicationFactory: WebApplicationFactory<Program>
{
    public ScreenSoundContext Context { get; }

    private IServiceScope scope;

    public ScreenSoundWebApplicationFactory()
    {
        this.scope = Services.CreateScope();
        Context = scope.ServiceProvider.GetRequiredService<ScreenSoundContext>();
    }

    public async Task<HttpClient> GetClientWithAccessTokenAsync()
    {
        var client = this.CreateClient();
        var email = "jason@gmail.com";
        var password = "Senha@123";
        var response = await client.PostAsJsonAsync("auth/login", new { email, password });

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result!.AccessToken);

        return client;
    }
}


public class AuthResponse() {
    public string AccessToken { get; set; } = string.Empty;
}
