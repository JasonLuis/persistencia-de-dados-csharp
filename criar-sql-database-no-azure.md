Esta atividade de preparação de ambiente tem como objetivo fazer uma revisão do passo a passo de como criar um recurso de banco de dados no portal do Azure. Este procedimento já foi visto no curso anterior.

> É de suma importância a sua execução para prosseguirmos com a aula, beleza?

Vamos iniciar acessando o portal do Azure e realizando o login. Na sequência, vamos criar um novo recurso. Para isso clique na opção “Create a resource”, conforme mostra a figura abaixo:


![alt text: Recorte da tela de serviços do Azure, destacando em vermelho a opção Create a resource.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+03.jpg)

Navegue até a opção **SQL Database**, e clique em “Create”:

![alt text: Recorte da tela do portal do Azure exibindo a opção “SQL Database”. Em destaque, a opção “Create” é indicada por uma seta vermelha. ](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+04.png)


Será aberta uma próxima página onde iremos definir a “Subscription” e o “Resource Group”. Caso ainda não tenha criado, você poderá criá-lo neste momento.


![alt text: Recorte da tela do portal do Azure, na aba “Create SQL Database”, exibindo a opção de seleção de “Subscription” e “Resource group”.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+05.png)

Ainda nesta tela, defina o nome do banco de dados e selecione o servidor. Vamos optar por criar o servidor neste momento.



![alt text: Recorte da tela do portal do Azure, na aba “Create SQL Database”, exibindo a opção de seleção do “Server” e uma seta apontando para o link de “Create new”.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+06.png)

Seremos redirecionados para outra página. Para criarmos o servidor, observe a sequência de imagens e configurações:

![alt text: Recorte da tela do portal do Azure, na aba “Create SQL Database Server”, exibindo os detalhes do servidor e destacando o campo para definição do nome do servidor e sua localização.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+07.png)

Ainda na tela de definição do servidor de banco de dados, você deverá definir o modo de autenticação:


![alt text: Recorte da tela do portal do Azure, na aba “Create SQL Database Server”, destacando o método de autenticação, onde a opção marcada foi “Use SQL authentication”.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+08.png)

Optamos por utilizar a “SQL authentication” para informarmos o usuário e a senha de conexão com o  nosso banco de dados. Após clicar em “OK”, você será redirecionado para a tela de criação do banco de dados.

Neste momento, é importante ter atenção às configurações “Workload environment”, que deve trazer selecionada a opção “Developement”. Nesta opção você economiza mais os seus créditos no Azure.  

Depois, em “Backup storage redundancy”, vamos definir “Locally-redundant backup storage” como local. Observe a imagem abaixo:



![alt text: Recorte da tela do portal do Azure, na aba “Create SQL Database”, exibindo as configurações de “Workload environment”, destacando com uma seta vermelha a opção “Development”. Na mesma imagem, também se encontra a configuração de ”Backup storage redundancy”, destacando com uma seta vermelha a opção “Locally-redundant backup storage”.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+09.png)


Depois, clique em “Review + create” e espere a plataforma disponibilizar os recursos para você. Esse processo de deploy pode levar alguns minutos. 

Na sequência, clique em “Go to resource”:


![alt text: Recorte da tela do portal do Azure exibindo a mensagem “Your deployment is complete” (“Seu deploy foi concluído”) e a opção “Go to resource”.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+10.png)

Agora, na página de overview, no menu lateral esquerdo, navegue até a opção “Connection strings” e copie a string de conexão na opção configurada de “SQL authentication”. Confira a figura abaixo:

![alt text: Recorte da tela do portal do Azure exibindo as opções de string de conexão do recurso de banco de dados criado.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+11.png)

É necessário também habilitar uma configuração no recurso do servidor de banco de dados no Azure. Sendo assim, clique em “Home”, depois no ícone “Resource Groups” e, na sequência, clique em “rg-screensound”, como na imagem a seguir:

![alt text: Recorte da tela do portal do Azure exibindo “rg-screensound” na página dos “Resource groups”.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+12.png)

