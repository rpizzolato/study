## NLW#3

## Sumário
1. Trilha OmniStack: Workshop 01
  - [1º Dia: Conceitos e estrutura web](#1º-Dia:-Conceitos-e-estrutura-web)
  - [Tecnologias utilizadas](#Tecnologias-utilizadas)
  - [Criando a estrutura do projeto](#Criando-a-estrutura-do-projeto)
  - [Componentes](#Componentes)
  - [Propriedades](#Propriedades)
  - [Ícones](#Ícones)
  - [Roteamento](#Roteamento)
  - [Link](#Link)
  - [Mapas](#Mapas)
2. Trilha OmniStack: Workshop 02
  - [2º Dia: Back-end com Node.js](#2º-Dia:-Back-end-com-Node.js)
  - [request x response](#request-x-response)
  - [Params](#Params)
  - [Banco de dados](#Banco-de-dados)
  - [Criando as tabelas](#Criando-as-tabelas)
  - [Models](#Models)
  - [Enviando os dados via POST](#Enviando-os-dados-via-POST)
  - [Abstraindo o código](#Abstraindo-o-código)
  - [Listagem de orfanatos](#Listagem-de-orfanatos)
  - [Armazenamento de imagens para os orfanatos](#Armazenamento-de-imagens-para-os-orfanatos)
3. Trilha Oministack: Workshop 03
  - [3º Dia: Finalizando front-end](#3º-Dia:-Finalizando-front-end)
  - [Marcação no mapa](#Marcação-no-mapa)
  - [abstração com componentes](#abstração-com-componentes)
  - [Interligando o front com o back-end](#Interligando-o-front-com-o-back-end)
  - [Listando os orfanatos no mapa](#Listando-os-orfanatos-no-mapa)
  - [detalhes do orfanato](#detalhes-do-orfanato)
  
### Trilha OmniStack: Workshop 01
#### 1º Dia: Conceitos e estrutura web

### Tecnologias utilizadas
- NodeJS
- TypeScript
- ReactJS

### Criando a estrutura do projeto
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
- um componente é uma função que retorna um conteúdo HTML ou conteúdo JSX, um conteúdo que pode ser reaproveitado.

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
  "dev": "ts-node-dev --transpile-only --ignore-watch node_modules src/server.ts"
}
```
**observação**: se deixarmos sem o `--transpile-only` toda vez o `ts-node-dev` ficará ouvindo e procurando por erros, o que o próprio editor com o _Lint_ já faz, assim deixando o processo mais rápido. `--ignore-watch node_modules` faz com que a pasta `node_modules` não seja observada, fazendo o desempenho melhorar também.
- por fim, execute `npm run dev` ou `yarn dev` e acesse `http://localhost:3333` para testar a aplicação. Lembrando que como ainda não há uma rota criada, será retornado um `error`.

### request x response
- considerando a rota abaixo:
```ts
import express from 'express';

const app = express();

app.get('/test', (request, response) => {
  response.send('Hello World');
})

app.listen(3333);
```
Toda aplicação RESTFull possui `request` e `response`. `request` diz respeito a toda requisição vinda lá do front-end, vindo em forma de cabeçalho. É onde, por exemplo, ao cadastrar um usuário, os dados vêm por esse caminho. `response` é como o back-end vai devolver uma resposta para o front-end, qual o tipo de resposta, qual o status, se haverá erro ou não. Nas respostas o ideal seria devolver um objeto/array em vez de um texto, como acontece com o método `send()`, logo poderíamos devolver um JSON, como mostrado abaixo:
```ts
response.json({ message: "Hello World" })
```

### Params
- `query params` são os parâmetros que vão diretamente na url, utilizados para filtros, paginação, etc. Exemplo: `http://localhost:3333/users?search=rodrigo&page=2`. Para obter um `query param` use `request.query`;
- `route params` são parâmetros que identificam algum recurso. Veja o exemplo: `http://localhost:3333/users/1` essa rota identificaria o usuário com ID igual a 1. Para obter um `route param` use `request.params`, no entanto no recurso da rota, precisa haver o parâmetro, como em `/users/:id`;
- `body params` normalmente são parâmetros vindo de um formulário, mais complexas, com mais fluxo de dados. Para obter o `body param` use `request.body`. No entanto dessa forma o valor retornado será `undefined`. Isso ocorre devido ao retorno vir como JSON, mas o express por si só não compreende JSON por padrão, logo, incluia `app.use(express.json())` na aplicação.

### Banco de dados
- será utilizado `sqlite`, para instalar, use `npm install typeorm sqlite3` ou `yarn add typeorm sqlite3`. Há 3 formas de uso: __driver nativo__, __Query builder__ e __ORM (Object Relational Mapping)__:
  - __driver nativo__: é um modo direto, sem muita abstração, usado como seria usado diretamente lá no _SQL_, utilizando como por exemplo, um `SELECT * FROM users` diretamente.
  - __query builder__: exemplo, o [Knex.js](http://knexjs.org/), como em `knex('users').select('*').where('name', 'Rodrigo')` ou seja, conseguimos escrever as queries do BD com JS, no entanto no final das contas o _knex_ converte para formato SQL;
  - __ORM__: possui uma classe no JS que simboliza a tabela no BD, assim cada resultado que voltar do BD será uma instância da classe definida em JS.<br />
  **observação**: uma vantagem de utilizar _query builder_ e _ORM_ é que se necessário alterar o BD, como passar de MySQL para MSSQL, a codificação não muda, diferente de utilizar diretamente o _driver nativo_.
- criar uma pasta `src/databases` para armazenar tudo que seja relacionado à BD. Na raíz do projeto, crie um arquivo chamado `ormconfig.json` para tratarmos tudo que seja referente a conexão com o BD. Dentro de `database` crie um arquivo chamado `database.sqlite`;
- dentro de `ormconfig.json` configure conforme abaixo:
```json
{
  "type": "sqlite",
  "database": "./src/database/database.sqlite"
}
```
- crie um arquivo `database/connection.ts` e crie a conexão com o BD conforme abaixo:
```ts
import { createConnection } from 'typeorm';

createConnection();
```
- crie um import do arquivo `connection.js` dentro de `server.js`: `import './database/connection';` (não precisa utilizar a extensão `.ts`)
- rode a aplicação para ver se não algum erro: `npm run dev` ou `yarn dev`.

### Criando as tabelas
- pode-se criar diretamente ao se conectar na interface do BD, no entanto será utilizado o conceito de _migrations_, cujo é possível criar versões do BD, ótimo quando desenvolvido por uma equipe grande de programadores.
- as _migrations_ não precisam ser criadas manualmente, há um comando para isso, o `typeorm`. No entanto esse comando apenas executa arquivos JS, para que possamos executar TS, é preciso fazer uma inclusão de `scripts` dentro do arquivo `package.json`, conforme segue abaixo:
```json
"scripts:" {
  "dev": "ts-node-dev --transpile-only --ignore-watch node_modules src/server.ts",
  "typeorm": "ts-node-dev ./node_modules/typeorm/cli.js"
},
```
- crie um pasta para as _migrations_ em `src/database/migrations`;
- no arquivo `ormconfig.json`, inclua a propriedade `migration`, informando o caminho onde estarão as _migrations_, delimitando pela extensão `.ts`. Crie também uma propriedade chamada `cli` e dentro dela, `migrationsDir`, a qual indicará onde as novas _migrations_ serão criadas. Cri também uma propriedade `entities`, para informar onde ficam os _models_:
```json
{
  "type": "sqlite",
  "database": "./src/database/database.sqlite",
  "migrations": [
    "./src/database/migrations/*.ts"
  ],
  "entities": [
    "./src/models/*.ts"
  ],
  "cli": {
    "migrationsDir": "./src/database/migrations"
  }
}
```
- para criar a primeira _migration_, usamos: `npm run typeorm migration:create -n create_orphanages` ou `yarn typeorm migration:create -n create_orphanages`. A flag `-n` indica o nome da _migration_. **alerta**: comigo precisei instalar o [yarn](https://classic.yarnpkg.com/en/docs/install#alternatives-stable) via `npm install --global yarn` para conseguir funcionar;
- um arquivo é criado, usando uma _timestamp_ com dois métodos, `up()` e `down()`. O método `up()` geralmente é usado para realizar alterações, como criar tabela, criar um novo campo, excluir algum campo, etc. O método `down()` é possível desfazer o que foi feito no método `up()`. Resumidamente, o que `up()` faz, `down()` fará o contrário;
- de forma a criar a tabela `orphanages` criamos o seguinte conteúdo para o arquivo _migration_:
```ts
import {MigrationInterface, QueryRunner} from "typeorm";

export class createOrphanages1602805060658 implements MigrationInterface {

  public async up(queryRunner: QueryRunner, Table): Promise<void> {
    await queryRunner.createTable(new Table({
      name: 'orphanages',
      columns: [
        {
          name: 'id',
          type: 'integer',
          unsigned: true,
          isPrimary: true,
          isGenerated: true,
          generationStrategy: 'increment',
        },
        {
          name: 'name',
          type: 'varchar'
        },
        {
          name: 'latitude',
          type: 'decimal',
          scale: 10,
          precision: 2,
        },
        {
          name: 'longitude',
          type: 'decimal',
          scale: 10,
          precision: 2,
        },
        {
          name: 'about',
          type: 'text',
        },
        {
          name: 'instructions',
          type: 'text',
        },
        {
          name: 'open_on_weekends',
          type: 'boolean',
          default: false,
        }
      ]
    }))
  }
  public async down(queryRunner: QueryRunner): Promise<void> {
    await queryRunner.dropTable('orphanages');
  }
}
```
- para executar a _migration_, usamos `yarn typeorm migration:run`. Para **desfazer** o que a _migration_ fez, use `yarn typeorm migration:revert`
- para visualizar no arquivo `database.sqlite` se realmente foi criado a tabela, o uso do [Beekeeper Studio](https://www.beekeeperstudio.io/) é recomendado.

### Models
- como no modelo _ORM_ é representado por uma classe, vamos criar os _models_ dessas classes, começando a criar um pasta `src/models`. Dentro crie o primeiro _model_, chamado `Orphanage.ts`, com o seguinte conteúdo:
```ts
export default class Orphanage {
  id: number;
  name: string;
  latitude: number;
  longitude: number;
  about: string;
  instructions: string;
  opening_hours: string;
  open_on_weekends: boolean;
}
```
**observação**: os tipos acima listados são primitivos do TS. Para remover os alertas emitidos em cada tipo de dado, altere o arquivo `tsconfig.json`, removendo o comentário em `strictPropertyInicialization`, e alterando o valor para `false`, assim eliminando ter que inicializar algum valor na classe ou tendo que ter algum construtor nela. Ainda em `tsconfig.json`, remova os comentários de `Experimental Options`, deixando `experimentalDecorators: true` e `forceConsistentCasingInFileNames: true`, para habilitar a _API Decorator_;
- dentro da classe `Orphanage.ts`, importe por meio do `typeorm` o seguinte: `import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';`. Para ativarmos o _decorator_, usamos a anotação `@Entity()`, a qual irá associar nossa classe com a tabela no BD e `@Column()` indicando cada coluna no BD. `@PrimaryGeneratedColumn('increment')` representa como uma chave primária incremental, conforme abaixo:
```ts
import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity('orphanages')
export default class Orphanage {
  @PrimaryGeneratedColumn('increment')
  id: number;
  @Column()
  name: string;
  @Column()
  latitude: number;
  @Column()
  longitude: number;
  @Column()
  about: string;
  @Column()
  instructions: string;
  @Column()
  opening_hours: string;
  @Column()
  open_on_weekends: boolean;
}
```
### Enviando os dados via POST
- Primeiro passo é criar uma rota _POST_ para a aplicação lá em `server.ts` e desestruturar o que vêm de `request.body`, lembrando que o que vêm de `request.body` é alimentado por um JSON lá vindo da requisição _POST_, por meio do Postman ou Imsominia, por exemplo.
- para gravarmos no banco de dados, ainda no arquivo `server.ts`, importe o `getRepository` da seguinte forma `import { getRepository } from 'typeorm';`, assim toda mudança no BD irá passar por esse repositório. Importe também o _model_  `Orphanages.ts`. O `getRepository()` possui as funções de criar, atualizar, listar, contar, etc no BD. Lembre-se que para gravar algo no BD, pode ser uma tarefa demorada, logo é recomendado usar `async/await`. O arquivo `server.js` ficaria dessa forma:
```ts
import express from 'express';
import { getRepository } from 'typeorm';

import './database/connection';
import Orphanage from './models/Orphanage';

const app = express();

app.use(express.json());

app.post('/orphanages', async (request, response) => {
  const {
    name,
    latitude,
    longitude,
    about,
    instructions,
    opening_hours,
    open_on_weekends
  } = request.body;

  const orphanageRepository = getRepository(Orphanage);

  //cria o orfanato
  const orphanage = orphanageRepository.create({
    name,
    latitude,
    longitude,
    about,
    instructions,
    opening_hours,
    open_on_weekends
  })

  //grava no BD
  await orphanageRepository.save(orphanage);

  //retorna o obj orphanage e retorna o status code 201 created
  return response.status(201).json(orphanage);
})

app.listen(3333);
```
- execute o projeto `yarn dev` e verifique no BD se realmente inseriu o registro.

### Abstraindo o código
- primeiro passo, é criar um arquivo chamado `src/routes.ts` para lidar somente com as rodas. Migre a rota criada para cadastro para dentro desse arquivo. Importe `getRepository` e `Orphanage` para esse arquivo, importe também o `Router` do pacote do `express`. Crie uma nova variável `routes` recebendo `Router()` para trocar a variável `app`. Exporte as rotas no final do arquivo (`export default`) e importe no arquivo `server.js`. Por fim, coloque `app.use(routes);` logo após `app.use(express.json());`. **Importante que seja logo após `app.use(express.json());`**;
- a fim de adequar o projeto ao modelo MVC (Model View Controller), criaremos uma arquivo `controllers/OrphanagesController.ts` para organizar. Dentro do arquivo `OrphanagesController.ts` irá retornar um objeto com toda a lógica de criação de um registro de orfanato, logo, ficará da seguinte forma (lembrando que iremos pegar a lógica presenta lá no arquivo `routes.ts`, inclusive fazer os imports `import { getRepository } from 'typeorm';` e `import Orphanage from './models/Orphanage';`):
```ts
import { Request, Response } from 'express';
import { getRepository } from 'typeorm';
import Orphanage from '../models/Orphanage';

export default {
  async create(request: Request, response: Response) {
    const {
      name,
      latitude,
      longitude,
      about,
      instructions,
      opening_hours,
      open_on_weekends
    } = request.body;
  
    const orphanageRepository = getRepository(Orphanage);
  
    //cria o orfanato
    const orphanage = orphanageRepository.create({
      name,
      latitude,
      longitude,
      about,
      instructions,
      opening_hours,
      open_on_weekends
    })
  
    //grava no BD
    await orphanageRepository.save(orphanage);
  
    //retorna o obj orphanage e retorna o status code 201 created
    return response.status(201).json(orphanage);
  }
}
```
- em `routes.ts` importe o controller acima, ficando assim `import OrphanagesController from './controllers/OrphanagesController';`;
- a rota de cadastro de orfanatos ficará dessa forma `routes.post('/orphanages', OrphanagesController.create)`, fazendo referencia ao método `create()` do controller `OrphanagesController.ts`;

### Listagem de orfanatos
- No arquivo `OrphanagesController.js` crie um novo método, com o nome de `index()`, conforme abaixo:
```ts
async index(request: Request, response: Response) {
  const orphanagesRepository = getRepository(Orphanage);
  const orphanages = await orphanagesRepository.find();
}
```
**observação**: caso quiséssemos [colocar mais algumas condições](https://github.com/typeorm/typeorm/blob/master/docs/find-options.md), basta usar o `find()` e adicionar um objeto no argumento, como por exemplo:
```js
find({ 
  order: {
    name: "ASC",
    id: "DESC"
  } 
});
```
- para listar somente um orfanato, criaremos uma função nos mesmos moldes de `index()`, mas essa será chamada de `show()`:
```ts
async show(request: Request, response: Response) {
    //pega o id que vem lá do Root Params
    const { id } = request.params;

    const orphanagesRepository = getRepository(Orphanage);

    const orphanage = await orphanagesRepository.findOneOrFail(id);

    return response.status(200).json(orphanage);  
  }
```

### Armazenamento/Upload de imagens para os orfanatos
- é uma boa prática armazenar apenas o nome da imagem no BD, e não a imagem em si, cuja será armazenada em uma pasta local;
- primeiro passo é criar a _migration_ `yarn typeorm migration:create -n create_images`. Na função `up()` usaremos o seguinte código:
```ts
public async up(queryRunner: QueryRunner): Promise<void> {
		await queryRunner.createTable(new Table({
			name: 'images',
			columns: [
				{
					name: 'id',
          type: 'integer',
          unsigned: true,
          isPrimary: true,
          isGenerated: true,
          generationStrategy: 'increment',
				},
				{
					name: 'path',
					type: 'varchar',
				},
				{
					name: 'orphanage_id',
					type: 'integer',
				}
			],
			foreignKeys: [
				{
					name: 'ImageOrphanage',
					columnNames: ['orphanage_id'],
					referencedTableName: 'orphanages',
					referencedColumnNames: ['id'],
					onUpdate: 'CASCADE',
					onDelete: 'CASCADE',
				}
			]
		}))
	}
```
**observação**: em `foreignKeys` faremos referência a tabela `Orphanages` e em `onUpdate` e `onDelete`, usaremos como `CASCADE`, assim, caso o orfanato mude o `id`, o mesmo será atualizado, e caso seja excluído, as fotos relacionadas a ele, serão excluídas também. No método `down()` resta somente excluir a tabela (`await queryRunner.dropTable('images')`). Execute a _migration_  `yarn typeorm migration:run`;
- para ligar com o upload de imagens (ou arquivos em geral), será utilizado a biblioteca [Multer](https://www.npmjs.com/package/multer), instalá-la no seguinte comando: `yarn add multer`;
- crie um arquivo de configuração em `src/config/upload.ts` e importe o _multer_ `import multer from 'multer';`. Considere instalar também `@types/multer` como dependência de desenvolvimento, devido ao uso do TS;
- importe o `path` do próprio Node.js `import path from 'path';` para lidarmos com os caminhos relativos na aplicação (como por exemplo a barra que é alterada em diferentes Sistemas Operacionais). A configuração de `upload.ts` ficará desta forma:
```ts
import multer from 'multer';
import path from 'path';

export default {
  storage: multer.diskStorage({
    //path relativo está separado devido SOs diferentes uso da barra
    destination: path.join(__dirname, '..', '..', 'uploads'),
    filename: (request, file, cb) => {
      //cria um timestamp da data atual + o nome original do arquivo
      const fileName = `${Date.now()}-${file.originalname}`;
      //callback caso haja erro
      cb(null, fileName);
    }
  })
};
```
**observação**: `path.join(__dirname, '')`: `__dirname` retorna o caminho de onde se encontra o arquivo que fez tal chamada.
- crie na raíz do projeto uma pasta com nome `uploads`;
- no arquivo de rotas, `routes.ts` importe o _multer_ e o arquivo `upload.ts` como `uploadConfig`. Crie uma variável `upload` que irá receber `multer(uploadconfig)`. Na rota de cadastro `/orphanages`, criaremos outra configuração antes do cadastro:
```ts
routes.post('/orphanages', upload.array('images'), OrphanagesController.create);
```
**observação**: utilizamos o método `array()` pois será mais de uma imagem, se fosse apenas uma, poderia usar o método `single()`. `images` é apenas o nome do campo.
- o envio de imagens não pode ser feito em _JSON_, tem que ser feito em _Multipart Form_ e vá preenchendo campo a campo. No campo _images_ troque para tipo _File_, podendo ser mais de um campo _images_;
- agora vamos criar o _model_ para _images_, criando um arquivo `Images.ts` dentro de `models`. No entanto como orfanatos pode ter diversas imagens, voltando em `Orphanages.ts`, vamos incluir na importação `OneToMany` e `JoinColumn` para referenciar nossa chave estrangeira em `images`, importe também `Images.ts`, e no final do arquivo `Orphanages.ts` inclua um campo _images_, mas sem o `@Column`, que irá receber um array de Images, faça o mesmo em `Images.ts`, no entanto usando `ManytoOne`, ficando os _models_ da seguinte forma:

`Orphanages.ts`
```ts
import { Entity, Column, PrimaryGeneratedColumn, OneToMany, JoinColumn } from 'typeorm';

import Image from './Images';

@Entity('orphanages')
export default class Orphanage {
  @PrimaryGeneratedColumn('increment')
  id: number;

  @Column()
  name: string;

  @Column()
  latitude: number;

  @Column()
  longitude: number;

  @Column()
  about: string;

  @Column()
  instructions: string;

  @Column()
  opening_hours: string;

  @Column()
  open_on_weekends: boolean;
  
  @OneToMany(() => Image, image => image.orphanage, {
    cascade: ['insert', 'update']
  })
  @JoinColumn({ name: 'orphanage_id' })
  images: Image[];
}
```
`Images.ts`
```ts
import { Entity, Column, PrimaryGeneratedColumn, ManyToOne, JoinColumn } from 'typeorm';

import Orphanage from './Orphanage';

@Entity('images')
export default class Image {
  @PrimaryGeneratedColumn('increment')
  id: number;

  @Column()
  path: string;

  @ManyToOne(() => Orphanage, orphanage => orphanage.images)
  @JoinColumn({ name: 'orphanage_id' })
  orphanage: Orphanage;
}
```
- as imagens, por padrão, vêm por meio do `request.files`, e para armazenarmos as imagens, precisamos usar o _hack_ `as` para forçar a tipagem como array, ficando assim: `const requestImages = request.files as Express.Multer.File[];`;
- por fim mapeie esse array e insira a variável `images` no método `create()`:
```ts
const images = requestImages.map(image => {
      return { path: image.filename }
    })
```
- para listar as imagens, altere o método `find()` e `findOneOrFail()` colocando um _relations_ para as imagens:
```ts
const orphanages = await orphanagesRepository.find({
      relations: ['images']
    });
    //...
const orphanage = await orphanagesRepository.findOneOrFail(id, {
      relations: ['images']
    });
```

### trabalhando com views
- crie um pasta `src/views` e dentro crie dois arquivos:<br />

`orphanages_views.ts`
```ts
import Orphanage from '../models/Orphanage';
import ImagesView from './images_view';

export default {
  render(orphanage: Orphanage) {
    return {
      id: orphanage.id,
      name: orphanage.name,
      latitude: orphanage.latitude,
      longitude: orphanage.longitude,
      about: orphanage.about,
      instructions: orphanage.instructions,
      opening_hours: orphanage.opening_hours,
      open_on_weekends: orphanage.open_on_weekends,
      images: ImagesView.renderMany(orphanage.images)
    };
  },

  renderMany(orphanages: Orphanage[]) {
    return orphanages.map(orphanage => this.render(orphanage));
  }
};
```
`images_view.ts`
```ts
import Image from '../models/Image';

export default {
  render(image: Image) {
    return {
      id: image.id,
      url: `http://localhost:3333/uploads/${image.path}`
      
    };
  },

  renderMany(images: Image[]) {
    return images.map(image => this.render(image));
  }
};
```
em `OrphanagesController.ts` troque nos métodos `index()`:
```ts
return response.status(200).json(orphanageView.renderMany(orphanages));
```
 e `show()`:
 ```ts
 return response.status(200).json(orphanageView.render(orphanage));
 ```
 - para a listagem de imagens funcionar, conforme configurado na propriedade `url` no arquivo `image_view.ts`, via `http://localhost:3333/uploads`, vá até o arquivo `server.ts` e adicione o seguinte trecho de código abaixo, logo após `app.use(routes)`:
 ```ts
 import path from 'path';
 //...
 app.use('/uploads', express.static(path.join(__dirname, '..', 'uploads')));
 ```
 **observação**: o ideal para urls, em vez de ser exibido explicitamente lá a propriedade, seria mostrar via variáveis de ambiente, para mais info, consultar no [blog](https://blog.rocketseat.com.br/variaveis-ambiente-nodejs/).

 ### Tratamento de erros
 - por padrão, o Express não consegue tratar os erros nos métodos `async/await`. Para tratar isso, instale `yarn add express-async-errors`. No arquivo `server.ts`, importe `import 'express-async-errors'` logo depois da importação do express. Dessa forma será retornado os possíveis erros da aplicação.
 - esses erros são muito complexos de ficar mostrando na API, o correto seria tratá-los e exibir apenas para gente, logo, crie um pasta em `src/errors`, dentro crie um arquivo chamado `handler.ts`:
 ```ts
import { ErrorRequestHandler } from 'express';

const errorHandler: ErrorRequestHandler = (error, request, response, next) => {
  //nos mostra o erro real
  console.error(error);
  
  //retorna ao user uma msg de error genérica
  return response.status(500).json({ message: 'Internal Server Error' });
}

export default errorHandler;
 ```
- dentro de `server.js` importe o `errorHandler` e use o `app.use(errorHandler);`

### Validação de dados
- para validar, instalar o módulo `yarn add yup`;
- no `OrphanagesController.ts`, antes da criação do orfanato, abstraia os dados do orfanato para uma variável `data`:
```ts
const data = {
      name,
      latitude,
      longitude,
      about,
      instructions,
      opening_hours,
      open_on_weekends,
      images
    };

    //cria o orfanato
    const orphanage = orphanageRepository.create(data);
```
- importe o `yup`, no entanto o `yup` não tem um `export default`, logo use: `import * as Yup from 'yup';`. Provavelmente será preciso instalar o `@types/yup -D` também. Após o `data`, insira:
```ts
//esquema para validar os campos
    const schema = Yup.object().shape({
      name: Yup.string().required(),
      latitude: Yup.number().required(),
      longitude: Yup.number().required(),
      about: Yup.string().required().max(300),
      instructions: Yup.string().required(),
      opening_hours: Yup.string().required(),
      open_on_weekends: Yup.boolean().required(),
      images: Yup.array(
        Yup.object().shape({
          path: Yup.string().required()
        })
      )
    });

    //valida os campos
    await schema.validate(data, {
      //retorna todos os erros de validação que encontrar
      abortEarly: false
    })
```
**observação**: desta forma, todos os erros ainda são retornados o _status_ 500, configurado lá em `handler.ts`. Ainda em `handler.ts`, deixe dessa forma:
```ts
import { ErrorRequestHandler } from 'express';
import { ValidationError } from 'yup';

interface ValidationErrors {
  [key: string]: string[];
}

const errorHandler: ErrorRequestHandler = (error, request, response, next) => {
  if (error instanceof ValidationError) {
    let errors: ValidationErrors = {};

    error.inner.forEach(err => {
      errors[err.path] = err.errors;
    });

    return response.status(400).json({ message: 'Validation fails', errors })
  }
  //nos mostra o erro real
  console.error(error);
  
  //retorna ao user uma msg de error genérica
  return response.status(500).json({ message: 'Internal Server Error' });
}

export default errorHandler;
```

### CORS
- usado para permitir acesso a todos os front-ends que irão consumir o back-end. Instale com `yarn add cors` e `yarn add @types/cors`. No arquivo `server.js`, importe o cors `import cors from 'cors';` e use-o como `app.use(cors());`

#### 3º Dia: Finalizando front-end
### Marcação no mapa
- cada marcação no mapa, usaremos a biblioteca do `leaflet`, adicionando o módulo `Marker` na importação. Coloque `<Marker />` logo após `<TileLayer>`. Importe o `leaflet` (`import Leaflet from 'leaflet';`). Crie uma variável para armazenar o ícone, se baseando no `leaflet`:
```ts
import '../styles/pages/orphanages-map.css';
//...
const mapIcon = Leaflet.icon({
  iconUrl: mapMarkerImg,
})
//...
<Map
  center={[-22.7244976,-47.6352641]}
  zoom={15}
  style={{ width: '100%', height: '100%' }}
>
  <TileLayer url="https://a.tile.openstreetmap.org/{z}/{x}/{y}.png" />

  <Marker 
    icons={mapIcon}
    position={[-22.7244976,-47.6381184]}
  />

  {/* <TileLayer
    url={`https://api.mapbox.com/styles/v1/mapbox/light-v10/tiles/256/{z}/{x}/{y}@2x?access_token=${process.env.REACT_APP_MAPBOX_TOKEN}`} /> */}
</Map>
```
**observação**: no entanto há um problema, a imagem deveria apontar as coordenadas no mapa na sua parte inferior, e no momento o ponto central é no meio da imagem (proximo ao sorriso). Para corrigir isso usamos a propriedade `iconAnchor`. Usando a propriedade `iconSize: [width, height]` em conjunto com `iconAnchor` definimos uma altura de 58 e largura de 68 para o `iconSize`, e no `iconAnchor`, colocamos o valor `[29, 68]`, ou seja, no eixo x, será metade do `width`, corrigindo o ponto central no desenho:
```ts
const mapIcon = Leaflet.icon({
  iconUrl: mapMarkerImg,
  iconSize: [58, 68],
  iconAnchor: [29, 68]
})
```
- para abrir um _popup_ ao clicar no desenho, importaremos `Popup` do pacote _leaflet_. Dentro do `<Marker></Marker>` colocaremos `<Popup></Popup>`. Adicione também a propriedade `popupAnchor: [170, 2]`:
```ts
const mapIcon = Leaflet.icon({
  iconUrl: mapMarkerImg,
  iconSize: [58, 68],
  iconAnchor: [29, 68],
  popupAnchor: [170, 2]
})
//...
<Popup closeButton={false} minWidth={240} maxHeight={240}>
  Lar das meninas
</Popup>
```
- para melhorar o CSS do popup, vamos adicionar a classe `className="map-popup"` em `<Popup>` e alterar o CSS em `orphanagesMap.css`:
```css

```
**observação**: a classe `.leaflet-popup-content-wrapper` é a padrão do _leaflet_ para alterarmos o _popup_. `.leaflet-popup-tip-container`é para formatar aquela flecha pequena na parte inferior, colocando `display=none`

### abstração com componentes
- lembrando que componentes são códigos que podem ser reaproveitados, logo, no cadastro de orfanatos e na listagem dos detalhes do orfanatos, há uma _sidebar_ que pode ser reaproveitada, criando um componente que a represente. Crie um pasta `src/components` e dentro crie um arquivo chamado `Sidebar.tsx` que irá representar o componente _sidebar_:
```ts
import React from 'react';
import { FiArrowLeft } from 'react-icons/fi';
import { useHistory } from 'react-router-dom';

import mapMarkerImg from '../images/map-marker.svg';
import '../styles/components/Sidebar.css';

export default function Sidebar() {
  const { goBack } = useHistory();
  
  return (
    <aside className="app-sidebar">
      <img src={mapMarkerImg} alt="Happy" />

      <footer>
        <button type="button" onClick={goBack}>
          <FiArrowLeft size={24} color="#FFF" />
        </button>
      </footer>
    </aside>
  );
}
```
crie também um arquivo `Sidebar.css` em `src/styles/components`:
```css
#page-create-orphanage aside {
  position: fixed;
  height: 100%;
  padding: 32px 24px;
  background: linear-gradient(329.54deg, #15B6D6 0%, #15D6D6 100%);

  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
}

aside.app-sidebar img {
  width: 48px;
}

aside.app-sidebar footer a,
aside.app-sidebar footer button {
  width: 48px;
  height: 48px;

  border: 0;

  background: #12AFCB;
  border-radius: 16px;

  cursor: pointer;

  transition: background-color 0.2s;

  display: flex;
  justify-content: center;
  align-items: center;
}

aside.app-sidebar footer a:hover,
aside.app-sidebar footer button:hover {
  background: #17D6EB;
}
```
**observação:** use `useHistory()`, importando `import { useHistory } from 'react-router-dom';` para criar o botão voltar.
Por fim, importe o `<Sidebar />` em `createOrphanage.tsx` e `Orphanage.tsx` no lugar da tag `<aside>`.
- Nos arquivos dentro de `src/pages` há a utilização do mesmo ícone entre os arquivos, outro ponto que poderia ser abstraído. Para isso, criaremos umas pasta `src/utils` que armazenará o arquivo `mapIcon.ts` (pode ser `.ts` em vez de `.tsx`, pois não será um componente realmente):
```ts
import Leaflet from 'leaflet';

import mapMarkerImg from '../images/map-marker.svg';

const mapIcon = Leaflet.icon({
  iconUrl: mapMarkerImg,
  iconSize: [58, 68],
  iconAnchor: [29, 68],
  popupAnchor: [170, 2]
})

export default mapIcon;
```

### Interligando o front com o back-end
- instalação do axios: `yarn add axios`. Crie um pasta `src/services`, a qual será utilizada para toda comunicação do front com o mundo externo, seja ele banco de dados, para uma API, para envio de algo, para _localStorage_, etc. Crie um arquivo chamado `api.ts`, que servirá para conectar o front com o back-end. Uma das vantagens de utilizar o axios, é poder criar uma baseURL, que no caso, será `http://localhost:3333`, conforme configuração abaixo:
```ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:3333',

})

export default api;
```

### Listando os orfanatos no mapa
- será utilizado o _Hooks_, mais especificamente o `useEffect(()=> {}, [])`, o qual recebe 2 parâmetros, sendo o primeiro **o que** será executado, e o segundo é **quando** o primeiro será executado. Normalmente **quando** é colocado em um vetor, e deixando-o vazio, significa que será executado somente uma vez. Começe importando o `{ useEffect } from 'react';`:
```ts
//...
function OrphanagesMap() {
  //inicia o state orphanages com uma lista vazia
  const [orphanages, setOrphanages] = useState([]);

  console.log(orphanages);

  useEffect(() => {
    api.get('orphanages').then(response => {
      //setOrphanages atualiza a variável orphanages (que está desestruturado)
      setOrphanages(response.data);
    });
  }, []);
  
  return (
    //...
```
- Toda manipulação de informação entre os componentes é feito por meio do estado (_state_), pois se armazenássemos da maneira convencional, por meio de uma variável por exemplo, toda vez que essa variável mudasse, o componente não seria atualizado, diferentemente quando armazenamos no _state_, qualquer mudança no mesmo, haverá uma nova renderização do componente. Assim usaremos o `useState`, do próprio _React_.
- o uso do `useState` é feito por meio de uma desestruturação, deixando a primeira o array desejado, e a segunda, uma função que atualiza o array (normalmente com o prefixo `set`). Tomando o exemplo de orfanatos, ficaria da seguinte forma:
```ts
const [orphanages, setOrphanages] = useState([]);
```
**observação**: se colocarmos um `console.log()` entre o momento que `useState` é declarado e alterado de valor(`setOrphanages()`), podemos notar no console do navegador que `console.log()` é chamado duas vezes, consequentemente são duas vezes que a tela é renderizada. Há casos que pode ocorrer de duplicidade, sendo essa mesma chamada, executada 4 vezes. Para corrigir isso, remova o `<React.StrictMode>` do arquivo `index.tsx`, deixe somente com `<>`;
- agora será necessário replicar o `<Map />` usando a função `map()`. Será utilizado o `map()` pois ele percorre uma lista (_array_) e retorna algo, já utilizando o `forEach`, apenas a lista é percorrida. Logo faça da seguinte forma:
```ts
{orphanages.map(orphanage => {
        return(
          <Map
            center={[-22.7244976,-47.6352641]}
            zoom={15}
            style={{ width: '100%', height: '100%' }}
          >
            <TileLayer url="https://a.tile.openstreetmap.org/{z}/{x}/{y}.png" />

            <Marker
              icon={mapIcon}
              position={[-22.7244976,-47.6381184]}
            >
              <Popup closeButton={false} minWidth={240} maxHeight={240} className="map-popup">
                Lar das meninas
                <Link to="/orphanages/1">
                  <FiArrowRight size={20} color="#fff" />
                </Link>
              </Popup>
            </Marker>

            {/* <TileLayer
              url={`https://api.mapbox.com/styles/v1/mapbox/light-v10/tiles/256/{z}/{x}/{y}@2x?access_token=${process.env.REACT_APP_MAPBOX_TOKEN}`} /> */}
          </Map>
        );
      })}
```
- agora resta ir substituindo os dados que irão renderizar na tela, como por exemplo, trocar os valores de latitude e longitude por `orphanage.latitude` e `orphanage.longitude`. É necessário destacar que desta forma irá ser retornado um alerta (`const orphanage: never[];`), devido o _front_ não saber especificamente o que são os valores `latitude` e `longitude`, e para contornar isso, crie uma _interface_ especificando:
```ts
interface Orphanage {
  id: number;
  latitude: number;
  longitude: number;
  name: string;
}
```
**observação**: é preciso lá em `useState` especificar o tipo da lista que será recebido, no caso, `Orphanages`, e atentar a colocar que é um vetor `[]`, ficando assim:
```ts
const [orphanages, setOrphanages] = useState<Orphanage[]>([]);
```
- ao fazer um `map()` para fazer um _looping_ dos orfanatos, precisamos adicionar uma propriedade chamada `key={orphanage.id}` no primeiro elemento HTML (no caso seria um `<Marker />`) que fica repetindo.

### detalhes do orfanato
- agora na página `Orphanage.tsx`, vamos copiar o `useState` e `useEffect` igual a página `OrphanageMap.tsx`, e acrescentar os campos a mais que teremos dentro da _interface_. Algumas mudanças no _Hooks_ deverão ser feitas, conforme código:
```ts
//...
import { useParams } from "react-router-dom";

interface Orphanage {
  latitude: number;
  longitude: number;
  name: string;
  about: string;
  instructions: string;
  opening_hours: string;
  open_on_weekends: string;
  images: Array<{
    id: number,
    url: string
  }>;
}

interface OrphanageParams {
  id: string;
}

export default function Orphanage() {
  const params = useParams<OrphanageParams>();

  const [orphanage, setOrphanage] = useState<Orphanage[]>();

  useEffect(() => {
    api.get(`orphanages/${params.id}`).then(response => {
      setOrphanage(response.data);
    });
  }, [params.id]);

  if (!orphanage) {
    return <p>Carregando...</p>
  }
```
**observação**: um `if` deverá ser feito para testar se há um orfanato adicionado, senão podemos colocar algo avisando que está carregando ainda. Recomendado utilizar [shimmer effect](https://blog.rocketseat.com.br/react-native-shimmer/). Os orfanatos estão vindo do backend no formato `http://localhost:3333/orphanages/1`, logo precisamos pegar esse numeral `1` via parâmetro. Para isso importe o `useParams` do pacote `react-router-dom`.
- para listar as imagens, crie um map para retornar imagem por imagem:
```ts
{orphanage.images.map(image => {
  return(
    <button key={image.id} className="active" type="button">
      <img src={image.url} alt={orphanage.name} />
    </button>
  );
})}
```
- para listar se o orfanato abre aos finais de semana, criaremos um `if` ternário (`?`,`:`):
```ts
<div className="open-details">
  <div className="hour">
    <FiClock size={32} color="#15B6D6" />
    Segunda à Sexta <br />
    {orphanage.opening_hours}
  </div>
  {orphanage.open_on_weekends ? (
    <div className="open-on-weekends">
      <FiInfo size={32} color="#39CC83" />
      Atendemos <br />
      fim de semana
    </div>
  ) : (
    <div className="open-on-weekends dont-open">
      <FiInfo size={32} color="#FF669D" />
      Não atendemos <br />
      fim de semana
    </div>
  )}
</div>
```
**observação**: é criado um novo estilo chamado `dont-open` para tratar o CSS caso não o orfanato não atenda aos finais de semana:
```css
.orphanage-details .orphanage-details-content .open-details div.open-on-weekends.dont-open {
  background: linear-gradient(154.16deg, #FDF8F5 7.85%, #FFFFFF 91.03%);
  border: 1px solid #FFBCD4;
  color: #FF669D;
}
```
- para definir uma rota no Google Maps, utilizamos:
```ts
<footer>
  <a target="_blank" rel="noopener noreferrer" href={`https://www.google.com/maps/dir/?api=1&destination=${orphanage.latitude},${orphanage.longitude}`}>Ver rotas no Google Maps</a>
</footer>
```
**observação**: lembre-se, quando utilizar a propriedade `_blank`, use também `rel="noopener noreferrer"` para evitar [essa](https://blog.saninternet.com/vulnerabilidade-tag-target_blank) vulnerabilidade.

### Imagem ativa do orfanato
- para definirmos isso, será necessário criar outro estado no código, agora para controlar a imagem ativa, logo crie:
```ts
//inicia activeImageIndex = 0
const [activeImageIndex, setActiveImageIndex] = useState(0);
```
- agora lá na renderização da imagem, troque o valor `0` do array `images` para `activeImageIndex`, assim na mudança via `button`, ao trocar o valor da array `images`, será renderizado uma nova imagem. No `button` adicionamos o evento `onClick` e dentro uma _arrow function_ recebendo `setActiveImageIndex`. Vale lembrar que na função `map()`, o primeiro parâmetro em o _array_ em si, e o segundo é seu _index_.
```ts
<img src={orphanage.images[activeImageIndex].url} alt={orphanage.name} />
//...
<div className="images">
  {orphanage.images.map((image, index) => {
    return(
      <button 
        key={image.id} 
        className={} 
        type="button"
        onClick={() => {
          setActiveImageIndex(index);
        }}>
        <img src={image.url} alt={orphanage.name} />
      </button>
    );
  })}
</div>
```