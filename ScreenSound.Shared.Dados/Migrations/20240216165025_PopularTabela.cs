using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Artistas", new string[] { "Nome", "Bio", "FotoPerfil" }, new object[] { "Fernanda Brum", "Fernanda Brum Costa da Cruz, mais conhecida como Fernanda Brum, é uma cantora, escritora, atriz, apresentadora, compositora, produtora e pastora, relacionada à música cristã contemporânea brasileira.", "https://www.vagalume.com.br/fernanda-brum/images/fernanda-brum.jpg" });

            migrationBuilder.InsertData("Artistas", new string[] { "Nome", "Bio", "FotoPerfil" }, new object[] { "Biguinho Sensação", "O músico é de Santarém, da comunidade Quilombo do Bom Jardim e já tinha um certo sucesso na região. Nas redes sociais seus vídeos bateram mais de mil visualizações se tornando um grande sucesso e até fazendo parte de memes com suas letras românticas e de sofrência.", "https://cdns-images.dzcdn.net/images/cover/660d4fc6960b122ad589905e2e09c4a7/264x264.jpg" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Artistas");
        }
    }
}
