import { useState } from "react"

import styles from './Form.module.css'

export default function Form() {
  
  function cadastrarUsuario(event) {
    event.preventDefault()
    console.log(`Usuário ${name} foi cadastrado com a senha: ${password}`)
  }

  const [name, setName] = useState()
  const [password, setPassword] = useState()

  return (
    <div>
      <h1 className={styles.formCabecalho}>Meu cadastro:</h1>
      {/* a função é chamada sem parenteses pois com, chamaria assim que o componente é criado */}
      <form onSubmit={cadastrarUsuario}>
        <div>
          <label htmlFor="name">Nome:</label>
          <input 
            type="text"
            id="name"
            name="name"
            placeholder="Digite o seu nome"
            onChange={(e) => setName(e.target.value)}
            />
        </div>

        <div>
          <label htmlFor="password">Senha:</label>
          <input
            type="password"
            id="password"
            name="password"
            placeholder="Digite a sua senha"
            onChange={(e) => setPassword(e.target.value)}
            />
        </div>

        <div>
          <input className={styles.formBotao} type="submit" value="Cadastrar" />
        </div>
      </form>
    </div>
  )
}
