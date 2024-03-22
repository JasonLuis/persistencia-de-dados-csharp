Continuando a ScreenSound, nossa aplicação web já permite o cadastro de músicas e de artistas, assim como a edição destes. No cadastro de artistas, inclusive, conseguimos fazer também o upload de imagens. Nessa próxima fase do nosso projeto, nosso objetivo é publicar a nossa solução na nuvem, no **Azure**. 

Deixamos uma atividade "Preparando o ambiente", na qual se configura e cria o banco de dados na sua conta do Azure. Agora vamos configurar novamente a nossa API no Azure para ser consumida pela nossa aplicação. Começaremos configurando o nosso projeto.

No nosso projeto da API, temos o `appsettings.json`, com a nossa configuração de string de conexão, que aponta para o nosso banco na nuvem. Temos também a configuração `appsettings.Development.json`, que aponta para o banco local. Essa configuração também precisa ir para a nuvem.

Portanto, vamos abrir a conta do Azure para configurar um novo recurso, que é o *App Configuration* (Configuração de App). Clicamos em "*App Configuration*" na barra superior de opções de serviço, que tem o ícone de uma nuvem com duas engrenagens, e acessamos outra página.

Na página do *App Configuration*, no canto esquerdo, abaixo do nome da página, clicamos no botão "*Create*" (Criar) para criarmos uma nova configuração de app. Após clicarmos em "*Create*", a página atualiza para um formulário chamado "*Create App Configuration*".

Na seção "*Project Details*" (Detalhes do Projeto), temos o campo "*Subscription*", já preenchido com "Azure for Students". Abaixo dele, temos o "*Resouce group*", onde associaremos o "rg-screensound", que definimos na atividade "Preparando o ambiente".

Também precisamos preencher o "*Resource name*", na seção "*Instance Details*". No caso, preenchemos como "screensound-configuration". Por fim, mudamos o "*Princing tier*" para "*Free*" e clicamos no botão azul "Review + Create", no canto inferior esquerdo.

> **Project Details**
> 
> Subscription: Azure for Students
>> Resource group: rg-screensound
> 
> **Instance Details**
> 
> Location: Brazil South
> 
> Resouce name: screensound-configuration
> 
> Pricing tier: Free

Após revisarmos que as informações estão corretas, podemos clicar em "Create", no canto inferior esquerdo. A página atualiza, informando que o recurso foi criado, e podemos clicar no botão "*Go to resource*" (Ir para o recurso), no centro da página.

Com isso, abrimos a página do "screensound-configurariam" e podemos configurar nosso componente de configuração da nossa solução no Azure. Na coluna do lado esquerdo, clicamos em "*Configuration Explorer*". Com essa seção aberta no lado direito da tela, na barra superior da seção, clicaremos em  "*Create > Key-value*", que são, respectivamente, a primeira opção da barra e a primeira opção do menu. 

Assim, abrimos o formulário "Create" na lateral direita da página. O primeiro campo é o "*Key*" (Chave), que será igual a do nosso arquivo `appsettings.json`. Retornando ao projeto, copiamos a chave string que passamos para no arquivo `Program.cs`, no meu caso, `ConnectionStrings: ScreenSoundDB`. De volta ao Azure, colamos a chave no campo "Key".

Abaixo temos o campo "*Value*" (Valor), que também copiaremos o do nosso projeto, dessa vez do arquivo `appsettings.json`. O valor é tudo que está dentro da string `ScreenSoundDB`. Copiamos tudo, sem as aspas, e colamos no campo "*Value*" do Azure. Em seguida, clicamos no botão azul escrito "*Apply*" (Aplicar), no canto inferior esquerdo do formulário. Assim, definimos a nossa string de conexão numa configuração do Azure. Nossa API vai olhar para esse componente, para buscar a string de conexão.

Para fazer o uso desse *App Configuration*, precisamos voltar ao nosso projeto e implementar um recurso. O primeiro passo é usarmos o gerenciador de pacotes do NuGet no projeto. Portanto, no explorador de arquivos da IDE, clicamos com o botão direito na pasta "ScreenSound.API" e selecionamos "Gerenciar Pacotes do NuGet".

Com o gerenciador aberto, clicamos na barra de pesquisa, no canto superior esquerdo, e buscamos pela biblioteca "AppConfiguration". Entre os resultados, selecionamos a `Microsoft.Azure.AppConfiguration.AskNetCore` e, na guia de informações sobre a biblioteca, clicamos no botão "Instalar". Uma janela se abre no centro da tela e clicamos no botão "Eu aceito", para aceitarmos as configurações.

Com a nossa biblioteca instalada, vamos injetar no nosso `Program.cs` uma configuração para utilizar o nosso *App Configuration*. Portanto, no explorador de arquivo, clicamos duas vezes no `Program.cs` para abri-lo e, na linha 12, abaixo do `WebApplication.CreateBuilder()`, vamos adicionar o seguinte código:

```c#
# código omitido

var builder = WebApplication.CreateBuilder()

builder.Host.ConfigureAppConfiguration(config =>
{
	var settings = config.Build();
	config.AddAzureAppConfiguration("String");
});

# código omitido
```

