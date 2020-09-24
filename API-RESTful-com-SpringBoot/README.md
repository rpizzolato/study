# carros-spring

Curso API RESTful com SpringBoot Udemy Prof Ricardo Lecheta

## Anotações para revisão

- [Seção 3: Resumo das seções 4 a 8](#Seção-3-Resumo-das-seções-4-a-8)
- [Seção 4: Web Services - básico](#Seção-4-Web-Services---básico)
- [Seção 5: API dos Carros](#Seção-5-API-dos-Carros)
- [Seção 6: HTTP Status Code - Parte 1](#Seção-6-HTTP-Status-Code---Parte-1)
- [Seção 9: Testes com Spring Boot](#Seção-9-Testes-com-Spring-Boot)
- [Seção 11: Tratamento de erros e exceções](#Seção-11-Tratamento-de-erros-e-exceções)

Algumas anotações para realizar consulta e revisão futuramente.

### Seção 3: Resumo das seções 4 a 8

- DTO significa Data Transfer Object;
- `@Data` substitui todas as demais propriedades da classe, como getters and setters, construtores padrão, toString, hashcode, etc.

### Seção 4: Web Services - básico

- Toda classe que vai ser uma API em Spring precisa estar com a anotação `@RestController`;
- Necessário usar o `@RequestMapping` também, para criar a rota desejada. Por exemplo `@RequestMapping("/api/v1/carros")` ou `@RequestMapping("/")`;
- API mais básica possível, seria usar o `@GetMapping` para que funcione, para trazer a lista de carros, por exemplo;
- No `@PostMapping`, usar com `@RequestParam`, para indicar os dados que irão ser inseridos no Banco de Dados;
- ao usar GET para envio de login ou senha, ambos ficam visíveis, e também a url pode ficar muito grande (2.048 caracteres).

### Seção 5: API dos Carros

- alterar a senha de root, nunca se usa update, use o seguinte comando: `ALTER USER 'root'@'localhost' IDENTIFIED BY '1234';` e em seguida para efetivar a mudança:  `flush privileges;`;
- `mysql -u root -p carros` (já se conecta no banco de dados `carros`);
- `mysql -u root -p1234 carros` (vai direto para o database `carros` já colocando a senha de acesso ao Banco de Dados (não é recomendado);
- JPA - Java Persistence API, é necessário acidionar nas dependências do arquivo `pom.xml`
```sh
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-data-jpa</artifactId>
</dependency>
```
- `@Entity` na classe Carro mapeia com o Banco de Dados `carros`, caso o nome fosse diferente, usa @Entity(name = "nome_do_bd");
- `@GeneratedValue(strategy = GenerationType.AUTO)` cria automaticamente um auto incremento;
- os demais campos, pode anotar com `@Column(name = "nome"`), mas como está identico ao Bando de Daados, não precisa anotar completamente;
- quanto mais próximo sua classe padrão (Carro) for da tabela do Banco de Dados, mais fácil fica;
- a classe `CrudRepository` aceita dois parâmetros: o Objeto e o Id dele;
- CRUD significa Create, Read, Update and Delete;
- Sempre que utilizar a JPA + Spring, a classe base (Carro) tem que ter um construtor padrão.

### Seção 6: HTTP Status Code - Parte 1
- `return ResponseEntity.ok(service.getCarros());` é um atalho para `return new ResponseEntity<>(service.getCarros(), HttpStatus.OK);`;
- o *status code not found* por ser feito de 3 formas: com *if* normal, *if* ternário ou *lambda*;
- *status code 204* = *No Content*. [Mais status code](https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status).

### Seção 9: Testes com Spring Boot
- [H2](https://www.h2database.com/html/main.html) é um tipo de BD que roda em memória no spring boot, usado para testes;
- se no Objeto for colocado `UrlFoto`, o hibernate vai traduzir para sql, como `url_foto`;
- pode utilizar um arquivo `schema.sql` com o esquema das tabelas que serão criadas ao executar o teste. Mas tem que lembrar de desligar o `create-drop` (colocando como `none`) no arquivo `application.properties`.

### Seção 11: Tratamento de erros e exceções
- Vantagem de utilizar a anotação `@ExceptionHandler` é que a camada de negócio fica mais limpa, como por exemplo, não é necessário ficar criaando `try catch` dentro dela.