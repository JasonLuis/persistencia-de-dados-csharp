﻿namespace ScreenSound.Web.Requests;
public record MusicaRequest(string Nome, int ArtistaId, int AnoLancamento, ICollection<GeneroRequest> Generos = null);

