# Piece-of-Work
Piece of Work é um app web para gerenciar suas tarefas.

# Estrutura
- O back-end foi desenvolvido em .net e foi dividido em três projetos:
	* DataModel
		- Este projeto é responsável pelo gerenciamento das Migrações do EntityFramework como pelo modelo de dados.
	* WebApi
		- O WebApi foi desenvolvido através de uma aplicação console com um servidor Self-Host utilizando Owin.
		- As rotas estão no controller TaswWorksController;
		- A regra de negócio está TaskWorkService;
		- Foi incluído ao projeto a ferramenta Swagger, para documentar e expor uma api de teste da aplicação: para o uso da ferramenta inicie a aplicação e acesse 'http://localhost:9000/swagger'.
	* WebApi.Test
		- Projeto para testar as regras de negócio contidas no serviço do projeto WebApi.
- O front-end foi desenvolvido em javascript com utilização das seguintes bibliotecas e frameworks:
	* Angularjs.
	* Bootstrap.
	* Font-awesome.
	* Angular-ui-bootstrap.
	
	Ferramentas necessárias para inicar:
	* Nodejs
	* Npm
	
	Para iniciar o servidor que lidará com os arquivos do front-end o usuário deve realizar o clone do repositório, acessar a pasta PoW.App e digitar o comando '''npm install'' (esse comando realiza o download dos pacotes necessários das bibliotecas acima mencionadas). Após este passo, o usuário deve digitar '''node server.js''' (esse comando iniciará o servidor na porta 9001). 
	Para acessar a interface do sistema acesse a url 'http://localhost:9001'.
