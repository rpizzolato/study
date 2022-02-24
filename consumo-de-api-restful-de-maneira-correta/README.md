# Consumindo APIs RESTFul no React de maneira certa

### Maneira mais básica (usando fetch nativo)
```ts
import { useEffect, useState } from 'react'


type Repository = {
  full_name: string;
  description: string;
}

function App() {
  const [repositories, setRepositories] = useState<Repository[]>([])

  useEffect(() => {
    fetch('https://api.github.com/users/rpizzolato/repos')
      .then(response => response.json())
      .then(data => {
        setRepositories(data);
      })
  }, [])

  return (
    <ul>
      {repositories.map(repo => {
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
```
>**Desvantagens**
>
>É muito verboso

### Utilizando o axios

Para instalar o axios, use `npm i axios`

```ts
import { useEffect, useState } from 'react'
import axios from 'axios'


type Repository = {
  full_name: string;
  description: string;
}

function App() {
  const [repositories, setRepositories] = useState<Repository[]>([])

  useEffect(() => {
    axios.get('https://api.github.com/users/rpizzolato/repos')
      .then(response => {
        setRepositories(response.data);
      })
  }, [])

  return (
    <ul>
      {repositories.map(repo => {
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
```

>**Vantagens**
>
>Apesar de não mudar muito, o axios já vem com algumas tratativas, de timeout por exemplo.

### Separando a chamada a API
- criaremos um arquivo em `src/hooks/useFetch.ts` (`useFetch.ts` pode ser qualquer outro nome, colocamos o prefixo `use` apenas por convenção e boas práticas)

>`useFetch.ts`
```ts
import axios from "axios";
import { useEffect, useState } from "react";

export function useFetch<T = unknown>(url: string) {
    const [data, setData] = useState<T | null>(null)

    useEffect(() => {
        axios.get(url)
            .then(response => {
                setData(response.data)
            })
    }, [])

    return { data }
}
```

>`App.tsx`
```ts
import { useFetch } from './hooks/useFetch'

type Repository = {
  full_name: string;
  description: string;
}

function App() {
  //é legal renomear data para algo mais próximo da realidade do contexto, no caso aqui 'repositories'
  const { data: repositories } = useFetch<Repository[]>('https://api.github.com/users/rpizzolato/repos')

  return (
    <ul>
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
```

### tratando a requisição (tela de loading/carregando...)

`useFetch.ts`
```ts
import axios, { AxiosRequestConfig } from "axios";
import { useEffect, useState } from "react";

const api = axios.create({
    baseURL: 'https://api.github.com'
})

export function useFetch<T = unknown>(url: string, options?: AxiosRequestConfig) {
    const [data, setData] = useState<T | null>(null)
    const [isFetching, setIsFetching] = useState(true)
    const [error, setError] = useState<Error | null>(null)

    useEffect(() => {
        api.get(url, options)
            .then(response => {
                //executa se está OK
                setData(response.data)
            })
            .catch(err => {
                //executa se der erro
                setError(err)
            })
            .finally(() => {
                //executa tanto se der erro ou se estiver OK
                setIsFetching(false)
            })
    }, [])

    return { data, error, isFetching }
}
```

`App.tsx`

```ts
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
```
## Explorando outras bibliotecas
### SWR (da VERCEL) e react query
- instalação do react query `npm i react-query`. O arquivo `hooks/useFetch.ts` não será mais necessário.
- criaremos `src/services/queryClient.ts`
```ts
import { QueryClient } from "react-query"

export const queryClient = new QueryClient
```
- Em `main.tsx`, precisamos colocar o `<App />` dentro de `<QueryClientProvider>`, dessa forma:
```ts
import React from 'react'
import ReactDOM from 'react-dom'
import { QueryClientProvider } from 'react-query'
import App from './App'
import { queryClient } from './services/queryClient'

ReactDOM.render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <App />
    </QueryClientProvider>
  </React.StrictMode>,
  document.getElementById('root')
)
```
- e por fim, no arquivo `App.tsx`:
```ts
import axios from 'axios'
import { useQuery } from 'react-query'

type Repository = {
  full_name: string;
  description: string;
}

function App() {
  const { data, isFetching } = useQuery<Repository[]>('repos', async () => {
    const response = await axios.get('https://api.github.com/users/rpizzolato/repos')

    return response.data
  })

  return (
    <ul>
      { isFetching && <p>Carregando...</p>}
      {data?.map(repo => {
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
```
>Dessa forma, ao mudarmos a descrição de algum repositório, o 'react-query' detecta a mudança e atualiza a tela quando a mesma recebe o focus do usuário. Basicamente a lib faz:
```ts
useEffect(() => {
    window.addEventListener('focus', () => {
        //refetching
    })
}, [])
```
>Mas utilizando a lib, não precisamos escrever esse código. Podemos **desabilitar** essa funcionalidade colocando:
```ts
function App() {
  const { data, isFetching } = useQuery<Repository[]>('repos', async () => {
    const response = await axios.get('https://api.github.com/users/rpizzolato/repos')

    return response.data
  }, {
        //setando para false
        refetchOnWindowFocus: false,
  })

  return (
    <ul>
      { isFetching && <p>Carregando...</p>}
      {data?.map(repo => {
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
```