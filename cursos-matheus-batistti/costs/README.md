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
Criaremos as páginas `Home.js`, `Company.js`, `Contact.js`, `Projects.js` e `NewProject.js` dentro de `src/components/pages`, com o conteúdo básico dentro de uma tag `<h1>`.

Dentro do arquivo `App.js` importaremos os componentes criados no passo anterior e criaremos o roteamento, da seguinte forma:

`App.js`
```js
import { 
  BrowserRouter as Router,
  Routes, 
  Route
} from 'react-router-dom'

import Home from './components/pages/Home'
import Company from './components/pages/Company'
import Contact from './components/pages/Contact'
import NewProject from './components/pages/NewProject'
import Projects from './components/pages/Projects'

import Container from './components/layout/Container'
import Navbar from './components/layout/Navbar'
import Footer from './components/layout/Footer'

function App() {
  return (
    <Router>
      <Navbar />
        <Container customClass="minHeight">
          <Routes>
            
            <Route path='/' element={<Home />} />
            <Route path='/company' element={<Company />} />
            <Route path='/projects' element={<Projects />} />
            <Route path='/contact' element={<Contact />} />
            <Route path='/newproject' element={<NewProject />} />

          </Routes>
        </Container>     
      <Footer />
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
    <Route path='/projects' element={<Projects />} />
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

### Navbar e Footer
Será necessário criar os arquivos `Navbar.js` e `Footer.js`. Como terão o uso do CSS para estilização, usaremos mais dois arquivos `Navbar.module.css` e `Footer.module.css`

`Navbar.js`
```js
import { Link } from "react-router-dom"

import Container from "./Container"

import styles from './Navbar.module.css'
import logo from '../../img/costs_logo.png'

export default function Navbar() {
  return (
    <nav className={styles.navbar}>
      <Container>

        <Link to="/">
          <img src={logo} alt="Costs" />
        </Link>

        <ul className={styles.list}>
          <li className={styles.item}><Link to="/">Home</Link></li>
          <li className={styles.item}><Link to="/projects">Projetos</Link></li>
          <li className={styles.item}><Link to="/company">Empresa</Link></li>
          <li className={styles.item}><Link to="/contact">Contato</Link></li>
        </ul>

      </Container>
    </nav>
  )
}
```

`Navbar.module.css`
```css
.navbar {
  display: flex;
  justify-content: space-between;
  background-color: #222;
  padding: 1em;
}

.list {
  display: flex;
  list-style: none;
  align-items: center;
}

.item {
  margin-right: 1em;
}

.item a {
  color: #fff;
  text-decoration: none;
}

.item a:hover {
  color: #FFBB33;
}
```

`Footer.js`
```js
import { FaFacebook, FaInstagram, FaLinkedin } from 'react-icons/fa'

import styles from './Footer.module.css'

export default function Footer() {
  return (
    <footer className={styles.footer}>
      <ul className={styles.social_list}>
        <li>
          <FaFacebook />
        </li>
        <li>
          <FaInstagram />
        </li>
        <li>
          <FaLinkedin />
        </li>
      </ul>
      <p className={styles.copy_right}>
        <span>Costs</span> &copy; 2022
      </p>
    </footer>
  )
}
```

`Footer.module.css`
```css
.footer {
  background-color: #222;
  color: #fff;
  padding: 3em;
  text-align: center;
}

.social_list {
  display: flex;
  justify-content: center;
  align-items: center;
  list-style: none;
}

.social_list li {
  margin: 0 1em;
}

.social_list li:hover {
  color: #ffbb22;
}

.social_list svg {
  font-size: 1.5em;
  cursor: pointer;
}

.copy_right {
  margin-top: 2em;
}

.copy_right span {
  font-weight: bold;
  color: #ffbb22
}
```

### Página NewProject.js

Para facilitar, é recomendado usar a característica de componentização do React, como teremos um formulário, iremos abstrair tudo para componentes.

Em `NewProject.js` criaremos um componente chamado <ProjectForm />, dentro de uma nova pasta chamada `project`, que ficará apenas para incluirmos itens relacionados a criação de um novo projeto.

`NewProject.js`
```js
import ProjectForm from '../project/ProjectForm'
...
return (
    <div className={styles.newproject_container}>
      <h1>Criar Projeto</h1>
      <p>Crie seu projeto para depois adicionar os serviços</p>
      <ProjectForm />
    </div>
  )
