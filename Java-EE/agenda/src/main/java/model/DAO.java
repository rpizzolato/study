package model;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;

// TODO: Auto-generated Javadoc
/**
 * The Class DAO.
 */
public class DAO {
	// Módulo de conexão
	/** The driver. */
	// Parâmetros de conexão
	private String driver = "com.mysql.cj.jdbc.Driver";
	
	/** The url. */
	private String url = "jdbc:mysql://127.0.0.1:3306/dbagenda?useTimezone=true&serverTimeZone=UTC";
	
	/** The user. */
	private String user = "root";
	
	/** The password. */
	private String password = "senha_do_banco";

	/**
	 * Conectar.
	 *
	 * @return the connection
	 */
	// Método de conexão
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

	/**
	 * Teste conexao.
	 */
	// teste conexão
	public void testeConexao() {
		try {
			Connection conn = conectar();
			System.out.println(conn);
			conn.close();
		} catch (Exception e) {
			System.out.println(e);
		}
	}

	/**
	 * Inserir contato.
	 *
	 * @param contato the contato
	 */
	/* CRUD CREATE */
	public void inserirContato(JavaBeans contato) {
		String create = "INSERT INTO contatos (nome, fone, email) VALUES (?,?,?)";
		try {
			// abrir conexão com o BD
			Connection conn = conectar();

			// Preparar a query para execução no BD
			PreparedStatement pst = conn.prepareStatement(create);

			// Substituir os parâmetros (?) pelo conteúdo das variáveis JavaBeans
			pst.setString(1, contato.getNome()); // o 1 é ref. ao 1º parametro (?) em create na linha String create,
													// primeira declaração do método inserirContato()
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

	/**
	 * Listar contatos.
	 *
	 * @return the array list
	 */
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

	/* CRUD UPDATE */
	/**
	 * Selecionar contato.
	 *
	 * @param contato the contato
	 */
	// selecionar o contato
	public void selecionarContato(JavaBeans contato) {

		String read2 = "SELECT * FROM contatos WHERE idcon = ?";

		try {
			Connection conn = conectar();
			PreparedStatement pst = conn.prepareStatement(read2);
			pst.setString(1, contato.getIdcon());
			ResultSet rs = pst.executeQuery();

			while (rs.next()) {
				// setar as variáveis JavaBeans
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

	/**
	 * Alterar contato.
	 *
	 * @param contato the contato
	 */
	// editar contato
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

	/**
	 * Deletar contato.
	 *
	 * @param contato the contato
	 */
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
}
