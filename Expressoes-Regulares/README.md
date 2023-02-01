# Expressões Regulares

Esse é uma anotação do vídeo JavaScript #10 - Expressões Regulares do Rodrigo Branas, disponível [aqui](https://www.youtube.com/watch?v=9r48XuOB4DA&t). Como por infelizmente não estar trabalhando diariamente com programação, acabo por vezes tendo que recorrer ao vídeo, e a ideia é deixar aqui como uma consulta rápida para estudos e aprimoramento de alguns projetos. Gostaria de deixar um grande obrigado ao Branas, por estar sempre contribuindo com a comunidade de forma gratuita, sou imensamente grato, e acredito que por meio do estudo e muito trabalho, conseguimos alcançar nossos objetivos, no meu caso, atualmente, é me tornar Dev Full Time!

## O que são

São estruturas formadas por uma sequência de caracteres que especificam um padrão formal. Utilizado muito em:
- validação de campos
- extração de dados
- substituição de caracteres em textos (longos)

## Criando a expressão

```js
/* forma literal entre barras // (não usa aspas, como em outras linguagens) */
var regExp = /<expressao regular>/;

// outra possibilidade de criar
var regExp = new RegExp("<expressao regular>");
```

## API de retorno
- `exec`: executa a RegExp, retornando os detalhes
- `test`: testa a RegExp, retornando `true` ou `false`

## Passo 1
Esse passo envolve o reconhecimento de um telefone simples: `9999-9999` (será evoluído ao decorrer dos passos)

```js
var regExp = /9999-9999/;
var telefone = "9999-9999";

console.log(regExp.exec(telefone));
```
O retorno deve ser:
```
[ '9999-9999', index: 0, input: '9999-9999', groups: undefined ]
```
Que no caso quer dizer que foi encontrado o padrão que colocamos, no caso no index 0. Lembrando que utilizamos o `exec`. Agora iremos utilizar o `test`, que nos returna apenas `true` ou `false`:

```js
var regExp = /9999-9999/;
var telefone = "9999-9999";

console.log(regExp.test(telefone));
```
Deve ser impresso:
```
true
```

## Passo 2
Agora o telefone tem código de área: (48) 9999-9999.

Se apenas adicionarmos (48) na expressão e no telefone, irá retornar `false`, pois caracteres como parênteses, barras, etc, são especiais dentro da linguagem.
```js
var regExp = /(48) 9999-9999/;
var telefone = "(48) 9999-9999";

console.log(regExp.test(telefone));
```
Para contornar isso, usamos a `\`, que é utilizada antes de caracteres especiais, com o objetivo de "escapá-los". Logo se adicionarmos uma `\` antes de cada parênteses, nosso cenário irá mudar para `true`.

```js
var regExp = /\(48\) 9999-9999/;
var telefone = "(48) 9999-9999";

console.log(regExp.test(telefone));
```

## Passo 3
Fazer com que o telefone seja reconhecido unicamente, não permitindo outros caracteres antes e depois.

Se colocássemos da seguinte forma:
```js
var regExp = /\(48\) 9999-9999/;
var telefone = "O telefone é (48) 9999-9999, tratar com João";

console.log(regExp.test(telefone));
```
Ainda sim retornaria `true`, pois foi encontrado o padrão `\(48\) 9999-9999` dentro da string `O telefone é: (48) 9999-9999, tratar com João`. Se testarmos com `exec` em vez de `test`, ele retornaria que foi encontrado o padrão no index 13.
```
[
  '(48) 9999-9999',
  index: 13,
  input: 'O telefone é (48) 9999-9999, tratar com João',
  groups: undefined
]
```

Para corrigir isso, ou seja, para forçarmos a retornar false, usaremos um delimitador, mostrando o início e o fim de um determinado caractere.
- `^` inicia com um determinado caractere
- `$` finaliza ocm um determinado caractere

Se incluirmos o `^` no início do nosso padrão, dessa forma `regExp = /^\(48\) 9999-9999/;`, já teremos um retorno do `false`. Assim, para deixarmos como `true` teremos que obrigatoriamente remover o texto `O telefone é ` da variável `telefone`, ficando tudo dessa forma:
```js
var regExp = /^\(48\) 9999-9999/;
var telefone = "(48) 9999-9999, tratar com João";

// retorna true
console.log(regExp.test(telefone));
```
Assim, teremos um retorno `true`.

Se delimitarmos o final do padrão usando `$`, voltaremos a ter um `false` como retorno, aí basta que retiremos a parte `, tratar com João` da variável telefone, ficando dessa forma:

```js
var regExp = /^\(48\) 9999-9999$/;
var telefone = "(48) 9999-9999";