```

Dentro de `ProjectForm` iremos também abstrair em componentes, os `inputs` e demais itens relacionados ao formulário. 

>**Dica**
>
>Podemos primeiramente desenhar como será o formulário, usando diretamente HTML, e depois ir abstraindo item a item.

#### Abstraindo o input, Select e Botão Submit

Criaremos um arquivo chamado `Input.js`, dentro de uma nova pasta chamada `form`, que será utilizada para armazenarmos o que for referente aos componentes do formulário. Da mesma forma criaremos `Select.js` e `SubmitButton.js`. Podemos criar os respectivos arquivos CSS para eles, usando o sufixo `COMPONENTE.module.css`

Especificamente no `SubmitButton.js` voltaremos dois níves da abstração, lá no arquivo `NewProject.js` e informaremos, por meio de props, no componente `<ProjectForm />` qual o tipo de formulário que estamos utilizando, apenas para termos uma referência se é um formulário de cadastro, alteração, etc. Ficando da seguinte forma:
```js
<ProjectForm btnText="Criar Projeto" />
```

Subindo um nível, em `ProjectForm.js`, resgataremos essa props por meio da desestruturação, e usaremos no componente `<SubmitButton />`, dessa forma:
```js
function ProjectForm({ btnText }) {
  ...
  <SubmitButton text={btnText} />
}
```

>**Dica**
>
>Para acessarmos o `placeholder` do `input` via CSS, usamos a classe utilizada seguido de `::` como em `.btn::placeholder { color: #fff }`

### Criando um backend fake para testes (conectando com API pelo React)

Lá no começo do projeto instalamos o `json-server` para podermos criar uma API fake para trabalharmos com acesso aos dados. Para podermos usar esse recurso, criaremos um arquivo `db.json` em `src` do projeto.

Dentro de `db.json` por enquanto, colocaremos um array vazio chamado `projects`, dessa forma:
```json
{
  "projects": []
}
```

Lá no arquivo `package.json` teremos que criar uma nova entrada em scripts, habilitando um comando para chamarmos o json-server:
```json
"scripts": {
    "start": "react-scripts start",
    "backend": "json-server --watch db.json --port 5000", //nova linha
    "build": "react-scripts build",
    "test": "react-scripts test",
    "eject": "react-scripts eject"
  }
```

Podemos testar executando o comando `npm run backend` e acessando no navegador `http://localhost:5000/projects`

Podemos também criar as categorias do projeto, inserindo da seguinte forma no arquivo `db.json`:
```json
{
  "projects": [],
  "categories": [
    {
      "id": 1,
      "name": "Infra"
    },
    {
      "id": 2,
      "name": "Desenvolvimento"
    },
    {
      "id": 3,
      "name": "Design"
    },
    {
      "id": 4,
      "name": "Planejamento"
    }
  ]
}
```

Para testarmos, podemos executar o backend (`npm run backend`) e acessar `http://localhost:5000/categories` para listarmos as categorias.

#### fetch das categorias

No arquivo `ProjectForm.js`, faremos uso do `useState()` para guardar os estados das categorias. Normalmente criamos uma constante dessa forma: `const [categories, setCategories] = useState([])`. Dessa forma iniciamos a constante com um array vazio (`[]`). Posteriormente podemos fazer o `fetch` da API mostrada anteriormente, da seguinte forma:
```js
const [categories, setCategories] = useState([])

  fetch('http://localhost:5000/categories', {
    method: 'GET', //requisição do tipo GET
    headers: {
      'Content-Type': 'application/json', //força retornar um json
    },
  })
    .then((resp) => resp.json())
    .then((data) => {
      setCategories(data) //preenche a constante com o json
    })
    .catch((err) => console.log(err)) //caso retorne erro, mostra no console
    ...
    <Select 
          name="category_id" 
          text="Selecione a categoria"
          options={categories} //informamos ao componente Select via props as categorias que vieram da API no formato json
        />
```

No nosso componente `Select.js` iremos utilizar a função `map()` do Javascript para renderizar as opções de categorias:
```js
function Select({ text, name, options, handleOnChange, value }) { //props já desmembradas
  return (
    <div className={styles.form_control}>
      <label htmlFor={name}>{text}:</label>
      <select name={name} id={name}>
        <option>Selecione uma opção</option>
        {
          options.map((option) => (
            <option value={option.id} key={option.id}>{option.name}</option>
          ))
        }
      </select>
    </div>
  )
}
```

Irá funcionar dessa forma, no entanto o `useState()` fica constantemente atualizando o componente buscando por alterações (vide aba de network do DevTools do Navegador). O recomendável é utilizarmos o Hook `useEffect()`, que executa uma vez depois de tudo carregado (similar ao `ComponentDidMount` e `ComponentDidUpdate`). Para isso, declaramos o `useEffect()` dessa forma:
```js
  useEffect(()=>{

  }, [])
```

Agora resta apenas adicionar toda a lógica do `fetch` dentro do `useEffect()`, dessa forma:
```js
useEffect(() => {
    fetch('http://localhost:5000/categories', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    })
      .then((resp) => resp.json())
      .then((data) => {
        setCategories(data)
      })
      .catch((err) => console.log(err))
  }, [])
```

>Observação
>
>Lembre-se que `useState()` e `useEffect()` precisam ser importados

### Inserindo dados via API

Em `NewProject.js` iremos importar o `useHistory` do `react-router-dom` e criaremos uma constante chamada *history* da seguinte forma: `const history = useHistory()`. [Olhar o comentário pois o react dom em sua nova versão mudou]

Criaremos uma função para lidar com a criação de projetos, e que irá inicializar a propriedade custo e serviços zerados/vazios. Lembrando que a função `createPost` irá receber o projeto como argumento. Dentro da função também usaremos o fetch com os cabeçalhos e corpo (body) para enviar os dados.
```js
  const history = useHistory();
...
function createPost(project) {
    //initialize cost and services
    project.cost = 0
    project.services = []

    fetch('http://localhost:5000/projects', {
      method: 'POST',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify(project)
    })
    .then(((resp) => resp.json())
    .then((data) => {
      console.log(data);
      //redirect
    })
    ).catch(err => console.log(err))
  }
```

Dentro de `ProjectForm.js` criaremos mais um `useState()`, agora para armazenar o projeto, e será inicializado pelo objeto projeto ou por um objeto vazio (`projectData || {}`)



