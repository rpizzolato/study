## Curso Java EE - J2EE
- curso disponível no [YouTube](https://www.youtube.com/playlist?list=PLbEOwbQR9lqz9AnwhrrOLz9cz1-TxoiUg)

Java EE: é um conjunto de especificações para a implementação de uma aplicação de Java na Web
Servlet: classe Java capaz de executar a linguagem Java e também gerar código HTML formando páginas dinâmicas
JSP (Java Server Pages): além do Servlet, é possível desenvolver páginas para web utilizando o JSP, que nada mais é que um arquivo baseado em HTML com a extensão .jsp

### Ambiente de desenvolvimento
- JDK (Java Development Kit)
- Eclipse IDE Enterprise Edition
- Apache tomcat
- MySQL

### Atualização na estrutura de pastas novas versões da IDE Eclipse

| Até a versão 2021-03  | Até a versão 2021-06 |
| ------------- | ------------- |
| Servlet e Classes Java ficam em `src` | Servlet e Classes Java ficam em `src/main/java`  |
| Arquivos do site ficam em `WebContent`  | Arquivos do site ficam em `src/main/webapp`  |

### Setup do ambiente de desenvolvimento (JDK-Eclipse-Tomcat-MySQL)
## JDK
- realizar o download do openJDK em [Adoptium](https://adoptium.net/temurin/releases/?version=11). Instala versão completa. Para testar, abrir o command e digitar `java -version`
A saída abaixo deverá ser exibida

```powershell
openjdk version "11.0.17" 2022-10-18
OpenJDK Runtime Environment Temurin-11.0.17+8 (build 11.0.17+8)
OpenJDK 64-Bit Server VM Temurin-11.0.17+8 (build 11.0.17+8, mixed mode)
```

podemos também testar o javac, digitando `javac -version`
```
javac 11.0.17
```
## Eclipse
- disponível em [https://www.eclipse.org/downloads/](https://www.eclipse.org/downloads/). Quando executar, escolher a opção *Elipse IDE for Enterprise Java Developers*

## Tomcat
- iremos baixar a versão .zip, pois não iremos instalar no windows, e sim o eclipse que fará as configurações. Descompactamos o aquivo .zip, e copiamos a pasta `apache-tomcat-10.0.27` para o disco local `C:\`. Depois iremos no Eclipse, na divisão inferior, há uma aba *Server*. Iremos escolher o servidor Apache Tomcat, indicar o caminho que copiamos o apache tomcat descompactado, e posteriormente clicar em *Use Tomcat installation* (segunda opção) e salvar tudo (botões parte superior esquerda)
- agora vamos testar se está funcionando: vamos em *file* > *new* > *Dynamic Web Project*. Nome do projeto pode ser *Hello tomcat*, vamos agora na pasta `src/main/webapp` e criaremos um novo arquivo HTML chamado *index.html*, criamos com o template de html5. Agora podemos executar o projeto.

## MySQL
- iremos até a pagina de downloads do MySQL [https://www.mysql.com/downloads/](https://www.mysql.com/downloads/), vamos descer até chegar me MySQL Community (GPL) Downloads. Agora vamos em MySQL Installer for Windows. Podemos escolher a versão web mesmo, que é a mais leve
- dentro do instalador, iremos escolher o *MySQL Server* e o *Workbench*. Esperaremos o download terminar. Depois só seguir as configurações padrão de usuário e senha, localhost, etc.

# Servlet
No modelo de páginas dinâmicas, o qual o cliente faz um requisição ao servidor, e esse, por sua vez, retorna os dados processados para o cliente, o servlet nada mais é do que isso. É uma classe Java que recebe as requisições, processa essas requisições e produz um conteúdo dinâmico. Esse conteúdo sempre será de uma forma que o navegador de internet do cliente seja capaz de interpretar (HTML, CSS, Javascript, por exemplo)

## Exemplo prático
Criaremos um novo Dynamic Web Project, chamado *Hello Servlet*, no final, marcaremos o checkbox *Generate web.xml deployment descriptor*. Marcando essa opção, criará o arquivo ```web.xml```. Esse arquivo carrega informações para deployment em servidores.

Agora dentro de `Java Resources`, criaremos um package para organizar os arquivos do projeto. Esse package pode ser chamado de *hello*

Dentro do package hello iremos criar um novo Servlet, o nome de classe será Hello, e avançaremos até a parte de escolha dos métodos. Desmarcaremos o *DoPost*, pois não iremos enviar requisição, apenas iremos receber, por meio do *DoGet*

Dentro do método `doGet`, iremos inserir as seguintes linhas de código (apagar o que tiver anteriormente)
```java
PrintWriter out = response.getWriter();
		out.println("<!doctype html>");
		out.println("<html>");
		out.println("<head>");
		out.println("<title>Hello Servlet</title>");
		out.println("<body>");
		out.println("<h1>Hello Servlet</h1>");
		out.println("</body>");
		out.println("</html>");
```
>Caso precise importar alguma classe, use o atalho **Ctrl + Shift + O** no Eclipse.

# JSP (Java Server Pages)
Foram criados para resolução de algumas limitações que tem no Servlet, no que diz respeito a formatação do documento HTML. A diferença do servlet para JSP, é que no JSP podemos executar código Java por meio de uma tag especial chamada `scriplet` (se assemelha bastante ao funcionamento do PHP)

Por trás dos panos, uma página JSP é automaticamente transformada em um servlet pelo servidor Tomcat

## Servlet X JSP`
Uma recomendação é utilizar JSP para interface do sistema, e servlet para cuidar da parte de lógica do sistema, ficando uma arquitetura flexível e fácil de manipular

## Sintaxe dos elementos JSP

|   |  |
| ------------- | ------------- |
| Scriplet | `<%     %>`  |
| Comentários | `<%--     --%>`  |
| Diretivas | `<%@     %>`  |
| Declarações | `<%!     %>`  |
| Expressões | `<%=     %>`  |

>**Importante!**
>
>Apenas o elemento de expressões que não fecha comando com `;`<br />
>Exemplo: `<p>Data: <%=new Date()%></p>`

## Exemplo prático
Criaremos um novo Dynamic Web Project, chamado *Hello JSP*, no final, marcaremos o checkbox *Generate web.xml deployment descriptor*.

Agora dentro de `src/main/webapp`, criaremos um novo arquivo JSP (JSP File), com o nome `hello.jsp`, aceitar a utilização de um template html5, e clique em *Finish*. Lembrar de mudar os *charset* para `utf-8`. Colocar também a propriedade `lang="pt-br"` para que possamos acertar a questão de acentos e outras características do idioma Português do Brasil, ficando da seguinte forma:

`hello.jsp`
```html
<%@ page language="java" contentType="text/html; charset=utf-8" pageEncoding="utf-8">

<%@ page import="java.util.Date"%>

<!DOCTYPE html>
<html lang="pt-br">
	<head>
		<meta charset="utf-8">
		<title>Hello JSP</title>
	</head>
	<body>
		<h1>Hello JSP</h1>

		<% out.println("Impressao texto usando Java - Scriplet"); %>

		<%-- Uso do elemento expressão --%>
		<p>Data: <%=new Date()%></p> <!-- para Date() funcionar, precisa importar no começo do documento usando Diretivas -->

		<%!int contador=0;%>
		<p>Visitas: <%=contador++%></p><!-- como é um comando de expressão, ele vai diretamente para o formato documento html -->

	</body>
</html>
```
Por fim, executar o projeto no servidor Tomcat

# Projeto Agenda de Contatos
## Tecnologias utilizadas
- Servidor Tomcat
- Java EE - Servlet e JSP
- IDE Eclipse
- MySQL (CRUD - Create Read Update Delete)
- MVC (Model View Controller)
- JavaBeans (Classe Java que segue um conjunto de especificações, relacionadas à segurança e utilização do código)
- JDBC (Java Database Connectivity): conjunto de classes e interfaces escritas em Java usadas para envios de instruções SQL ao BD
- iText (utilizada para gerar relatórios no formato `.pdf`)
- além de html, css e javascript

## Banco de Dados
No desenvolvimento de um projeto, devemos:
1. definir a necessidade do cliente
2. desenvolver o BD de forma a atender os requisitos do sistema

Iremos armazenar os campos **nome**, **fone**, **e-mail** dos contatos. Sendo os campos **nome** e **fone**, sendo `not null`, ou seja, eles são obrigatórios. Além de que cada contato será gerado um código, para posteriormente haver atualização de dados e exclusão.

Dentro do MySQL Workbench, iremos executar os scripts de criação da base de dados e da tabela **contatos**:
```sql
create database dbagenda;
show databases;
use dbagenda;
create table contatos(
	idcon int primary key auto_increment,
    nome varchar(50) not null,
    fone varchar(15) not null,
    email varchar(50)
);

show tables;
desc contatos;
```

Para conexão posteriormente com o Java, precisaremos de algumas informações, tais como:
- **url**: 127.0.0.1:3306/dbagenda
- **user**: root
- **password**: a senha que foi definido na instalação do MySQL

## MVC (Model View Controller)
Visa principalmente separar, organizar, e melhorar o desempenho e segurança do sistema, além de permitir a reutilização do código. Esse padrão ainda permite que uma equipe trabalhe separadamente em pontos distintos do sistema

- Model: fica com o processamento pesado, que é a camada que tem acesso ao banco de dados (são duas classes: JavaBeans, para tratar da segurança, e a classe DAO, para tratar acesso e conexão com o bando de dados)

- View: é responsável pela interface com o usuário, ou seja, é a visualização do documento de forma dinâmica. Em um projeto Web, são os arquivos que podem ser renderizados pelo navegador, ou seja, o html, o css e o javascript. (geralmente arquivos html, css, js e até mesmo jsp)

- Controller: coordena e controla o fluxo de dados, que basicamente trabalha com requisições e respostas (por exemplo, o servlet)

## Criando a estrutura MVC no Eclipse
- Iremos em File > New > Dynamic Web Project. O nome do projeto poderá ser **agenda**.
- Dentro de `src/main/java`, criaremos um package chamado `controller` e um package chamado `model`.
- Dentro do package `controller`, criaremos um novo Servlet chamado `Controller`, desmarcando o método `doPost`
- Agora dentro do package `model` criaremos duas novas classes chamadas `DAO` e `JavaBeans`
- Criaremos um arquivo `index.html` dentro da pasta `src/main/webapp` com o conteúdo:
```html
<!DOCTYPE html>
<html lang="pt-br">
<head>
<meta charset="utf-8">
<title>Agenda Contatos</title>
</head>
<body>
	<h1>Agenda Contatos</h1>
	<a href="">Acessar</a>
</body>
</html>
```
- Agora podemos executar o projeto no servidor Tomcat para testar.

## Criando o estilo (CSS)
Primeiro vamos criar um pasta `images` dentro de `src/main/webapp`, e inserir duas imagens, uma será o favicon, e a outra ficará na página inicial. (`agenda.png` e `favicon.png`)

Agora no arquivo `index.html`, vamos adicionar as seguintes linhas logo após a tag `<title>`:
```html
<link rel="icon" href="images/favicon.png">
<link rel="stylesheet" href="style.css">
```
e dentro da tag `<body>`, antes da tag `<h1>`, vamos adicionar a imagem da agenda e adicionar uma classe chamada **Botao1** ao link chamado **Acessar**
```html
<img src="images/agenda.png">
...
<a href="" class="Botao1">Acessar</a>
```

Agora criaremos um arquivo chamado `style.css` no mesmo nível de pasta do arquivo `index.html`, e importaremos dentro de `style.css` a fonte *Open Sans* diretamente do Google Fonts, e já formatando a tag **body** , a tag **h1** e por fim a tag de link **a href**
```css
@charset "utf-8";
@import url('https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;700&display=swap');

body {
	font-family: 'Open Sans', sans-serif;
	font-size: 1em;
	font-weight: 400;
}

h1 {
	color: #66bbff;
}

.Botao1 {
	text-decoration: none;
	background-color: #66bbff;
	padding: 5px 10px 5px 10px;
	color: #fff;
	font-size: 1.2em;
	font-weight: 700;
	border-radius: 5px;
}
```
>**Observação**
>
>A medida **1em** equivale a **16px** ou fonte de tamanho **12**

## Chamando o Servlet
Em `index.html`, lá na tag `<a href="">`, ao qual aponta a lugar algum, pois a propriedade `href` está em branco (`""`), inseriremos o valor `main`, ficando `<a href="main"class="Botao1">Acessar</a>`. Esse valor `main` será recebido pela classe `Controller.java`, então vamos abri-la.

Assim que clicarmos em "Acessar", um erro 404 será disparado, com as informações:
```
HTTP Status 404 – Não Encontrado
Type Status Report

Message The requested resource [/agenda/main] is not available

Description The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.

Apache Tomcat/10.0.27
```
Isso ocorre pois ainda não tratamos a requisição na camada Controller. Iremos adicionar o código abaixo antes da declaração da classe Controller:

`Controller.java`
```java
@WebServlet(urlPatterns = {"/Controller", "/main"})
```

Ao executarmos novamente o projeto, o retorno deverá ser igual o abaixo demonstrado:
```
Served at: /agenda
```

## Camada Model (que tem acesso ao BD)
Nessa camada trabalharemos com as classes `JavaBeans.java` e a classe `DAO.java`:
