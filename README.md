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
