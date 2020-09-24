## Acompanhado o CodeQuinta #1 do Canal Rockeseat
#### Estou criando esse repositório com o intuito de praticar e entender melhor o conceito de _Redux_, utilizando _React_, e ter uma fonte de consulta futuramente.

### Gerenciamento de Estado
- Todo componente possui seu próprio estado, sendo este, onde são armazenados as variáveis que vão modificar algo no processo de renderização do componente.

### Gerenciamento de Estado nos Componentes Vs Gerenciamento de Estado no _Redux_

- No Componente, o estado é acessível e modificável apenas dentro do próprio componente, já no Redux há apenas um estado para aplicação, sendo ele **Global**, onde é guardado em um único local.

### _view_
- _View_ seria basicamente o _HTML_, onde ficam os elementos visíveis aos usuários, como botões, _inputs_, etc. Onde ficam as ações (que são disparadas) também, como _click_, _hover_, etc.

### _actions_
- transmitem o que será enviado da _view_ para a _store_. As _actions_ não chamam algum evento, elas são ouvidas pelo _middleware_, que por sua vez são recebidas pelos _reducers_;
- no exemplo, é criado uma pasta dentro de `src` chamada _actions_, para armazená-las;
- por convenção _actions_ são funções, e que não é bom exportá-las como _default_, apenas usar o _export_;
- para que os _reduces_ possam ouvir as mudanças que são disparadas por meio das _actions_, utilizamos um `return` que vai ter que retornar um objeto que tenha a propriedade `type`;
- no exemplo de lista de _to do_, o segundo parâmetro do retorno do objeto, pode ser o texto efetivamente que adicionará uma tarefa na lista.

```js
export function addTodo(text) {
    return {
        type: 'ADD_TODO',
        text: text
    }
}
```

### observação: em ES6, podemos retornar o text direto:
```js
    return {
        type: 'ADD_TODO',
        text
    }
```

