## Maratona Full Cycle Development 4.0

#### Acompanhando a Maratona Full Cycle Development 4.0, toda anotação aqui tem o fim apenas para aprendizado, para lembrar comandos ou configurações dos arquivos, todos os direitos são reservados ao idealizador da maratona, Wesley Willians, o qual sou muito grato por disponibilizar todo conteúdo de qualidade gratuítamente.
---
## Conteúdo
- [Aula 24/08 - Full Cycle Development e estudo de caso](#Aula-24/08-Full-Cycle-Development-e-estudo-de-caso)
  - [Microsserviços](#Microsserviços)
  - [O que é um Microsserviço?](#O-que-é-um-Microsserviço?)
  - [No estudo de caso vai ser utilizado microsserviços?](#No-estudo-de-caso-vai-ser-utilizado-microsserviços?)
  - [Estudo de caso](#Estudo-de-caso)
  - [Tecnologias](#Tecnologias)
  - [Breve resumo Docker](#Breve-resumo-Docker)
    - [Dockerfile](#Dockerfile)
    - [Subindo um servidor nginx na porta 8080](#Subindo-um-servidor-nginx-na-porta-8080)
    - [Executando comandos no container](#Executando-comandos-no-container)
    - [Desafio 1](#Desafio-1)
    - [Solução que desenvolvi](#Solução-que-desenvolvi)
- [Aula 25/08 - Autenticação com OpenID Connect e Keycloak](#Aula-25/08-Autenticação-com-OpenID-Connect-e-Keycloak)
  - [OAuth 2](#OAuth-2)
- [Lives](#)
  - 
- [Aula 26/08 - Backend com Nest.js, Loopback e Websockets](#Aula-26/08-Backend-com-Nest.js,-Loopback-e-Websockets)
---

### Aula 24/08 - Full Cycle Development e estudo de caso
- Não será desenvolvido sistema monolítico, ou seja, aquele que já tem tudo embutido no pacote, pois há possiblidade de algo parar de funcionar e afetar todo resto;

### Microsserviços
- microsserviços não são módulos;
- contextos;
- DDD (Domain Driven Design);
- Baseado no negócio (tem que resolver alguma parte do negócio);
- Pouca responsabilidade;
- Equipes menores e especializadas;

### O que é um Microsserviço?
  - Disponibiliza informação;
  - Realiza transações;
  - Resolve problemas de negócio;
  - Independente de tecnologia ou protocolo;
  - Pode estabelecer comunicação com diversos "clientes".

### No estudo de caso vai ser utilizado microsserviços?
  - Não necessariamente;
  - Terá 3 aplicações interdependentes (Backend, SPA e Auth Server);
  - Cada aplicação é executada em servidores diferentes, serão monitoradas de forma independente;
  - Outras aplicações ou até mesmo microsserviços podem ser adicionados;
  - Backend e frontend possuem muita responsabilidade.

### Estudo de caso
  - Plataforma de comunicação em tempo real parecida com o Discord ou Slack;
  - O sistema está dividido em 3 partes: Backend, frontend com uma SPA e um servidor de autenticação;
  - O usuário poderá se registrar. No momento do registro, a conta será criada no servidor de autenticação;
  - O usuário poderá realizar o login no servidor de autenticação e receber os tokens de acesso;
  - Após login, o usuário poderá criar um novo servidor;
  - Dentro de um servido o usuário também poderá criar categorias, bem como os canais de comunicação;
  - O usuário também poderá gerar um convite para que oturos usuários acessem seu servidor;
  - Os usuários poderão se comunicar em tempo real.

### Tecnologias
  - **Autenticação**
    - OAuth 2;
    - OpenID Connect;
    - Keycloak.
  - **Backend**
    - TypeScript;
    - Nest.js;
    - Loopback;
    - Elasticsearch;
    - Kibana.
  - **SPA (Single Page Application)**
    - TypeScript;
    - React.js;
    - Websockets.
  - **Controle de versão / CI / CD**
    - Github;
    - Políticas de acesso aos branches;
    - Code Owner;
    - Code Review;
    - Integração contínua / Google Cloud Build;
    - Kubernetes no GKE;
    - Autoscaling com HPA (Horizontal Pod Autoscaler - Faz o próprio Kubernetes escalar).
  - **Observabilidade**
    - Elasticsearch;
    - Filebeat;
    - Metrics;
    - APM Server.

### Breve resumo Docker
  - containers são processos, que já rodam no Sistema Operacional, divididos em:
    - **Namespaces**: isola os processos;
    - **Cgroup**: controla os recursos;
    - **File System** = OFS (Overlay File System)
  - imagem de um container é imutável, logo toda alteração feita nele é perdido caso inicie o container novamente. As mudanças são feitas na camada `Read / Write`;
  - quando utilizado o `docker pull`, a imagem é armazenada em um cache, para uso futuro.
#### Dockerfile
  - conjunto de instruções para construção de um container:
    - **FROM**: ImageName;
    - **RUN**: Comandos ex: apt-get install;
    - **EXPOSE**: 8080
  - por meio do _Dockerfile_ é gerado um _build_ da imagem;
  - **commit** na imagem gera uma nova versão da imagem (alterada por alguma necessidade), no entanto **não** é uma boa prática;
  - _image registry_ é o local onde ficam as imagens (como um repositório do _Github_);
  - **exemplo**: considere o seguinte arquivo `main.go` escrito em linguagem de programação Go:
  ```go
  package main

  import (
    "fmt"
    "log"
    "net/http"
  )

  func handler(w http.ResponseWriter, r *http.Request) {
    fmt.Fprintf(w, "Olá Full Cycle Developers!")
  }

  func main() {
    http.HandleFunc("/", handler)
    log.Fatal(http.ListenAndServe(":8000", nil))
  }
  ```
  agora o arquivo `Dockerfile`:
  ```dockerfile
  FROM golang:1.14
  COPY . .
  RUN go build main.go
  EXPOSE 8080
  ENTRYPOINT [ "./main" ]
  ```
  por fim, execute o comando para criar uma nova imagem com as configurações/customizações feitas acima: `docker build -t image-customizada/esquenta-mfc4-docker .`. O `-t` vem de tag, no caso, da nova imagem. O ponto (`.`) é para indicar para pegar os arquivos da pasta corrente.
  - para subir a imagem para um repositório de imagens (Docker Hub, por exemplo), use: `docker push nome_imagem:latest`

#### Subindo um servidor nginx na porta 8080
- execute: `docker run --name webserver -p 8080:80 -d nginx`
`docker run`: **cria** um novo container<br/>
`--name`: nome do container. Se não usar `--name` o próprio Docker cria alguns nomes(estranhos)<br/>
`-p 8080:80`: expõe a porta 8080 no host que está executando o Docker, acessando a porta 80 do container (no caso, nginx)<br/>
`-d`: vem de _detach_. Roda o container em _background_<br/>
- para excluir um container, `docker rm id_container/nome_container`, no entanto é preciso para o container antes, `docker stop id_container/nome_container`.

#### Executando comandos no container
- `docker exec`:
  - pode ser utilizado `docker exec nome_container ls`, sendo o `ls` o comando que será executado no container. Outro exemplo poderia ser listar o conteúdo do arquivo `index.html` dentro de `/usr/share/nginx/html/`, com uma imagem _nginx_: `docker exec nginx cat /usr/share/nginx/html/index.html`;
  - acessando o bash do container: `docker exec -it nginx bash`. `i` vem de --interactive e `t` vem de pseudo-TTY, logo, faz um _attach_ no TTY basicamente.

#### Docker images
- `docker images`: lista as imagens que estão em cache no computador;
- `docker rmi nome_imagem`: remove a imagem do cache no computador;

#### Docker compose
- gerenciar diversos container de uma vez só é algo bem trabalhoso, nisso é possível utilizar o Docker Compose, que visa facilitar esse gerenciamento;
- para utilizar, crie um arquivo chamado `docker-compose.yaml`. Vejamos um exemplo:
```docker-compose
version: '3'

services:
  nginx:
    image: nginx
    volumes:
      - ./nginx:/usr/share/nginx/html
    ports:
      - 8080:80
```
no script acima, é criado um serviço chamado `nginx`, com volume que copia a pasta `./nginx` localmente para a pasta `/usr/share/nginx/html` no container. A Porta `80` do container é acessada pela porta `8080` do computador local.
- para executar (ou subir) o serviço, basta executar: `docker-compose up`. Acesse `http://localhost:8080` para testar a aplicação;
- `docker-compose up -d` fica rodando em background

### Desafio 1
- Criar uma imagem docker que quando executada deverá expor a porta 8080 exibindo a mensagem: **Eu sou Full Cycle**.

#### Solução que desenvolvi
- iniciar um projeto NodeJS com o `npm init -y`;
- instalar o Express via `npm install express`;
- criar um arquivo `index.js` com o conteúdo:
```js
const express = require("express");
const app = express();

app.get('/', (req, res) => {
  res.send("Eu sou Full Cycle");
});

app.listen(8080);
```
- criar um arquivo `Dockerfile` com o seguinte conteúdo:
```dockerfile
# versão LTS do Node
FROM node:12

# pasta do App
WORKDIR /usr/src/app

# copia o package.json e também o lock
COPY package*.json ./

# instala as dependências
RUN npm install

# copia os arquivos para dentro do container
COPY . .

# expõe a porta 8080
EXPOSE 8080

# executa o arquivo index.js por meio do node
CMD [ "node", "index.js" ]
```
- é criado os arquivos `.gitignore` para evitar cópia da pasta `node_modules` para o controle de versão. É criado também um arquivo `.dockerignore` para evitar a cópia da pasta anteriormente citada, assim evitando que seja sobrescrito;
- para construir a imagem, use: `docker build -t <username>/<image_name> .`;
- para criar um container com a imagem criada: `docker run -p 8080:8080 --name desafio-01 -d <username>/<image_name>`;
- acesso `localhost:8080` para visualizar o resultado;
- para subir a imagem no Docker Hub: `docker push <username>/<image_name>`;

- Caso queira fazer o download da imagem no Docker Hub, execute: `docker pull rpizzolato/desafio-01`, ou acesse pela url: [https://hub.docker.com/repository/docker/rpizzolato/desafio-01](https://hub.docker.com/repository/docker/rpizzolato/desafio-01)
- Referências:
  - [Dockerizing a Node.js web app](https://nodejs.org/de/docs/guides/nodejs-docker-webapp/);
  - [node Docker Official Images](https://hub.docker.com/_/node).
---
### Aula 25/08 - Autenticação com OpenID Connect e Keycloak
- delegação de responsabilidade: é um processo de delegação temporária de um recurso para um terceiro;
  - Resource owner;
  - Client;
  - Resource Server;
  - Authorisation Server;
#### OAuth 2
  - é um framework de autorização (**e não de autenticação**) que permite que aplicações de terceira possam ter acesso limitado a um serviço _HTTP_;
- para aproveitar tudo que é oferecido pelo OAuth 2 para oferecer serviços de autenticação, é utilizado o **OpenID Connect**, que é uma camada que fica no top do OAuth 2 que permite identificar a identidade de um usuário final através do processo de autenticação do servidor de autorização;
- para gerenciar tudo isso, é utilizado o **Keycloak**:
  - **Keycloack** (Open Source Identity and Access Management - Sistema Open Source de Identidade e Gerenciamento de Acesso);
  - **Keycloack** com Docker: execute:
```docker
docker run -p 8080:8080 -e KEYCLOAK_USER=admin -e KEYCLOAK_PASSWORD=admin quay.io/keycloak/keycloak:11.0.1
```
  mais informações pode ser obtido direto do [site](https://www.keycloak.org/getting-started/getting-started-docker);<br/>
  - primeiro ponto é que o **Keycloak** possui vários _realms_, e no caso do primeiro login, você estará no _realm master_, que cuida da própria autenticação do **Keycloak**, logo para as aplicações que serão construídas, um novo _realm_ irá ser criado.

  ### Aula 26/08 - Backend com Nest.js, Loopback e Websockets
  #### Nest.js
  - É um framework Node.js progressivo para construir aplicações server-side eficiente, confiável e escalável;

## Lives
### Performance no Stack Overflow
- tecnologias utilizadas:
  - .net core
  - c#
  - asp.net core
  - sql server
  - typescript/jquery
  - elasticsearch
  - redis
  - ha proxy (único SO Linux que utilizam)



