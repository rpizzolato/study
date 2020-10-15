## NLW#3

### Trilha OmniStack: Workshop 01
#### 1º Dia: Conceitos e estrutura web

### Tecnologias utilizadas
- NodeJS
- TypeScript
- ReactJS

## Criando a estrutura do projeto
- utilizando `npx create-react-app my-app --template typescript` ou `yarn create react-app my-app --template typescript`
- depois é excluído alguns conteúdos não importantes como:
  - deixar a pasta `public` somente com o arquivo `index.html`;
  - dentro do arquivo `index.html`, no cabeçalho deixar somente o ` <meta charset="utf-8" />` e `<meta name="viewport" content="width=device-width, initial-scale=1" />`. Remove os comentários entre a `<div id="root"></div>` e o `</body>`;
  - em `src` excluir: `App.test.tsx`, `App.css`, `index.css`, `logo.svg`, `serviceWorker.ts` e `setupTests.ts`, ficando apenas: `App.tsx`, `idnex.tsx` e `react-app-env.d.ts`.
  - em `index.tsx` remover:
    - `import * as serviceWorker from './serviceWorker';`;
    - `import './index.css';`;
    - parte inferior de comentários, juntamente com `serviceWorker.unregister();`;
  - em `App.tsx` remover:
    - `import logo from './logo.svg';` e `import './App.css';`;
    - na `<div className="App">` deixar somente um `<h1>Hello World</h1>` por enquanto apenas visualizar.
- rodando o projeto: `npm start`

### Componentes
- um componente é uma função que retorna um conteúdo HTML ou conteúdo JSX

