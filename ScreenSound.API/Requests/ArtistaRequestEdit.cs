﻿namespace ScreenSound.API.Requests;
public record ArtistaRequestEdit(int id, string nome, string bio, string? fotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png") : ArtistaRequest(nome, bio, fotoPerfil);

