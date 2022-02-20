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