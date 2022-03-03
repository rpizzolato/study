# Projeto final em React do Curso do Matheus Batistti denominado COSTS
Criar o projeto com `npx create-react-app costs` e posteriormente instalar as dependências `npm i json-server react-icons react-router-dom uuid`. A dependência `json-server` é para criarmos um backend fake, e o `uuid` é para colocar um id único nos projetos que serão cadastrados posteriormente no aplicativo. Por fim rode `npm start` para iniciarmos o projeto

Deixar apenas os arquivos `App.js`, `index.css` e `index.js`. Vamos aproveitar para colocar um CSS global em `index.css` como abaixo:
```css
* {
  padding: 0;
  margin: 0;
  box-sizing:  border-box;
  font-family: 'Open Sans', sans-serif;
}

html, 
body, 
#root {
  background-color: #efefef;
  height: 100%;
}
```

Ir no Google Fonts e copiar o código da fonte `Open Sans` dentro do `public/index.html`, logo após a tag `<title>` (já aproveita para mudar o `<title>` para **Costs**)

Adicionar as imagens em um diretório `src/img`, se tiver `favicon.ico` podemos fazer a substituição