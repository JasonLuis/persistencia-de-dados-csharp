Transcrição
Na última aula, fizemos a configuração do nosso projeto e as implementações necessárias para consumir a API e criamos componentes no Blazor para listar as músicas e artistas. Além disso, disponibilizamos uma atividade para você praticar.

Lembre-se que um componente é um elemento que pode ser renderizado como uma página. Pode representar um botão, um select, entre outras coisas. Associado a esse componente, temos alguns arquivos CSS para serem utilizados.

Contudo, não queremos criar um componente do zero. Sabemos que existem várias bibliotecas que podem ajudar as pessoas desenvolvedoras a entregarem algo com mais agilidade.

Para o Blazor, não é diferente. Temos disponíveis no mercado algumas bibliotecas que entregam componentes que vão nos ajudar a ganhar produtividade na criação dos sistemas. A partir de agora, utilizaremos uma biblioteca chamada MudBlazor.

Conhecendo e instalando o MudBlazor
Abrimos o navegador e acessamos o site da MudBlazor. Na barra de menu superior, clicamos em "Docs". Na nova página, no menu lateral esquerdo, clicamos em "Components". Abre uma lista com componentes, como alert, appbar, avatar, buttons, card, carroussel, entre outros. São componentes já prontos que precisamos apenas adicionar no nosso projeto e começar a utilizar.

Portanto, nesse vídeo, faremos a configuração do MudBlazor e definiremos um layout mais atraente, próximo do que mostramos no início do curso, no Figma.

Para instalar o MudBlazor no projeto, No Visual Studio, no Gerenciador de Soluções, clicamos com o botão direito em ScreenSoundWeb e depois em "Gerenciar Pacotes do NuGet". Na janela que abre, encontramos uma barra de busca. Escrevemos "MudBlazor" e clicamos na primeira opção. Depois, na lateral direita, clicamos no botão "Instalar" e aceitamos os termos de utilização da biblioteca.

Configurando o MudBlazor
Ao concluir a instalação, precisamos fazer algumas configurações. Seguiremos as orientações da documentação oficial do MudBlazor. Para isso, no gerenciador de Soluções, clicamos em _Imports.razor.

Para acessas essas informações na documentação acesse a página do MudBlazor. Na barra de menu superior, clique em "Getting Started". Feito isso, você encontra o processo de instalação do pacote e as configurações necessárias.

Na linha 14 passamos @using MudBlazor e na 15 @using MudBlazor.Utilities.

@using System.Net.Http
@using System.Net.Http.Json
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Web.Virtualization
@using Microsoft.AspNetCore.Components.WebAssembly.Http
@using Microsoft.JSInterop
@using ScreenSound.Web
@using ScreenSound.Web.Layout
@using ScreenSound.Web.Response
@using ScreenSound.Web.Requests
@using ScreenSound.Web.Services
@using MudBlazor
@using MudBlazor.Utilities
COPIAR CÓDIGO
Após, acessamos o arquivo Program.cs. Precisamos registrar o serviço do MudBlazor, então, adicionamos na linha 10, passamos builder.Services.addMudServices().

//Código omitido

builder.Services.AddMudServices();

//Código omitido
COPIAR CÓDIGO
Em sequencia, abrimos o arquivo index.html. Vamos utilizar o MudBlazor para fazer o layout que vamos utilizar. Nesse caso, precisaremos do CSS.

Para isso, removemos a linha 9. No lugar, passamos o seguinte trecho de código:

//Código omitido

<link href="_content/MudBlazor/MudBlazor.min.css" rel="stylesheet" />

//Código omitido
COPIAR CÓDIGO
Outra configuração que precisamos fazer é referente ao JavaScript, para ser utilizado no nosso index.html. Então, nesse mesmo arquivo, na linha 30, passamos a linha de código abaixo:

//Código omitido

<script src="_content/MudBlazor/MudBlazor.min.js"></script>