// retorna true
console.log(regExp.test(telefone));
```

## Passo 4
Agora é preciso começar a flexibilizar para que seja possível começar a aceitar qualquer tipo de número de telefone, por meio de grupos

### Grupos de caracteres
- `[abc]` - aceita qualquer caractere dentro do grupo, nesse caso: a, b e c
- `[^abc]` -  **não** aceita qualquer caractere dentro do grupo, nesse caso: a, b ou c
- `[0-9]` - aceita qualquer caractere entre 0 e 9 (geralmente `-` significa ter um range)
- `[^0-9]` - **não** aceita qualquer caractere entre 0 e 9

Se tentarmos mudar o DDD por exemplo, de 48 para 90, já retornaria `false`. Ou melhor ainda, se trocássemos todo o telefone, para `(80) 9876-1234`, por exemplo. Com certeza teremos um retorno falso.

Iremos usar us grupos de caractere `[0-9]`, ficando dessa forma:

```js
var regExp = /^\([0-9][0-9]\) [0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]$/;
var telefone = "(80) 9876-1234";

console.log(regExp.test(telefone));
```

Isso já faria que o retorno volte a ser `true`, no entanto perceba que o código ficou bem verboso.

## Passo 5
Para evitar a repetição exagerada de padrões em um expressão regular, podemos usar quantificadores.

### Quantificadores - Parte 1
Os quantificadores podem ser aplicados a caracteres, grupos, conjuntos ou metadados.

- `{n}` quantifica um número específico
- `{n,}` quantifica um número mínimo
- `{n,m}` quantifica um número mínimo e um número máximo

Agora, na frente do número que desejamos quantificar, usaremos da seguinte forma:

```js
var regExp = /^\([0-9]{2}\) [0-9]{4}-[0-9]{4}$/;
var telefone = "(80) 9876-1234";

console.log(regExp.test(telefone));
```

Agora podemos mudar o número normalmente, para qualquer outro número, que o retorno será sempre `true`.

## Passo 6
Em casos que é necessário utilizar números com 8 ou 9 dígitos, podemos utilizar um quantificador para especificar um determinado intervalo. Se inserirmos um novo dígito, ficando dessa forma: `(80) 99876-1234`, a expressão regular irá retornar `false`. Para corrigir, basta flexibilizarmos o intervalo, dessa forma (`{4,5}`):

```js
var regExp = /^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}$/;
var telefone = "(80) 99876-1234";

console.log(regExp.test(telefone));
```

Podemos criar mais um telefone, e testar ambos os valores:

```js
var regExp = /^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}$/;
var telefone1 = "(80) 9876-1234";
var telefone2 = "(80) 99876-1234";

// ambos retornam true
console.log(regExp.test(telefone1));
console.log(regExp.test(telefone2));
```

## Passo 7
Caso o hífen seja opcional, pois é bem comum ser escrito sem o hífen. Podemos utilizar um quantificador para torná-lo opcional.

### Quantificadores - Parte 2
Os quantificadores podem ser aplicados a caracteres, grupos, conjuntos ou metacaracteres.

- `?` zero ou um
- `*` zero ou mais
- `-` um ou mais

Assim usaremos o ponto de interrogação para flexibilizar o uso ou não do hífen:

```js
var regExp = /^\([0-9]{2}\) [0-9]{4,5}-?[0-9]{4}$/;
var telefone1 = "(80) 99876-1234"; // com hífen
var telefone2 = "(80) 998761234"; // sem hífen

// ambos retornam true
console.log(regExp.test(telefone1));
console.log(regExp.test(telefone2));
```

## Passo 8
Caso o telefone esteja em uma estrutura de tabela, e fazer reconhecer cada linha.

A estrutura em tabela:

```html
<table>
    <tr>
        <td>(80) 999778899</td>
    </tr>
</table>
```

Para isso, removeremos os caracteres de delimitação `^` e `$` do nosso padrão, e incluiremos as tag `<table><tr><td>` e fecharemos no final com `<\/td><\/tr><\/table>` (lembrando que precisamos "escapar" os caracteres de fechamento da tag, `/`).
```js
var regExp = /<table><tr><td>\([0-9]{2}\) [0-9]{4,5}-?[0-9]{4}<\/td><\/tr><\/table>/;

var telefone = "<table><tr><td>(80) 999778899</td></tr></table>";

// retorna true
console.log(regExp.test(telefone));
```

No entanto, no caso de usarmos uma estrutura com mais de um número de telefone, teremos que usar novamente o quantificador. Vejamos a estrutura com mais de um contato:

```html
<table>
    <tr>
        <td>(80) 999778899</td>
        <td>(90) 99897-8877</td>
        <td>(70) 98767-9999</td>
    </tr>
</table>
```

Dessa forma, se executarmos a validação, retornaria `false`. Para resolver isso, envolveremos a tag `<td>` com parenteses, e no fechamento do parenteses colocaremos o sinal de mais (`+`), para informar que pode ocorrer mais de uma vez.

```js
var regExp = /<table><tr>(<td>\([0-9]{2}\) [0-9]{4,5}-?[0-9]{4}<\/td>)+<\/tr><\/table>/;

