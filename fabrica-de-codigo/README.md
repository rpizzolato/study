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
>**Observação**
>
>Lembrando para os *IntelliSense* funcionarem, é necessário ativar a extensão do C# aperte **F1**, digite **OminiSharp: Select Project** e escolha a sua `sln`

- criaremos um arquivo `.editorconfig` na raiz do projeto e colocaremos o conteúdo disponível na documentação da [Microsoft](https://docs.microsoft.com/pt-br/dotnet/fundamentals/code-analysis/code-style-rule-options). Logo após, faz o reload do Visual Studio (**F1** digita **Reload Window**)