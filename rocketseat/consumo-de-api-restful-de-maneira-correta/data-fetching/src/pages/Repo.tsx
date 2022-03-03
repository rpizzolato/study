import { useQueryClient } from "react-query"
import { useParams } from "react-router-dom"
import { Repository } from "./Repos"

export function Repo() {
  const params = useParams()
  const currentRepository = params['*'] as string

  const queryClient = useQueryClient()

  //caso precise atualizar os dados, essa função invalida o staleTime
  async function handleChangeRepositoryDescription() {
    // chamada API para atualizar a descrição do repositório

    const previousRepos = queryClient.getQueryData<Repository[]>('repos')


    //aqui dentro conseguimos atualizar o valor da descrição mudando apenas no cache
    if (previousRepos) {
      const nextRepos = previousRepos.map(repo => {
        if (repo.full_name === currentRepository) {
          return { ...repo, description: 'Testando'}
        } else {
          return repo
        }
      })

      queryClient.setQueriesData('repos', nextRepos)
    }
  }
  
  return (
    <div>
      <h1>{currentRepository}</h1>
      <button onClick={handleChangeRepositoryDescription}>Alterar</button>
    </div>
  )
}