Navegue até o recurso do servidor criado e clique nele. No menu lateral esquerdo, vá até a opção “Security” e, em seguida, em “Networking”:

![alt text: Recorte da tela do portal do Azure exibindo nas configurações do servidor de banco de dados, destacando com uma seta vermelha a opção “Networking”.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+13.png)

Na página de “Networking”, em “Public Access”, marque a opção “Selected networks”:

![alt text: Recorte da tela do portal do Azure exibindo as configurações de acesso público, na aba “Networking”. Em destaque, com uma seta vermelha, encontra-se a opção “Selected networks”.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+14.png)

Na sessão de “Firewall rules”, você deve adicionar seu endereço IPV4. Para isso, você só precisa clicar no botão com sinal de `+`. Observe a figura abaixo:

![alt text: Recorte de tela do portal do Azure exibindo as configurações em “Firewall rules”, destacando o IP da máquina do cliente a ser adicionado para liberação de acesso.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+15.png)

Essa configuração é importante para que a sua máquina local consiga acessar o servidor de banco de dados no Azure. Outra opção que vamos marcar é a opção “Allow Azure services and resources to access this server” (“Permitir que os serviços e recursos do Azure acessem este servidor”) para garantir que os recursos criado na nossa conta no Azure consigam acessar o nosso servidor também sem maiores problemas.

![alt text: Recorte da tela do portal do Azure, permitindo acesso dos recursos do servidor do Azure ao servidor de banco de dados.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+16.png)

Após todas essas configurações, precisamos acessar nosso projeto e executar as migrações, que criam as tabelas e os dados iniciais do projeto do Screensound.  É agora que iremos configurar a string de conexão em nosso projeto de dados.

Sendo assim, acesse o projeto `ScreenSound.Shared.Dados` e, na classe de contexto, altere a string de conexão para a string copiada do Azure. Abaixo, temos um exemplo: 


```
private string connectionString = "Server=tcp:screensoundserver.database.windows.net,1433;Initial Catalog=screensoundV0;Persist Security Info=False;User ID=andre;Password={SUA_SENHA};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
```
Configure a execução do projeto para **ScreenSound**, como mostra a figura abaixo:

![alt text: Recorte da tela do Visual Studio Community 2022, onde selecionamos o projeto ScreenSound para execução.](https://caelum-online-public.s3.amazonaws.com/3508-asp-net/IMAGENS/Imagem+17.png)

Agora, vamos executar nossas migrações. Para isso, abra o **Console do Gerenciador de Pacotes** e, em projeto padrão, escolha `ScreenSound`. No prompt, digite o comando:
```
Update-Database
```

A saída esperada é similar a apresentada abaixo:
```
Applying migration ‘20231110171503_projetoInicial’.
Applying migration ‘20231128021008_PopularTabela’.
Applying migration ‘20231128015225_AdicionarColunaAnoLancamento’.
Applying migration ‘20231128021008_PopularMusicas’.
Applying migration ‘20231128015225_RelacionarArtistaMusica’.
Applying migration ‘20231128015225_AdicaoDaTabelaGenero’.
Applying migration ‘20231128021008_RelacionandoMusicaGenero’.
```
Essa é a configuração inicial a ser executada para esta aula. 

Temos muitos passos, mas na utilização do Azure como plataforma de publicação, você deverá fazer isso com bastante frequência. 



### Acessando o banco de dados do Azure pelo Visual Studio 

Caso queira testar, você pode navegar na estrutura pelo Azure ou **configurar o Visual Studio para acesso**. Veja como:


1º No Visual Studio, vá ao Menu e clique em “Exibir”;

2º Vá até a opção “Pesquisador de objetos do SQL Server”;

3º Com o botão direito do mouse, clique em “Adicionar SQL Server”.

4º Na tela que se abre, configure as seguintes informações:

* Nome do servidor no Azure até a definição da porta
* Modo de autenticação
* Usuário e senha de acesso 
* Nome do banco de dados

Feitos esses passos, clique em “Conectar”.

Ao final, você terá acesso pelo Visual Studio ao seu banco de dados hospedado na plataforma do Azure. 

Continue focado e bons estudos!