## ANOTAÇÕES BOOTCAM DIO AVANADE .NET (DECOLA TECH 2ª EDIÇÃO)

- Bootcamp disponível em [https://web.dio.me/track/decola-tech-2a-edicao](https://web.dio.me/track/decola-tech-2a-edicao)

### Sumário
- [Primeiros passos com .NET](#primeiros-passos-com-.net)
- [ Programando com Orientação a Objeto com C#](#programando-com-orientação-a-objeto-com-c#)

# MENTORIA #2
- 

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
- [revisão do using](#declaração-de-variáveis-e-constantes-locais)
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

# Construtores, Propriedades, Delegates e Eventos em .NET
## Construtures
- Um construtor é um método especial, que contém o mesmo nome do seu tipo de classe, e tem o objetivo de definir valores padrão, limitar uma instância e facilitar a instanciação de um objeto.
    - um construtor não possui um retorno
    - um contrutor padrão é sempre definido quando não declaramos nenhum para sua classe
    -uma classe pode ter mais de um construtor
- quando você define um construtor, automaticamente o construtor padrão deixa de valor, ou seja, você não poderá usar um construtor sem parâmetros (ao menos que você defina isso)
### Construtor privado
- usado quando é necessário ter e garantir apenas uma instância da classe, também chamado de [singleton](https://pt.wikipedia.org/wiki/Singleton#:~:text=Singleton%20%C3%A9%20um%20(anti%2D),de%20acesso%20ao%20seu%20objeto.&text=Alguns%20projetos%20necessitam%20que%20algumas%20classes%20tenham%20apenas%20uma%20inst%C3%A2ncia.)

`Log.cs` 
```C#
using System;

namespace ExemploConstrutores.Models
{
    public class Log
    {
        private static Log _log;

        public string PropriedadeLog { get; set; }

        private Log()
        {
            
        }

        public static Log GetInstance()
        {
            if (_log == null)
            {
                _log = new Log();
            }
            return _log;
        }
    }
}
```

`Program.cs`
```C#
static void Main(string[] args)
{
    Log log = Log.GetInstance();
    log.PropriedadeLog = "Teste instancia";

    Log log2 = Log.GetInstance();
    System.Console.WriteLine(log2.PropriedadeLog);

    //imorime "Teste instancia", mesmo sendo declarado outra variável
}
```

### Chamando o contrutor da herança
- quando, por exemplo, você cria o seu próprio construtor em uma determinada classe, e essa classe irá herdar suas informações para outra classe (classe Pessoa herda para classe Aluno, por exemplo), obrigatoriamente na classe que herdou as informações, você precisará criar um construtor com as mesmas regras do construtor da classe pai, e ainda usar a notação `base()`. **A ordem de executação dos construtores** SEMPRE será do que está na classe Pai e depois na classe filho. Vejamos um exemplo abaixo:

`Pessoa.cs`
```C#
using System;

namespace ExemploConstrutores.Models
{
    public class Pessoa
    {
        private string nome;
        private string sobrenome;

        //construtor definido manualmente
        public Pessoa(string nome, string sobrenome)
        {
            this.nome = nome;
            this.sobrenome = sobrenome;
            System.Console.WriteLine("Construtor classe Pessoa");
        }

        public void Apresentar()
        {
            System.Console.WriteLine($"Olá, meu nome é: {nome} {sobrenome}");
        }
    }
}
```

`Aluno.cs`
```C#
using System;

namespace ExemploConstrutores.Models
{
    public class Aluno : Pessoa
    {
        //definimos o construtor a partir da classe Pessoa, usando base e os atributos necessários
        //colocamos mais um atributo, disciplina. No entanto não é preciso usá-lo, pois é pertencente a classe filho, no caso Aluno
        public Aluno(string nome, string sobrenome, string disciplina) : base(nome, sobrenome)
        {
            System.Console.WriteLine("Construtor classe Aluno");
        }
```

## Getters e Setters
- Get e Set serve para que você possa atribuir um valor em uma variável de maneira controlada e com validações. Se deixá-la publica e recevendo qualquer valor, há possibilidade de inserção de dados que não fazem sentido (como receve mês 13 na descrição de meses, por exemplo).
```C#
namespace ExemploConstrutores.Models
{
    public class Data
    {
        private int mes;

        public int GetMes() {
            return this.mes;
        }

        public void SetMes(int mes)
        {
            if (mes > 0 && mes <= 12)
            {
                this.mes = mes;
            }
        }
    }
}
```
### Propriedades
- Em vez de criarmos métodos get e set, podemos usar a propriedade diretamente

`Data.cs`
```C#
using System;

namespace ExemploConstrutores.Models
{
    public class Data
    {
        private int mes;
        private bool mesValido;

        //value (lá no set) é pego quando depois de instanciado, na atribuição de valor, no caso lá na classe Program.cs
        //data.Mes = 22;
        //value no caso é o 22
        public int Mes 
        {
            get 
            {
                return this.mes;
            }
            set
            {
                if (value > 0 && value <= 12)
                {
                    this.mes = value;
                    this.mesValido = true;
                }
            }
        }

        public void ApresentarMes() {
            if (this.mesValido)
            {
                System.Console.WriteLine(this.mes);
            } else {
                System.Console.WriteLine("Mês inválido");
            }
        }
    }
}
```

`Program.cs`
```C#
Data data = new Data();

//na realidade aqui está fazendo o set definido lá na classe Data.cs, inclusive implantando a validação
data.Mes = 22;

//data.Mes é o mesmo que o GetMes
System.Console.WriteLine(data.Mes);
data.ApresentarMes();
```
```C#
```
## Modificadores
#### Readonly
- o modificador readonly (somente leitura) bloqueia um campo contra alterações que não sejam em sua inicialização ou pelo próprio construtor
```C#
public class Pessoa
{
    private readonly string nome = "Rodrigo";
    private readonly string sobrenome;

    public Pessoa(string nome, string sobrenome)
    {
        this.nome = nome;
        this.sobrenome = sobrenome;
    }

    public void AlterarNome()
    {
        //essa linha irá retornar erro, pois só podemos declarar algo no readonly na inicilização ou no construtor.
        this.nome = "Nome alterado";
    }
}
```
#### Constante
- uma constante representa uma valor que somente pode ser atribuído no momento de sua inicialização, e não pode ser modificado posteriormente (contrário de uma variável). **Não é possível declarar uma constante sem atribuir valor a ela**
```C#
static void Main(string[] args) {
    cost double pi = 3.14;
    System.Console.WriteLine(pi);

    //essa linha irá retornar erro
    pi = 0;
}
```

## Delegates
- Um delegate é uma maneira de passar um método como referência, podendo ser usado como um callback, aceitando qualquer método que contenha a mesma assinatura.
```C#
public class Calculadora
{
    public static void Somar(int num1, int num2)
    {
        System.Console.WriteLine($"Adição: {num1 + num2}");
    }

    public static void Subtrair(int num1, int num2)
    {
        System.Console.WriteLine($"Subtração: {num1 - num2}");
    }
}
```
```C#
public delegate void operacao(int x, int y);

static void Main(string[] args)
{
    //há duas maneiras de chamar o delegate, uma direto e outra usando new
    Operacao op = new Operacao(Calculadora.Somar);
    operacao op = Calculadora.Somar;

    //ao chamar o delegate, também é possível de duas formas
    op.Invoke(10, 10);
    op(10, 10);
}
```
### Multi Cast Delegate
- uma maneira de realizar várias chamadas de métodos em um único delegate.
```C#
static void Main(string[] args)
{
    //há duas maneiras de chamar o delegate, uma direto e outra usando new
    Operacao op = new Operacao(Calculadora.Somar);

    //multi cast delegate. usa-se += para que op não perca a referêcia anterior
    op += Calculadora.Subtrair;

    operacao op = Calculadora.Somar;

    //ao chamar o delegate, também é possível de duas formas
    op.Invoke(10, 10);
    op(10, 10);

    //o resultado será: (sim, irá chamar ambos os métodos)
    //Adição: 20
    //Subtração: 0
}
```
**lembrando que o multi cast delegate obedece uma ordem FIFO, o primeiro a entrar, é o primeiro a sair**

## Eventos
- eventos é um mecanismo de comunicação entre objetos, onde existe um publisher, que realiza o evento e o sbuscriver, que inscreve em um evento. Basicamente funciona quando você se inscreve para receber notícias de algum site. Após inscrito, todas novidades que são publicadas por esse determinado site, são disparados aos inscritos, as atualizações.
```C#
public class Calculadora
{
    //Delegate
    public delegate void DelegateCalculadora();

    //Evento
    public static event DelegateCalculadora EventoCalculadora;

    public static void Somar(int x, int y)
    {
        if (EventoCalculadora != null)
        {
            System.Console.WriteLine($"Adição: {x + y}");
            EventoCalculadora();
        } else {
            System.Console.WriteLine("Nenhum inscrito");
        }
    }
}
```

# Fundamentos de Coleções e LINQ com .NET
## Arrays
- o array é uma estrutura de dados que armazena valores do mesmo tipo, com tamanho fixo. Conhecido também como **vetor**, **matriz** e **arranjo**.
    - `int[] array = new int[4]`;
    - `int[] array = new int[] { 42, 75, 77, 61};`(já coloca a capacidade do array como 4 posições)
    - `string[] nomes = {"Jan", "Fev"}`;
>**Observação**
>
>- tem que ser do mesmo tipo, e ter um tamanho fixo
>- caso precise aumentar o tamanho, precisa-se criar um novo array com a capacidade desejada e copiar os dados do array anterior. **NÃO** pode mudar o tamanho quando já está em memória, seja para aumentar como para diminuir
>- o que você pode, é alterar os valores, mas diminuir/aumentar seu tamanho, não é permitido
>- para acessar a posição de um array, sempre use **n - 1**, sendo **n** a capacidade do array
### Array Multidimensional
- o array pode possuir mais de uma dimensão (semelhante ao Excel):
```C#
int[,] array = new int[4,2];
```
- 4 = número de linhas
- 2 - número de colunas
- ficando dessa forma:

c1|c2   
--|--
1 | 2
3 | 4
5 | 6
7 | 8

- Acessar um valor: array[1,1]

### Percorrendo um array
```C#
static void Main(string[] args)
{
    int[] arrayInteiros = new int[3];
    
    arrayInteiros[0] = 10;
    arrayInteiros[1] = 20;
    arrayInteiros[2] = 30;

    //percorrer array com for
    System.Console.WriteLine("Percorrendo o array pelo for");
    for (int i = 0; i <arrayInteiros.Length; i++)
    {
        System.Console.WriteLine(arrayInteiros[i]);
    }

    //percorrer array com foreach
    System.Console.WriteLine("Percorrendo o array pelo foreach");
    foreach (var item in arrayInteiros)
    {
        System.Console.WriteLine(item);
    }
}
```
>**Observação**
>
>Caso você precise ter um controle nas posições em que o array está, é recomendável que use a condição **for**, pois com ela você consegue usar colchetes `[]` para indicar a posição com um contador.
### Debugando um array
- para debugar, tenha certeza que a extensão do C# esteja rodando, caso não esteja, apertar **F1** para escolher o projeto por meio do **OmniSharp: Select Project** e escolhendo a **sln**.
- depois aperte **F5**, selecione o ambiente **.NET Core**, e irá ser criado um arquivo **json** automaticamente.
- aperte **F10** para ir debugando passo a passo

### Acessando um array de forma segura
```C#
int[] arrayInteiros = new int[3];

arrayInteiros[0] = 10;
arrayInteiros[1] = 20;

//se for um número mesmo sendo string, o método int.Parse() tenta corrigir
arrayInteiros[2] = int.Parse("30");

//aqui dará uma excessão que excedeu os limites do array, e quebrará o programa
arrayInteiros[3] = 40;
```

### Array Multidimensional (prática)
- inserindo e percorrendo os elementos:
```C#
//array multidimensional com 4 linhas e 2 colunas
int[,] matriz = new int[4,2]
{
    { 8, 8 },//linha e coluna respectivamente
    { 10, 20 },
    { 50, 100 },
    { 90, 200 },
};

//for para percorrer as linhas
for (int i = 0; i < matriz.GetLength(0); i++)
{
    //for para percorrer as colunas
    for (int j = 0; j < matriz.GetLength(1); j++)
    {
        System.Console.WriteLine(matriz[i, j]);
    }
}
```

## Manipulando Arrays
### Ordenando Arrays
- para ordernar um array, existem diversos algoritmos de ordenação, diferentes técnicas e casos a serem considerados

⠀|⠀|⠀|⠀|⠀|⠀|⠀|⠀|⠀|
-|-|-|-|-|-|-|-|-|
valores não ordenados|12|5|15|14|1|18|4|15
valores ordenados|1|4|5|12|14|15|15|18

#### Bubble sort
- usando o *bubble sort*, será feito diversos laços comparando o valor atual com o próximo, e invertendo as posições caso o valor anterior seja maior que o ulterior. O *bubble sort* é recomendado para arrays pequenos:

`OperacoesArray.cs`
```C#
using System;

namespace Colecoes.Helper
{
    public class OperacoesArray
    {
        //passaremos como ref para garantirmos que o retorno será o mesmo array
        public void OrdenarBubbleSort(ref int[] array)
        {
            int temp = 0;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length -1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        public void ImprimirArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                System.Console.WriteLine(array[i]);
            }
        }
    }
}
```

`Program.cs`
```C#
int[] array = new int[5] { 6, 3, 8, 1, 9};

OperacoesArray op = new OperacoesArray();

System.Console.WriteLine("Array original");
op.ImprimirArray(array);

op.OrdenarBubbleSort(ref array);

System.Console.WriteLine("Array ordenado");
op.ImprimirArray(array);
```

#### Debugando o Bubble sort
- como melhoria, vamos transformar a impressão do array em uma única linha, usando:
```C#
public void ImprimirArray(int[] array)
{
    //usamos Join(), colocamos vírgula e um espaço em branco + a nossa array
    var linha = string.Join(", ", array);
    System.Console.WriteLine(linha);
}
```

#### Classe Array
- a classe Array é uma classe do C# que nos oferece diversos métodos que nos auxiliam a trabalhar com arrays

#### Ordenação pela Classe Array
```C#
using System;

public void Ordenar(ref int[] array)
{
    Array.Sort(array);
}
```

#### Copiar um array para outro
- usaremos o método `void Array.Copy(Array sourceArray, Array destinationArray, int length)`. No terceiro argumento geralmente usamos `array.Length` para usar todo o tamanho do array
```C#
public void Copiar(ref int[] array, ref int[]arrayDestino)
{
    Array.Copy(array, arrayDestino, array.Length);
}
```
```C#
OperacoesArray op = new OperacoesArray();

int[] array = new int[5] { 6, 3, 8, 1, 9};
int[] arrayCopia = new int[10];

System.Console.WriteLine("Array antes da cópia:");
op.ImprimirArray(arrayCopia);

op.Copiar(ref array, ref arrayCopia);
System.Console.WriteLine("Array após a cópia");
op.ImprimirArray(arrayCopia)
```
#### Verificando um elemento existente (retornando true ou false)
```C#
public bool Existe(int[] array, int valor)
{
    //pode-se usar a expressão quer for necessária >=, !=, por exemplo
    //essa expressão é um Predicate
    return Array.Exists(array, elemento => elemento == valor);
}
```
```C#
OperacoesArray op = new OperacoesArray();

int[] array = new int[5] { 6, 3, 8, 1, 9};
int[] arrayCopia = new int[10];

var valorProcurado = 1;
bool existe = op.Existe(array, valorProcurado);
if (existe)
{
    System.Console.WriteLine("Encontrei o valor: {0}", valorProcurado);
} else {
    System.Console.WriteLine("Não encontrei o valor: {0}", valorProcurado);
}
```

#### verificando todos os elementos do array
```C#
public bool TodosMaiorQue(int[] array, int valor)
{
    return Array.TrueForAll(array, elemento => elemento > valor);
}
```
```C#
OperacoesArray op = new OperacoesArray();

int[] array = new int[5] { 6, 3, 8, 1, 9};
int[] arrayCopia = new int[10];

var valorProcurado = 0;

bool todosMaiorQue = op.TodosMaiorQue(array, valorProcurado);

if (todosMaiorQue)
{
    System.Console.WriteLine("Todos os valores são maior que {0}", valorProcurado);
} else {
    System.Console.WriteLine("Existe valores que não são maiores do que {0}", valorProcurado);
}
```

#### Encontrando um elemento no array
```C#
public int ObterValor(int[] array, int valor)
{
    return Array.Find(array, elemento => elemento == valor);
}
```
```C#
OperacoesArray op = new OperacoesArray();

int[] array = new int[5] { 6, 3, 8, 1, 9};
int[] arrayCopia = new int[10];

var valorProcurado = 9;
                //se ObterValor não encontrar o valor, será retornado 0 (zero), que é o valo padrão de int
int valorAchado = op.ObterValor(array, valorProcurado);

//se na condição não achar o valor procurado, é retornado o valor padrão do tipo, que o caso é 0 (zero)
if (valorAchado > 0)
{
    System.Console.WriteLine("Encontrei o valor");
} else {
    System.Console.WriteLine("Não encontrei o valor");
}
```

#### Encontrando o índice de um valor
- podemos usar tanto `int Array.FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)`, que é possível usar um `Predicate` para formularmos a expressão que desejamos. Como também é possível usar `int Array.IndexOf(Array array, object? value)` que vai receber diretamente o valor a ser comparado, sempre buscando a igualdade.
- caso o `int Array.IndexOf(Array array, object? value)` não encontre o valor que procuramos, ele irá retornar `-1` (retorna `-1` pois se retornasse `0`, é uma posição no array)
```C#
public int ObterIndice(int[] array, int valor)
{
    return Array.IndexOf(array, valor);
}
```
```C#
OperacoesArray op = new OperacoesArray();

int[] array = new int[5] { 6, 3, 8, 1, 9};
int[] arrayCopia = new int[10];

var valorProcurado = 9;

int indice = op.ObterIndice(array, valorProcurado);
//é retornado -1, pois 0 poderia ser uma posição no array
if (indice > -1)
{
    System.Console.WriteLine("O índice do elemento {0} é: {1}", valorProcurado, indice);
} else {
    System.Console.WriteLine("Valor não existente no array");
}
```

#### Redimensionando um array
- usaremos `void Array.Resize<int>(ref int[]? array, int newSize)`
```C#
public void RedimencionarArray(ref int[] array, int novoTamanho)
{
    Array.Resize(ref array, novoTamanho);
}
```
```C#
int[] array = new int[5] { 6, 3, 8, 1, 9};

System.Console.WriteLine($"Capacidade atual do array: {array.Length}");

//vamos dobrar o tamanho do array
op.RedimencionarArray(ref array, array.Length * 2);

System.Console.WriteLine($"Capacidade atual do array após redimencionar: {array.Length}");
```
>**Observação**
>
>É importante ressaltar que por trás dos panos, o que o método `Resize()` faz é criar um novo array e passa a referenciá-lo.

#### Converter um array (de um tipo para outro | int para string, por exemplo)
- usaremos `TOutput[] Array.ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)`
```C#
public string[] ConverterParaArrayString(int[] array)
{
    return Array.ConvertAll(array, elemento => elemento.ToString());
}
```
```C#
string[] arrayString = op.ConverterParaArrayString(array);
```
## Coleções genéricas
- No C#, existem classes de coleções que agrupam valores, e essas classes são padronizadas para as operações mais comuns, como:
    - Ordenação
    - Obter valor por índice
    - Obter valor com expressões
    - Trabalhar com tamanhos dinâmicos
- Classes `System.Collections.Generic`
    - você pode criar uma coleção genérica usando uma das classes no namespace `System.Collections.Generic`. Uma coleção genérica é útil quando cada item na coleção tem o mesmo tipo de dados. Uma coleção genérica impões tipagem forte, permitindo que apenas o tipo de dados desejado seja adicionao.
- Vejamos algumas das classes frequentemente usadas no namespace `System.Collections.Generic`:

Classe | Descrição
-------|----------
`Dictionary<TKey,TValue>` | Representa uma coleção de pares chave-valor organizadas com base na chave
`List<T>` | Representa uma lista de objetos que podem ser acessados por índice. Fornece métodos para pesquisar, classificar e modificar listas
`Queue<T>` | Representa uma coleção de objetos PEPS(primeiro a entrar, primeiro a sair)
`SortedList<TKey,TValue>` | Representa uma coleção de pares chave/valor que são classificados por chave com base na implementação de `IComparer<T>` associada.

### Coleção List<T>
```C#
using System;
using System.Collections.Generic;
using Colecoes.Helper;

namespace Colecoes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> estados = new List<string>();

            //adicionar itens
            estados.Add("SP");
            estados.Add("RJ");
            estados.Add("MG");

            //Count para obter quantidade de itens na lista
            System.Console.WriteLine($"Quantidade de elementos na lista: {estados.Count}\n");

            //exibir os elementos da lista com foreach
            System.Console.WriteLine("Impressão pelo foreach");
            foreach (var item in estados)
            {
                System.Console.WriteLine(item);
            }

            System.Console.WriteLine("\nImpressão usando for");
            for (int i = 0; i < estados.Count; i++)
            {
                System.Console.WriteLine($"Índice {i}, Valor: {estados[i]}");
            }
        }
    }
}

```

### Remoção de elementos List<T>
- vamos dar uma melhorada na classe `OperacoesLista.cs` colocando um método para impressão da lista:
```C#
public void ImprimirListaString(List<string> lista)
{
    for (int i = 0; i < lista.Count; i++)
    {
        System.Console.WriteLine($"Índice {i}, Valor: {lista[i]}");
    }
}
```
```C#
OperacoesLista opLista = new OperacoesLista();
List<string> estados = new List<string>();
estados.Add("SP");
estados.Add("MG");
estados.Add("RJ");

System.Console.WriteLine($"Quantidade de elementos na lista: {estados.Count}\n");

opLista.ImprimirListaString(estados);

estados.Remove("MG");

System.Console.WriteLine("Removendo o elemento");

opLista.ImprimirListaString(estados);
```
>**Observação**
>
>Apesar de `MG` estar na posição `[1]`, quando o excluímos com `estados.Remove("MG")`, `RJ` passa ocupar a posição `[1]`, remanejando toda a lista, inclusive atualizando seu tamanho retornado pelo `Count` 

### Adicionando coleções na lista
- podemos incluir valores na lista já na sua inicialização, dessa forma `List<string> estados = new List<string> { "SP", "MG", "BA" };`
- caso seja necessário adicionar novos valores a essa lista (sem apagar os anteriores), podemos usar o métodos `void List<string>.AddRange(IEnumerable<string> collection)`, da seguinte forma:
```C#
OperacoesLista opLista = new OperacoesLista();
List<string> estados = new List<string> { "SP", "MG", "BA" };
string[] estadosArray = new string[2] { "SC", "MT" };//novos valores

System.Console.WriteLine($"Quantidade de elementos na lista: {estados.Count}\n");

opLista.ImprimirListaString(estados);

estados.AddRange(estadosArray);
System.Console.WriteLine("Adicionado novos valores");

opLista.ImprimirListaString(estados);
```

### Adicionar elemento por índice (em um ponto específico)
- usaremos `void List<string>.Insert(int index, string item)`, como abaixo:
```C#
OperacoesLista opLista = new OperacoesLista();
List<string> estados = new List<string> { "SP", "MG", "BA" };
string[] estadosArray = new string[2] { "SC", "MT" };

System.Console.WriteLine($"Quantidade de elementos na lista: {estados.Count}\n");

opLista.ImprimirListaString(estados);

//repectivamente, 1 é o índice que queremos adicionar, e "RJ" é o elemento
estados.Insert(1, "RJ");
System.Console.WriteLine("Adicionado novos valores");

opLista.ImprimirListaString(estados);
```
>**Observação**
>
>A capacidade da lista, ou seja, o seu `Count` já é atualizado automaticamente


## Coleções específicas
- as coleções específicas implementam regras para sua ordem de acesso e manupilação de seus elementos, são elas
    - **Queue (Fila)**: Obedece a ordem FIFO (Fist In First Out)
    - **Stack (Pilha)**: Obedece a ordem LIFO (Last In First Out)

### Implementando uma Queue
- utilizaremos a classe `Queue<T>` pertencente a `System.Collections.Generic`
```C#
using System;
using System.Collections.Generic;
using Colecoes.Helper;

namespace Colecoes
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> fila = new Queue<string>();

            //Enqueue() adiciona elemento à fila
            fila.Enqueue("Rodrigo");
            fila.Enqueue("Fulano");
            fila.Enqueue("Ciclano");

            System.Console.WriteLine($"Pessoas na fila: {fila.Count}");

            //while para percorrermos e remover elementos da fila
            while (fila.Count > 0)
            {
                //Peek() pega o primeiro elemento da fila (o que está na frente da fila)
                System.Console.WriteLine($"Vez de: {fila.Peek()}");

                //Dequeue() mostra e remove o elemento da fila (o que está na frente da fila)
                System.Console.WriteLine($"{fila.Dequeue()} atendido!");
            }

            System.Console.WriteLine($"Pessoas na fila: {fila.Count}");
        }
    }
}

```
>**Observação**
>
>Não há um método para remover elemento da fila por posição, pois a fila precisa obedecer a ordem FIFO.

### Implementando uma Stack
```C#
using System;
using System.Collections.Generic;
using Colecoes.Helper;

namespace Colecoes
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<string> pilhaLivros = new Stack<string>();

            //insere o objeto no topo da pilha
            pilhaLivros.Push(".NET");
            pilhaLivros.Push("DDD");
            pilhaLivros.Push("Código Limpo");

            //retorna 3
            System.Console.WriteLine($"Livros para a leitura: {pilhaLivros.Count}");

            //Leitura da pilha
            while(pilhaLivros.Count > 0) //enquanto houver elementos
            {
                 //exibe o objeto no topo da pilha, mas sem removê-lo
                System.Console.WriteLine($"Próximo livro para leitura: {pilhaLivros.Peek()}");

                //obtem o livro no topo da pilha e o remove da pilha
                System.Console.WriteLine($"{pilhaLivros.Pop()} lido com sucesso!");
            }

            //retorna 0
            System.Console.WriteLine($"Livros para a leitura: {pilhaLivros.Count}");
        }
    }
}

```

## Dicionários
- um dicionário é uma coleção de chave e valor, permitindo que você recupere rapidamente seus itens baseado em sua chave.
- o dicionário armazena a sua chave em hash
- criação: `Dictionary<string, string> estados = new Dictionary<string,string>()`
```C#
using System;
using System.Collections.Generic;
using Colecoes.Helper;

namespace Colecoes
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> estados = new Dictionary<string, string>();

            //adicionar (NÃO pode repetir chaves, apenas os valores)
            estados.Add("SP", "São Paulo");
            estados.Add("MG", "Minas Gerais");
            estados.Add("BA", "Bahia");

            //percorrer o dicionário
            foreach (KeyValuePair<string, string> item in estados)
            {
                System.Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            }
        }
    }
}
```
>**Observação**
>
>Não podemos ter chaves repetidas! Os valores podem, mas as chaves, **NÃO**!

### Acessando um valor do Dictionary
```C#
Dictionary<string, string> estados = new Dictionary<string, string>();

//adicionar (NÃO pode repetir chaves)
estados.Add("SP", "São Paulo");
estados.Add("MG", "Minas Gerais");
estados.Add("BA", "Bahia");

string valorProcurado = "BA";
//passar o valor a ser procurado pela chave (entre colchete)
//apesar de estar passando a chave, o retorno SEMPRE será o valor
System.Console.WriteLine(estados[valorProcurado]);

//retornará Bahia
```

### Atualizando um valor do Dictionary
```C#
Dictionary<string, string> estados = new Dictionary<string, string>();

//adicionar (NÃO pode repetir chaves)
estados.Add("SP", "São Paulo");
estados.Add("MG", "Minas Gerais");
estados.Add("BA", "Bahia");

string valorProcurado = "BA";

System.Console.WriteLine($"Valor original");
System.Console.WriteLine(estados[valorProcurado]);

//Atualização do valor
estados[valorProcurado] = "BA - Teste Atualização";

System.Console.WriteLine($"Valor atualizado");
System.Console.WriteLine(estados[valorProcurado]);
```

### Removendo um valor do Dictionary
```C#
Dictionary<string, string> estados = new Dictionary<string, string>();

//adicionar (NÃO pode repetir chaves)
estados.Add("SP", "São Paulo");
estados.Add("MG", "Minas Gerais");
estados.Add("BA", "Bahia");

//percorrer o dicionário
foreach (KeyValuePair<string, string> item in estados)
{
    System.Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
}

//valor original
string valorProcurado = "BA";

System.Console.WriteLine($"Removendo o valor: {valorProcurado}");

//para remover basta informarmos a chave desejada
estados.Remove(valorProcurado);

foreach (KeyValuePair<string, string> item in estados)
{
    System.Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
}
```

### Acessando de maneira segura o Dictionary
- sempre que tentarmos acessar uma chave que não exista, retornará uma excessão, e nosso programa será interrompido (semelhante a quando tentamos acessar um índice que está fora da delimitação de um array, por exemplo)
- para evitar isso, e termos um acesso mais seguro, usaremos `bool Dictionary<string, string>.TryGetValue(string key, out string value)`. Esse método tenta obter o valor do Dictionary, e caso encontre algo, conforme informando no primeiro argumento, ele retorna no segundo argumento o que encontrou.
```C#
Dictionary<string, string> estados = new Dictionary<string, string>();

//adicionar (NÃO pode repetir chaves)
estados.Add("SP", "São Paulo");
estados.Add("MG", "Minas Gerais");
estados.Add("BA", "Bahia");

//valor original
string valorProcurado = "BA";

//tentar acessar diretamente um estado que não existe, o programa quebra
//var teste = estados["SC"];

//mesmo que não exista o valor, dessa forma não deixa o programa quebrar
if (estados.TryGetValue(valorProcurado, out string estadoEncontrado))
{
    System.Console.WriteLine(estadoEncontrado);
    //retorna Bahia
} else {
    System.Console.WriteLine($"Chave {valorProcurado} não existe no dicionário");
}
```
## Operações LINQ
- recurso presente desde 2008 no C#, mas bem útil até os dias de hoje.
- significa **L**anguage-**IN**tegrated **Q**uery (LINQ)
- LINQ é uma maneira de você utilizar uma sintaxe de consulta padronizada para coleções de objetos
Exemplo
```C#
int[] numbers = { 5, 10, 8, 3, 6, 12 };

//Query syntax:
IEnumerable<int> numQuery1 =
    from num in numbers
    where num % 2 ==0
    orderby num
    select num;

//Method syntax:
IEnumerable<int> numQuery2 = numbers.Where(num => num % 2 == 0).OrderBy(n => n).ToList();
```
>**Observação**
>
>1. Ambos os casos são idênticos em relação ao tratamento e a performance
>2. Lembre-se de adicionar `using System.Linq;`

### Exemplo na prática de como obter números pares de uma lista (array)
`Program.cs`
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using Colecoes.Helper;

namespace Colecoes
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrayNumeros = new int[5] { 1, 4, 8, 15, 19 };

            //Query syntax:
            var numerosPares = 
                from num in arrayNumeros
                where num % 2 == 0
                orderby num
                select num;

            //Method syntax:
            var numerosParesMetodo = arrayNumeros.Where(x => x % 2 == 0).OrderBy(x => x).ToList();

            System.Console.WriteLine("Números pares query: " + string.Join(", ", numerosPares));
            System.Console.WriteLine("Números pares método: " + string.Join(", ", numerosParesMetodo));
        }
    }
}
```

### Obtendo valores mínimo, máximo e médio com LINQ
```C#
int[] arrayNumeros = new int[7] { 100, 1, 4, 0, 8, 15, 19 };

var minimo = arrayNumeros.Min();
var maximo = arrayNumeros.Max();
var medio = arrayNumeros.Average();

System.Console.WriteLine($"Mínimo: {minimo}");
System.Console.WriteLine($"Máximo: {maximo}");
System.Console.WriteLine($"Médio: {medio}");
```

### Sum e Distinct
- `Sum()` somar todos os elementos presentes na coleção
- `Distinct()` retorna uma nova coleção com valores únicos
```C#
int[] arrayNumeros = new int[10] { 100, 1, 4, 0, 8, 15, 19, 19, 4, 100 };

var soma = arrayNumeros.Sum();
var arrayUnico = arrayNumeros.Distinct().ToArray();

System.Console.WriteLine($"Soma: {soma}");
System.Console.WriteLine($"Array original: {string.Join(", ", arrayNumeros)}");
System.Console.WriteLine($"Array distinto: {string.Join(", ", arrayUnico)}");
```
>**Resultado**
><br>
>Soma: 270<br>
>Array original: 100, 1, 4, 0, 8, 15, 19, 19, 4, 100<br>
>Array distinto: 100, 1, 4, 0, 8, 15, 19

# Introdução a microserviços com .NET
## por que microserviços?
- analogia com uma colméia, que "uma junta a outra"
- monolitos (sistema grande único):
    - rápido e fácil para iniciar
    - infraestrutura simples
    - debug rápido, fascinante<br/>
    **Problemas**
    - problemas com merge-conflicts
    - conexões simultâneas TCP é limitada
    - deadlock e concorrência
    - bugs e defeitos colaterais(único ponto de falha)
    - build/deploy longos e pesados
    - **Baixa escalabidade**
    - agregação de tecnologia
    - demora de aculturamento
    - etc

## pensando em escalabidade vertical x horizontal
- escalabilidade vertical (a longo prazo, tudo vai ficando mais pesado e caro)
    - mais memória
    - mais processamento
- escalabidade horizontal (mais micros, também vai ficando mais caro e pesado)
    - load balancer (micro que também precisa de escalabidade)
    - escalar verticalmente nesses computadores também
- [https://martinfowler.com/articles/microservices.html](https://martinfowler.com/articles/microservices.html)
>- A monolithic application puts all its functionality into a single process...and scales by replicanting the monolith on multiple servers<br/>
>- A microservices architecture puts each element of functionality into a separate service... and scales by distributing these services across servers, replcating as needed.
- escalabidade horizontal com microsserviços
    - API manager Load Balancer para os vários microserviços
    - uma anologia ao e-comerce, seria um microsserviço para o carrinho de compras, uma outro para descrição do produto, etc. Nisso vai escalando o que for preciso considerando a demanda.

## Metodologia Ágil
- microserviço em si, é uma forma de metodologia ágil

## Ecossistema de Microsserviços
- a cada microsserviço, eu preciso de um BD, pois se precisar escalar, escala o BD junto.
- dentro de um **Shopping Cart UI** (podemos segregar)
    - **User Account**
        - Web Pages
        - Classes
        - Config Files
        - e o BD
    - **Product Catalog**
        - Web Pages
        - Classes
        - Config Files
        - e o BD
    - **Inventory**
        - Web Pages
        - Classes
        - Config Files
        - e o BD
    - **Order**
        - Web Pages
        - Classes
        - Config Files
        - e o BD

## O que é uma API Pública e como se interagem
- microsserviços dependem apenas um do outro via APIs públicas
- microsserviços podem (e devem) ser poliglotas em seus stacks
- microsserviços encapsulam estado e comportamento
    - **Estados (Dados)** e **Comportamento (Regras de Negócio)** = **Business Capability**
- microsserviços **devem** possuir deploy independentes
- microsserviços devem ter tratamentos isolados à falhas

# Design Patterns em C#
- **Design Patters**: "Alguém já resolveu o seu problema!"
- São soluções elegantes, testadas e aprovadas para problemas recorrentes que temos no design e implementação de software
- Surgiram da experimentação e repetição
- Não é um padrão pronto para ser aplicado no seu código, é uma descrição/template de como resolver o seu problema nas mais diferentes situações
- GOF - Gang of Four: criaram a "bíblia" sobre o assunto
    - Catalogaram 23 patterns divididos em 3 grupos:
    1. Creational Pattern
    2. Structural Pattern
    3. Behavioral Pattern

## Design Patterns - Hands On

Singleton | Repository | Facade
----------|------------|-------
Garante uma única instância da classe e acesso global para ela, ou seja, centraliza e compartilha recursos | Faz a abstração ("meio de campo") entre o seu domínio e sua camada de dados, ou seja, contribui para o isolamento da camada de acesso a dados | Define uma interface que abstrai a complexidade de interface de subsistemas, ou seja, simplifica a utilização de subsistemas  complexos

# Criando um APP simples de cadastro de séries em .NET - Parte 1
- **Classes Abstratas**
    - Classes que podem conter métodos abstratos
        - um método abstrato é um método que é declarado, porém não contém implementação
    - Não pode ser instanciada
    - Exite subclasses que tenham implementação dos métodos abstratos
- **Interfaces**
    - Interface é muito semelhante a uma classe abstrata, mas não possui atributos e não pode definir como os métodos devem ser implementados
    - Em vez disso, é simplesmente uma lista de métodos que **devem** ser implementados

# Criando um APP simples de cadastro de séries em .NET - Parte 2
- 