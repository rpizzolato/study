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

- Model: fica com o processamento pesado, que é a camada que tem acesso ao banco de dados (são duas classes: JavaBeans, para tratar da segurança, e a classe DAO que significa Data Access Object, para tratar acesso e conexão com o bando de dados)

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
Nessa camada trabalharemos com as classes `JavaBeans.java` e a classe `DAO.java`

Em `JavaBeans.java`, iremos criar as variáveis `idcon`, `nome`, `fone` e `email`, seguindo o padrão do banco de dados, com os modificadores de acesso com valor `private`

No Eclipse, em Source > Generate Getters and Setters, criaremos os métodos Getters e Setters.

Ainda no Eclipse, vamos em Source > Generate Constructors from Superclass para criar um Construtor, e posteriormente vamos em Source novamente > Generate Constructor using Fields e criar um novo Construtor com todos os campos.

## Conectando com o Banco de Dados
- **JDBC** (Java™ EE Database Connectivity): reúne um conjunto de classes o qual possibilita se conectar através de um driver específico ao banco de dados desejado
	- DriverManager: classe que usaremos para gerenciar o driver
	- Connection: interface para conectar ao BD, irá estabelecer uma conexão entre o Java e o BD

No modelo, depois do JavaBeans, temos o `DAO.java` (Data Access Object - Padrão de Projeto que encapsula os mecanismos de acesso aos dados, escondendo os detalhes da execução). A classe `DAO.java` é a única classe capaz de estabelecer uma conexão com o Banco de Dados, com o módulo de conexão, utilizando:

	- Driver: é uma espécie de tradutor na troca de mensagens entre o banco de dados e o Java (tipo de banco de dados)
	- URL (IP ou domínio do servidor)
	- Banco (Nome do banco de dados)
	- Autenticação (Usuário e senha)

Agora vamos até [https://dev.mysql.com/downloads/](https://dev.mysql.com/downloads/), e vamos escolher o Connector (o connector vai ser referente ao driver) referente [Connector/J](https://dev.mysql.com/downloads/connector/j/) e faça o download utilizando a plataforma independente na escola do Sistema Operacional, pois assim poderemos baixar a versão `.zip`. Faça o download conforme versão do MySQL que tiver instalado.
Após descompactar, usaremos o arquivo `mysql-connector-j-8.0.31.jar`, que no caso é o nosso driver. Arquivo `.jar` é um arquivo executável do Java.
Iremos copiar o arquivo `mysql-connector-j-8.0.31.jar` dentro da pasta `src/main/webapp/WEB-INF/lib`, no caso, a pasta `lib`, que é uma abreviação de *library*.

Agora no arquivo `DAO.java` vamos criar efetivamente a nossa conexão com o Banco de Dados.
Iremos dividir em duas partes, sendo os **parâmetros de conexão** e o **método de conexão**

Criaremos as variáveis driver, url, user e password, todas do tipo String e com modificações de acesso private, criaremos também um método conectar(), ficando dessa forma:
```java
package model;

import java.sql.Connection;
import java.sql.DriverManager;

public class DAO {
	//Módulo de conexão
	//Parâmetros de conexão
	private String driver = "com.mysql.cj.jdbc.Driver";
	private String url = "jdbc:mysql://127.0.0.1:3306/dbagenda?useTimezone=true&serverTimeZone=UTC";
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
Agora para testarmos a conexão, iremos até o arquivo Controller.java, e dentro do método Controller, criamos uma instância de DAO.java, lembrando que precisamos importar DAO.java para a classe Controller.java. Dentro do método doGet(), usaremos a instância de DAO.java e executamos o método testeConexao().
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
Se tudo correr bem, devemos ver a instância da conexão lá no console no Eclipse
```
com.mysql.cj.jdbc.ConnectionImpl@3b323d9b
```

Caso retorne erro, será impresso **null** e mostra a Exception. O método testeConexao() dentro da classe `DAO.java` pode ser removido ou comentado, pois o utilizamos apenas por uma questão didática, assim como a instância dele utilizada (`dao.testeConexao();`) dentro de `Controller.java`.

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
Note que reaproveitamos os estilos padrão que foi usado em `index.html`, agora, quando iniciamos o aplicativo, após clicarmos em **Acessar**, chamamos o servlet, mas ele ainda não sabe tratar e chamar o arquivo `agenda.jsp`. Para isso, precisamos configurar nosso `Controller.java`.
No `controller.java`, iremos reaproveitar o método doGet(), reescrevendo-o logo abaixo com o nome de `contatos()`.
No método `doGet()`, criaremos uma action, que pega o caminho do servlet, e compararemos com o `/main`, e chamaremos esse segundo método (que foi baseado no método `doGet`), o qual usa o método `sendRedirect()` para redirecionar para `agenda.jsp`, ficando dessa forma:
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
>**Shift + Alt + Y** = faz a quebra automática de linha<br>
>**Ctrl + Shift + F** = para indentar o código

# CRUD

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

Dentro de style.css, faremos algumas modificações na classe Botao1, e adicionaremos as classes para cuidar do formulário, conforme abaixo:

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

Dentro de `agenda.jsp`, preencheremos a propriedade `href` para buscar a página `novo.html`: `<a href="novo.html" class="Botao1">Novo contato</a>`

## Inserindo os dados

No arquivo `novo.html`, na tag `form`, preencheremos a propriedade `action` com o valor **insert**, ficando assim: `<form name="frmContato" action="insert">`

Agora, boa parte das ações, será feito pela classe `Controller.java`, logo, iremos nela e na linha que descreve os padrões de **url**, adicionaremos `/insert` para que `Controller.java` seja capaz de lidar com esse novo padrão.

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

Próximo passo é armazenarmos essas variáveis (`nome`, `fone` e `email`) na classe `JavaBens.java`, que por sua vez estão encapsulados, logo, teremos que importar `JavaBeans.java` lá na classe `Controller.java`, e por fim, instanciar um novo objeto JavaBens, para termos acesso aos métodos públicos (**getters** and **setters**) e armazenar nas variáveis `private`

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
		
		//setar as variáveis JavaBeans
		contato.setNome(request.getParameter("nome"));
		contato.setFone(request.getParameter("fone"));
		contato.setEmail(request.getParameter("email"));
...
```
## Inserindo dados efetivamente no BD (exemplo com MySQL e diretamente na classe DAO.java)

Abrindo o MySQL Workbench, conseguimos inserir dados na tabela contatos usando o comando `insert into contatos (nome, fone, email) values ('Bill Gates','9999-1111','bill@outlook.com');` e posteriormente conseguimos ler esse dados por meio do comando `select * from contatos;`

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
			pst.setString(1, contato.getNome()); // o 1 é ref. ao 1º parametro (?) em create na linha String create, primeira declaração do método inserirContato()
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

Em `Controller.java`, logo após setar as variáveis `nome`, `fone` e `email`, iremos adicionar o seguinte código:

```java
	//invocar o método inserirContato passando o objeto contato
	dao.inserirContato(contato);
	
	//redirecionar para o documento agenda.jsp
	response.sendRedirect("main");
```

Após isso podemos rodar o projeto e testar o cadastro, assim como checar posteriormente no MySQL com o comando `select`.

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
			ResultSet rs = pst.executeQuery();// ResultSet armazena temporariamente o retorno do BD (faz parte do JDBC)

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

Agora o que precisamos fazer é redirecionar ao arquivo `agenda.jsp` levando consigo os dados armazenados no `ArrayList`

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
					<td><%=lista.get(i).getIdcon() %></td><!-- quando usa %= geralmente é para obter algum valor, e não usa ; (ponto e vírgula)-->
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