using System;
using System.Collections.Generic;
using System.Linq;
using Colecoes.Helper;

namespace Colecoes
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrayNumeros = new int[10] { 100, 1, 4, 0, 8, 15, 19, 19, 4, 100 };

            var minimo = arrayNumeros.Min();
            var maximo = arrayNumeros.Max();
            var medio = arrayNumeros.Average();
            var soma = arrayNumeros.Sum();
            var arrayUnico = arrayNumeros.Distinct().ToArray();

            System.Console.WriteLine($"Mínimo: {minimo}");
            System.Console.WriteLine($"Máximo: {maximo}");
            System.Console.WriteLine($"Médio: {medio}");
            System.Console.WriteLine($"Soma: {soma}");
            System.Console.WriteLine($"Array original: {string.Join(", ", arrayNumeros)}");
            System.Console.WriteLine($"Array distinto: {string.Join(", ", arrayUnico)}");

            // var numerosPares = 
            //     from num in arrayNumeros
            //     where num % 2 == 0
            //     orderby num
            //     select num;

            // var numerosParesMetodo = arrayNumeros.Where(x => x % 2 == 0).OrderBy(x => x).ToList();

            // System.Console.WriteLine("Números pares query: " + string.Join(", ", numerosPares));
            // System.Console.WriteLine("Números pares método: " + string.Join(", ", numerosParesMetodo));

            // Dictionary<string, string> estados = new Dictionary<string, string>();

            // //adicionar (NÃO pode repetir chaves)
            // estados.Add("SP", "São Paulo");
            // estados.Add("MG", "Minas Gerais");
            // estados.Add("BA", "Bahia");

            // //percorrer o dicionário
            // foreach (KeyValuePair<string, string> item in estados)
            // {
            //     //System.Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            // }

            // //valor original
            // string valorProcurado = "BA";

            // //tentar acessar diretamente um estado que não existe, o programa quebra
            // //var teste = estados["SC"];

            // //mesmo que não exista o valor, dessa forma não deixa o programa quebrar
            // if (estados.TryGetValue(valorProcurado, out string estadoEncontrado))
            // {
            //     System.Console.WriteLine(estadoEncontrado);
            // } else {
            //     System.Console.WriteLine($"Chave {valorProcurado} não existe no dicionário");
            // }

            // System.Console.WriteLine($"Removendo o valor: {valorProcurado}");

            // //para remover basta informarmos a chave desejada
            // estados.Remove(valorProcurado);

            // foreach (KeyValuePair<string, string> item in estados)
            // {
            //     System.Console.WriteLine($"Chave: {item.Key}, Valor: {item.Value}");
            // }

            // System.Console.WriteLine($"Valor original");
            // System.Console.WriteLine(estados[valorProcurado]);

            // estados[valorProcurado] = "BA - Teste Atualização";
            // System.Console.WriteLine($"Valor atualizado");
            // System.Console.WriteLine(estados[valorProcurado]);

            // Stack<string> pilhaLivros = new Stack<string>();

            // //insere o objeto no topo da pilha
            // pilhaLivros.Push(".NET");
            // pilhaLivros.Push("DDD");
            // pilhaLivros.Push("Código Limpo");

            // System.Console.WriteLine($"Livros para a leitura: {pilhaLivros.Count}");

            // //Leitura da pilha
            // while(pilhaLivros.Count > 0) //enquanto houver elementos
            // {
            //      //exibe o objeto no topo da pilha, mas sem removê-lo
            //     System.Console.WriteLine($"Próximo livro para leitura: {pilhaLivros.Peek()}");

            //     //obtem o livro no topo da pilha e o remove da pilha
            //     System.Console.WriteLine($"{pilhaLivros.Pop()} lido com sucesso!");
            // }

            // System.Console.WriteLine($"Livros para a leitura: {pilhaLivros.Count}");

            // Queue<string> fila = new Queue<string>();

            // //Enqueue() adiciona elemento à fila
            // fila.Enqueue("Rodrigo");
            // fila.Enqueue("Fulano");
            // fila.Enqueue("Ciclano");

            // System.Console.WriteLine($"Pessoas na fila: {fila.Count}");

            // //while para percorrermos e remover elementos da fila
            // while (fila.Count > 0)
            // {
            //     //Peek() pega o primeiro elemento da fila (o que está na frente da fila)
            //     System.Console.WriteLine($"Vez de: {fila.Peek()}");

            //     //Dequeue() mostra e remove o elemento da fila (o que está na frente da fila)
            //     System.Console.WriteLine($"{fila.Dequeue()} atendido!");
            // }

            // System.Console.WriteLine($"Pessoas na fila: {fila.Count}");

            // OperacoesLista opLista = new OperacoesLista();
            // List<string> estados = new List<string> { "SP", "MG", "BA" };
            // string[] estadosArray = new string[2] { "SC", "MT" };


            // System.Console.WriteLine($"Quantidade de elementos na lista: {estados.Count}\n");

            // opLista.ImprimirListaString(estados);

            // // estados.Remove("MG");
            // // System.Console.WriteLine("Removendo o elemento");

            // // estados.AddRange(estadosArray);
            // estados.Insert(1, "RJ");
            // System.Console.WriteLine("Adicionado novos valores");

            // opLista.ImprimirListaString(estados);

            // OperacoesArray op = new OperacoesArray();

            // int[] array = new int[5] { 6, 3, 8, 1, 9};
            // int[] arrayCopia = new int[10];
            // string[] arrayString = op.ConverterParaArrayString(array);

            // var valorProcurado = 10;

            // System.Console.WriteLine($"Capacidade atual do array: {array.Length}");

            // //vamos dobrar o tamanho do array
            // op.RedimencionarArray(ref array, array.Length * 2);

            // System.Console.WriteLine($"Capacidade atual do array após redimencionar: {array.Length}");

            // int indice = op.ObterIndice(array, valorProcurado);
            // //é retornado -1, pois 0 poderia ser uma posição no array
            // if (indice > -1)
            // {
            //     System.Console.WriteLine("O índice do elemento {0} é: {1}", valorProcurado, indice);
            // } else {
            //     System.Console.WriteLine("Valor não existente no array");
            // }

                            //se ObterValor não encontrar o valor, será retornado 0 (zero), que é o valo padrão de int
            // int valorAchado = op.ObterValor(array, valorProcurado);

            // //se na condição não achar o valor procurado, é retornado o valor padrão do tipo, que o caso é 0 (zero)
            // if (valorAchado > 0)
            // {
            //     System.Console.WriteLine("Encontrei o valor");
            // } else {
            //     System.Console.WriteLine("Não encontrei o valor");
            // }

            // bool todosMaiorQue = op.TodosMaiorQue(array, valorProcurado);

            // if (todosMaiorQue)
            // {
            //     System.Console.WriteLine("Todos os valores são maior que {0}", valorProcurado);
            // } else {
            //     System.Console.WriteLine("Existe valores que não são maiores do que {0}", valorProcurado);
            // }

            // bool existe = op.Existe(array, valorProcurado);
            // if (existe)
            // {
            //     System.Console.WriteLine("Encontrei o valor: {0}", valorProcurado);
            // } else {
            //     System.Console.WriteLine("Não encontrei o valor: {0}", valorProcurado);
            // }
            
            // System.Console.WriteLine("Array original");
            // op.ImprimirArray(array);


            //op.OrdenarBubbleSort(ref array);
            // op.Ordenar(ref array);

            // System.Console.WriteLine("Array ordenado");

            // System.Console.WriteLine("Array antes da cópia:");
            // op.ImprimirArray(arrayCopia);

            // op.Copiar(ref array, ref arrayCopia);
            // System.Console.WriteLine("Array após a cópia");
            // op.ImprimirArray(arrayCopia);



            // //array multidimensional com 4 linhas e 2 colunas
            // int[,] matriz = new int[4,2]
            // {
            //     { 8, 8 },//linha e coluna respectivamente
            //     { 10, 20 },
            //     { 50, 100 },
            //     { 90, 200 },
            // };

            // //for para percorrer as linhas
            // for (int i = 0; i < matriz.GetLength(0); i++)
            // {
            //     //for para percorrer as colunas
            //     for (int j = 0; j < matriz.GetLength(1); j++)
            //     {
            //         System.Console.WriteLine(matriz[i, j]);
            //     }
            // }

            // int[] arrayInteiros = new int[3];
            
            // arrayInteiros[0] = 10;
            // arrayInteiros[1] = 20;
            // arrayInteiros[2] = int.Parse("30");
            // arrayInteiros[3] = 40;

            // //percorrer array com for
            // System.Console.WriteLine("Percorrendo o array pelo for");
            // for (int i = 0; i <arrayInteiros.Length; i++)
            // {
            //     System.Console.WriteLine(arrayInteiros[i]);
            // }

            // //percorrer array com foreach
            // System.Console.WriteLine("Percorrendo o array pelo foreach");
            // foreach (var item in arrayInteiros)
            // {
            //     System.Console.WriteLine(item);
            // }
        }
    }
}
