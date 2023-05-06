###📰 GateNews API

Bem-vindo à API do GateNews, uma API de gerenciamento de notícias desenvolvida em ASP.NET Core 7.0 usando padrões de arquitetura DDD (Domain-Driven Design) e autenticação JWT. Este projeto utiliza o banco de dados SQL Server para armazenamento de dados.
🌟 Funcionalidades

    CRUD completo de notícias, autores e categorias.
    Filtragem de notícias por título, autor, categoria, data e palavras-chave.
    Autenticação e autorização usando JWT (JSON Web Tokens).
    Validação de conteúdo inapropriado usando a API OpenAI.
    API RESTful seguindo as boas práticas de design.

#🚀 Começando

Para começar a usar a API do GateNews, siga as etapas abaixo:

    Clone este repositório em sua máquina local.
    Configure a string de conexão do SQL Server no arquivo appsettings.json.
    Execute o comando dotnet restore para restaurar os pacotes NuGet necessários.
    Execute o comando dotnet run para iniciar a aplicação.

A API estará disponível no endereço http://localhost:5000.
#📚 Rotas

A API do GateNews possui as seguintes rotas:

Notícias

    GET /api/news/title/{title}/page/{pageNumber}: Pesquisa notícias pelo título.
    GET /api/news/author/{authorFullName}/page/{pageNumber}: Pesquisa notícias pelo nome completo do autor.
    GET /api/news/category/{categoryCode}/page/{pageNumber}: Pesquisa notícias pela categoria.
    GET /api/news/category/{categoryCode}/author/{authorFullName}/page/{pageNumber}: Pesquisa notícias pela categoria e nome completo do autor.
    GET /api/news/date/{pageNumber}: Retorna todas as notícias ordenadas pela data de publicação.
    POST /api/news/search: Pesquisa notícias por palavras-chave.
    POST /api/news: Cria uma nova notícia.
    PUT /api/news/{id}: Atualiza uma notícia existente.
    DELETE /api/news/{id}: Exclui uma notícia.

Autores

    GET /api/authors/{id}: Retorna um autor específico pelo ID.
    GET /api/authors/name/{fullName}: Retorna um autor específico pelo nome completo.
    DELETE /api/authors: Exclui um autor autenticado.
    PUT /api/authors: Atualiza um autor autenticado.

Autenticação

    POST /api/auth/register: Cria um novo usuário.
    POST /api/auth/login: Autentica um usuário existente.
    POST /api/auth/change-password: Altera a senha do usuário autenticado.

Categorias

    GET /api/category: Retorna todas as categorias.


#🛠️ Tecnologias utilizadas

    ASP.NET Core 7.0
    Entity Framework Core 7.0
    SQL Server
    AutoMapper
    Dapper
    Swagger
    JWT (JSON Web Tokens)
    OpenAI API

#📖 Conclusão

A API do GateNews é uma solução completa para gerenciamento de notícias, seguindo as melhores práticas de desenvolvimento e arquitetura. Com esta API, você pode criar, atualizar, excluir e pesquisar notícias, autores e categorias, além de implementar autenticação e autorização seguras usando JWT. A API também possui integração com a API OpenAI para validar o conteúdo das notícias e garantir que não haja conteúdo inapropriado.

Além disso, a API segue os princípios RESTful, facilitando a integração com outras aplicações e garantindo escalabilidade e manutenibilidade.

Esperamos que você aproveite este projeto e que ele atenda às suas necessidades de gerenciamento de notícias. Se você tiver alguma dúvida ou sugestão, sinta-se à vontade para abrir uma issue ou enviar um pull request.

Boa sorte e feliz codificação! 🚀👩‍💻👨‍💻


##English Version

###📰 GateNews API

Welcome to the GateNews API, a news management API developed in ASP.NET Core 7.0 using Domain-Driven Design (DDD) architecture patterns and JWT authentication. This project utilizes SQL Server for data storage.

#🌟 Features

    Full CRUD for news, authors, and categories.
    News filtering by title, author, category, date, and keywords.
    Authentication and authorization using JWT (JSON Web Tokens).
    Inappropriate content validation using the OpenAI API.
    RESTful API following good design practices.

#🚀 Getting Started

To start using the GateNews API, follow the steps below:

    Clone this repository on your local machine.
    Set up the SQL Server connection string in the appsettings.json file.
    Run the dotnet restore command to restore the required NuGet packages.
    Run the dotnet run command to start the application.

The API will be available at http://localhost:5000.

#📚 Routes

The GateNews API has the following routes:

News

    GET /api/news/title/{title}/page/{pageNumber}: Search news by title.
    GET /api/news/author/{authorFullName}/page/{pageNumber}: Search news by author full name.
    GET /api/news/category/{categoryCode}/page/{pageNumber}: Search news by category.
    GET /api/news/category/{categoryCode}/author/{authorFullName}/page/{pageNumber}: Search news by category and author full name.
    GET /api/news/date/{pageNumber}: Returns all news sorted by publication date.
    POST /api/news/search: Search news by keywords.
    POST /api/news: Create a new news article.
    PUT /api/news/{id}: Update an existing news article.
    DELETE /api/news/{id}: Delete a news article.

Authors

    GET /api/authors/{id}: Returns a specific author by ID.
    GET /api/authors/name/{fullName}: Returns a specific author by full name.
    DELETE /api/authors: Delete an authenticated author.
    PUT /api/authors: Update an authenticated author.

Authentication

    POST /api/auth/register: Create a new user.
    POST /api/auth/login: Authenticate an existing user.
    POST /api/auth/change-password: Change the password of an authenticated user.

Categories

    GET /api/category: Returns all categories.

#🛠️ Technologies used

    ASP.NET Core 7.0
    Entity Framework Core 7.0
    SQL Server
    AutoMapper
    Dapper
    Swagger
    JWT (JSON Web Tokens)
    OpenAI API

#📖 Conclusion

The GateNews API is a comprehensive solution for news management, following the best development and architecture practices. With this API, you can create, update, delete, and search news, authors, and categories, as well as implement secure authentication and authorization using JWT. The API also features integration with the OpenAI API for validating news content and ensuring there is no inappropriate content.

Additionally, the API follows RESTful principles, making it easy to integrate with other applications and ensuring scalability and maintainability.

We hope you enjoy this project and that it meets your news management needs. If you have any questions or suggestions, please feel free to open an issue or submit a pull request.

Good luck and happy coding! 🚀👩‍💻👨‍💻