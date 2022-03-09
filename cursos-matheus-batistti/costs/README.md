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

## Roteamento
Criaremos as páginas `Home.js`, `Company.js`, `Contact.js` e `NewProject.js` dentro de `src/components/pages`

Dentro do arquivo `App.js` importaremos os componentes criados no passo anterior e criaremos o roteamento, da seguinte forma:

`App.js`
```js
import { 
  BrowserRouter as Router,
  Routes, 
  Route,
  Link
} from 'react-router-dom'

import Home from './components/pages/Home'
import Company from './components/pages/Company'
import Contact from './components/pages/Contact'
import NewProject from './components/pages/NewProject'

function App() {
  return (
    <Router>

      <ul>
        <li><Link to="/">Home</Link></li>
        <li><Link to="/contact">Contato</Link></li>
        <li><Link to="/company">Empresa</Link></li>
        <li><Link to="/newproject">Novo Projeto</Link></li>
      </ul>

      <Routes>

        {/* na v6 do roteamento muda um pouco a declaração */} 
        <Route path='/' element={<Home />} />
        <Route path='/company' element={<Company />} />
        <Route path='/contact' element={<Contact />} />
        <Route path='/newproject' element={<NewProject />} />

      </Routes>


    </Router>
  );
}

export default App;

```
>Observação
>
>O React teve uma atualização (v6) no router, veja [aqui](https://reactrouter.com/docs/en/v6/getting-started/overview) a documentação oficial (o curso está na versão anterior, v5)

### Envolvendo tudo dentro de um <Container>

Uma prática comum a componentes que terão um papel parecido e repetitivo, é criar um componente que empacote todo o conteúdo que seguirá um padrão. Assim criaremos um componente em `src/layout` (diretório `layout` pois será a respeito de visual mesmo), chamado `Container.js`, e será utilizado a `props` retornando `children`, da seguinte forma: 

`Container.js`
```js
import styles from './Container.modules.css'

export default function Container(props) {
  return (
    <div>
      {props.children}
    </div>
  )
}
```

Assim, lá em `App.js`, importaremos esse componente `Container.js` e o utilizaremos envolvendo o componente de `<Routes>`, da seguinte forma:

`App.js`
```js
import Container from './components/layout/Container'
...
<Container customClass="minHeight">
  <Routes>   
    <Route path='/' element={<Home />} />
    <Route path='/company' element={<Company />} />
    <Route path='/contact' element={<Contact />} />
    <Route path='/newproject' element={<NewProject />} />
  </Routes>
</Container>
```

Dessa forma, o que for filho de `<Container>`, será mostrado, no caso os componentes `<Routes>` e consequentemente `<Route>`. Em `App.js`, na abertura de `<Container>` aproveitamos para passar a *props* `customClass` lá para o componente `<Container>`. E em `<Container>`, utilizaremos o CSS dessa forma:
```js
import styles from './Container.module.css'
...
...
<div className={`${styles.container} ${styles[props.customClass]}`}>
  {props.children}
</div>
```
