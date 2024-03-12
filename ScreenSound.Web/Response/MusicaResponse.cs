namespace ScreenSound.Web.Response;

public record MusicaResponse(int Id, string Nome, string? NomeArtista, int AnoLancamento, ICollection<GeneroResponse> Generos = null);