Essa `"String"` dentro do `AddAzureAppConfiguration()` está no Azure. Podemos voltar ao Azure e, no menu da esquerda, na seção "*Settings*", selecionaremos a opção "*Access keys*". Copiaremos a "Connection string", que é a terceira opção de string, clicando no botão azul com o ícone de copiar, no final do campo.

> **Dica**: Na barra superior da seção, temos o botão "*Values*", que podemos clicar para visualizar ou ocultar os valores das chaves.

Após copiarmos a chave, podemos voltar ao arquivo `Program.cs` e colar a chave que copiamos no lugar de `string`. Depois, salvamos as alterações e recompilamos o projeto.

```c#
# código omitido

var builder = WebApplication.CreateBuilder()

builder.Host.ConfigureAppConfiguration(config =>
{
	var settings = config.Build();
	config.AddAzureAppConfiguration("Endpoint=https://screensound-configuration.azconfig.io;Id=3HVQ;Secret=wG635SERfsnjk+sSDG5DFAF5=");
});

# código omitido
```

Após garantirmos que não houve nenhum erro na compilação, para evitar qualquer erro. Agora, queremos publicar a nossa API no Azure. Portanto, vamos utilizar também o recurso próprio do Visual Studio para isso. Com o botão direito, clicamos sobre "ScreenSoundAPI" e selecionamos a opção "Publicar". Com isso, a janela "Publicar" aparecer no centro da tela. Nela, selecionaremos o "Azure" como destino

Antes de prosseguirmos, precisamos definir um Web App no Azure. Então, retornando à página "Home" do Azure, na barra superior dos serviços, clicaremos em "*Create a resouce*". Na página de recursos, selecionaremos a opção "Web App". Ao clicarmos em "Web App", abrimos o formulário "Create Web App". 

A seção "*Project Details*" é parecida com a do formulário anterior, então basta adicionarmos o "rg-screensound" como *Resource Group*. Em seguida, nomearemos o Web App como "screensound-api", preenchendo o campo "*Name*" (Nome). No campo "*Runtime stack*", selecionaremos o ".NET 8 (LTS)", que é o mais recente na data que esse vídeo foi gravado.

Em "*Region*" (Região), selecionamos "Brazil South" e, no "*Princing plan*", selecionaremos "Free F1". Depois, voltaremos para o começo do formulário e, na parte superior, onde estão as abas com as páginas do formulário, selecionamos "*Monitoring*". Nessa seção, o primeiro campo é o "*Enable Applications Insights*", que está marcado como "*Yes*". Marcaremos como "*No*". Feito isso, clicaremos no botão "*Review + Create*" e, após revisar as opções, no botão "Create", ambos no canto inferior esquerdo.

Com o recurso criado, clicamos no botão "*Go to resource*", no centro da tela. Agora nossa API já no ar e faremos a seguinte configuração: voltemos para o Visual Studio, onde a janela "Publicar" continua aberta.

Nela, selecionaremos a opção "Azure" para o destino e clicaremos no botão "Próximo", na parte inferior direita da janela. Depois, selecionamos "Serviço de Aplicativo do Azure" e clicamos em próximo. Na janela, aparecerá a pasta "rg-screensound", que clicaremos na seta do canto esquerdo e selecionamos o "screensound-api", o nosso Web App que acabamos de criar no Azure.

Clicamos em "Próximo" e, na janela seguinte, marcamos a caixa de seleção "Ignorar esta etapa", na parte inferior da janela. Clicamos em "Próximo" e deixamos o "Publicar (gera o arquivo pubxml)", e clicamos em "Concluir", na parte inferior direita da janela.

Uma nova aba é aberta no Visual Studio para criarmos um perfil de publicação. Na parte superior, abaixo da mensagem do "screensound-api", temos o botão "Novo perfil" e, ao lado dele, o menu suspenso "Mais ações. Clicaremos em "Mais ações > Editar".

Abrimos uma nova janela "Publicar" no centro da tela, com seção "Configurações" aberta. No campo "Modo de Implantação", mudaremos de "Dependente do framework" para "Autossuficiente" e clicamos no botão "Salvar", na parte inferior direita da janela. Com isso, a janela fecha e voltamos para aba de publicação no Visual Studio, onde clicaremos no botão "Publicar", no canto direito da mensagem do "screensound-api".

O perfil foi criado e executado. Notem que uma das informações que temos na aba é o campo "Site", com o endereço da nossa API. Clicando nesse link, ele abre outra janela no navegador, mas que retorna um erro de página não encontrada.

Isso porque temos um Swagger configurado na nossa API, então, ao adicionarmos `/swegger/index.html` no final do endereço, acessamos nossa API. Ao selecionarmos, por exemplo, "/Artistas" e depois em "Try out" e "Execute", temos o retorno dos dados. Portanto, estamos consumindo a nossa API que acabou de ser publicada no endereço [screensound-api.azurewebsites.net](https://screensound-api.azurewebsites.net/swagger/index.html).

Fizemos a publicação da nossa API no Azure e a configuração de um novo recurso do Azure, que é o ***App Configuration***, onde definimos a nossa string de conexão, que está sendo utilizada agora pela nossa API. Continuaremos a publicação da nossa aplicação a seguir. Começamos pela nossa API e depois partimos para a publicação da nossa aplicação web com o Blazor.