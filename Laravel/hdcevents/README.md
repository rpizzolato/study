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

Vamos começar estendendo o layout criado anteriormente, adicionando no topo do documento, o valor `@extends('layouts.main')`.

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

O exemplo mais basico de rotas, seria esse aqui:

`web.php`

```php
Route::get('/produtos/{id}', function ($id) {
  return view('product', ['id' => $id]);
});
```

`product.blade.php`

```php
@extends('layouts.main')

@section('title', 'Produto')

@section('content')

  <p>Exibindo produto id: {{ $id }}</p>

@endsection
```

Dessa forma, ao acessarmos a rota `http://127.0.0.1:8000/produtos/300`, será impresso:

```
Exibindo produto id: 300
```

No entanto, podemos utilizar do operador `?`, e deixarmos um padrão para quando não haja entrada do número do produto, ficando dessa forma:

```php
// nesse caso deixamos como 1, mas podemos deixar como null
Route::get('/produtos/{id?}', function ($id = 1) {
  return view('product', ['id' => $id]);
});
```

>Observação
>
>Lembre-se sempre de deixar o valor padrão dentro da `function`

Da forma anterior, ainda exibiria `Exibindo produto id:`, ficando nulo a informação do produto, mas contornarmos isso, e somente exibir se houver valor, podemos utilizar a diretiva `@if` lá no `product.blade.php`:

```php
@extends('layouts.main')

@section('title', 'Produto')

@section('content')

  @if($id != null)
    <p>Exibindo produto id: {{ $id }}</p>
  @endif

@endsection
```

### Query params

Podemos também utilizar query params, e recuperar seu valor por meio do método `request()`. Considerando a seguinte url `http://127.0.0.1:8000/produtos?search=camisa`:

`web.php`

```php
Route::get('/produtos', function () {

    $busca = request('search');

    return view('products', ['busca' => $busca]);
});
```

`products.blade.php`

```php
@extends('layouts.main')

@section('title', 'Produtos')

@section('content')
  <h1>Tela de produtos</h1>
  @if($busca != '')
  <p>O usuário está buscando por: {{ $busca }}</p>
  @endif
@endsection
```

Teremos o seguinte retorno:

```
Tela de produtos
O usuário está buscando por: camisa

HDC Events © 2023
```

### Criando o header

Aproveitando nessa aula, será criado o header da página, com o seguinte código:

```html
<header>
  <nav class="navbar navbar-expand-lg navbar-light">
    <div class="collapse navbar-collapse" id="navbar">
      <a href="/" class="navbar-brand">
        <img src="/img/hdcevents_logo.svg" alt="logo da hdcevents">
      </a>
      <ul class="navbar-nav">
        <li class="nav-item">
          <a href="/" class="nav-link">Eventos</a>
        </li>
        <li class="nav-item">
          <a href="/" class="nav-link">Criar Eventos</a>
        </li>
        <li class="nav-item">
          <a href="/" class="nav-link">Entrar</a>
        </li>
        <li class="nav-item">
          <a href="/" class="nav-link">Cadastrar</a>
        </li>
      </ul>
    </div>
  </nav>
</header>
```

Como o logo acabou ficando muito grande, criaremos uma especificação em css para lidar com essa situação, lá no arquivo `style.css`:

```css
* {
    font-family: 'Roboto';
}

#navbar img {
  width: 50px;
}
```

## Controllers

- Os **Controllers** são parte fundamental de toda aplicação em Laravel
- Geralmente condensam a maior parte da lógica
- Tem o papel de **enviar e esperar resposta do banco de dados**
- tem também o papel de **receber e enviar alguma resposta para as views**
- podem ser criados via **artisan**
- é comum retornar uma view ou redirecionar para uma URL pelo Controller

A classe pai do Controller, fica em `app\Http\Controllers\Controller.php`, a qual iremos herdar para usar as demais.

