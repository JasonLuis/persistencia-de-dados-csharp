namespace ScreenSound.API.Response;
public record ArtistaResponse(int Id, string Nome, string Bio, string? FotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png")
{
    public double? Classificacao { get; set; }
};

