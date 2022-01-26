## ANOTAÇÕES BOOTCAM DIO AVANADE .NET (DECOLA TECH 2ª EDIÇÃO)

- Bootcamp disponível em [https://web.dio.me/track/decola-tech-2a-edicao](https://web.dio.me/track/decola-tech-2a-edicao)

### Sumário
- [Primeiros passos com .NET](#Primeiros-passos-com-.NET)
- [ Programando com Orientação a Objeto com C#](#Programando-com-Orientação-a-Objeto-com-C#)

# MENTORIA #3
- Link da [mentoria](https://www.youtube.com/watch?v=bTgGQrU-WXU).

## ORIENTAÇÃO A OBJETOS (POO)
- É um paradigma de programação;
- Reaproveitamento de código;
- Aproximar o código de alguma coisa do mundo real;
- Facilita manutenção de código.

## 4 PILARES DA ORIENTAÇÃO A OBJETOS
- HERANÇA
- ENCAPSULAMENTO (um exemplo simples, seria quando no método construtor, você já *encapsula* o que vai querer que seja feito por padrão na instanciação do objeto)
- POLIMORFISMO
- ABSTRAÇÃO

#### EXTENSÕES VSCODE
- [C# Microsoft](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [C# Extensions](https://marketplace.visualstudio.com/items?itemName=kreativ-software.csharpextensions)

#### SERÁ UTILIZADO O .NET 5
- download do [.NET](https://dotnet.microsoft.com/en-us/download/dotnet)
- verificar qual versão do **DotNet** está instalado: `dotnet --list-sdks`
- criar um _global json_ para especificar qual versão do .NET irá utilizar: `dotnet new globaljson` 
- criar uma aplicação de console: `dotnet new console`
- rodar o programa: `dotnet run`
- `Ctrl + .`: sugestões de autocompletar

### Program.cs
- **namespace** seria como uma pasta virtual, para separar o código.
- `ToString()`, normalmente retorna o *namespace* de onde o objeto está, pois já é um método padrão de *object*. Por exemplo, `carro.ToString()`. No entanto é possível sobrescrever esse método ao seu gosto (*override*), como por exemplo (podemos chamar somente o objeto, que ele irá chamar a o `ToString()`):
```C#
public override string ToString() {
    return "Meu nome é " + this.Nome + "\n"
                + "Nível: " + this.Nivel + "\n"
                + "Classe: " + this.ClasseFantastica + "\n"
                + "Ponto de vida" + this.PontosDeVida + "\n"
                + "Ponto de magia" + this.PontosDeMagia + "\n";
}
```

### Entities
- Por convenção, as demais classes que serão instanciados como objetos, vão para um diretório chamado `src/Entities`, assim, criaremos as classes `Heroi.cs` e `Mago.cs`. `Mago.cs` herda de `Heroi.cs`. A herança em C# é feito por meio da declaração `public class Mago : Heroi`. Como os atributos são comuns entre as classes pai e filho, a herança já resolve um problema de não precisar repetir os atributos, logo iremos apenas precisar sobrescrever o método `Atacar()`, pois os personagens irão atacar com armas diferentes (que no caso, diferentemente dos atributos, **não** são comuns entre si). Para sobrescrever o método `Atacar()`, na classe pai, `Heroi.cs`, iremos tornar *virtual* o método `Atacar()`, dessa forma: `public virtual string Atacar()`. **Observação:** Se precisar reescrever novamente, não é necessário usar o *override*. O método rescrito em `Mago.cs`, ficará:
```C#
public override string Atacar() {
    Random dado = new Random();
    int forcaDoAtaque = this.Nivel + dado.Next(1,10); //nível do mago tende a ser menor

    return this.Nome + " Ataca com o seu cajado e dá " +
            forcaDoAtaque + " de dano.";
}
```

# Primeiros passos com .NET
- Inicialmente a Microsoft começou com o Basic, nos anos 70;
- Nos anos 80 surge o DOS, como OS padrão para computadores IBM;
- Anos 90, criou as IDEs e runtimes, como o Visual Studio 97(Visual Basic 5, FoxPro 5, C++ 5 e J++);
- Em 1998, lança o Visual Studio 6, com o Visual Basic 6, FoxPro 6 C++ e J++ (J++ era uma versão do Java que a Microsoft mantinha);
- Em 1999 Scott Guthrie criou uma ferramenta web com Java, chamada de ASP+ (depois passou a ser chamado de ASP Next e depois ASPX). Jason Zander ajudou na criação de um common runtime para VB e C++ (CLR). Ainda em 1999 a Sun Microsystem fez um acordo para a Microsoft não mexer mais no Java. Assim, Anders Hejlsberg começou a trabalhar no C#;
- No ano 2000, é lançado o .NET 1.0 (chamado de Next Generation Windows Services - NGWS)
- Em 2001, Miguel de Icaza começa a trabalhar no projeto Mono, sendo uma reimplementação black box do .NET, open source e multiplataforma;
- 2002 vem o lançamento do Visual Studio .NET com C# 1.0. Era conhecido como 22 linguagens, 1 plataforma:
    - C#.net
    - C++.net
    - VB.net
    - J#.net
    - Entre outras
- Em 2003 lançamento do .NET 1.1 com o Visual Studio 2003 com melhorias CLR CLR2 (pegava diversas linguagens e convertia em linguagem de máquina);
- 2005 vem o .NET 2.0 com o C# 2.0 com o Visual Studio 2005, começando a evoluir na web;
- 2007-2008 lançamento do .NET 3.5 com C# 3.0 no Visual Studio 2008, com Silverlight, WPF e WCF. Microsoft contrata time forte em open source e começam a atuar na criação do ASP.NET MVC. Começa a se falar em Windows Azure;
- 2010 lançamento do .NET 4.0 com C# 4.0 no Visual Studio 2010, também com F# (voltado para programação estruturada) com Visual Studio 2010. MS lança comercialmente o Windows Azure. Anders Hejlsberg começa a trabalhar no Typescript;
- 2011 Miguel de Icaza inicia Xamarin, que basicamente ele desenvolvia em C# aplicativos que rodam em Android e iOS;
- 2012 lançamento do .NET 4.5 com C# 5.0 no Visual Studio 2012. Lançamento do Typescript;
- 2013 lançamento do .NET 4.5.1 no Visual Studio 2013. Início do Roslyn, um novo compilador para C# e VB.NET. ASP.NET mais consolidado com MVC, Web API e SignalR
- 2014 Satya Nadella se torna CEO da MS e direciona o foco da empresa para Cloud. Criação do .NET Foundation. Windows Azure passa a se chamar Microsoft Azure. Introduzido o conceito do ASP.NET vNext, posteriormente chamado de ASP.NET Core;
- 2015 lançamento do .NET 4.6 com o C# 6.0 no Visual Studio 2015. Lançamento do Visual Studio Code;
- 2016 MS adquire a Xamarin e adiciona o produto como parte de sua stack .NET e projetos open source. Visual Studio for Mac. Lançamento do .NET Core 1.0 (reescrita do zero), open source e multiplataforma;
- 2017 lançamento do .NET Framework 4.7 com C# 7.0 no Visual Studio 2017. Lançamento do .NET Core 2.0 com C# 7.0 no Visual Studio 2017, Visual Studio Code ou Visual Studio for Mac 2017;
- 2019 lançamento do .NET Framework 4.8 com C# 7.3 no Visual Studio 2017. Lançamento do .NET Core 3.0 com C# 8.0 no Visual Studio 2019, Visual Studio Code ou Visual Studio for Mac 2019;
- 2020 .NET Framework está pronto na versão 4.8, que passará a se chamar .NET 5. (.NET Framework é passado)

### Parte 2: O que é, Como e Onde usar .NET
#### O que é?
- Infraestrutura para desenvolvimento de software criada pela Microsoft.
- Uma aplicação .NET é desenvolvida para e roda em uma das seguintes **implementações** do .NET:
    - .NET Core
    - .NET Framework
    - Mono
    - Universal Windows Platform (UWP). Ex Xbox, IoT, etc
#### Como?
- Cada implementação inclui um ou mais .NET Runtimes (ambiente de execução):
    - .NET Core: CoreCLR e CoreRT
    - .NET Framework: CLR
    - Mono: Mono Runtime
    - UWP: .NET Native
- Atualmente a Microsoft desenvolve e suporta 3 linguagens para .NET:
    - C#
    - F#
    - VB

#### Onde?
- Desktop: WPF, Windows Forms, UWP
- WEB: ASP.NET
- Cloud: Azure
- Mobile: Xamarin
- Gaming: Unity
- IoT: ARM32, ARM64
- AI: ML.NET, .NET for Apache Spark

### Links úteis
- Download .NET: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
- Documentação do .NET: [https://docs.microsoft.com/en-us/dotnet](https://docs.microsoft.com/en-us/dotnet)
- .NET Foundation: [https://dotnetfoundation.org](https://dotnetfoundation.org)

### Preparando o ambiente
- instalação padrão seguindo o [link](https://dotnet.microsoft.com/download). Depois no terminal, testar com o comando `dotnet --version`

### Conhecendo o CLI do .NET
- `dotnet --help` ou `dotnet -h`: traz opções do uso dos comandos;
- `dotnet --info`: traz informações das instalações do .NET no computador, assim como informações do sistema;
- `dotnet new --help`: traz a "documentação" do `new`. Pode ser usado para outra varição listada no `dotnet --help`. O `dotnet new --help` traz ainda os *Templates*

### Criando uma aplicação Console
- Aplicação console é uma aplicação que será executada a partir de um console, bash, etc.
- `dotnet new console -n DigitalInnovationOne`: cria nosso projeto
- `dotnet restore`: restaura pacotes que estão no arquivo `csproj` (parecido com `npm install`)
- `dotnet build`: faz o `restore` e cria `dll` dentro da pasta `bin`
- `dotnet run`: roda o programa
#### Dica de String Interpolation
```C#
string name = "Mark";
var date = DateTime.Now;

// Composite formatting:
Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
// String interpolation:
Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
// Both calls produce the same output that is similar to:
// Hello, Mark! Today is Wednesday, it's 19:40 now.
```

### Conhecendo o C#
- É uma linguagem elegante, orientada a objetos e fortemente tipada. A sintaxe do C# é similar a do C, C++ ou Java. Suporta os conceitos de encapsulamento, herança e polimorfismo (OO)
- Os programas C# são executados no .NET, o que inclui:
    - CLR (Common Language Runtime)
    - Conjunto unificado de bibliotecas de classes
- Atualmente o compilador do C# é o **Roslyn**

#### Como funciona o C#
- O código-fonte escrito em C# é compilado em uma linguagem intermediária (IL). O código e os recursos de IL são armazenados no disco em um arquivo executável chamado assembly, geralmente com uma extensão **.exe** ou **.dll**
- Quando o programa C# é executado, o assembly é carregado no CLR. Em seguida o CLR executará a compilação *just in time* (JIT) para converter o código IL em instruções de máquinas nativas.
- O CLR também fornece outros serviços:
    - Garbage Collector
    - Exception Handler
    - Resources Manager

### Estrutura de programa
- Os principais conceitos organizacionais em C# são:
    - Programas
    - namespaces
    - tipos
    - membros
    - assemblies
- Programas em C# consistem em um ou mais arquivos. Os programas declaram tipo, que contêm membros e podem ser organizados em namespaces. Classes e interfaces são exemplos de tipos. Campos, métodos, propriedades e eventos são exemplos de membros

#### Exemplo pratico
- [Pilha.cs](/primeiros-passos-com-dotnet/DigitalInnovationOne/Exemplo/Pilha.cs)
- [Program.cs](/primeiros-passos-com-dotnet/DigitalInnovationOne/Program.cs)
obs: caso tenha que colocar, por exemplo, o arquivo `Pilha.cs` dentro de um diretório (diretório `Exemplo`), no namespace do arquivo `Pilha.cs`, teremos que acrescentar manualmente `.Exemplo`, ficando `namespace DigitalInnovationOne.Exemplos`. Já no arquivo `Program.cs` colocamos `using DigitalInnovationOne.Exemplos;` no início do arquivo.

### Variáveis e instruções
#### Tipo de valor
- numéricos: **sbyte**, **short**, **int**, **long**, **byte**, **ushort**, **uint**, **ulong**
- caracteres unicode: **char**
- pontos flutuantes: **float**, **double**, **decimal**
- booleano: **bool**
- **enum**, **struct** e tipos **nullable** (Exemplo **int?**)

#### Tipos de referência
- Variáveis de tipos de referência armazenam referências a seus dados. É possível que duas variáveis façam referência ao mesmo objeto e, portanto, que operações em uma variável afetem o objeto referenciado pela outra variável:
- Tipos de Classe: **class**, **object**, **string**
- Tipos de Arrays: **int[]**, **int[,]**, etc...
- **interface**, **delegate**

#### Instruções
- Ações de um programa são expressas usando as instruções
```C#
{
    //um bloco permite que várias instruções sejam escritas em contextos
}
```
- Declaração de variáveis e constantes locais:
    - if
    - switch
    - while
    - do
    - for
    - foreach
- instruções auxiliares
    - break
    - continue
    - return
    - throw
    - try .. catch .. finally
    - **using**: além de ser utilizado no início dos arquivos `.cs`, podemos usá-lo para evitar o uso do `Dispose` em operações de IO (arquivos e banco de dados, por exemplo). Sem o **using**, ficaria algo do tipo:
    ```C#
    static void InstrucaoUsing(string[] args) {
        System.IO.TextWriter w;
        w = System.IO.File.CreateText("teste.txt");
        w.WriteLine("yz");
        w.Dispose();
    }
    ```

    usando o **using**, ficaria algo da seguinte forma:
    ```C#
    static void InstrucaoUsing(string[] args) {
        using (System.IO.TextWriter w = w = System.IO.File.CreateText("teste.txt"))        
        w.WriteLine("yz");
        //sem necessidade do uso do Dispose()
    }
    ```

### Arrays
#### Declaração
- Exemplo abaixo:
```C#
int[] a = new int[10];

for (int i = 0; i < a.Length; i++) {
    a[i] = i * i;
}

for (int i = 0; i < a.Length; i++) {
    Console.WriteLine($"a[{i}] = {a[i]}")
}
```
#### Array multidimensional
```C#
int[,] a2 = new int[10, 5];
int[,,] a3 = new int[10, 5, 2];
```
#### Inicializador de array
```C#
int [] a = new int[] {1, 2, 3};
//ou
int [] a = {1, 2, 3};
//ou
int[] t = new int[3];
t[0] = 1;
t[1] = 2;
t[2] = 3;
int[] a = t;
```
### Classes e Objetos em C#
- Classes são os tipos mais fundamentais de C#. Ela é uma estrutura dados que combina estado (campos) e ações (métodos).
- Objetos são instâncias de uma classe (criados com o operador **new**). As classes suportam herança e polimorfismo, mecanismos pelos quais as classes derivadas podem estender e especializar as classes base.

#### Membros
- Os membros de uma classe podem ser estáticos ou membros da instância. **Membros estáticos** pertencem a classe e membros de instância pertencem ao objeto. (Constantes, variáveis, métodos, propriedades, construtores, etc...)

#### Acessibilidade
- Cada membro de uma classe tem uma acessibilidade associada, que controla as regiões do texto do programa que podem acessar o membro.
    - **public** - acessível em qualquer local
    - **protected** - acessível apenas quando um classe herda para outra
    - **internal** - acessado apenas no *assembly* ao qual faz parte, visível apenas dentro do seu próprio *project*.
    - **private** - acessado única e exclusivamente dentro da classe ao qual está contido

#### Herança
- Uma declaração de classe pode especificar uma classe base, herdando os membros **public**, **internal** e **protected** da classe base. Omitir uma especificação de classe base é o mesmo que derivar do tipo *object*.

#### Métodos
- Um método é um membro que implementa uma  computação ou ação que pode ser executada por um objeto ou classe. Indica uma ação, logo é recomendado usar verbos, diferentes das propriedades, que normalmente são substantivos
- **static** pertence a Classe e não a instância, logo, objetos instanciados **não** terão acesso.
- **virtual** permite que uma classe filha o sobreescreva.

### Structs
- Como as classes, as structs são estruturas de dados que podem conter membros de dados e membros de ação, mas, diferentemente das classes, as structs são tipos de valor, e não requerem alocação de heap. Heap é o local na memória onde está alocado alguma referência (a struct não precisa fazer esse tipo de referência)
- Structs não aceitam herança determinada pelo desenvolvedor. São úteis para pequenas estruturas de dados que possuem semântica de valor: números complexos, pontos em um sistema de coordenadas ou pares de chave-valor em um dicionário são bons exemplos de utilização.
- **vantagem**: menos alocação de memória
- Exemplo: imagem o seguinte código, sendo ele o arquivo:`Ponto.cs`:
```C#
public class Ponto {
    public int x, y;
    public Ponto(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
```
Aí a classe Principal teria que instanciar uma lista de Pontos, conformo o seguinte:
```C#
public static void Main() {
    Ponto[] pontos = new Ponto[100];
    for(int i = 0; i < 100; i++>) {
        pontos[i] = new Ponto(i, i);
    }
}
```
Imaginem o espaço em memória que seria necessário para alocar em um uso tão simplificado. Com struct ficaria:
```C#
public struct Ponto {
    public int x, y;
    public Ponto(int x, int y){
        this.x = x;
        this.y = y;
    }
}
```
### Interfaces
- Uma interface define um contrato que pode ser implementado por classes e structs. Uma interface pode conter métodos, propriedades, eventos e indexadores.
- Uma interface não fornece implementações dos membros que define - apenas suas "assinaturas". As interfaces podem emrpegar herança múltipla
### Enums
- é um tipo de valor distinto com um conjunto de constantes noemados (status de uma Nota Fiscal, por exemplo). Exemplo de código:
```C#
enum Cor {
    Vermelho,
    Verde,
    Azul
}

//uso na aplicação
static void EscreverCor(Cor cor) {
    switch(cor) {
        case Cor.Vermelho:
            Console.WriteLine("Vermelho");
            break;
        case Cor.Verde:
            Console.WriteLine("Verde");
            break
    }
}
```
- caso não seja informado o tipo do emum, ele assume como tipo inteiro. Vejamos outro exemplo:
```C#
enum Alinhamento: sbyte {
    Esquerda = -1,
    Centro = 0,
    Direita = 1
}
```

### Praticando primeiros passos
- Será desenvolvido uma aplicação console que adiciona alunos e suas notas. Para criar o projeto, use `dotnet new console -n Revisao`
- **dica**: quando precisar usar a declaração de uma variável sem saber o tipo, use `var nome_variavel;`, que o C# irá tentar atribuir de forma correta o determinado tipo
- quando for ler a nota do aluno, usaremos a função `TryParse()`, o qual retorna true ou false caso consiga fazer o parse do valor. Seu segundo argumento, coloca-se a variável que deseja que seja atribuído o valor a ser recuperado pelo `ReadLine()`, ficando assim:
```C#
Console.WriteLine("Informe a nota do aluno:");
if (decimal.TryParse(Console.ReadLine(), out decimal nota)) {
    aluno.Nota = nota;
} else {
    throw new ArgumentException("Valor da nota deve ser decimal");
}
```

# Programando com Orientação a Objeto com C#
### Abstração
- abstrair um objeto do mudno real para um contexto específico, considerando apenas os atributos importantes.

#### Criando o projeto
- Primeiro passo é criar uma *Solution*, que seria um agrupamento para o projeto. Dentro do diretório do projeto, abra o prompt de comandos, e digite: `dotnet new sln --name ExemploPOO`
- Cria a pasta do projeto usando `mkdir ExemploPOO`, e acessa por meio do `cd ExemploPOO`
- dentro da pasta `ExemploPOO` iremos criar o projeto de fato, com o comando `dotnet new console`.
- voltamos um nível nas pastas, usando `cd ..`, e executamos o seguinte comando para adicionarmos nosso projeto no arquivo de *Solutions*: `dotnet sln add ExemploPOO/ExemploPOO.csproj`
- criaremos um diretório chamado `Models` e dentro dela, a classe `Pessoa.cs`, com o seguinte conteúdo para teste:
```C#
using System;

namespace ExemploPOO.Models
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }

        public void Apresentar() {
            System.Console.WriteLine($"Olá, meu nome é {Nome} e tenho {Idade} anos");
        }
    }
}
```
- Lá na classe `Program.cs`, aperte **F1**, digite **OminiSharp: Select Project** e escolha **ExemploPOO.sln** para que a extensão reconheça nosso projeto e carregue o *IntelliSense*(para quando for declarar um objeto Pessoa, da classe `Pessoa.cs`). 
- Dentro da Classe `Program.cs` teremos o seguinte código para instanciarmos um objeto Pessoa:
```C#
using System;
using ExemploPOO.Models;

namespace ExemploPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            Pessoa p1 = new Pessoa();
            p1.Nome = "Rodrigo";
            p1.Idade = 35;

            p1.Apresentar();
        }
    }
}
```

### Emcapsulamento
- o emcapsulamento serve para proteger uma classe e definir limites para alteração de suas propriedades. Serve para ocultar seu comportamento e expor somente o necessário.
- No exemplo a seguir, é utilizado o emcapsulamento nas propriedades da classe `Retangulo.cs`, onde eles são privados, só sendo possível manipulá-los por meio dos métodos publicos:
```C#
using System;

namespace ExemploPOO.Models
{
    public class Retangulo
    {
        private double comprimento;
        private double largura;
        private bool valido;

        public void DefinirMedidas(double comprimento, double largura) {
            //garante que não tenhamos valor negativo
            if(comprimento > 0 && largura > 0) {
                this.comprimento = comprimento;
                this.largura = largura;
                valido = true;
            } else {
                System.Console.WriteLine("Valores inválidos");
            }
        }

        public double ObterArea(){
            if(valido) {
                return comprimento * largura;
            } else {
                System.Console.WriteLine("Preencha valores válidos");
                return 0;
            }
        }
    }
}
```

### Herança
- A herança nos permite reutilizar atributos, métodos e comportamentos de uma classe em outras classes. Serve para agrupar objetos que são do mesmo tipo, porém com características diferentes.
### Polimorfismo
- Vem do grego e significa "muitas formas". Com o polimorfismo podemos sobrescrever métodos das classes filhas para que se comportem de maneira diferente e ter sua própria implementação
#### Polimorfismo em tempo de compilação (overload/early binding)
- exemplo:
```C#
namespace ExemploPOO.Models
{
    public class Calculadora
    {
        public int Somar(int n1, int n2)
        {
            return n1 + n2;
        }
        public int Somar(int n1, int n2, int n3)
        {
            return n1 + n2 + n3;
        }
    }
}
```
#### Polimorfismo em tempo de execução (override/late binding)

```C#
using System;

namespace ExemploPOO.Models
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }

        //***  usa virtual   ***
        public virtual void Apresentar() {
            System.Console.WriteLine($"Olá, meu nome é {Nome} e tenho {Idade} anos");
        }
    }
    public class Aluno : Pessoa
    {
        public int Nota { get; set; }

        //***  usa override   ***
        public override void Apresentar() {
            Console.WriteLine($"Olá, meu nome é {Nome}. Sou um aluno de nota {Nota}");
        }
    }
}
```

### Classe abstrata
- tem como objetivo ser exclusivamente um modelo para ser herdado, portanto não pode ser instanciada. Você pode implementar métodos ou deixa-los a cargo de quem herdar.
- Para classes, basta usar `public abstract class NomeClasse {}`
- Para métodos, é o mesmo conceito, no entanto, ele não deverá implementar nada, sendo por exemplo, dessa forma: `public abstract void Creditar();`(pois ela serve exclusivamente como modelo). Dentro da classe abstrata, podemos colocar outros métodos que não sejam abstratos sem problemas algum. **No entanto se implementarmos esse método abstract em uma classe herdada, devemos obrigatoriamente sobrescrever esse método**
- **Observação importante**: quando a classe pai tem uma propriedade que é **protected**, ela pode ser acessar pela sua classe filha (herdado). Para acessar diretamente esse atributo, podemos usar a notação **this**, no entanto é recomendado usar a notação **base** (entretanto ambos tem o mesmo efeito).

#### Classe selada (sealed)
- uma classe selada tem como objetivo de impedir que outras classes façam uma herança dela, ou seja, nenhuma classe pode ser sua derivada. **Também existem métodos e propriedades seladas (não podem ser sobrescritos)**
- a implementação é bem simples, basta usar a palavra reservada **sealed**, como por exemplo em: `public sealed class Professor : Pessoa`

#### Classe object (herdam de System.Object)
- A classe System.Object é a mãe de todas as classes na hierarqueia do .NET. Todas as classes derivam, direta ou indiretamente da classe Object, e ela tem como objetivo prover serviços de baixo nível para suas classes filhas. Vejamos alguns exemplos:
    - **Equals(Object)**: determina se o objeto especificado é igual ao objeto atual.
    - **Equals(Object, Object)**: determina se as instâncias do objeto especificadas são consideradas iguais.
    - **Finalize()**: permite que um objeto tente liberar recursos e executar outras operações de limpeza antes de ser recuperado pela coleta de lixo
    - **GetHashCode()**: serve como a função de hash padrão
    - **GetType()**: obtém o **Type** da instância atual
    - **MemberwiseClone()**: Cria uma cópia superficial do Object atual
    - **ReferenceEquals()**: Determina se as instâncias de Object especificadas são a mesma instância.
    - **ToString()**: retorna uma cadeia de caracteres que representa o objeto atual.
- implicitamente, o que faz seria o mesmo que `public class Computador : System.Object`, ou seja, a classe **System.Object** herda para a classe **Computador** suas características. É possível também você sobrescrever os métodos que são **virtual**, como o `ToString()`, por exemplo.
- quando for sobrescrever algum método da classe *System.Object*, faça a declaração normalmente, como em `public override `, logo após o *override*, dê um **Espaço** para listar os possíveis métodos que poderão ser sobrescritos.

### Interfaces
- é um contrato que pode ser implementado por uma classe. É como se fosse uma classe abstrata, podendo definir métodos abstratos para serem implementados. Assim como uma classe abstrata, uma interface não pode ser instanciada.
- a interfaces supri a necessidade de herança múltipla. Como em C# as classes não podem ser herdadas de duas classes ao mesmo tempo, a interface permite essa característica.
- uma boa prática é ao definir uma Interface, colocar em um diretório chamado `Interfaces`, e o nome do arquivo começar com a letra `I`, como por exemplo: `ICalculadora.cs`:
```C#
using System;

namespace ExemploPOO.Interfaces
{
    public interface ICalculadora
    {
        int Somar(int n1, int n2);
        int Subtrair(int n1, int n2);
        int Multiplicar(int n1, int n2);
        int Dividir(int n1, int n2);
    }
}
```
- **observação: NÃO é necessário usar os modificadores de acesso para assinar o método.**
- para a classe `Calculadora.cs` **implementar** a interface, usa `:` igual como fosse extender, ficando: `public class Calculadora : ICalculadora`.
- para usarmos a interface, usamos `ICalculadora calc = new Calculadora();`
#### Interface com método padrão
- caso dentro da Interface haja um método já implementado, não é necessário implementar na classe que será usado (ou pode implementar também, é opcional). (**isso mudou no C# 8**)

### Manipulação de Arquivo usando C#
- o C# nos apresenta algumas classes estáticas que facilitam o trabalho com arquivos, dentre elas:
    - **File**: criar, mover, apagar arquivos, etc
    - **Directory**: operações com diretórios, como criar, obter, etc
    - **Path**: organizar os caminhos de uma maneira lógica
    - as três classes são *estáticas*, sem necessidade de instanciá-las, basta apenas importar a **System.IO**

#### Listar diretórios
- criar uma classe `FileHelper.cs`:
```C#
using System;
using System.Collections.Generic;
using System.IO;

namespace ExemploPOO.Helper
{
    public class FileHelper
    {
        public void ListarDiretorios(string caminho) {
            //GetDirectories é static, não precisa instanciar
            //GetDirectories possui sobrecarga de método
            //Usando os três parâmetros, trazemos o diretório recursivamente (subdiretórios)
            var retornoCaminho = Directory.GetDirectories(caminho, "*", SearchOption.AllDirectories);

            //Se usarmos somente com o primeiro parâmetro, invocamos automaticamente o SearchOption.TopDirectoryOnly
            //**Lembre-se que precisa descomentar um ou outro
            var retornoCaminho = Directory.GetDirectories(caminho);

            foreach(var retorno in retornoCaminho) {
                System.Console.WriteLine(retorno);
            }
        }
    }
}
```
#### Listar arquivos
- praticamente é o mesmo jeito da listagem de diretórios, para que seja recursivo, usamos `SearchOptions.AllDirectories`.
- o `"*"` (asterisco) que usamos nada mais que um filtro. Para listarmos arquivos de texto, por exemplo, usamos `*txt`.
```C#
public void ListarArquivosDiretorios(string caminho) {
    var retornoArquivos = Directory.GetFiles(caminho, "*", SearchOption.AllDirectories);

    foreach(var retorno in retornoArquivos) {
    System.Console.WriteLine(retorno);
}
```
#### Criar diretório
- usaremos `Directory.CreateDirectory(caminho)`. No entanto, lá em `Program.cs`, usaremos a classe `Path.Combine()`, o qual nos ajuda a evitar a escrever caminhos usando `\`.
```C#
//Com Path.Combine()
 helper.CriarDiretorio(Path.Combine(caminho, "Pasta teste 3", "SubPastaTeste3"));

//Sem Path.Combine(), ficaria como abaixo
var caminho = "C:\\TrabalhandoComArquivos\\Pasta teste 3\\SubPastaTeste3";
```
- outra vantagem do `Path.Combine()` é que o .NET é multiplataforma, logo, é uma preocupação a menos na forma que for organizar as `\`.
#### Apagar diretório
- usaremos `Directory.Delete()`. Lembrando que para excluir um diretório e seus arquivos, devemos usar um segundo argumento de `Delete()`, deixando-o como **true**

`FileHelper.cs`
```C#
public void  ApagarDiretorio(string caminho, bool apagarArquivos) {
    Directory.Delete(caminho, apagarArquivos);
}
```
`Program.cs`
```C#
var caminhoPathCombine = Path.Combine(caminho, "PastaTeste 1");
FileHelper helper = new FileHelper();
helper.ApagarDiretorio(caminhoPathCombine, true);
```
**Observação: `Delete()` apaga diretamente do Sistema Operacional (do HD), não vai para lixeira**

#### Criar arquivo de texto
- usaremos a classe `File`. o C# quando utilizado com essa classe, ela possue alguns métodos que trabalham em forma de **stream**, ou seja, muito semelhante a carregamento de vídeo por streaming, o qual para você assistir um determinado vídeo/filme, não precisa esperar carregar completamente o arquivo, pode ir assistindo conforme vai carregando. Forma essa recomendada quando for trabalhar com arquivos grandes.
- iremos usar o método `WriteAllText()` cujo especificamente não trabalha em forma de stream, ou seja, ele carregará todo conteúdo em memória.

`FileHelper.cs`
```C#
public void CriarArquivoTexto(string caminho, string conteudo) {
    File.WriteAllText(caminho, conteudo);
}
```
`Program.cs`
```C#
var caminhoArquivo = Path.Combine(caminho, "arquivo-teste.txt");
FileHelper helper = new FileHelper();
helper.CriarArquivoTexto(caminhoArquivo, "Olá, teste de escrita de arquivo");
```
- **observação:** dessa maneira, no entanto, temos um problema: toda vez que executarmos o programa, o arquivo será sobrescrito, perdendo o conteúdo anterior. Para corrigir, usaremos um `if` para verificarmos se o arquivo existe.

`FileHelper.cs`
```C#
public void CriarArquivoTexto(string caminho, string conteudo) {
    //se o arquivo NÃO existir, crie, senão, não faça nada
    if (!File.Exists(caminho)) {
        File.WriteAllText(caminho, conteudo);
    }            
}
```

#### Criando arquivo usando stream
- criaremos um método chamado `CriarArquivoTextoStream` dentro da classe `FileHelper.cs`. Esse método receberá dois parâmetros, o primeiro é o caminho, e o segundo é uma lista de string, dessa forma: `CriarArquivoTextoStream(string caminho, List<string> conteudo)`. Lembrando que **List<>** deverá ser importado de `using System.Collections.Generic`;

`FileHelper.cs`
```C#
public void CriarArquivoTextoStream(String caminho, List<String> conteudo) {
    //using faz o close automaticamente o Dispose
    using (var stream = File.CreateText(caminho)) {
        foreach(var linha in conteudo){
            stream.WriteLine(linha);
        }
    }
}
```
[revisão do using](#instruções-auxiliares)
(testar se esse link funciona no github)
- o uso de **stream** é interessante e recomendado quando for fazer uma gravação de muitos dados (milhões talvez) para termos uma melhor consistência, o que não seria recomendado gravar de forma direta.

#### Adicionar novas linhas em um arquivo
- usaremos o `AppendAllText()` para uso geral com arquivos menores, e `AppendText(caminho)` para arquivos stream.
```C#
public void AdicionarTexto(string caminho, string conteudo) {
            File.AppendAllText(caminho, conteudo);
        }

        public void AdicionarTextoStream(String caminho, List<String> conteudo) {
            using (var stream = File.AppendText(caminho)) {
                foreach(var linha in conteudo){
                    stream.WriteLine(linha);
                }
            }
        }
```

#### Leitura de arquivos
- Assim como na gravação, há duas forma de fazer:
    1. carregar o arquivo diretamente em memória
    2. carregar o arquivo de forma gradual, por meio do stream
```C#
public void LerArquivo(string caminho) {
            var conteudo = File.ReadAllLines(caminho);

            foreach (var linha in conteudo){
                System.Console.WriteLine(linha);
            }
        }

        public void LerArquivoStream(string caminho) {
            string linha = string.Empty;

            using (var stream = File.OpenText(caminho)){
                while ((linha = stream.ReadLine()) != null){
                    System.Console.WriteLine(linha);
                }
            }
        }
```

#### Movendo arquivo
- basta usar `void File.Move(string sourceFileName, string destFileName)`. Caso já haja um arquivo de mesmo nome, para sobrescrever, usamos um terceiro parâmetro, ficando `void File.Move(string sourceFileName, string destFileName, bool overwrite)`

`FileHelper.cs`
```C#
public void MoverArquivo(string caminho, string novoCaminho, bool sobrescrever) {
    File.Move(caminho, novoCaminho, sobrescrever);
}
```

`Program.cs`
```C#
var caminho = "C:\\TrabalhandoComArquivos";

var caminhoArquivo = Path.Combine(caminho, "arquivo-teste-stream.txt");

//informa o novo caminho e o arquivo que quer mover
var NovoCaminhoArquivo = Path.Combine(caminho, "Pasta Teste 2", "arquivo-teste-stream.txt");

FileHelper helper = new FileHelper();
//o false indica que não sobrescreva o arquivo caso já exista com mesmo nome
helper.MoverArquivo(caminhoArquivo, NovoCaminhoArquivo, false);
```

#### Copiar arquivo
- exatamente como em mover arquivos, usamos `File.Copy(string sourceFileName, string destFileName)`, no entanto, caso o destino já contenha um arquivo com o mesmo nome, o método não o fará. Para que seja possível sobrescrever o arquivo, o método precisa ser sobrecarregado, usando um terceiro parâmetro, como em `File.Copy(string sourceFileName, string destFileName, bool overwrite)`

`FileHelper.cs`
```C#
public void CopiarArquivo(string caminho, string novoCaminho, bool sobrescrever) {
    File.Copy(caminho, novoCaminho, sobrescrever);
}
```

`Program.cs`
```C#
var caminho = "C:\\TrabalhandoComArquivos";
var caminhoArquivoTeste = Path.Combine(caminho, "arquivo-teste.txt");
var caminhoArquivoCopia = Path.Combine(caminho, "arquivo-teste-backup.txt");

FileHelper helper = new FileHelper();

helper.CopiarArquivo(caminhoArquivoTeste, caminhoArquivoCopia, false);
```

#### Excluir arquivo
- basta usar `void File.Delete(string path)`

`FileHelper.cs`
```C#
public void DeletarArquivo(string caminho) {
    File.Delete(caminho);
}
```

`Program.cs`
```C#
var caminhoArquivoCopia = Path.Combine(caminho, "arquivo-teste-backup.txt");

FileHelper helper = new FileHelper();

helper.DeletarArquivo(caminhoArquivoCopia);
```