import axios from 'axios'
import { useQuery } from 'react-query'
import { Link } from 'react-router-dom'

export type Repository = {
  full_name: string;
  description: string;
}

export function Repos() {
  const { data, isFetching } = useQuery<Repository[]>('repos', async () => {
    const response = await axios.get('https://api.github.com/users/rpizzolato/repos')

    return response.data
  }, {
    //desabilita buscar novos dados onfocus
    refetchOnWindowFocus: false,
    //tempo para armazenar a requisição em cache, sem refazer nova consulta
    staleTime: 1000 * 60, //1 minuto
  })

  return (
    <ul>
      { isFetching && <p>Carregando...</p>}
      {data?.map(repo => {
        return (
          <li key={repo.full_name}>
            <Link to={`repos/${repo.full_name}`}>{repo.full_name}</Link>
            <p>{repo.description}</p>
          </li>
        )
      })}
    </ul>
  )
}