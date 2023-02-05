## Curso Java EE - J2EE
- curso disponível no [YouTube](https://www.youtube.com/playlist?list=PLbEOwbQR9lqz9AnwhrrOLz9cz1-TxoiUg)
>Lembrando que esse repositório é composto apenas de anotações que fiz durante a visualização do curso, para que eu possa estudar posteriormente de tempos em tempos, e ter uma fonte de consulta rápida. Gostaria muito de agradecer ao Professor José de Assis, por disponibilizar o curso de forma gratuita. Fica registrado aqui meu muito obrigado Professor!

### Sumário
- [Java EE](#java-ee)
- [Ambiente de desenvolvimento](#ambiente-de-desenvolvimento)
- [Tipo de dados primitivo](#tipos-de-dados-primitivos)
- [Atualização estrutura pastas Eclipse](#atualização-na-estrutura-de-pastas-novas-versões-da-ide-eclipse)
- [Setup do ambiente de desenvolvimento](#setup-do-ambiente-de-desenvolvimento-jdk-eclipse-tomcat-MySQL)
	- [JDK](#jdk)
	- [Eclipse](#eclipse)
	- [Tomcat](#tomcat)
	- [MySQL](#mysql)
- [Servlet](#servlet)
	- [Exemplo prático](#exemplo-prático)
- [JSP](#jsp-java-server-pages)
	- [Servlet X JSP](#servlet-x-jsp)
	- [Sintaxe dos elementos JSP](#sintaxe-dos-elementos-jsp)
	- [Exemplo prático JSP](#exemplo-prático-jsp)
- [Projeto Agenda de Contatos](#projeto-agenda-de-contatos)
	- [Tecnologias utilizadas](#tecnologias-utilizadas)
	- [Banco de Dados](#banco-de-dados)
	- [MVC (Model View Controller)](#mvc-model-view-controller)
	- [Criando a estrutura MVC no Eclipse](#criando-a-estrutura-mvc-no-eclipse)
	- [Criando o estilo (CSS)](#criando-o-estilo-css)
	- [Chamando o Servlet](#chamando-o-servlet)
	- [Camada Model (que tem acesso ao BD)](#camada-model-que-tem-acesso-ao-bd)
	- [Conectando com o Banco de Dados](#conectando-com-o-banco-de-dados)
	- [Controller](#controller)
- [CRUD](#crud-create-read-update-e-delete)
	- [Validação do formulário](#validação-do-formulário)
	- [Inserindo os dados](#inserindo-os-dados)
	- [Inserindo dados efetivamente no BD (exemplo com MySQL e diretamente na classe DAO.java)](#inserindo-dados-efetivamente-no-bd-exemplo-com-mysql-e-diretamente-na-classe-daojava)
	- [Listando os contatos com ArrayList](#listando-os-contatos-com-arraylist)
	- [Listagem por meio do JSP](#listagem-por-meio-do-jsp)
	- [Colocando os dados em uma tabela](#colocando-os-dados-em-uma-tabela)
	- [Edição do contato](#edição-do-contato)
	- [CRUD Delete (Exclusão de contato)](#crud-delete-exclusão-de-contato)
- [Gerando um relatório PDF de todos os contatos](#gerando-um-relatório-pdf-de-todos-os-contatos)
- [Documentação](#documentação)
	- [Documentação do BD e Revisão do código fonte](#documentação-do-bd-e-revisão-do-código-fonte)
		- [BD](#bd)
		- [Revisão do código](#revisão-do-código)
		- [Documentação das classes Java](#documentação-das-classes-java)
- [Hospedagem (WAR)](#hospedagem-war)

# Java EE

Java EE: é um conjunto de especificações para a implementação de uma aplicação de Java na Web
Servlet: classe Java capaz de executar a linguagem Java e também gerar código HTML formando páginas dinâmicas
JSP (Java Server Pages): além do Servlet, é possível desenvolver páginas para web utilizando o JSP, que nada mais é que um arquivo baseado em HTML com a extensão `.jsp`

### Ambiente de desenvolvimento
- JDK (Java Development Kit)
- Eclipse IDE Enterprise Edition
- Apache tomcat
- MySQL

### Tipos de dados primitivos

Um tipo de dado primitivo especifica o tamanho e o tipo do valor da variável, e não tem método adicional.

Exitem oito tipos de dado primitivo em Java:

| Tipo de Dado | Tamanho | Descrição |
|---|---|---|
| byte | 1 byte | Armazena números inteiros de -128 to 127 |
| short | 2 bytes |	Armazena números inteiros de -32,768 to 32,767 |
| int |	4 bytes | Armazena números inteiros de -2,147,483,648 to 2,147,483,647 |
| long | 8 bytes | Armazena números inteiros de -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 |
| float | 4 bytes |	Armazena números fracionários. Suficiente para armazenar de 6 a 7 dígitos decimais |
| double | 8 bytes | Armazena números fracionários. Suficiente para armazenar 15 dígitos decimais |
| boolean |	1 bit |	Armazena valores verdadeiros (**true**) ou falsos (**false**) |
| char | 2 bytes | Armazena um único caractere/letra ou valores ASCII |

### Atualização na estrutura de pastas novas versões da IDE Eclipse

| Até a versão 2021-03  | Até a versão 2021-06 |
| ------------- | ------------- |
| Servlet e Classes Java ficam em `src` | Servlet e Classes Java ficam em `src/main/java`  |
| Arquivos do site ficam em `WebContent`  | Arquivos do site ficam em `src/main/webapp`  |

### Setup do ambiente de desenvolvimento (JDK-Eclipse-Tomcat-MySQL)
## JDK
- realizar o download do openJDK em [Adoptium](https://adoptium.net/temurin/releases/?version=11). Instalar versão completa. Para testar, abrir o command e digitar `java -version`
A saída abaixo deverá ser exibida

```powershell
openjdk version "11.0.17" 2022-10-18
OpenJDK Runtime Environment Temurin-11.0.17+8 (build 11.0.17+8)
OpenJDK 64-Bit Server VM Temurin-11.0.17+8 (build 11.0.17+8, mixed mode)
```

podemos também testar o *javac*, digitando `javac -version`
```
javac 11.0.17
```
## Eclipse
- disponível em [https://www.eclipse.org/downloads/](https://www.eclipse.org/downloads/). Quando executar, escolher a opção *Elipse IDE for Enterprise Java Developers*

## Tomcat
- iremos baixar a versão `.zip`, pois não iremos instalar no windows, e sim o eclipse que fará as configurações. Descompactamos o aquivo `.zip`, e copiamos a pasta `apache-tomcat-10.0.27` para o disco local `C:\`. Depois iremos no Eclipse, na divisão inferior, há uma aba *Server*. Iremos escolher o servidor **Apache Tomcat**, indicar o caminho que copiamos o apache tomcat descompactado, e posteriormente clicar em *Use Tomcat installation* (segunda opção) e salvar tudo (botões parte superior esquerda)
- agora vamos testar se está funcionando: vamos em *file* > *new* > *Dynamic Web Project*. Nome do projeto pode ser *Hello tomcat*, vamos agora na pasta `src/main/webapp` e criaremos um novo arquivo HTML chamado `index.html`, criamos com o template de **html5**. Agora podemos executar o projeto.

## MySQL
- iremos até a pagina de downloads do MySQL [https://www.MySQL.com/downloads/](https://www.MySQL.com/downloads/), vamos descer até chegar em *MySQL Community* (GPL) Downloads. Agora vamos em MySQL *Installer for Windows*. Podemos escolher a versão web mesmo, que é a mais leve
- dentro do instalador, iremos escolher o *MySQL Server* e o *Workbench*. Esperaremos o download terminar. Depois só seguir as configurações padrão de *usuário* e *senha*, *localhost*, etc.

# Servlet
No modelo de páginas dinâmicas, o qual o cliente faz um requisição ao servidor, e esse, por sua vez, retorna os dados processados para o cliente, o *servlet* nada mais é do que isso. É uma classe Java que recebe as requisições, processa essas requisições e produz um conteúdo dinâmico. Esse conteúdo sempre será de uma forma que o navegador de internet do cliente seja capaz de interpretar (HTML, CSS, Javascript, por exemplo)

## Exemplo prático
Criaremos um novo *Dynamic Web Project*, chamado *Hello Servlet*, no final, marcaremos o checkbox *Generate web.xml deployment descriptor*. Marcando essa opção, criará o arquivo ```web.xml```. Esse arquivo carrega informações para *deployment* em servidores.

Agora dentro de `Java Resources`, criaremos um *package* para organizar os arquivos do projeto. Esse *package* pode ser chamado de *hello*

Dentro do *package* hello iremos criar um novo Servlet, o nome de classe será Hello, e avançaremos até a parte de escolha dos métodos. Desmarcaremos o *DoPost*, pois não iremos enviar requisição, apenas iremos receber, por meio do *DoGet*

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
>**Dica**
>
>Caso precise importar alguma classe, use o atalho `Ctrl + Shift + O` no Eclipse.

# JSP (Java Server Pages)
Foram criados para resolução de algumas limitações que tem no Servlet, no que diz respeito a formatação do documento HTML. A diferença do servlet para JSP, é que no JSP podemos executar código Java por meio de uma tag especial chamada `scriplet` (se assemelha bastante ao funcionamento do PHP)

>Por trás dos panos, uma página JSP é automaticamente transformada em um servlet pelo servidor Tomcat

## Servlet X JSP
Uma recomendação é utilizar JSP para interface do sistema, e *servlet* para cuidar da parte de lógica do sistema, ficando uma arquitetura flexível e fácil de manipular

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
>Apenas o **elemento de expressões** que não fecha comando com `;`<br />
>Exemplo: `<p>Data: <%=new Date()%></p>`

## Exemplo prático jsp
Criaremos um novo *Dynamic Web Project*, chamado *Hello JSP*, no final, marcaremos o *checkbox* *Generate web.xml deployment descriptor*.

Agora dentro de `src/main/webapp`, criaremos um novo arquivo JSP (JSP File), com o nome `hello.jsp`, aceitar a utilização de um template html5, e clique em *Finish*. Lembrar de mudar os *charset* para `utf-8`. Colocar também a propriedade `lang="pt-br"` para que possamos acertar a questão de acentos e outras características do idioma Português do Brasil, ficando da seguinte forma:

`hello.jsp`
```jsp
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
		<!-- para Date() funcionar, precisa importar no começo do documento usando Diretivas -->
		<p>Data: <%=new Date()%></p>

		<%!int contador=0;%>
		<!-- como é um comando de expressão, ele vai diretamente para o formato documento html -->
		<p>Visitas: <%=contador++%></p>

	</body>
</html>
```
Por fim, executar o projeto no servidor Tomcat para testar.

# Projeto Agenda de Contatos
## Tecnologias utilizadas
- Servidor Tomcat
- Java EE - Servlet e JSP
- IDE Eclipse
- MySQL (CRUD - *Create Read Update Delete*)
- MVC (*Model View Controller*)
- JavaBeans (Classe Java que segue um conjunto de especificações, relacionadas à segurança e utilização do código)
- JDBC (*Java Database Connectivity*): conjunto de classes e interfaces escritas em Java usadas para envios de instruções SQL ao BD
- iText (utilizada para gerar relatórios no formato `.pdf`)
- além de HTML, CSS e Javascript

## Banco de Dados
No desenvolvimento de um projeto, devemos:
1. definir a necessidade do cliente
2. desenvolver o BD de forma a atender os requisitos do sistema

Iremos armazenar os campos **nome**, **fone** e **e-mail** dos contatos. Sendo os campos **nome** e **fone** `not null`, ou seja, eles são **obrigatórios**. Além de que cada contato será gerado um código, para posteriormente termos a possibilidade de atualizar os dados e excluí-los.

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

>Comentários no MySQL
>
>**single line (primeira forma)**: # comment<br />
>**single line (segunda forma)**: -- comment<br />
>**multi line**: /* comment */<br />

## MVC (Model View Controller)
Visa principalmente separar, organizar, e melhorar o desempenho e segurança do sistema, além de permitir a reutilização do código. Esse padrão ainda permite que uma equipe trabalhe separadamente em pontos distintos do sistema:

- **Model**: fica com o processamento pesado, que é a camada que tem acesso ao banco de dados (são duas classes: `JavaBeans`, para tratar da segurança, e a classe `DAO` que significa *Data Access Object*, para tratar acesso e conexão com o bando de dados)

- **View**: é responsável pela interface com o usuário, ou seja, é a visualização do documento de forma dinâmica. Em um projeto Web, são os arquivos que podem ser renderizados pelo navegador, ou seja, o HTML, o CSS e o Javascript. (geralmente arquivos `.html`, `.css`, `.js` e até mesmo `.jsp`)

- **Controller**: coordena e controla o fluxo de dados, que basicamente trabalha com requisições e respostas (por exemplo, o servlet)

## Criando a estrutura MVC no Eclipse
- Iremos em *File* > *New* > *Dynamic Web Project*. O nome do projeto poderá ser **agenda**.
- Dentro de `src/main/java`, criaremos um *package* chamado `controller` e um *package* chamado `model`.
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
>Comentários em HTML
>
>**single line** e **multi line**: `<!-- Comment -->`. <br />

- Agora podemos executar o projeto no servidor Tomcat para testar (sempre selecionando o arquivo `index.html`).

## Criando o estilo (CSS)
Primeiro vamos criar um pasta `images` dentro de `src/main/webapp`, e inserir duas imagens, uma será o favicon, e a outra ficará na página inicial. (`agenda.png` e `favicon.png`)

>Comentários no CSS
>
>**single line e multi line**: /* comment */<br />

Agora no arquivo `index.html`, vamos adicionar as seguintes linhas logo após a tag `<title>`:
```html
<link rel="icon" href="images/favicon.png">
<link rel="stylesheet" href="style.css">
```
Dentro da tag `<body>`, antes da tag `<h1>`, vamos adicionar a imagem da agenda e adicionar uma classe chamada `Botao1` ao link chamado "Acessar"
```html
<img src="images/agenda.png">
...
<a href="" class="Botao1">Acessar</a>
```

Agora criaremos um arquivo chamado `style.css` no mesmo nível de pasta do arquivo `index.html`, e importaremos dentro de `style.css` a fonte *Open Sans* diretamente do Google Fonts, e já formatando a tag `body` , a tag `h1` e por fim a tag de link a `href`.
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
Em `index.html`, lá na tag `<a href="">`, a qual aponta a lugar algum, pois a propriedade `href` está em branco (`""`), inseriremos o valor `main`, ficando `<a href="main"class="Botao1">Acessar</a>`. Esse valor `main` será recebido pela classe `Controller.java`, então vamos abri-la.

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
Nessa camada trabalharemos com as classes `JavaBeans.java` e a classe `DAO.java`

Em `JavaBeans.java`, iremos criar as variáveis `idcon`, `nome`, `fone` e `email`, seguindo o padrão do banco de dados, com os modificadores de acesso com valor `private`.

No Eclipse, em *Source* > *Generate Getters and Setters*, criaremos os métodos `Getters` e `Setters`.

Ainda no Eclipse, vamos em *Source* > *Generate Constructors from Superclass* para criar um Construtor, e posteriormente vamos em *Source* novamente > *Generate Constructor using Fields* e criar um novo Construtor com todos os campos.

## Conectando com o Banco de Dados

- **JDBC** (Java™ EE Database Connectivity): reúne um conjunto de classes o qual possibilita se conectar através de um *driver* específico ao banco de dados desejado
	- **DriverManager**: classe que usaremos para gerenciar o *driver*
	- **Connection**: *interface* para conectar ao BD, irá estabelecer uma conexão entre o Java e o BD

No modelo, depois do `JavaBeans.java`, temos o `DAO.java` (*Data Access Object* - Padrão de Projeto que encapsula os mecanismos de acesso aos dados, escondendo os detalhes da execução). A classe `DAO.java` é a única classe capaz de estabelecer uma conexão com o Banco de Dados, com o módulo de conexão, utilizando:

- **Driver**: é uma espécie de tradutor na troca de mensagens entre o banco de dados e o Java (tipo de banco de dados)
- **URL**: (IP ou domínio do servidor)
- **Banco**: (Nome do banco de dados)
- **Autenticação**: (Usuário e senha)

Agora vamos até [https://dev.MySQL.com/downloads/](https://dev.MySQL.com/downloads/), e vamos escolher o *Connector* (o *connector* vai ser referente ao *driver*) referente [Connector/J](https://dev.MySQL.com/downloads/connector/j/) e faça o *download* utilizando a plataforma independente na escola do Sistema Operacional, pois assim poderemos baixar a versão `.zip`. 

Faça o download conforme versão do MySQL que tiver instalado.
Após descompactar, usaremos o arquivo `MySQL-connector-j-8.0.31.jar`, que no caso é o nosso *driver*.

>Arquivo `.jar` é um arquivo executável do Java.

Iremos copiar o arquivo `MySQL-connector-j-8.0.31.jar` para dentro da pasta `src/main/webapp/WEB-INF/lib`, no caso, a pasta `lib`, que é uma abreviação de *library*.

Agora no arquivo `DAO.java` vamos criar efetivamente a nossa conexão com o Banco de Dados.
Iremos dividir em duas partes, sendo os **parâmetros de conexão** e o **método de conexão**

Criaremos as variáveis `driver`, `url`, `user` e `password`, todas do tipo `String` e com modificações de acesso `private`, criaremos também um método `conectar()`, ficando dessa forma:
```java
package model;

import java.sql.Connection;
import java.sql.DriverManager;

public class DAO {
	//Módulo de conexão
	//Parâmetros de conexão
	private String driver = "com.MySQL.cj.jdbc.Driver";
	private String url = "jdbc:MySQL://127.0.0.1:3306/dbagenda?useTimezone=true&serverTimeZone=UTC";
	private String user = "root";
	private String password = "senha_mysql";
	
	//Método de conexão
	private Connection conectar() {
		Connection conn = null;
		
		try {
			Class.forName(driver);
			conn = DriverManager.getConnection(url, user, password);
			return conn;			
		} catch (Exception e) {
			System.out.println(e);
			return null;
		}
		
	}
	//teste conexão
	public void testeConexao() {
		try {
			Connection conn = conectar();
			System.out.println(conn);
			conn.close();
		} catch (Exception e) {
			System.out.println(e);
		}
	}
}
```
Agora para testarmos a conexão, iremos até o arquivo `Controller.java`, e dentro do método `Controller`, criamos uma instância de `DAO.java`, lembrando que precisamos importar `DAO.java` para a classe `Controller.java`. Dentro do método `doGet()`, usaremos a instância de `DAO.java` e executamos o método `testeConexao()`.
```java
import model.DAO;
...
public class Controller extends HttpServlet {
	private static final long serialVersionUID = 1L;
    
	DAO dao = new DAO();
	...
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		response.getWriter().append("Served at: ").append(request.getContextPath());
		//teste de conexão
		dao.testeConexao();
	}
```
Se tudo correr bem, devemos ver a instância da conexão lá no console no Eclipse, conforme impresso logo abaixo:
```
com.MySQL.cj.jdbc.ConnectionImpl@3b323d9b
```

Caso retorne erro, será impresso **null** e será mostrado a `exception`. O método `testeConexao()` dentro da classe `DAO.java` pode ser removido ou comentado, pois o utilizamos apenas por uma questão didática, assim como a instância dele utilizada (`dao.testeConexao();`) dentro de `Controller.java`.

>Comentário em Java
>
>**single line**: // comment <br />
>**multi line**: /* comment */ <br />
>**documentation**: /** comment **/ <br />

## Controller
Dentro de `index.html`, temos um botão que irá fazer uma consulta ao servlet, e esse por sua vez, irá nos trazer o arquivo `agenda.jsp`, arquivo esse que contém elementos de HTML e também é capaz de executar a linguagem Java, ou seja, ele possui um conteúdo que é gerado de forma dinâmica.

Dentro de `/src/main/webapp` criaremos o arquivo `agenda.jsp`, com o seguinte conteúdo
```jsp
<%@ page language="java" contentType="text/html; charset=utf-8"
    pageEncoding="utf-8"%>
<!DOCTYPE html>
<html lang="pt-br">
<head>
<meta charset="utf-8">
<title>Agenda de contatos</title>
<link rel="icon" href="images/favicon.png">
<link rel="stylesheet" href="style.css">
</head>
<body>
	<h1>Agenda de Contatos</h1>
	<a href="" class="Botao1">Novo contato</a>
</body>
</html>
```
Note que reaproveitamos os estilos padrão que foi usado em `index.html`, agora, quando iniciamos o aplicativo, após clicarmos em "Acessar", chamamos o servlet, mas ele ainda não sabe tratar e chamar o arquivo `agenda.jsp`. Para isso, precisamos configurar nosso `Controller.java`.
No `controller.java`, iremos reaproveitar o método `doGet()`, reescrevendo-o logo abaixo com o nome de `contatos()`.
No método `doGet()`, criaremos uma `action`, que pega o caminho do servlet, e compararemos com o `/main`, e chamaremos esse segundo método (que foi baseado no método `doGet`), o qual usa o método `sendRedirect()` para redirecionar para `agenda.jsp`, ficando dessa forma:
```java
protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	String action = request.getServletPath();
	
	if (action.equals("/main")) {
		contatos(request, response);			
	}
}
	
//Lista contatos
protected void contatos(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	response.sendRedirect("agenda.jsp");
}
```
Dessa forma, o redirecionamento já vai funcionar.

>**Dicas de formatação do código**
>
>`Shift + Alt + Y`: faz a quebra automática de linha<br>
>`Ctrl + Shift + F`: para indentar o código

# CRUD (Create, Read, Update e Delete)

## Validação do formulário
Iremos criar um arquivo chamado `novo.html`, e um arquivo `validador.js`. Esse validador ficará em uma pasta chamada `scripts`, dentro de `/src/main/webapp/`

`novo.html`
```html
<!DOCTYPE html>
<html lang="pt-br">
<head>
<meta charset="utf-8">
<title>Agenda Contatos</title>
<link rel="icon" href="images/favicon.png">
<link rel="stylesheet" href="style.css">
</head>
<body>
	<h1>Criar novo contato</h1>
	<form name="frmContato" action="insert">
		<table>
		<tr>
			<td><input type="text" name="nome" placeholder="Nome" class="Caixa1"></td>
		</tr>
		<tr>
			<td><input type="text" name="fone" placeholder="Fone" class="Caixa2"></td>
		</tr>
		<tr>
			<td><input type="text" name="email" placeholder="Email" class="Caixa1"></td>
		</tr>
		</table>
		<input type="button" value="Adicionar" class="Botao1" onClick="validar()">
	</form>
	<script src="scripts/validador.js"></script>
</body>
</html>
```

Dentro de `style.css`, faremos algumas modificações na classe `Botao1`, e adicionaremos as classes para cuidar do formulário, conforme abaixo:

`style.css`
```css
.Botao1 {
	text-decoration: none;
	background-color: #66bbff;
	padding: 5px 10px 5px 10px;
	color: #fff;
	font-size: 1.2em;
	font-weight: 700;
	border-radius: 5px;
	border: 0;
	cursor: pointer; 
}

.Caixa1 {
	width: 320px;
	padding: 5px;
	margin-bottom: 10px;
	border: 1px solid #66bbff;
	border-radius: 5px;
}

.Caixa2 {
	padding: 5px;
	margin-bottom: 10px;
	border: 1px solid #66bbff;
	border-radius: 5px;
}
```

O arquivo `validador.js` ficará da seguinte forma:
```js
/**
 * Validação de formulário
 @autor Rodrigo
 */

 function validar() {
	let nome = frmContato.nome.value;
	let fone = frmContato.fone.value;
	
	if(nome === "") {
		alert('Preencha o campo nome');
		frmContato.nome.focus();
		return false;
	} else if (fone === '') {
		alert('Preencha o campo fone');
		frmContato.fone.focus();
		return false;
	} else {
		document.forms["frmContato"].submit();
	}
}
```

>Comentário em Javascript
>
>**single line**: // comment <br />
>**multi line**: /* comment */ <br />

Dentro de `agenda.jsp`, preencheremos a propriedade `href` para buscar a página `novo.html`:<br />
`<a href="novo.html" class="Botao1">Novo contato</a>`

## Inserindo os dados

No arquivo `novo.html`, na tag `form`, preencheremos a propriedade `action` com o valor "insert", ficando assim: `<form name="frmContato" action="insert">`

Agora, boa parte das ações, deverão ser feitas pela classe `Controller.java`, assim, iremos nela e na linha que descreve os padrões de **url**, adicionaremos `/insert` para que `Controller.java` seja capaz de lidar com esse novo padrão.

```java
@WebServlet(urlPatterns = {"/Controller", "/main", "/insert"})
```

Agora vamos criar um novo `else if` para lidar com o padrão `insert`, que irá chamar um novo método, e inserimos algumas informações para testarmos se os parâmetros `nome`, `fone` e `email` estão chegando corretamente.

`Controller.java`
```java
if (action.equals("/main")) {
	contatos(request, response);			
} else if (action.equals("/insert")) {
	novoContato(request, response);
} else {
	response.sendRedirect("agenda.jsp");
}

//Novo contato
protected void novoContato(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	System.out.println(request.getParameter("nome"));
	System.out.println(request.getParameter("fone"));
	System.out.println(request.getParameter("email"));
```

Próximo passo é armazenarmos essas variáveis (`nome`, `fone` e `email`) na classe `JavaBeans.java`, que por sua vez estão encapsulados, logo, teremos que importar `JavaBeans.java` lá na classe `Controller.java`, e por fim, instanciar um novo objeto JavaBeans, para termos acesso aos métodos públicos (`getters` and `setters`) e armazenar nas variáveis com o modificador de acesso do tipo `private`

`Controller.java`

```java
...
import model.JavaBeans;
...
    
DAO dao = new DAO();
JavaBeans contato = new JavaBeans();
...
	//teste de recebimento dos dados do formulário
	System.out.println(request.getParameter("nome"));
	System.out.println(request.getParameter("fone"));
	System.out.println(request.getParameter("email"));
	
	//setar (alocar) as variáveis JavaBeans
	contato.setNome(request.getParameter("nome"));
	contato.setFone(request.getParameter("fone"));
	contato.setEmail(request.getParameter("email"));
...
```
## Inserindo dados efetivamente no BD (exemplo com MySQL e diretamente na classe DAO.java)

Abrindo o MySQL Workbench, conseguimos inserir dados na tabela contatos usando o comando `INSERT INTO contatos (nome, fone, email) VALUES ('Bill Gates','9999-1111','bill@outlook.com');` e posteriormente conseguimos ler esse dados por meio do comando `SELECT * FROM contatos;`

Agora, para efetuarmos essa inserção por meio da classe `DAO.java`, no final da classe, antes do último fechamento de chaves `}`, iremos inserir:

```java
/* CRUD CREATE */
public void inserirContato(JavaBeans contato) {
	String create = "INSERT INTO contatos (nome, fone, email) VALUES (?,?,?)";
	try {
		// abrir conexão com o BD
		Connection conn = conectar();

		// Preparar a query para execução no BD
		PreparedStatement pst = conn.prepareStatement(create);

		// Substituir os parâmetros (?) pelo conteúdo das variáveis JavaBeans
		/* o 1 é ref. ao 1º parâmetro (?) em create na linha String create, primeira declaração do método inserirContato() */
		pst.setString(1, contato.getNome()); 
		pst.setString(2, contato.getFone());
		pst.setString(3, contato.getEmail());

		// Executar a query (efetivamente insere os dados no BD)
		pst.executeUpdate();

		// encerrar a conexão com o BD
		conn.close();

	} catch (Exception e) {
		System.out.println(e);
	}
}
```

Em `Controller.java`, logo após setar (alocar) as variáveis `nome`, `fone` e `email`, iremos adicionar o seguinte código:

```java
//invocar o método inserirContato passando o objeto contato
dao.inserirContato(contato);

//redirecionar para o documento agenda.jsp
response.sendRedirect("main");
```

Após isso podemos rodar o projeto e testar o cadastro, assim como checar posteriormente no MySQL com o comando `SELECT`.

## Listando os contatos com ArrayList

Em `DAO.java`, criaremos o método `listarContatos()`, do tipo `ArrayList<JavaBeans>`:
```java
/* CRUD READ */
public ArrayList<JavaBeans> listarContatos() {
	// Criando objeto para acessar a classe JavaBeans
	ArrayList<JavaBeans> contatos = new ArrayList<>();
	
	String read = "SELECT * FROM contatos ORDER BY nome";

	try {
		Connection conn = conectar();
		PreparedStatement pst = conn.prepareStatement(read);
		/* ResultSet armazena temporariamente o 
		retorno do BD (faz parte do JDBC) */
		ResultSet rs = pst.executeQuery();

		// o laço abaixo será executado enquanto houver contatos
		while (rs.next()) {
			// variáveis de apoio que recebem os dados do BD
			String idcon = rs.getString(1);
			String nome = rs.getString(2);
			String fone = rs.getString(3);
			String email = rs.getString(4);

			// populando o ArrayList
			contatos.add(new JavaBeans(idcon, nome, fone, email));
		}
		conn.close();
		return contatos;

	} catch (Exception e) {
		System.out.println(e);
		return null;
	}
}
```

E no `Controller.java`, criaremos um método chamado `contatos`, o qual pegará as informações diretamente da classe `DAO.java`, e os exibirá, por enquanto, utilizando `System.out.println` apenas para testarmos.

`Controller.java`
```java
//Lista contatos
protected void contatos(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
	//response.sendRedirect("agenda.jsp");
	
	//Criando um objeto que irá receber os dados JavaBeans
	ArrayList<JavaBeans> lista = dao.listarContatos();
	
	//teste de recebimento da lista		
	for (int i = 0; i < lista.size(); i++) {
		System.out.println(lista.get(i).getIdcon());
		System.out.println(lista.get(i).getNome());
		System.out.println(lista.get(i).getFone());
		System.out.println(lista.get(i).getEmail());
	}
}
```

Como retorno no console, devemos ter a listagem do contatos cadastrados no BD.

## Listagem por meio do JSP

Agora o que precisamos fazer é redirecionar ao arquivo `agenda.jsp` levando consigo os dados armazenados no `ArrayList`.

Podemos comentar o teste de recebimento da lista, logo, iniciaremos o encaminhamento desses dados para `agenda.jsp`, utilizando o `RequestDispacher`, o qual importaremos de `import jakarta.servlet.RequestDispatcher;`, da seguinte forma:

`Controller.java`

```java
// Encaminhar a lista ao documento agenda.jsp		
	request.setAttribute("contatos", lista);
	RequestDispatcher rd = request.getRequestDispatcher("agenda.jsp");
	rd.forward(request, response);
```

Agora iremos em `agenda.jsp` e iniciaremos algumas importações que teremos que ter:

`agenda.jsp`
```jsp
<%@ page import="model.JavaBeans"%>
<%@ page import="java.util.ArrayList"%>
```

Para testarmos se está tudo OK, iremos exibir os dados vindo do `ArrayList`, da seguinte forma:

```jsp
<%
	//recebe o objeto lista
	ArrayList<JavaBeans> lista = (ArrayList<JavaBeans>) request.getAttribute("contatos");

	//lista os dados
	for (int i = 0; i < lista.size(); i++) {
		out.println(lista.get(i).getIdcon());
		out.println(lista.get(i).getNome());
		out.println(lista.get(i).getFone());
		out.println(lista.get(i).getEmail());
	}
%>
```

Se tudo der certo, é para retornar os dados no cabeçalho da página web, agora basta apenas colocarmos em um formato de tabela.

## Colocando os dados em uma tabela

Aqui iremos misturar Java com o documento HTML, da seguinte forma:

`agenda.jsp`

```jsp
<body>
	<h1>Agenda de Contatos</h1>
	<a href="novo.html" class="Botao1">Novo contato</a>
	
	<table>
		<thead>
			<tr>
				<th>Id</th>
				<th>Nome</th>
				<th>Fone</th>
				<th>E-mail</th>
			</tr>
		</thead>
		<tbody>
			<%for (int i = 0; i < lista.size(); i++) {%>
				<tr>
				<!-- quando usa %= geralmente é para obter algum valor, e não usa ; (ponto e vírgula)-->
					<td><%=lista.get(i).getIdcon() %></td>
					<<td><%=lista.get(i).getNome() %></td>
					<td><%=lista.get(i).getFone() %></td>
					<td><%=lista.get(i).getEmail() %></td>
				</tr>
			<%} %>
		</tbody>
	</table>
</body>
```

Por fim, basta criarmos um estilo para a tabela. Na tag `<table>` criaremos uma propriedade `id="tabela"` para identificar a tabela perante ao CSS. No arquivo `style.css`, criaremos a seguinte formatação:

`style.css`
```css
#tabela {
	margin-top: 30px;
	border-collapse: collapse;/*une as bordas da tabela*/
}

#tabela th {
	border: 1px solid #ddd;
	padding: 10px;
	text-align: left;
	background-color: #66bbff;
	color: #fff;
}

#tabela td {
	border: 1px solid #ddd;
	padding: 10px;
}
```

## Edição do contato

Primeiro passo é criarmos uma nova coluna na tabela que exibe os contatos, com o conteúdo chamado "Opções", no arquivo `agenda.jsp`, dessa forma: `<th>Opções</th>`.
Dessa mesma forma, criaremos um novo campo contendo um link com conteúdo chamado "Editar", dessa forma `<td><a href="" class="Botao1">Editar</a></td>`. Com isso, se executarmos o projeto, será mostrado essa nova coluna, e com um botão (que na verdade é um link) "Editar" para cada contato.

Agora para tratarmos a requisição com o `id` do contato, dentro do link criado anteriormente, colocaremos o conteúdo `select`, ficando assim: `<a href="select" class="Botao1">`, e no arquivo `Controller.java`, lá em `@WebServlet`, criaremos um novo padrão (pattern) chamado `"/select"`, ficando essa linha em questão, da seguinte forma: `@WebServlet(urlPatterns = {"/Controller", "/main", "/insert", "/select"})`.

Ainda em `Controller.java`, onde verificamos a String `action`, criaremos um novo `else if` para tratar requisições para `"/select"`, assim como criar um novo método para tratar essa requisição, que se chamará `listarContato()`, ficando dessa forma:
```java
//Editar contato
protected void listarContato(HttpServletRequest request, HttpServletResponse response)
		throws ServletException, IOException {
	//conteúdo método listarContato()
}
```

Nesse ponto podemos rodar o nosso projeto, e clicar no botão "Editar" para testarmos se a requisição está sendo enviada ao sevlet, no caso, a essa URL `http://localhost:8080/agenda/select`.

Agora precisamos enviar o `id` a classe `Controller.java`, pode meio do formulário em `agenda.jsp`. Para o arquivo `agenda.jsp` enviar esse `id`, precisaremos editar o link utilizando variável por meio do ponto de interrogação (?), ficando dessa forma:
```jsp
<a href="select?idcon=<%=lista.get(i).getIdcon()%>" class="Botao1">Editar</a>
```

Agora em `Controller.java`, no método `listarContato()`, criaremos uma String que receberá esse `id`, e irá alocar (setar) na classe `JavaBeans.java` por meio do `SetIdcon(idcon)`, ficando da seguinte forma:

```java
//recebimento do id do contato que será editado
String idcon = request.getParameter("idcon");	

//imprime no console como teste se recebeu realmente a variável vinda de agenda.jsp
System.out.println(idcon);

contato.setIdcon(idcon);
```

Como somente nossa classe `DAO.java` que faz acesso ao BD, criaremos um novo método para buscar no Banco de Dados o contato que será feito a atualização de dados, essa busca deve ser por `id`, para evitar puxar o dado incorreto ou não desejado. O método `selecionarContato()`, dentro de `DAO.java`, ficará da seguinte forma:
```java
/* CRUD UPDATE */
// selecionar o contato
public void selecionarContato(JavaBeans contato) {
	
	String read2 = "SELECT * FROM contatos WHERE idcon = ?";
	
	try {
		Connection conn = conectar();
		PreparedStatement pst = conn.prepareStatement(read2);
		pst.setString(1, contato.getIdcon());
		ResultSet rs = pst.executeQuery();
		
		while(rs.next()) {
			//setar (alocar) as variáveis JavaBeans
			contato.setIdcon(rs.getString(1));
			contato.setNome(rs.getString(2));
			contato.setFone(rs.getString(3));
			contato.setEmail(rs.getString(4));
		}
		
		conn.close();
		
	} catch (Exception e) {
		System.out.println(e);
	}
	
}
```

Depois, dentro de `Controller.java`, daremos continuidade ao método `listarContato()`, o qual chamaremos o método `selecionarContato()` lá da classe `DAO.java`, e iremos imprimir, para fins de teste, as variáveis contidas no BD.
```java
//Editar contato
protected void listarContato(HttpServletRequest request, HttpServletResponse response)
		throws ServletException, IOException {
	//recebimetno do id do contato que será editado
	String idcon = request.getParameter("idcon");	
	
	//imprime no console como teste se recebeu realmente a variável vinda de agenda.jsp
	System.out.println(idcon);
	
	contato.setIdcon(idcon);
	
	//Executar o método selecionarContato() da classe DAO.java
	dao.selecionarContato(contato);
	
	//teste de recebimento
	System.out.println(contato.getIdcon());
	System.out.println(contato.getNome());
	System.out.println(contato.getFone());
	System.out.println(contato.getEmail());
}
```

Agora precisaremos criar um formulário que irá receber esses dados, para posteriormente serem alterados e gravados no BD. Dessa forma, criaremos um novo arquivo em `\src\main\webapp` chamado `editar.jsp`.
Como esse novo formulário é muito semelhante ao formulário anterior, copiaremos o formulário do arquivo `novo.html` e colaremos dentro de `editar.jsp`. Iremos retirar as propriedades `placeholder`, mudar o valor do botão "Adicionar" para "Salvar", assim como removeremos o conteúdo da propriedade action da tag `<form>`. A validação dos campos podemos deixar por conta do arquivo `validador.js`, que já está na tag `<script>`.
Teremos que criar um novo campo para visualizarmos o `id` do contato, assim, podemos copiar a linha do campo `nome` e replicá-lo logo acima, e alterar a propriedade `name` para `idcon`, ficando `name="idcon"`. Mudaremos também a classe para estilos, no caso mudaremos para o uso da propriedade `id`, que chamaremos de `caixa3`, ficando da seguinte forma: `<input type="text" name="idcon" id="caixa3" readonly>` (perceba que está com a propriedade `readonly`, pois não queremos que esse campo seja editável).
No estilo, criaremos esse identificador, utilizando `#` (apenas classes usamos o ponto `.`), e incluiremos os seguintes valores:
```css
#caixa3 {
	padding: 5px;
	margin-bottom: 10px;
	border: 1px solid #ff0000;
}
```

Em `Controller.java` iremos setar (alocar) os atributos e direcioná-los para o arquivo `editar.jsp`, ficando assim:
```java
		// setar (alocar) os atributos do formulário com o conteúdo JavaBeans
		request.setAttribute("idcon", contato.getIdcon());
		request.setAttribute("nome", contato.getNome());
		request.setAttribute("fone", contato.getFone());
		request.setAttribute("email", contato.getEmail());
		
		// encaminhar ao documento editar.jsp
		RequestDispatcher rd = request.getRequestDispatcher("editar.jsp");
		rd.forward(request, response);
```

E por fim, em `editar.jsp`, adicionaremos a propriedade `value`, trazendo as informações para o preenchimento automático dos campos, ficando dessa forma:
```jsp
<form name="frmContato" action="">
	<table>
		<tr>
			<td><input type="text" name="idcon" id="caixa3" readonly
				value="<%out.print(request.getAttribute("idcon"));%>"></td>
		</tr>
		<tr>
			<td><input type="text" name="nome" class="Caixa1"
				value="<%out.print(request.getAttribute("nome"));%>"></td>
		</tr>
		<tr>
			<td><input type="text" name="fone" class="Caixa2"
				value="<%out.print(request.getAttribute("fone"));%>"></td>
		</tr>
		<tr>
			<td><input type="text" name="email" class="Caixa1"
				value="<%out.print(request.getAttribute("email"));%>"></td>
		</tr>
	</table>
	<input type="button" value="Salvar" class="Botao1" onClick="validar()">
</form>
```

Agora iremos no `Controller.java`, e criaremos o *urlPatterns* chamado `/update`, assim como no *form action*, do arquivo `editar.jsp`, colocaremos o valor `update`, ficando assim: `<form name="frmContato" action="update">`.
Em `Controller.java`, criaremos um método `editarContato()`, o qual irá setar (alocar) as variáveis no `JavaBeans` e chamar o método `alterarContato()`, que fica dentro do arquivo `DAO.java`, e por fim, é redirecionado para o documento `agenda.jsp`, por meio do `sendRedirect("main");`. O método `editarContato()` ficará da seguinte forma:
```java
// Editar contato
protected void editarContato(HttpServletRequest request, HttpServletResponse response)
		throws ServletException, IOException {

	// teste recebimento
	/*
		* System.out.println(request.getParameter("idcon"));
		* System.out.println(request.getParameter("nome"));
		* System.out.println(request.getParameter("fone"));
		* System.out.println(request.getParameter("email"));
		*/

	// setar (alocar) as variáveis JavaBeans
	contato.setIdcon(request.getParameter("idcon"));
	contato.setNome(request.getParameter("nome"));
	contato.setFone(request.getParameter("fone"));
	contato.setEmail(request.getParameter("email"));
	
	//executar o método alterarContato()
	dao.alterarContato(contato);
	
	//redirecionar para o documento agenda.jsp (atualizando as alterações)
	response.sendRedirect("main");
	
}
```

Método `alterarContato()`, que fica dentro de `DAO.java` ficará assim:
```java
public void alterarContato(JavaBeans contato) {
	String create = "UPDATE contatos SET nome=?,fone=?,email=? WHERE idcon=?";
	
	try {
		Connection conn = conectar();
		PreparedStatement pst = conn.prepareStatement(create);
		pst.setString(1, contato.getNome());
		pst.setString(2, contato.getFone());
		pst.setString(3, contato.getEmail());
		pst.setString(4, contato.getIdcon());
		
		pst.executeUpdate();
		
		conn.close();
	} catch (Exception e) {
		System.out.println(e);
	}
}
```

## CRUD Delete (Exclusão de contato)

Primeiro passo, em `agenda.jsp` iremos duplicar a última coluna para criarmos um botão excluir, com a propriedade do link `href` vazio (`""`). Depois iremos criar um arquivo para validar a exclusão, chamado `confirmador.js`, que ficará na pasta `src\main\webapp\scripts`. Dentro do `confirmador.js`, criaremos o seguinte conteúdo, que por enquanto é apenas um teste:
```js
function confirmar(idcon) {
	let resposta = confirm("Confirma a exclusão deste contato?")
	if (resposta === true) {
		alert(idcon)
	}
}
```
No arquivo `agenda.jsp` teremos que criar um link entre esses arquivos (`agenda.jsp` e `confirmador.js`), utilizando a tag `<script>`, dessa forma `<script src="scripts/confirmador.js"></script>`

Agora na propriedade `href` que deixamos em branco anteriormente, referente à exclusão do contato, preencheremos chamando o método `confirmar()`, por meio da propriedade do javascript usado diretamente no link, ficando dessa forma `<a href="javascript: confirmar(<%=lista.get(i).getIdcon()%>)" class="Botao2">Excluir</a>`. Agora podemos testar se o método `alert()` será chamado corretamente, com o argumento `idcon`.
Para finalizarmos essa primeira etapa, comentaremos o `alert()` no arquivo `confirmador.js` e usaremos a propriedade `window.location.href`, para nos redirecionar até o padrão que usaremos para excluir o contato, ficando o arquivo `confirmador.js` da seguinte forma:
```js
 function confirmar(idcon) {
	let resposta = confirm("Confirma a exclusão deste contato?")
	if (resposta === true) {
		//alert(idcon)
		window.location.href = "delete?idcon=" + idcon
	}
}
```

Agora lá em `Controller.java`, criaremos um novo `urlPatterns` para tratar requisições de `/delete`, assim como nas outras vezes, criaremos um `else if` e um método para tratar, chamado `removerContato()`. Como das outras vezes, criaremos uma variável para receber o `idcon` por `getParameter()` e iremos imprimir para testar, ficando dessa forma o conteúdo do método `removerContato()`:
```java
String idcon = request.getParameter("idcon");
System.out.println(idcon);
```

Em `DAO.java`, ficará dessa forma:
```java
/* CRUD DELETE */
public void deletarContato(JavaBeans contato) {
	String delete = "DELETE FROM contatos WHERE idcon=?";
	try {
		Connection conn = conectar();
		PreparedStatement pst = conn.prepareStatement(delete);
		pst.setString(1, contato.getIdcon());
		pst.executeUpdate();
		conn.close();
	} catch (Exception e) {
		System.out.println(e);
	}
}
```

Em `Controller.java`, ficará da seguinte forma:
```java
// Remover um contato
protected void removerContato(HttpServletRequest request, HttpServletResponse response)
		throws ServletException, IOException {
	// recebimento do id do contato a ser excluído (validador.js)
	String idcon = request.getParameter("idcon");

	// setar (alocar) a variável idcon JavaBeans
	contato.setIdcon(idcon);

	// executar o método deletarContato (DAO) passando o objeto contato
	dao.deletarContato(contato);

	// redirecionar para o documento agenda.jsp (atualizando as alterações)
	response.sendRedirect("main");
}
```

Agora podemos testar a exclusão de um contato, basta executar o projeto novamente.

## Gerando um relatório PDF de todos os contatos

Iremos utilizar a biblioteca [itextpdf](https://github.com/itext/itextpdf/releases/tag/5.5.13.3), que é de código aberto. Iremos fazer o download da [versão 5](https://github.com/itext/itextpdf/releases/download/5.5.13.3/itextpdf-5.5.13.3.zip) dele.
Agora basta extrair o conteúdo, e copiar o arquivo `itextpdf-5.5.13.3.jar` para a pasta `src/main/webapp/WEB-INF/lib` (a mesma pasta que está o driver do BD MySQL).

Agora iremos criar um botão de gerar o relatório no arquivo `agenda.jsp`, logo após o botão "Novo Contato", ficando dessa forma:
```html
<a href="report" class="Botao2">Relatório</a>
```

Em `Controller.java`, criaremos um novo `urlPattern` para ligar com o padrão `report` colocado no link anterior, e assim como nos demais métodos, criaremos um novo `else if` e um novo método para lidar com a geração do relatório, que no caso, irá se chamar `gerarRelatorio()`

No método `gerarRelatorio()` criaremos um objeto `Document`, para termos acesso aos métodos da biblioteca `itextpdf`. Apertando `Ctrl + Shift + O`, podemos escolher de onde importar `Document`, no caso será de `com.itextpdf.text.Document`. A criação do objeto ficará da seguinte forma: `Document documento = new Document();`. A importação ficará assim: `import com.itextpdf.text.Document;`

Assim como vimos em BD, podem ocorrer exceções, no caso da criação de um arquivo PDF, também podem ocorrer exceções, portanto devemos usar um `try catch` para tratar isso. No `catch`, podemos imprimir a exceção, juntamente iremos fechar (`close();`) o documento.

Agora devemos criar:<br />
1. tipo de conteúdo: `response.setContentType("application/pdf");`<br />
2. nome do documento: `response.addHeader("Content-Disposition", "inline; filename="+"contatos.pdf");`<br />
3. criar efetivamente o documento: `PdfWriter.getInstance(documento, response.getOutputStream());`<br />
4. abrir o documento para manipular seu conteúdo:<br />
	4.1. `documento.open();` e logo em seguida `documento.add(new Paragraph("Lista de contatos:"));`. Lembrando que precisamos importar `Paragraph`, da seguinte forma: `import com.itextpdf.text.Paragraph;`<br />

Para testarmos se está tudo correto, podemos fechar o documento (`documento.close()`) e executar o projeto para confirmar se está sendo gerado o arquivo PDF com o conteúdo do Parágrafo "Lista de contatos:".

Agora iremos adicionar a listagem de contatos ao documento PDF. Essa listagem é uma tabela, portanto usaremos o `itextpdf` da seguinte forma:
```java
//criar uma tabela
PdfPTable tabela = new PdfPTable(3); //3 = 3 colunas

PdfPCell col1 = new PdfPCell(new Paragraph("Nome"));
PdfPCell col2 = new PdfPCell(new Paragraph("Fone"));
PdfPCell col3 = new PdfPCell(new Paragraph("Email"));

tabela.addCell(col1);
tabela.addCell(col2);
tabela.addCell(col3);

documento.add(tabela);
```

A partir desse ponto, já podemos, dinamicamente, "popular" (alocar) os dados por meio do Java, ficando dessa forma:
```java
//popular a tabela com os contatos
ArrayList<JavaBeans> lista = dao.listarContatos();

for (int i = 0; i < lista.size(); i++) {
	tabela.addCell(lista.get(i).getNome());
	tabela.addCell(lista.get(i).getFone());
	tabela.addCell(lista.get(i).getEmail());
}

documento.add(tabela);

documento.close();
```

## Documentação
A documentação é muito importante para a comunicação e a transferência do conhecimento. Será desenvolvido em 4 etapas, sendo a primeira, a documentação do BD, a segunda etapa será a revisão do código fonte, revisão essa relacionada a remoção de comentários que foram usados por questões didáticas, indentação do código, padronização de nome usados em variáveis e métodos, assim como simplificação do código. Na terceira etapa será gerado documentação específica das classes Java, por meio de um *plugin* que será instalado no Eclise, e gerar a documentação no formato HMTL usando o *Java Doc*. Na última etapa será mostrado como enviar essa documentação ao github.
### Documentação do BD e Revisão do código fonte
#### BD
No MySQL Workbench, faremos um *dump* do BD, para isso iremos na aba na lateral esquerda, escolhemos a opção `Administration`, vamos em `Data Export`, e selecionamos o BD **dbagenda**. Na seleção ao lado, podemos escolher entre fazer o dump dos dados e da estrutura, somente a estrutura, e somente os dados. Iremos fazer apenas da estrutura, e posteriormente clicar em **Start Export**.

Agora iremos criar o diagrama do BD, por meio do menu *Database* > *Reverse Engineer*, posteriormente poderá ser salvo como arquivo PDF, por meio do *arquivo* > *exportar*.

#### Revisão do código
Agora iremos fazer a revisão do código, que em teoria removeríamos os comentários, segundo essa aula do professor, no entanto vou manter por questão didática para caso necessite estudar o código futuramente.

No arquivo `agenda.jsp` há um *warning* na criação do vetor de lista, que devemos inserir um comando para que seja ignorado o aviso que é dado, usando acima `@ SuppressWarnings ("unchecked")`
### Documentação das classes Java

Iremos utilizar uma extensão do Eclipse para criar a documentação Java Doc. Vamos no menu *Help* > *Eclipse Marketplace* e pesquisar por `jautodoc` e instalar o plugin. O Eclipse pedirá para reiniciar. Agora em cada classe, na última linha, devemos clicar com o botão direito do mouse e ir em `JAutodoc` e ir em `Add Javadoc`.

Agora vamos fazer a documentação de todo o projeto. Para isso, vamos clicar com o botão direito do mouse no projeto `agenda` e vamos em `Export`, vamos expandir a pasta `Java` e selecionar a opção `Javadoc`. Após a criação dessa documentação, uma nova pasta `doc` é criada em `eclipse-workspace\agenda\doc`. Então podemos navegar até essa pasta e abrir o arquivo `index.html`

## Hospedagem (WAR)
Para hospedagem, no servidor que ficará disponível para, teremos um banco de dados, o qual importaremos os dados do nosso criado no decorrer do curso. Após importado, iremos modificar a classe `DAO.java` para mudanças de usuário e senha do banco, e após isso, criaremos um pacote **War**, que contém todos os componentes do projeto Java, que é executado no servidor Tomcat.
Para gerarmos o arquivo **War**, vamos até a pasta principal do projeto, botão direito > *Export* > *Web* > *War File* e nomear o arquivo, assim como escolher o local para salvá-lo.