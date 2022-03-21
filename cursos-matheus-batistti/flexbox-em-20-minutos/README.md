# Anotações do vídeo Aprenda Flexbox em 20 minutos com o Matheus Batistti
[vídeo](https://www.youtube.com/watch?v=P9TrFDNwor4)

>**Observação importante**
>
>Como o github bloqueia formatação CSS no arquivo `README.md`, recomendo que faça o download desse arquivo, abra com o *VSCode*, e abra o *Preview*, que assim verá as formatações do CSS dos exemplos abaixo

### Sumário
1. [Alinhando horizontalmente](#Alinhando-horizontalmente)
2. [Alinhando verticalmente ](#Alinhando-verticalmente )
3. [Limitação de Elementos](#Limitação-de-Elementos)
4. [flex-basis](#flex-basis)
5. [Alterando a ordem dos elementos](#Alterando-a-ordem-dos-elementos)

Primeiro passo é criar uma estrutura básica HTML e CSS. Criaremos uma `<div>` que será o *container* dos demais itens (filhos).

>`index.html`
```html
<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Flexbox</title>
  <link rel="stylesheet" href="style.css">
</head>

<body>
  <!-- Container -->
  <div class="container">
    <div class="item">1</div>
    <div class="item">2</div>
    <div class="item">3</div>
  </div>
</body>

</html>
```
<hr>

>`style.css`
```css
.container {
  background-color: #eee;
  border: 2px solid red;
  padding: 15px;
  max-width: 300px;
  height: 300px;
}

.item {
  background-color: #000;
  border: 2px solid blue;
  height: 50px;
  width: 50px;
}
```
<div class="container" style="
  background-color: #eee;
  border: 2px solid red;
  padding: 15px;
  max-width: 300px;
  height: 300px;"
>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">1</div>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">2</div>    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">3</div>
  </div>
  <br />

  Se removermos o `width` da classe `.item`, teremos um comportamento de bloco por parte da `<div>`, o que é um comportamento normal dessa tag, ocupando todo espaço restante, da seguinte forma:
  <div class="container" style="
  background-color: #eee;
  border: 2px solid red;
  padding: 15px;
  max-width: 300px;
  height: 300px;"
>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
    ">1</div>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
    ">2</div>    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
    ">3</div>
  </div>
  <br />

  Se mudarmos o `display` para **flex**, mudaremos o comportamento padrão dos elementos filhos, conforme podemos notar a seguir:
  >`style.cs`
  ```css
  .container {
  background-color: #eee;
  border: 2px solid red;
  padding: 15px;
  max-width: 300px;
  height: 300px;

  display: flex;
}

.item {
  background-color: #ccc;
  border: 2px solid blue;
  height: 50px;
  width: 50px;
}
  ```
  <div class="container" style="
    background-color: #eee;
    border: 2px solid red;
    padding: 15px;
    max-width: 300px;
    height: 300px;
    display: flex"
  >
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">1</div>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">2</div>    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">3</div>
  </div>
  <br />

>**Observação**
>
>Nesse momento, os elementos já passam a se comportar com **flex**. Se notar, não serão mais blocos e sim *inline*

## Alterando o alinhamento
### Alinhando horizontalmente
**(row - linha - eixo X)**

- `justify-content: center;` ► centralizar horizontalmente
  <div class="container" style="
    background-color: #eee;
    border: 2px solid red;
    padding: 15px;
    max-width: 300px;
    height: 300px;
    display: flex;
    justify-content: center;"
  >
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">1</div>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">2</div>    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">3</div>
  </div>
  <br />
- `justify-content: space-around;` ► gera espaçamento entre os elementos e em volta deles também
  <div class="container" style="
    background-color: #eee;
    border: 2px solid red;
    padding: 15px;
    max-width: 300px;
    height: 300px;
    display: flex;
    justify-content: space-around;"
  >
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">1</div>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">2</div>    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">3</div>
  </div>
  <br />
- `justify-content: space-between;` ► gera espaçamento somente entre os elementos (o espaçamento que temos em volta é devido ao `padding: 15px` da classe `.container`)
  <div class="container" style="
    background-color: #eee;
    border: 2px solid red;
    padding: 15px;
    max-width: 300px;
    height: 300px;
    display: flex;
    justify-content: space-between;"
  >
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">1</div>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">2</div>    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">3</div>
  </div>
  <br />

### Alinhando verticalmente 
**(col - coluna - eixo Y)**
- `align-items: center;` ► centralizar verticalmente
  <div class="container" style="
    background-color: #eee;
    border: 2px solid red;
    padding: 15px;
    max-width: 300px;
    height: 300px;
    display: flex;
    justify-content: space-between;
    align-items: center;"
  >
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">1</div>
    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">2</div>    <div class="item" style="
      background-color: #ccc;
      border: 2px solid blue;
      height: 50px;
      width: 50px;
    ">3</div>
  </div>
  <br />

- Se usarmos `justify-content: center;` e `align-items: center;` combinado, temos o seguinte resultado
  <div class="container" style="
    background-color: #eee;
    border: 2px solid red;
    padding: 15px;
    max-width: 300px;
    height: 300px;
    display: flex;
    justify-content: center;
    align-items: center;"
  >
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">1</div>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">2</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">3</div>
</div>
<br />

## Limitação de Elementos

- O *flexbox* não possui uma limitação de elementos. Se tentarmos inserir mais itens, o *flexbox* vai tentar ajustar ao tamanho disponível (que são os **300px** + o ** ** de padding). Vamos inserir mais itens no código HTML
```html
<body>
  <!-- Container -->
  <div class="container">
    <div class="item">1</div>
    <div class="item">2</div>
    <div class="item">3</div>
    <div class="item">4</div>
    <div class="item">5</div>
    <div class="item">6</div>
    <div class="item">7</div>
    <div class="item">8</div>
  </div>
</body>
```
<div class="container" style="
background-color: #eee;
border: 2px solid red;
padding: 15px;
max-width: 300px;
height: 300px;
display: flex;
justify-content: center;
align-items: center;"
>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">1</div>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">2</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">3</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">4</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">5</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">6</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">7</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">8</div>
</div>
<br />

- `flex-wrap: wrap;` ► determina que quando a **área máxima** seja atingida, os itens vão automaticamente para uma segunda linha, e assim por diante
<div class="container" style="
background-color: #eee;
border: 2px solid red;
padding: 15px;
max-width: 300px;
height: 300px;
display: flex;
justify-content: space-between;
align-items: center;
flex-wrap: wrap;"
>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">1</div>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">2</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">3</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">4</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">5</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">6</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">7</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">8</div>
</div>
<br />

- Caso a quantidade de itens seja igual em ambas as linhas, o *flexbox* viabilizará de forma automática, a melhor opção de alinhamento para os itens. Vamos adicionar mais dois itens no HTML para vermos o resultado
<div class="container" style="
background-color: #eee;
border: 2px solid red;
padding: 15px;
max-width: 300px;
height: 300px;
display: flex;
justify-content: space-between;
align-items: center;
flex-wrap: wrap;"
>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">1</div>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">2</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">3</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">4</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">5</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">6</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">7</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">8</div><div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">9</div><div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">10</div>
</div>
<br />

- Se adicionarmos mais um item, será criado uma **terceira** linha, como podemos notar
<div class="container" style="
background-color: #eee;
border: 2px solid red;
padding: 15px;
max-width: 300px;
height: 300px;
display: flex;
justify-content: space-between;
align-items: center;
flex-wrap: wrap;"
>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">1</div>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">2</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">3</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">4</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">5</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">6</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">7</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">8</div><div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">9</div><div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">10</div><div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    width: 50px;
  ">11</div>
</div>
<br />

- outra característica do *flexbox* é que se não definirmos uma largura para o elemento, será ajustado conforme o tamanho do conteúdo 
<div class="container" style="
background-color: #eee;
border: 2px solid red;
padding: 15px;
max-width: 300px;
height: 300px;
display: flex;
justify-content: space-between;
align-items: center;
flex-wrap: wrap;"
>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
  ">1</div>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
  ">2</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
  ">3</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
  ">4</div>
</div>
<br />

## flex-basis
- `flex-basis: 10px;` ► define um valor base para o elemento que ele é aplicado. Caso ele seja reduzido devido ao tamanho da tela, ele manterá uma base de `10px`.


- `flex-grow: 2;` ► define um peso em que o elemento ocupará em termos de espaço em tela (ou o espaço disponível dentro de algum container)

<div class="container" style="
background-color: #eee;
border: 2px solid red;
padding: 15px;
max-width: 300px;
height: 300px;
display: flex;
justify-content: space-between;
align-items: center;
flex-wrap: wrap;"
>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    flex-basis: 30px;
    flex-grow: 2;
  ">1</div>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    flex-basis: 10px;
    flex-grow: 1;
  ">2</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    flex-basis: 10px;
    flex-grow: 1;
  ">3</div>
</div>
<br />

- `flex-shrink: 2;` ► controlará a intensidade quando o elemento diminui, no caso vai diminuir 2x mais proporcionalmente do que os outros elementos que não possuem essa propriedade. Por exemplo, se removermos o `flex-wrap: wrap;` (que evitará o elemento ir para outra linha) e deixarmos `flex-basis: 100px`, `flex-grow: 1;` e `flex-shrink: 1;` dentro da classe `.box` (fazendo com que todos os itens tenham essa propriedade e fiquem iguais e mantenham a proporção ao diminuírem de tamanho - limitado ao `basis` de **100px**, é claro) e colocar `flex-shrink: 2` no `#item-3` e diminuirmos a tela, o `#item-3` irá diminuir sempre o **dobro** dos outros elementos. Resumidamente, trabalha de forma contrária ao `flex-grow`

>**Observação**
>
>Podemos usar um *shorthand* para o *grow*, *shrink* e o *basis* de uma vez só: ► ficando dessa forma: `flex: 1 1 50px;` (respectivamente grow, shrink e basis)

## Alterando a ordem dos elementos

- `order: 1` ► faz o elemento selecionado ficar na primeira posição.
<div class="container" style="
background-color: #eee;
border: 2px solid red;
padding: 15px;
max-width: 300px;
height: 300px;
display: flex;
justify-content: space-between;
align-items: center;
"
>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    flex-basis: 100px;
    order: 3;
  ">1</div>
  <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    flex-basis: 100px;
    order: 1;
  ">2</div>    <div class="item" style="
    background-color: #ccc;
    border: 2px solid blue;
    height: 50px;
    flex-basis: 100px;
    order: 2;
  ">3</div>
</div>
<br />