import { useFetch } from './hooks/useFetch'

type Repository = {
  full_name: string;
  description: string;
}

function App() {
  //é legal renomear data para algo mais próximo da realidade do contexto, no caso aqui 'repositories'
  const { data: repositories, isFetching } = 
    useFetch<Repository[]>('/users/rpizzolato/repos')

  return (
    <ul>
      { isFetching && <p>Carregando...</p>}
      {repositories?.map(repo => {
        return (
          <li key={repo.full_name}>
            <strong>{repo.full_name}</strong>
            <p>{repo.description}</p>
          </li>
        )
      })}
    </ul>
  )
}

export default App