var telefone = "<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>";

// retorna true
console.log(regExp.test(telefone));
```

Dessa forma, se apagarmos a tabela de telefones (deixar sem nenhum contato), retornaria `false`. Retornaria true se usássemos o asterisco (`*`) no lugar do sinal de mais.

- `n+` corresponde a qualquer string que contenha pelo menos um n.
- `n*` corresponde a qualquer string que contenha zero ou mais ocorrências de n.
- `n?` corresponde a qualquer string que contenha zero ou uma ocorrências de n.

## Passo 9 (Metacharacters)
Em muitos casos, é possível substituir os grupos por [metacaracteres](https://www.w3schools.com/jsref/jsref_obj_regexp.asp) específicos:

- `.` - representa qualquer caractere
- `\w` - representa o conjunto `[a-zA-Z0-9_]`
- `\W` - representa o conjunto `[^a-zA-Z0-9_]`
- `\d` - representa o conjunto `[0-9]`
- `\D` - representa o conjunto `[^0-9]`
- `\s` - representa um espaço em branco
- `\S` - representa um não espaço em branco
- `\n` - representa uma quebra de linha
- `\t` - representa um tab

Com isso, podemos fazer algumas substituições na nossa expressão regular, começando a substituir `[0-9]` por `\d` e espaços em branco por `\s`, ficando dessa forma:

```js
var regExp = /<table><tr>(<td>\(\d{2}\)\s\d{4,5}-?\d{4}<\/td>)+<\/tr><\/table>/;
```

Mesmo após essa mudança, nossa expressão ainda deve estar retornando `true`.

### String API

- `match` - executa uma busca na String e retorna uma array contendo os dados encontrados, ou null.
- `split` - divide a String com base em uma outra String fixa ou expressão regular.
- `replace` - substitui partes da String com base em uma outra String fixa ou expressão regular.

## Passo 10
Extrair os telefones das linhas da tabela. Primeiramente o telefone da primeira linha.

Primeiramente em `console.log()`, iremos chamar o método `match` da String telefone, e informar como parâmetro, nossa regExp:

```js
console.log(telefone.match(regExp));
```
Isso retornará todo nosso conteúdo dentro de telefone. O que, ainda, não é o que queremos.
```
[
  '<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>',
  '<td>(70) 98767-9999</td>',
  index: 0,
  input: '<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>',
  groups: undefined
]
```

Na nossa variável `regExp`, vamos remover todo conteúdo de `<table>`, pois não faz mais sentido, considerando que agora iremos extrair informação da variável `telefone`, ficando a variável `regExp` dessa forma:

```js
ar regExp = /\(\d{2}\)\s\d{4,5}-?\d{4}/;
```

Agora se executarmos, será retornado primeiro telefone, no index 15,conforme saída do programa:

```
[
  '(80) 999778899',
  index: 15,
  input: '<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>',
  groups: undefined
]
```

## Passo 11
Retornando todas as linhas de telefone (retornando todos os telefones)

### Modificadores

- `i` - case-insensitive matching
- `g` - global matching (pega tudo, não vai mais parar no primeiro match)
- `m` - multiline matching (quebra de linha)

No nosso caso, iremos colocar o `g` no final da expressão

```js
var regExp = /\(\d{2}\)\s\d{4,5}-?\d{4}/g; // incluímos o g

var telefone = "<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>";

console.log(telefone.match(regExp));
```
Será retornado um array com os telefones:
```
[ '(80) 999778899', '(90) 99897-8877', '(70) 98767-9999' ]
```

Dessa forma, podemos fazer algumas variações, como por exemplo, retornar somente os prefixos, ou somente os telefones sem o prefixo:

#### Retornando somente os prefixos

```js
var regExp = /\(\d{2}\)/g;

var telefone = "<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>";

console.log(telefone.match(regExp));
```

```
[ '(80)', '(90)', '(70)' ]

```

#### Retornando somente os telefones sem o prefixo

```js
var regExp = /\s\d{4,5}-?\d{4}/g;

var telefone = "<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>";

console.log(telefone.match(regExp));
```

```
[ ' 999778899', ' 99897-8877', ' 98767-9999' ]
```

## Passo 12
Substituir todos os telefones da tabela (`replace` no lugar do `match`)

```js
var regExp = /\(\d{2}\)\s\d{4,5}-?\d{4}/g;

var telefone = "<table><tr><td>(80) 999778899</td><td>(90) 99897-8877</td><td>(70) 98767-9999</td></tr></table>";

console.log(telefone.replace(regExp, "telefone"));
```
O retorno será:
```
<table><tr><td>telefone</td><td>telefone</td><td>telefone</td></tr></table>
```

## Fontes para estudo recomendado
- [MDN](https://developer.mozilla.org/pt-BR/docs/Web/JavaScript/Reference/Global_Objects/RegExp)
- [W3Schools](https://www.w3schools.com/js/js_regexp.asp)