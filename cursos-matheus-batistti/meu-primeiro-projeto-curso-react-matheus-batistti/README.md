# Série de vídeos do curso do Matheus Battisti sobre React
#### Sumário
1. [Props](#Props)
2. [Fragments](#Fragments)
3. [Validando props](#Validando-props)
4. [Renderização de listas](#Renderização-de-listas)
5. [State Lift](#State-Lift)
6. [React Router](#React-Router)
7. [React Icons](#React-Icons)


## Props
São valores passados para componentes, com intuito de deixar os componentes dinâmicos. O componente **filho** vai receber a propriedade do componente **pai**, ou seja, no componente filho, traremos a props via argumento da função, e usamos o ponto (.), como em:
```js
function SayMyName(props) {
  return(
    <div>
      <p>Meu nome é {props.nome}</p>
    </div>
  )
}
```
No componente pai, como por exemplo, no arquivo `App.js`, importaremos o componente filho, e o usamos com a propriedade definida lá no componente filho, como em:
```js
...
import SayMyName from './components/SayMyName'

function App() {
  const nome = 'Maria'
  return (
    <div>
      <SayMyName nome="Rodrigo">
      <SayMyName nome="Matheus">
      <SayMyName nome={nome}>

    </div>
  )
}
```
>**Dica**
>
>Quando temos uma **props** com diversos campos (**nome**, **idade**, e **telefone**, por **exemplo**), podemos desestruturá-la usando chaves (**{ nome, idade, telefone }**), em vez de ficar usando `props.nome`, `props.idade` e `props.telefone`  Vejamos o exemplo abaixo:
`Pessoa.js`
```js
function Pessoa({ nome, idade, telefone }) {
  return (
    <div>
      <h3>{nome}</h3>
      <p>{idade}</p>
      <p>{telefone}</p>
    </div>
  )
}
export Pessoa
```

## CSS
Podemos implementar ao nível global (`index.css`) ou componente (`Componente.module.css`).
No documento CSS, como em:
```css
/* Sempre use camelcase ou underline para nomear as propriedades*/
.formCabecalho {
  background-color: #333;
  color: #fff;
  border: 1px solid #111;
}
```
Depois importaremos o CSS dentro do componente correspondente e usaremos ele da seguinte forma:
```js
import styles from './Form.module.css'

return (
    <div>
      <h1 className={styles.formCabecalho}>Exemplo cabeçalho</h1>
    </div>
)
```

## Fragments
Muitas vezes quando é necessário inserir um componente dentro de outro (uma lista por exemplo), e evitar que seja inserido mais uma `<div>` (o que normalmente é usado para encapsular o conteúdo do componente) dentro do componente, usamos os fragmentos, que é representado pela sintaxe `<>conteúdo</>` (também tem o propósito de descomplicar os nós do DOM). Vejamos um exemplo clássico de lista:

`List.js`
```js
import Item from './Item'
...
...
...
return(
  <>
    <h1>Exemplo Lista</h1>
    <ul>
      <Item marca="Ferrari" />
      <Item marca="Fiat" />
      <Item marca="Chevrolet" />
    </ul>
  </>
)
```

`Item.js`
```js
function Item(props) {
  return (
    <>
      <li>{props.marca}</li>
    </>
  )
}
```

## Validando props
Usaremos o `PropTypes` do pacote `react`. Lembrando que na validação em si, usaremos `propTypes` com o `p` em caixa baixa (minúsculo). Vejamos o exemplo abaixo:

`Item.js`
```js
import PropTypes from 'prop-types'

export function Item({marca, ano_lancamento}) {
  return (
    <>
      <li>{marca} - {ano_lancamento}</li>
    </>
  )
}

// validação dos tipos
Item.propTypes = {
  marca: PropTypes.string.isRequired,
  ano_lancamento: PropTypes.number,
}

// colocando um valor padrão para as propriedades, caso não seja definido
Item.defaultProps = {
  marca: 'Faltou marca',
  ano_lancamento: 0,
}
```

`App.js`
```js
import { Item } from './components/Item'

function App() {
  return (
    <div className="App">
      <Item marca="Ferrari" ano_lancamento={1986} />

      {/* no console vai indicar que esperava number */}
      <Item marca="Ferrari" ano_lancamento="1986" />

      {/* usará os valores de defaultProps*/}
      <Item />
    </div>
  );
}

export default App;
```

## Renderização de listas
Primeiramente precisamos de um array (normalmente será um array de objetos). Para percorrer os itens, usaremos a função `map()` e imprimir algo na tela, sendo possível utilizar com operadores condicionais na renderização da lista (para quando, por exemplo, não tivermos nada na lista)

`Lista.js`
```js
export default function Lista({ itens }) {
  return (
    <>
      <h3>Lista de coisas boas:</h3>
      
      {itens.length > 0 ? (
        itens.map((item, index) => <p key={index}>{item}</p>)
      ) : (
        <p>Não há itens na lista!</p>
      )}
    </>
  )
}
```

`App.js`
```js
import Lista from './components/Lista';

function App() {
  
  const meusItens = ['React', 'Angular', 'Vue']

  return (
    <div className="App">
      <h1>Renderização de Listas</h1>

      <Lista itens={meusItens} />

      {/* lista vazia */}
      <Lista itens={[]}/>
    </div>
  );
}
export default App;
```

## State Lift
É uma técnica usada para compartilhar o **state**, sendo normal vários componentes **dependerem do mesmo estado**. Para isso é preciso elevar o nível (o **Lift** vem daí) do mesmo a um componente pai. Logo centralizamos o **state** no componente pai, e definimos quem usa e quem define (**setState**) 

`SeuNome.js` (componente que irá obter preencher o **setState**, usando a props como função, ou seja, a função `setNome`)
 ```js
 export default function SeuNome({ setNome }) {
  return (
    <div>
      <p>Digite o seu nome:</p>
      <input onChange={(e) => setNome(e.target.value)} type="text" placeholder="Qual é o seu nome?" />
    </div>
  )
}
```

`App.js` (componente pai que gera o setState para os filhos)
```js
import { useState } from "react";
import Saudacao from "./components/Saudacao";
import SeuNome from "./components/SeuNome";

function App() {
  
  const [nome, setNome] = useState()

  return (
    <div className="App">
      <h1>State Lift</h1>

      <SeuNome setNome={setNome} />

      <Saudacao nome={nome} />
    </div>
  );
}
export default App;
```

`Saudacao.js` (componente filho que faz uso do setState que foi compartilhado pelo componente pai `App.js`)
```js
export default function Saudacao({ nome }) {
  function gerarSaudacao(algumNome) {
    return `Olá, ${algumNome}, tudo bem?`
  }
  return <>{nome && <p>{gerarSaudacao(nome)}</p>}</>
}
```

## React Router
Usado para mudança de URLs da aplicação, sem o page reload. Pode-se mudar toda a view ou parte dela (do layout da aplicação). Para instalar o pacote use `npm i react-router-dom`
>Observação
>
>O React teve uma atualização (v6) no router, veja [aqui](https://reactrouter.com/docs/en/v6/getting-started/overview) a documentação oficial (o curso está na versão anterior, v5)

`NavBar.js`
```js
import { Link } from 'react-router-dom'

export default function NavBar() {
  return (
    <>
      <ul className={styles.list}>
        <li className={styles.item}><Link to="/">Home</Link></li>
        <li className={styles.item}><Link to="/empresa">Empresa</Link></li>
        <li className={styles.item}><Link to="/contato">Contato</Link></li>
      </ul>
    </>
  )
}
```
`App.js` (onde implementamos o Router)
```js
import { 
  BrowserRouter as Router,
  Routes, 
  Route,
} from 'react-router-dom'
import Footer from './components/layout/Footer';
import NavBar from './components/layout/NavBar';

import Contato from './pages/Contato';
import Empresa from './pages/Empresa';
import Home from './pages/Home';

function App() {

  return (
    <Router>

      <NavBar />

      <Routes>

        <Route path='/' element={<Home />} />
        <Route path='/empresa' element={<Empresa />} />
        <Route path='/contato' element={<Contato />} />

      </Routes>

      <Footer />

    </Router>
  );
}

export default App;
```

## React Icons
É um pacote de ícones externo, adicionado via npm. Permite adicionar ícones com uma sintaxe parecida com a de componentes. Há uma grande quantidade de ícones disponíveis. Instalado com o comando `npm i react-icons`

`Footer.js`
```js
import { FaFacebook, FaInstagram, FaLinkedin } from 'react-icons/fa'
import styles from './Footer.module.css'

export default function Footer() {
  return (
    <div>
      <ul className={styles.social_list}>
        <li><FaFacebook /></li>
        <li><FaInstagram /></li>
        <li><FaLinkedin /></li>
      </ul>
      <p>Nosso rodapé</p>
    </div>
  )
}
```

`Footer.module.css`
```css
.social_list {
  display: flex;
  justify-content: center;
  align-items: center;
  list-style: none;
}

.social_list li {
  margin: 0 1em;
}

.social_list svg {
  font-size: 1.5em;
  cursor: pointer;
}
```