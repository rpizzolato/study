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
``` 