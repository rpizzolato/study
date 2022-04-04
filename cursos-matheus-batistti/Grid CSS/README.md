# Anotações do vídeo Aprenda CSS Grid em 30 minutos com o Matheus Batistti
[vídeo](https://www.youtube.com/watch?v=8VapN6x897U)

Basicamente o conceito de o elemento pai controlar os demais elementos pertencentes a ele. No caso usaremos a propriedade `display: grid`

- `grid-template-columns: 50px auto 10%;` no elemento pai (`.container`), fará que a tenhamos três colunas com os respectivos valores definidos acima. Mas podemos definir mais colunas se for necessário.

- `grid-template-rows: 70% 30%;` também no elemento pai (`.container`), fará que as duas linhas que temos, defina o tamanho conforme os valores colocados na propriedade.

- `gap: 5px;` cria um espaçamento interno entre os itens. Nesse caso cria tanto horizontal como verticalmente. Caso queira apenas horizontalmente, use `grid-row-gap: 10px` para criar um espaçamento de 10 pixels, ou use `grid-column-gap: 15px` para criar um espaçamento de 15 pixels verticalmente.

>`index.html`
```html
<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Grid CSS</title>
  <link rel="stylesheet" href="style.css">
</head>

<body>
  <!-- grid início -->
  <div class="container">
    <div>1</div>
    <div>2</div>
    <div>3</div>
    <div>4</div>
    <div>5</div>
    <div>6</div>
  </div>
</body>

</html>
```
<hr>

>`styles.css`
```css
.container {
  background-color: #333;
  border: 3px solid red;
  padding: 15px;

  display: grid;
  grid-template-columns: 50px auto 10%;
}

.container div {
  background-color: #ccc;  
  padding: 20px;
  border: 1px solid blue;
}
```
<div class="container" style="
  background-color: #eee;
  border: 3px solid red;
  padding: 15px;
  display: grid;
  grid-template-columns: 50px auto 10%;
  height: 35vh;
  grid-template-rows: 70% 30%;
  gap: 5px"
>
    <div style="
      background-color: #ccc;  
      padding: 20px;
      border: 1px solid blue;
    ">1</div>
    <div style="
      background-color: #ccc;  
      padding: 20px;
      border: 1px solid blue;
    ">2</div>
    <div style="
      background-color: #ccc;  
      padding: 20px;
      border: 1px solid blue;
    ">3</div>
    <div style="
      background-color: #ccc;  
      padding: 20px;
      border: 1px solid blue;
    ">4</div>
    <div style="
      background-color: #ccc;  
      padding: 20px;
      border: 1px solid blue;
    ">5</div>
    <div style="
      background-color: #ccc;  
      padding: 20px;
      border: 1px solid blue;
    ">6</div>
    
  </div>
  <br />

## Template areas
Útil quando iremos dividir um site em diversas áreas, como por exemplo, cabeçalho, sidebar, conteúdo e rodapé.

Usaremos 