### _reducer_
- único local onde podemos alterar o estado da aplicação. Funciona como _event listener_. Ele ouve as _actions_. Quando há o caso de ter mais de um _reducer_, este por sua vez estará conectado com uma funcionalidade do projeto que esteja trabalhando;
- cada vez que um ou mais _reducers_ alterar o estado da aplicação, ele gera um novo estado da aplicação (um novo _state_);
- o _reducer_ é obrigatoriamente uma _**Pure Function**_, ou seja, ele não faz _request_ em _API_, não faz transação em Banco de Dados, etc (normalmente isso ocorre lá na _action_, já se for alguma requisição _async_ seria no _middleware_). Sua única tarefa seria apenas alterar o estado da aplicação;
- o estado da aplicação (objeto que armazena todas as informações da aplicação) é imutável, ou seja, toda alteração que é feita no estado da aplicação não altera, logo é criado um novo estado e retorna um _Redux_. Essa operação é criado um novo array e é retornado para o _Redux_. O array antigo acaba se perdendo;
- toda variável que for declarada dentro do _Redux_ é imutável;
- geralmente o nome do _reducer_ será o nome da variável que ficará guardado no estado;
- o primeiro parâmetro da função do _reducer_ precisa ser inicializado diretamente, no caso da lista de _to do_, será algo assim:
```js
export default function todos(state = [], action) {
    //nesse caso é inicializado um array vazio
}
```
- o segundo parâmetro é o _action_, que nada mais é que o objeto que retorna lá em `./actions/todos.js`, já visto acima:
```js
    return {
        type: 'ADD_TODO',
        text: text
    }
```
- a forma mais conveniente de fazer a verificação do _type_ da _action_ é por meio do `switch`:
```js
export default function todos(state = [], action) {
    switch (action.type) {
        case 'ADD_TODO':
            //adiciona novo todo
        case 'DELETE_TODO':
            //exclui um todo
        default:
            return state;
    }
}
```
- caso nenhuma das condições se satisfaça no `action.type`, é retornado o _state_ sem alteração alguma;
- quando for adicionar um novo _to do_, essa informação (texto) virá lá da _action_. Logo usamos a seguinte forma para adicionar um novo _to do_:
```js
    case 'ADD_TODO':
            return [ ...state, {
                id: Math.random(), //apenas gera um id para a lista
                text: action.text
            }]
        case 'DELETE_TODO':
            //exclui um todo
        default:
            return state;
```
- como o _state_ em _reducer_ é imutável, não devemos apenas adicionar uma nova posição no _array_ de lista de tarefas. Logo devemos criar um nova cópia, recebendo tudo que já havia no _state_, utilizando o [Spread Operator](https://developer.mozilla.org/pt-BR/docs/Web/JavaScript/Reference/Operators/Spread_operator);
- por convenção, como haverão muitos _reducers_ dentro de uma aplicação, utiliza-se um arquivo, `index.js` por exemplo, que englobará todos os _reducers_, para que futuramente o _store_ possa ouvi-los. Nesse caso, utilizá-se o `combineReducers`, que pode ser importado de `redux`:
```js
import { combineReducers } from 'redux';
import todos from './todos';

export default combineReducers({
    todos, //único reducer que temos, mas se tivéssemos outros, era só ir adicionando
});
```

### state
- é um grande objeto que contém as informações da aplicação;
- é repassado para as _views_ da aplicação, as quais ficam ouvindo e por fim é renderizado, adicionando ou excluindo alguma informação em tela.

## Na prática
### instalação dos pacotes necessários:
- instalação do _redux_:
```js
npm install -D redux react-redux
```

### criação do store
- dentro de `src`, crie um arquivo `store.js`. Dentro desse arquivo, criaremos nosso _store_, primeiramente importando `createStore` e declarando uma constante `store` que por enquanto vai receber uma função vazia:
```js
import { createStore } from 'redux';

const store = createStore(() => {});
```

### <Provider></Provider>
- vai ficar em volta dos componentes em `App.js`, pois ele vai ser responsável por ficar ouvindo as alterações e forçar a renderização dos componentes. É preciso importá-lo:
```js
import { Provider } from 'react-redux';
```
- No componente <Provider> há uma propriedade chamada `store`, que irá receber nosso _store_ criado anteriormente, não se esqueça de importá-lo:
```js
import store from './store';

<Provider store={store}>
    <div>App</div>
</Provider>
```

### passo-a-passo sequencial
- cria pasta `actions`, dentro cria um arquivo `.js` com a _action_ desejada, no caso, `todos.js`;
- dentro da _action_ cria a função que será ouvida pelo _reducer_. É preciso exportá-la, mas não como _default_ pois pode haver mais de uma _action_;
- cria o _reducer_ para ouvir a _action_, no caso o mesmo nome da _action_, ou seja, `todos.js`;
- dentro do _reducer_ haverá uma função com mesmo nome da _action_, com dois parâmetros, o `state` e a `action`. O `state` deve ser inicializado, no caso foi como vazio mesmo;
- dentro do _reducer_ deve decidir o `type` por meio do `switch`, e em cada case, deve ter um `return` que irá fazer uma cópia do conteúdo que estará vindo com o `state` (`...state`) e o objeto a ser modificado, coletando os dados que vêm com a _action_ (`action.text` por exemplo);
- depois do `reducer` pronto, criamos um novo arquivo, dentro de `./reducer` mesmo, chamado `index.js`, que englobará todos os _reducers_ criados, por meio do `import` e o `combineReducer`, que vêm do pacote `redux`;
- depois volta no _store_ e importa os _reducers_ e depois o passa para a função vazia;
- agora falta apenas disparar a ação por meio do botão de novo _todo_, que será feito pelo `bindActionsCreators` do pacote `redux`;
- importa as ações (que são funções) dentro de uma variável que será um grande objeto com todas as funções:
```js
import * as todoActions from './actions/todos; 
```
- para poder acessar as funções das _actions_ como propriedade dos componentes, no final do arquivo utilizamos um `dispatch`:
```js
const mapStateToProps = state => ({ todos: state.todos })
const mapDispatchToProps = dispatch => bindActionCreators(todoActions, dispatch);
```
- no entanto para que isso funcione é necessário importar o `connect` do pacote `react-redux` e exportá-la. Lembrando que `connect()` recebe dois parâmetros, sendo o primeiro o estado `mapStateToProps`, e o segundo o `mapDispatchToProps`:
```js
import { connect } from 'react-redux';
//lá no export default:
export default connect(mapStateToProps, mapDispatchToProps)(TodoList); //o componente TodoList é exportado como segundo parâmetro
```
- para conferir se outro componente consegue realmente acessar esse _state global_, criaremos um novo componente `<Counter />`, que retorna um `<h2>` dizendo a quantidade de tarefas temos no _to do list_. Utilizaremos da mesma forma do componente anterior:
```js
const mapStateToProps = state => ({
	todos: state.todos
});

export default connect(mapStateToProps)(Counter);
```
- para acessar o _state_, lá dentro do `<h2>`, utilizamos `{props.todos.length}`.

### Referências
- Principal referência é o vídeo em si, disponível no canal da [Rocketseat no Youtube](https://www.youtube.com/watch?v=69e1MoUWE1g&t=2280s);
- Outra fonte que auxilia no entendimento dos conceitos seria um artigo do site [Tableless](https://tableless.com.br/bem-vindo-ao-redux/).