//Código omitido
COPIAR CÓDIGO
Lembrando que o index.html é referente à nossa página única, a aplicação Blazor é um SPA, Single Page Application (Aplicativo de Página Única).

Feito isso, completamos as configurações. Então, salvamos. O próximo passo é utilizar o MudBlazor. Queremos alterar o layout da nossa aplicação, incluindo o menu, para ficar mais próximo do template do Figma.

Utilizando o MudBlazor
Acessamos o arquivo MainLayout.razor. Usaremos os componentes do MudBlazor para começar a compor o layout, então selecionamos todo o código desse arquivo e apagamos. No lugar, colamos o seguinte trecho de código:

@inherits LayoutComponentBase

<MudThemeProvider />
<MudDialogProvider />
<MudSnackbarProvider />

<MudLayout>
    <MudAppBar Color="Color.Surface" Fixed="true" Elevation="2">
        <MudImage Src="images/screensound-logo.png"></MudImage>
    </MudAppBar>
    <MudDrawer Open="true" ClipMode="DrawerClipMode.Always" Elevation="2">
        <NavMenu></NavMenu>
    </MudDrawer>
    <MudMainContent>
        @Body
    </MudMainContent>
</MudLayout>
COPIAR CÓDIGO
Adicionamos o MudThemeProvider, MudDialogProvider e MudSnackbarProvider. Definimos o MudLayout, que é o layout da nossa página e o MudImage que é onde exibiremos o logo do Screensound.

Esse logo continua na pasta "images", que ainda não temos no projeto. Para incluí-la, a copiamos. Depois, em wwroot, clicamos com o botão direito e em "Colar". Repare que agora, na pasta images, temos o screensound-logo.png.

Outra configuração importante é no menu lateral, também o substituiremos por um código MudBlazor. Acessamos o arquivo NavMenu.razor. Pressionamos "Ctrl + A" para selecionar tudo e depois apagamos. No lugar, passamos o seguinte código:

<MudNavMenu Class="mud-width-full mt-4">

    <MudNavLink Href="/" Icon="@Icons.Material.Filled.Home">Home</MudNavLink>
    <MudNavGroup Title="Artistas" Icon="@Icons.Material.Filled.People" Expanded="true">
        <MudNavLink Href="/Artistas">Exibir</MudNavLink>
        <MudNavLink Href="/CadastrarArtista">Cadastrar</MudNavLink>
    </MudNavGroup>
    <MudNavGroup Title="Músicas" Icon="@Icons.Material.Filled.QueueMusic" Expanded="true">
        <MudNavLink Href="/MusicasPorArtista">Músicas por artista</MudNavLink>
        <MudNavLink Href="/MusicasPorGenero">Músicas por gênero</MudNavLink>
        <MudNavLink Href="/CadastrarMusica">Cadastrar</MudNavLink>
    </MudNavGroup>
</MudNavMenu>
COPIAR CÓDIGO
Definimos um MudNavGroup para artistas e músicas, onde estamos referenciando os componentes que criamos na atividade prática. Por exemplo, artistas, a página de cadastro de artistas que implementaremos em sequência. Feito isso, salvamos.

Agora, podemos testar o MudBlazor para conferirmos como será renderizada nossa aplicação. Na barra de menu superior, clicamos no botão "Iniciar", indicado por um triangulo deitado na cor verde.

Feito isso, abrimos o navegador. Repare que estamos utilizando o MudLayout do MudBlazor e o menu lateral também já está mais interessante, com Home, Artistas, Exibir, Cadastras, Música e outros.

Nesse vídeo, adicionamos ao nosso projeto a biblioteca MudBlazor, que entrega uma série de componentes prontos. Dessa forma, não precisamos criar componentes do zero.

Na sequência, continuaremos implementando nossa solução, fazendo a parte de cadastro de artistas e músicas utilizando o MudBlazor.

Te esperamos lá!