> Dica
>
>Geralmente quando for criar um Controller, é interessante nomeá-lo no singular. Por exemplo, como estamos criando um sistema de eventos, iremos nomeá-lo como **Event**

Para criar um controller utilizando o artisan, usaremos o comando: `php artisan make:controller EventController`.

O arquivo `EventController.php` será criado no caminho que comentamos anteriormente, `app\Http\Controllers\Controller.php`, com o seguinte conteúdo:

```php
<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

class EventController extends Controller
{
    //
}
```

Agora, vamos até o arquivo `web.php` e vamos recortar a lógica da rota `/`. Vamos até o arquivo `EventController.php`, e vamos criar uma function chamada `index()` (**podemos também chamar essa function de action**), e colar a lógica recortada anteriormente dentro desta função. Ficando da seguinte forma:

```php
class EventController extends Controller
{
  public function index() {

  $nome = "Rodrigo";
  $idade = 36;

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
  }
}
```

Agora, voltamos ao arquivo `web.php` e vamos importar o Controller recém criado, para que possamos utilizá-lo. Para importar, usamos:

```php
use App\Http\Controllers\EventController;
```

Agora na rota `/`, dentro de `web.php`, vamos configurar para que possamos executar a função `index()`, que fica lá no nosso Controller:

```php
use App\Http\Controllers\EventController;

Route::get('/', [EventController::class, 'index']);
```

Após estes ajustes, podemos executar a aplicação para testar. 
Teoricamente não é para ter alterado nada na aplicação.

### Criando sequencia lógica da aplicação

No arquivo `web.php`, vamos criar uma nova rota que aponte para `/events/create`:

```php
Route::get('/events/create', [EventController::class, 'create']);
```

E vamos também criar uma nova action (ou function) chamada de `create()`, lá no Controller, que retornará a view `create`:

```php
  public function create() {
    return view('events.create');
  }
```

Agora criaremos essa view chamada `create`, a qual colocaremos na pasta `resources\views\events`, que também teremos que criar.

Esse arquivo se chamará `create.blade.php`, e terá o seguinte conteúdo inicialmente:

```php
@extends('layouts.main')

@section('title', 'Criar Evento')

@section('content')

  <h1>Crie um evento</h1>

@endsection
```

Por fim, lá no nosso layout, `main.blade.php`, no link "Criar Eventos", vamos colocar na propriedade href, o conteúdo da rota que criamos anteriormente, ou seja, `/events/create`, ficando dessa forma:

```html
<a href="/events/create" class="nav-link">Criar Eventos</a>
```

