# Curso Laravel

Curso feito de acordo com a playlist do Matheus Battisti, do Hora de Codar, disponível no YouTube, mais especificamente [aqui](https://www.youtube.com/watch?v=4OxYHiEkqBg&list=PLnDvRpP8BnewYKI1n2chQrrR4EYiJKbUG)

Este repositório apenas contem anotações que achei importantes no decorrer do curso. Fica aqui também registrado meu sincero agradecimento ao Matheus, por disponibilizar esse material riquíssimo de forma totalmente gratuita!

## Instalação

A instalação é bastante simples. Primeiramente instalamos o XAMP (para Windows), pois já teremos o PHP e o Banco de Dados que iremos utilizar. Posteriormente instalaremos o Composer, e indicaremos o caminho do PHP no XAMP.

Com isso, já podemos separar um diretório para a aplicação que será desenvolvida (um gerenciador de eventos), e no terminal (neste mesmo diretório), executaremos `composer create-project --prefer-dist laravel/laravel hdvevents` para criar um projeto Laravel. Posteriormente, navegaremos dentro de `hdvevents` e executaremos `php artisan serve` para executar a aplicação em localhost na porta 8000.

Para testarmos uma edição no projeto, vamos até o diretório `hdcevents\resources\views` e abrir o arquivo `welcome.blade.php`

Entre as tags `<head>` e `<body>`, vamos adicionar um `<h1>` para testar e salver, e por fim, recarregar a página aberta anteriormente. Se tudo estiver correto, veremos a edição que acabamos de fazer.

```html
 </head>
    <body class="antialiased">
        <h1>Hello World</h1> <!-- tag h1 que adicionamos -->
```

## Rotas

Assim como em outras lib ou frameworks estruturados, iremos utilizar rotas, como por exemplo `/login` ou somente `/`, indicando uma rota principal. Iremos manipular as rotas lá em `hdcevents\routes`, mais especificamente no arquivo `web.php`

```php
Route::get('/', function () {
    return view('welcome');
});
```

Nesse caso, a rota `/` retorna uma **view**, e a encontramos lá em `hdcevents\resources\views`, que no caso, seria o arquivo `welcome.blade.php`.

Caso criemos uma nova rota chamada `/contact`:

```php
Route::get('/contact', function () {
    return view('contact');
});
```

E criemos uma nova view chamada `contact.blade.php`:

```html
<h1>Esta é uma página de contato</h1>
<a href="/">Voltar para Home</a>

```
Dessa forma, irá funcionar tranquilamente, e ainda incluímos a possibilidade de voltar para página principal por meio da tag `<a>`, indicando a rota principal `/`.

## Blade

- É uma template engine do Laravel
- utilizando Blade, podemos deixar as views dinâmicas, ou seja, receber dados processados pelo PHP, seja ele de um BD ou de alguma regra de negócio, e inseri-los na view, por meio de tags HTML.
- Podemos dizer que as Views serão responsabilidade do Blade

Para inserir esse conteúdo dinâmico, utilizamos `@`. Vamos fazer um teste com o comando de condição, no caso o `@if()`

```php
@if(10 > 5)
    <p>A condição é true</p>
@endif
```

No caso, irá imprimir a frase entre a tag `<p>`

Podemos também passar valores entre a rota (`web.php`) e a view (`welcome.blade.php`), criando uma variável na rota e colocando-a como parâmetro do tipo chave/valor, e recuperando seu valor lá na view, utilizando a estrutura de duas chaves `{{ $nome }}`

`web.php`
```php
Route::get('/', function () {

    $nome = "Rodrigo";

    return view('welcome', ['nome' => $nome]);
});
```

`welcome.blade.php`
```php
<p>{{ $nome }}</p>
```

Imprime "Rodrigo"

Podemos ainda utilizar o `@else`, em cláusulas `@if`, e `@elseif`, como em:

```php
@if($nome == "Pedro")
    <p>O nome é Pedro</p>
@elseif($nome == "Rodrigo")
    <p>O nome é {{ $nome }}</p>
@else
    <p>O nome não é Pedro</p>
@endif
```

Podemos também utilizar mais de uma variável, como em (**utilizando o array chave/valor**):

`web.php`

```php
Route::get('/', function () {

    $nome = "Rodrigo";
    $idade = 36;

    return view('welcome', ['nome' => $nome, 'idade' => $idade, 'profissao' => "Programador"]);
});
```

`welcome.blade.php`

```php
@if($nome == "Pedro")
    <p>O nome é Pedro</p>
@elseif($nome == "Rodrigo")
    <p>O nome é {{ $nome }} e ele tem {{ $idade }} anos e trabalha como {{ $profissao }}</p>
@else
    <p>O nome não é Pedro</p>
@endif
```

## Explorando o Blade

- é possível criar estruturas de repetição no Blade (como `for` e `foreach`)
- executar PHP puro
- escrever comentários nos arquivos de view
- o Blade realmente é muito versártil e nos permite chegar em um resultado excelente de renderização de views

### For e foreach

Consideremos que tenhamos as seguintes arrays, lá no arquivo `web.php`:

```php
    $arr = [10,22,30,40,50];

    $nomes = ["Matheus", "Maria", "João", "Saulo"];

return view('welcome', 
    [
        'nome' => $nome, 
        'idade' => $idade,
        'profissao' => "Programador",
        'arr' => $arr,
        'nomes' => $nomes
    ]);
});
```

Para que possamos imprimir seus valores, usaremos o for e o foreach, lembrando sempre que é necessário fechar a diretiva.

```php
@for($i = 0; $i < count($arr); $i++)
    <p>{{ $arr[$i] }} - {{ $i }}</p>
    @if($i == 2)
        <p>O i é 2</p>
    @endif
@endfor

@foreach($nomes as $nome)

    <p>{{ $loop->index }}</p>
    <p>{{ $nome }}</p>

@endforeach
```

>Observação
>
>No foreach, há uma variável injetada, chamada de `$loop`, o qual podemos extrair os índices do array.

### Inserindo PHP puro

Há algumas situações que teremos que incluir PHP puro no código, para isso usaremos a diretiva `@php`:

```php
@php

    $name = "João";

    echo $name;

@endphp
```

Lembrando sempre de fechar a diretiva, com `@endphp`.

### Comentários

As vezes é interessante os comentários não aparecerem na hora de inspecionar código, para isso podemos utilizar os comentários em Laravel.

```html
    <!-- Comentário em HTML, que aparece no inspecionar código -->

    {{-- Este é o comentário do Blade, não aparece em inspicionar, não é renderizado, etc --}}
```

## Arquivos estáticos (CSS, JS e Imagens)

- Esses arquivos/recursos ficam na pasta `public`, e tem acesso direto nas tags que trabalham com arquivos estáticos

Podemos criar uma pasta `css`, `js` e `img`. Para acessar, a partir da view, usaremos o caminho `css/style.css`, por exemplo.

## Layouts com Blade

Se percebermos, toda página/view criada, teremos que repetir código, seja ele de um header ou de um footer, por exemplo.

Para evitar essa situação, podemos utilizar os Layouts com Blade

- essa funcionalidade permite o reaproveitamento de código (exemplo header e footer)
- é possível criar seções do site por meio do layout e também mudar o title da página

Vamos criar uma nova view chamada `view/layout/main.blade.php`, e nela criaremos nosso layout inicial:

```html
<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
    <head>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">

        <title>@yield('title')</title>

        <link rel="stylesheet" href="/css/style.css">  
        <script src="/js/script.js"></script>

    </head>

    <body>
    @yield('content')
    <footer>
        <p>HDC Events &copy; 2023</p>
    </footer>
    </body>
</html>
```

Agora lá em `welcome.blade.php` vamos apagar tudo até a tag `<body>`, deixando apenas o conteúdo das outras aulas.

Vamos começar extendendo o layout criado anteriormente, adicionando no topo do documento, o valor `@extends('layouts.main')`.

Agora podemos adicionar as sections, que irão completar as `@yield,` criadas em `main.blade.php`

Ficando o exemplo dessa maneira:

```php
@extends('layouts.main')

@section('title', 'HDC Events')

@section('content')
<h1>Algum título</h1>
<img src="img/banner.jpg" alt="">

@if(10 > 5)
    <p>A condição é true</p>
@endif

<p>{{ $nome }}</p>

@if($nome == "Pedro")
    <p>O nome é Pedro</p>
@elseif($nome == "Rodrigo")
    <p>O nome é {{ $nome }} e ele tem {{ $idade }} anos e trabalha como {{ $profissao }}</p>
@else
    <p>O nome não é Pedro</p>
@endif

@for($i = 0; $i < count($arr); $i++)
    <p>{{ $arr[$i] }} - {{ $i }}</p>
    @if($i == 2)
        <p>O i é 2</p>
    @endif
@endfor

@foreach($nomes as $nome)

    <p>{{ $loop->index }}</p>
    <p>{{ $nome }}</p>

@endforeach

@php

$name = "João";

echo $name;

@endphp

<!-- Comentário em HTML, que aparece no inspecionar código -->

{{-- Este é o comentário do Blade, não aparece em inspicionar, não é renderizado, etc --}}
    
@endsection
```

> Observação
>
> Não esquecer que precisa fechar o @section com @endsection

Para finalizar, vamos adicionar o Google Fontes, fonte Roboto, e o Bootstrap (aproveitei para deixarmos o CSS e o Javascript com um comentário também).

`main.blade.php`

```html
<!-- Fonte do Google -->
<link href="https://fonts.googleapis.com/css2?family=Roboto" rel="stylesheet">

<!-- CSS do Bootstrap -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous">

<!-- CSS da aplicação -->
<link rel="stylesheet" href="/css/style.css">

<!-- Javascript -->
<script src="/js/script.js"></script>
```

## Parâmetros nas rotas
- Podemos mudar como uma view nos responde adicionando parâmetros a uma rota;
- Ao definir a rota devemos colocar o parâmetro desta maneira: `{id}`
- Podemos ter parâmetros opcionais também, adicionando uma `?`
- O Laravel aceita também query parameters, utilizando a seguinte sintaxe: `?nome=Matheus&idade=29`