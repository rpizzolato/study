## Acompanhando aula de .NET do Canal Fábrica de Código
### Usando o .NET com o GraphQL

- `dotnet new webapi -n ToDoApi`
- cria uma pasta `ToDoApi` joga tudo dentro
- cria uma `sln` com `dotnet new sln`
- adiciona a `sln` no projeto, com `dotnet sln add .\ToDoApi`
- faz um `dotnet build` para testarmos se está tudo OK
- adiciona o pacote `dotnet add package HotChocolate.AspNetCore` dentro do projeto
- exclui a classe `WeatherForecast.cs`, os `Controllers`, `Properties`
- o arquivo `Startup.cs` deverá ficar da seguinte forma:
```C#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL();
                });
        }
    }
}
```
>**Observação 1**
>
>Lembrando para os *IntelliSense* funcionarem, é necessário ativar a extensão do C# aperte **F1**, digite **OminiSharp: Select Project** e escolha a sua `sln`

>**Observação 2**
>
>Para que alguma propriedade possa receber o valor `null`, usamos um ponto de interrogação `?` no tipo da propriedade, como por exemplo `public string? Title`. Ou podemos usar dessa forma `public Nullable<string> Title`, que teremos o mesmo efeito

- criaremos um arquivo `.editorconfig` na raiz do projeto e colocaremos o conteúdo disponível na documentação da [Microsoft](https://docs.microsoft.com/pt-br/dotnet/fundamentals/code-analysis/code-style-rule-options). Logo após, faz o reload do Visual Studio (**F1** digita **Reload Window**)

### Conexão com o Banco de dados
- necessário instalar a biblioteca `dotnet add package Microsoft.EntityFrameworkCore.InMemory` e importar em `TodoContext.cs` a linha `using Microsoft.EntityFrameworkCore;`. O `InMemory` é um banco de dados para teste apenas, que fica em memória. Para outros tipos de banco de dados, pode ser consultado [aqui](https://docs.microsoft.com/pt-br/ef/core/providers/?tabs=dotnet-core-cli)