Para finalizar a esse módulo, vamos inserir o ion icons no arquivo `main.blade.php`, indo até a página do [ionicons](https://ionic.io/ionicons/usage) e copiando a tag `<script>` para o final, antes do fechamento da tag `<body>`, ficando dessa forma:

```html
  </footer>
  <script type="module" src="https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.esm.js"></script>
  <script nomodule src="https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.js"></script>
</body>
```

## Conexão com o Banco de Dados

- A conexão do Laravel com o BD é configurada pelo arquivo `.env`. Isso proporciona mais segurança e maior liberdade na aplicação.
- O Laravel utiliza um **ORM** (Object-Relational Mapping), chamada **Eloquent**
- E também para a criação de tabelas, as **migrations**

Vamos até o arquivo `.env`, que fica na raiz do projeto, e vamos deixar a configuração de DB dessa forma: `DB_DATABASE=hdceventscurso`. Agora para testarmos, podemos executar o comando `php artisan migrate`, que irá criar as migrations em `database\migrations`

## Migrations

- As **migrations** funcionam como um versionamento de BD, podendo **avançar e retroceder** a qualquer momento
- Podemos ainda **adicionar e remover colunas** de forma facilitada
- Fazer o setup de DB de uma nova instalação em apenas um comando
- Podemos verificar as migrations com `migrate:status`

Para criarmos uma migration, usamos o comando `php artisan make:migration create_products_table` (migrate com nome `create_products_table`, o nome da tabela será **products**)

Usamos o comando `php artisan migrate:status` para vermos o status das migrations (perceba que há uma migration que não foi executada ainda, que fica como **Pending**)

Para efetivamente criarmos a tabela no BD, usamos o comando `php artisan migrate`. Por curiosidade, execute novamente o comando de verificar o status, verá que agora a **migration** referente a products aparecerá como executada.

Vamos adicionar um novo campo a essa tabela, no caso, o campo "name", ficando dessa forma o arquivo da migration referente a produtos:

`2023_02_26_001221_create_products_table.php`

```php
public function up(): void
{
  Schema::create('products', function (Blueprint $table) {
    $table->id();
    $table->string('name');
    $table->timestamps();
  });
}
```

>Observação
>
>`string` é equivalente a `varchar`, podendo ser usado delimitando o tamanho, como por exemplo, `$table->string('name', 100);` (limita a 100 caracteres)

Agora para executarmos com as mudanças que fizemos, usamos: `php artisan migrate:fresh`. O que esse comando faz é simplesmente excluir (drop) as tabelas, e criar novamente, agora com as mudanças

Vamos aplicar mais algumas mudanças na tabela, dessa forma:

```php
public function up(): void
{
  Schema::create('products', function (Blueprint $table) {
    $table->id();
    $table->string('name', 100);
    $table->integer('qty');
    $table->text('description');
    $table->timestamps();
  });
}
```

>Observação
>
>`$table->text('description');` criará um tipo TEXT, e NÃO VARCHAR ou CHAR. O tipo TEXT tem mais características em comparação ao VARCHAR e o CHAR.<br> Ele é útil quando usado com textos longos, que tomam de 1 byte a 4 Giga Bytes. Normalmente usado para armazenar artigos em corpos de sites, especificação de produto em sites de e-commerce.<br> Diferentemente de VARCHAR e CHAR, você não precisa especificar o tamanho que quer usar na coluna, e diferentemente de VARCHAR e CHAR, que o MySQL remover espaços em branco ao inserir ou recuperar a informação, isso não ocorre com TEXT.<br> É importante frisar também que TEXT não é armazenado na memória do servidor, assim, sempre que um texto do tipo TEXT for requisitado, o MySQL irá lier diretamente do disco rígido, o que é muito mais lento em comparação ao CHAR e o VARCHAR.<br> Temos ainda 4 tipos de TEXT, o **TINYTEXT** - 255 Bytes (255 characters), **TEXT** - 64KB (65,535 characters), **MEDIUMTEXT** - 16MB (16,777,215 characters) e o **LONGTEXT** - 4GB (4,294,967,295 characters). Fonte [aqui](https://www.mysqltutorial.org/mysql-text/), para mais detalhes.

## Avançando em Migrations

- Quando for preciso adicionar um novo campo a uma tabela, **devemos criar uma nova migration**
- Porém devemos tomar cuidado para não rodar o comando `fresh`, e apagar os dados existentes
- O comando `rollback` pode ser utilizado para voltar uma migration
- para voltar todas as migrations, podemos utilizar o comando `reset`
- para votar todas as migrations e rodar o migrate novamente utilizamos o comando `refresh`

### Adicionado campo a tabela sem fresh (drop e create)

Há casos específicos que necessitamos adicionar um novo campo a alguma tabela sem excluir os dados existentes, para isso faremos o seguinte

1. rodamos o comando `php artisan make:migration add_category_to_products_table`. Nesse caso criamos uma migration para lidar com a criação de um novo campo, chamado "category" a tabela "products".

2. no no arquivo criado, `2023_02_26_130251_add_category_to_products_table.php`, ele faz uma referência à `Schema::table`, diferentemente de uma migration comum, que faz referência à `Schema::create`

3. vamos adicionar o campo que queremos na função `up()` e na função `down()`. Colocamos na função `down()` para que o campo seja removido em um `rollback`, e repare que trocamos para `droColumn` lá no método `down()`, ficando dessa forma:

```php
public function up(): void
{
  Schema::table('products', function (Blueprint $table) {
      $table->string('category', 100);
  });
}

public function down(): void
{
  Schema::table('products', function (Blueprint $table) {
    $table->dropColumn('category');
  });
}
```

4. Vamos conferir se a migration está como pendente, executando `php artisan migrate:status`. Estando tudo certo, vamos executar a migrate, com o comando: `php artisan migrate`. E assim muito provavelmente a tabela "products" já estará com seu campo novo "category".

5. Caso tenhamos que remover o campo "category", basta executarmos `php artisan migrate:rollback`.

>**Importante!**
>
>Lembre que o `rollback` desfaz toda a ação anterior que tenha feito. Se caso tenha criado o BD com o `fresh` por exemplo, já com o campo "category", conforme fizemos anteriormente, e dermos um `rollback`, vai ser excluído tudo, pois nossa ação anterior foi de criar tudo.


### Reset

Se quisermos dar um `rollback` em tudo, usamos o comando `reset`, dessa forma: `php artisan migrate:reset`. Você pode conferir que apagou tudo por meio do comando `php artisan migrate:status`, ou ir diretamente no BD e conferir que não há mais tabelas.

Caso queira recriar tudo novamente, use `php artisan migrate`

### Refresh

O que acabamos de fazer com o comando `reset`, podemos fazer com um único comando, `php artisan migrate:refresh`.

### Fresh vs Refresh

- `migrate:fresh`: exclui (DROP) todas as tabelas no database e executa novamente todas as migrations.
- `migrate:refresh`: faz um "rollback" de cada migration e então roda novamente todas as migrations

A maior diferença é que `migrate:fresh` limpa toda a database instantaneamente, enquanto `migrate:refresh` faz isso passo a passo. Saiba mais [aqui](https://www.aldohadinata.com/laravel-migrate-fresh-and-refresh-difference/)

## Eloquent

- É uma **ORM** (Object-Relational Mapping) do Laravel;
- Cada tabela tem um Model que é responsável pela interação entre as requisições ao banco
- A convenção para o Model é o nome da entidade no singular, enquanto a tabela é a entidade no plural: No caso do projeto, Event e events
- No Model faremos poucas alterações dos arquivos, geralmente configurações específicas

### Migration events

Podemos começar excluindo as migrations de products e efetivamente criando a migration de events, com esse comando: `php artisan make:migration create_events_table`

Vamos preencher a migration events da seguinte forma:

```php
public function up(): void
{
  Schema::create('events', function (Blueprint $table) {
    $table->id();
    $table->timestamps();
    $table->string("title");
    $table->text("description");
    $table->string("city");
    $table->boolean("private");
  });
}
```
Agora podemos executar com o comando `php artisan migrate` (se preferir, pode verificar com o `migrate:status`)

Teremos que criar o Model, entidade essa que vai fazer a conexão entre o BD e a aplicação. Repare que já temos uma pasta de Models, que fica em `app\Models`, e podemos utilizar o artisan para criar os models que iremos usar. Executaremos o comando `php artisan make:model Event`, que criará o arquivo `Event.php` lá na pasta `app\Models`

Agora vamos no BD para criar alguns dados para nossa aplicação. Repare que a tabela "products" ainda está lá, rode `php artisan migrate:fresh` para zerarmos tudo, e deixar apenas a tabela "events".

Após inserir alguns dados, vamos até o nosso Controller `EventController.php`, pois é no Controller que geralmente colocamos toda a lógica da aplicação, assim como buscar os dados no BD.

Primeiramente precisamos importar o Model que criamos anteriormente, pois é por meio dele que iremos acessar esses dados. Portanto, no nosso arquivo `EventController.php`, ficará dessa forma:

```php
<?php
namespace App\Http\Controllers;

use Illuminate\Http\Request;

// importa Event que iremos usar
use App\Models\Event;

class EventController extends Controller
{
  public function index() {

  // var $events recebe todos os eventos
  // método all() simplesmente pega todos os registros
  $events = Event::all();

  // pegamos todos os eventos e enviamos à view
  return view('welcome',['events' => $events]);
  }

  public function create() {
    return view('events.create');
  }
}

```

Agora vamos fazer algumas limpezas, começando por excluir as views de products. Na view welcome, vamos apenas limpar o conteúdo das outras aulas. Vamos também nas rotas e limpar as rotas de produtos

### Recuperando dados com foreach

No arquivo `welcome.blade.php`, vamos resgatar os dados por meio do comando foreach, ficando esse arquivo em questão dessa forma:

```php
@extends('layouts.main')

@section('title', 'HDC Events')

@section('content')

  @foreach ($events as $event)

    <p>{{ $event->title }} -- {{ $event->description }}</p>

  @endforeach

@endsection
```

Após essa alteração, já será possível ver os dados sendo resgatados do BD

## Finalizando a Home

Essa parte como foge um pouco do escopo, não será incluído aqui, no entanto, é possível ver todo HTML e CSS gerados no projeto, acessando seus respectivos arquivos.

## Adicionando registro ao BD

- No Laravel é comum ter uma action específica para o post chamada de **store**
- Lá será criado um objeto e composto ele com base nos dados enviados pelo **POST**
- Com o objeto formado, utilizamos o método **save** para persistir os dados.

Primeiro passo é criarmos um formulário dentro de `create.blade.php`:

```html
<div id="event-create-container" class="col-md-6 offset-md-3">
  <h1>Crie seu evento</h1>
  <form action="/events" method="POST">
    <div class="form-group">
      <label for="title">Evento:</label>
      <input type="text" name="title" id="title" class="form-control" placeholder="Nome do evento">
    </div>
    <div class="form-group">
      <label for="title">Cidade:</label>
      <input type="text" name="city" id="city" class="form-control" placeholder="Local do evento">
    </div>
    <div class="form-group">
      <label for="title">O Evento é privado?</label>
      <select name="private" id="private" class="form-control">
        <option value="0">Não</option>
        <option value="1">Sim</option>
      </select>
    </div>
    <div class="form-group">
      <label for="title">Descrição:</label>
      <textarea name="description" id="description" class="form-control" placeholder="O que vai acontecer no evento"></textarea>
    </div>
    <input type="submit" class="btn btn-primary" value="Criar Evento">
  </form>
</div>
```

Agora vamos nas rotas e criaremos uma rota de POST, ficando a rota dessa forma:

```php
Route::post('/events', [EventController::class, 'store']);
```

Aí partimos para o `EventController.php`, onde conterá o método `store()`, adicionado ali no final da rota.

```php
// por meio do Request conseguimos recuperar o que vem do form
public function store(Request $request) {
  // criamos uma instancia/objeto do event
  $event = new Event;

  // configuramos os campos com os valores recebidos
  $event->title = $request->title;
  $event->city = $request->city;
  $event->private = $request->private;
  $event->description = $request->description;

  // persistimos no BD essas informações
  $event->save();

  // redirecionamos o usuário para a página principal
  return redirect('/');
}
```

O Laravel possui uma proteção de [CSRF](https://laravel.com/docs/10.x/csrf), e portanto, para enviarmos o formulário, precisamos colocar a diretiva `@csrf` dentro do form.

```html
<form action="/events" method="POST">
  @csrf
  <div class="form-group">
```

Agora é só testar se está cadastrando o evento com sucesso

## Flash Messages

- Podemos adicionar mensagens ao usuário por **session**
- Estas mensagens são conhecidas por **flash messages**
- Podemos adicionar com o método `with` nos Controllers
- Utilizadas para apresentar um feedback ao usuário
- No blade podemos verificar a presença da mensagem pela diretiva `@session`
