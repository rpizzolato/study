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

		<%!int contador=0%>
		<p>Visitas: <%=contador++%></p><!-- como é um comando de expressão, ele vai diretamente para o formato documento html -->

	</body>
</html>
```
Por fim, executar o projeto no servidor Tomcat