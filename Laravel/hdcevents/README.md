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
