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