### Propriedades
- `props` são acessados pelo componente filho, conforme exemplo abaixo:
```ts
import React from 'react';

//interface para tipagem no TS
interface TitleProps {
  text: string;
}

//Componente Filho
function Title(props: TitleProps) {
  return <h1>{props.text}</h1>;
}

//Componente Pai
function App() {
  return (
    <div className="App">
      <Title text="Título 1" />
      <Title text="Título 2" />
      <Title text="Título 3" />
    </div>
  );
}

export default App;
```
### Ícones
- rode: `npm install react-icons` ou `yarn add react-icons`;
- importe os ícones desejados, no caso do exemplo abaixo, estará usando [Feather Icons](https://feathericons.com/):
```ts
import { FiArrowRight } from 'react-icons/fi';

<FiArrowRight size={26} color="rgba(0, 0, 0, 0.6)" />
```

### Roteamento
- será utilizado o [react-router-dom](https://reactrouter.com/web/guides/quick-start). Utilize `npm install react-router-dom` ou `yarn add react-router-dom`. Por estarmos ligando com TS, será necessário ainda instalar como *dependência*, o seguinte pacote: `npm install @types/react-router-dom -D` ou `yarn add @types/react-router-dom -D`, para que possamos, ao apertar `Ctrl + Espaço`, listar o conteúdo do _react-router-dom_;
- dentro de `src`, crie um arquivo chamado `routes.tsx` para ligar com as rotas. Dentro dele, comece importando o _react-router-dom_:
```ts
import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import Landing from './pages/Landing';
import OrphanagesMap from './pages/OrphanagesMap';

function Routes() {
  return(
    <BrowserRouter>
      <Route path="/" exact component={Landing} />
      <Route path="/app" component={OrphanagesMap} />
    </BrowserRouter>
  );
}

export default Routes;
```
**observação**: caso utilize o <Route /> sem a propriedade `exact`, será chamado as duas rotas configuradas acima. Para outros caso que possam ser chamadas duas ou mais rotas, as colocamos em volta do <Switch>, pois assim será chamado apenas uma rota por vez.
```ts
<BrowserRouter>
  <Switch>
    <Route path="/" exact component={Landing} />
    <Route path="/app" component={OrphanagesMap} />
  </Switch>
</BrowserRouter>
```

### Link
- faz parte do pacote _react-router-dom_, faz com o uso de âncoras/links sejam realizados sem o _reload_ da página completa. Comece importando:
```ts
import { Link } from 'react-router-dom';

<Link to="/app" className="enter-app">
  <FiArrowRight size={26} color="rgba(0, 0, 0, 0.6)" />
</Link>
``` 

### Mapas
- existem algumas alternativas como o Google Maps, no entanto é um solução paga, embora haja plano gratuíto, é necessário o cadastro de um cartão de crédito;
- uma alternativa gratuíta é o uso do [Leaflet](https://leafletjs.com/), que nada mais é que a inserção de mapas no JS. Para uso no React, há o [React Leaflet](https://react-leaflet.js.org/) para integração do mapa;
- para instalar o Leaflet, execute: `npm install leaflet react-leaflet` ou `yarn add leaflet react-leaflet`. Utilizando TS, use `@types/react-leaflet -D`;
- importe o `Map` do `react-leaflet`. Importe também o `TileLayer`, que seria a imagem do Mapa. Como _Tile_ padrão, irá ser utilizado o [OpenStreetMap](https://www.openstreetmap.org/#map=4/-15.13/-53.19) que é gratuíto:
```ts
import { Map, TileLayer } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';

<Map
  center={[-22.7244976,-47.6352641]}
  zoom={15}
  style={{ width: '100%', height: '100%' }}
  >
  <TileLayer url="https://a.tile.openstreetmap.org/{z}/{x}/{y}.png" />
</Map>
```
Na propriedade `center`, o valor esperado é um array, logo é colocado entre colchetes `[]`. Em `style` usa-se duas chaves `{{}}` pois o estilo aqui precisa ser escrito como um **objeto**, logo o primeiro par de chaves é da própria propriedade de `style`, e o segundo é para indicar que se trata de um **objeto**.
- outra alternativa é utilizar o [MapBox](https://www.mapbox.com/), que possui um plano gratuíto generoso e não precisa cadastrar cartão, sendo o único adendo ter que criar uma conta para uso. Ao criar a conta, é gerado um token privado para seu uso, não sendo aconselhável guardá-lo em qualquer arquivo que vá ficar público. Para isso é recomendável guardar em um **arquivo de ambiente**. Crie um arquivo chamado `.env` na raíz do projeto e preencha da seguinte forma: `REACT_APP_MAPBOX_TOKEN=token_copiado_do_mapbox`. É importante salientar que toda variável de ambiente no React deve começar com `REACT_APP_`, sendo seu final customizável. No código, mudaremos apenas o `<TileLayer>`, conforme abaixo:
```ts
<TileLayer url={`https://api.mapbox.com/styles/v1/mapbox/light-v10/tiles/256/{z}/{x}/{y}@2x?access_tokens=${process.env.REACT_APP_MAPBOX_TOKEN}`} />
```
**observação**: caso seja necessário estilizar a tag que fica em volta do mapa, use a marcação `.leaflet-container`, caso seja necessário utilizar `z-index` por exemplo.

### Trilha OmniStack: Workshop 02
#### 2º Dia: Back-end com Node.js

- primeiro passo é diferenciarmos a pasta `web` da nova pasta a ser criada, a `backend`. Dentro de `backend` execute via terminal `npm init -y` ou `yarn init -y` para criar o arquivo `package.json`;
- instale o [express](https://expressjs.com/pt-br/) com `npm install express` ou `yarn add express`. Instale também a variação `@types` para que o TS;
- criar um arquivo em `src/server.ts` cujo será o arquivo inicial da aplicação, com a seguinte configuração:
```ts
import express from 'express';

const app = express();

app.listen(3333);
```
**observação**: para que o TS funcione ao executar o arquivo `server.ts` com o Node, instale `npm install typescript -D` ou `yarn add typescript -D`. Execute `npm tsc --init` ou `yarn tsc --init` para gerar o arquivo `tsconfig.json`. Caso os dois últimos comandos não funcionem, tente rodar ` npx tsc --init`.
- no arquivo `tsconfig.json` altere a parte que está como ` "target": "es5",` para ` "target": "es2017",`. Dessa forma a conversão no TS será menor;
- para finalizar, instale o pacote `npm install ts-node-dev -D` ou `yarn add is-node-dev -D` para que possamos executar TS no Node.js;
- em `package.json`, após a propriedade `license`, crie uma propriedade `scripts`, como mostrado abaixo:
```json
"scripts": {
  "dev": "ts-node-dev --transpile-only src/server.ts"
}
```
**observação**: se deixarmos sem o `--transpile-only` toda vez o `ts-node-dev` ficará ouvindo e procurando por erros, o que o próprio editor com o _Lint_ já faz, assim deixando o processo mais rápido.
- por fim, execute `npm run dev` ou `yarn dev` e acesse `http://localhost:3333` para testar a aplicação. Lembrando que como ainda não há uma rota criada, será retornado um `error`.