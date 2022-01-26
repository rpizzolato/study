using System;
using System.Collections.Generic;
using System.IO;
using ExemploPOO.Helper;
using ExemploPOO.Interfaces;
using ExemploPOO.Models;

namespace ExemploPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            var caminho = "C:\\TrabalhandoComArquivos";
            var caminhoPathCombine = Path.Combine(caminho, "PastaTeste 1");
            var caminhoArquivo = Path.Combine(caminho, "arquivo-teste-stream.txt");
            var caminhoArquivoTeste = Path.Combine(caminho, "arquivo-teste.txt");
            var caminhoArquivoCopia = Path.Combine(caminho, "arquivo-teste-backup.txt");
            var NovoCaminhoArquivo = Path.Combine(caminho, "Pasta Teste 2", "arquivo-teste-stream.txt");

            var listaString = new List<string> { "Linha 1", "Linha 2", "Linha 3" };
            var listaStringContinuacao = new List<string> { "Linha 4", "Linha 5", "Linha 6" };

            FileHelper helper = new FileHelper();

            helper.DeletarArquivo(caminhoArquivoCopia);

            // helper.CopiarArquivo(caminhoArquivoTeste, caminhoArquivoCopia, false);

            // helper.MoverArquivo(caminhoArquivo, NovoCaminhoArquivo, false);

            // helper.LerArquivoStream(caminhoArquivo);

            // helper.CriarArquivoTextoStream(caminhoArquivo, listaString);
            // helper.AdicionarTextoStream(caminhoArquivo, listaStringContinuacao);

            // helper.CriarArquivoTexto(caminhoArquivo, "Olá, teste de escrita de arquivo");

            // helper.ApagarDiretorio(caminhoPathCombine, true);

            // System.Console.WriteLine("Criando o diretório: " + caminhoPathCombine);
            // helper.CriarDiretorio(caminhoPathCombine);
            
            // helper.ListarArquivosDiretorios(caminho);           
            
            // helper.ListarDiretorios(caminho);
            // ICalculadora calc = new Calculadora();
            // System.Console.WriteLine(calc.Somar(1,1));

            // Computador comp = new Computador();
            // System.Console.WriteLine(comp.ToString());
            // Corrente conta = new Corrente();
            // conta.Creditar(100);

            // conta.ExibirSaldo();
            // Retangulo r = new Retangulo();
            // r.DefinirMedidas(30, 30);
            // System.Console.WriteLine($"Área: {r.ObterArea()}");

            // Aluno aluno = new Aluno();
            // Professor professor = new Professor();

            // aluno.Nome = "Rodrigo";
            // aluno.Nota = 10;

            // professor.Nome = "Bla";
            // professor.Salario = 1234.43;

            // aluno.Apresentar();
            // professor.Apresentar();


        }
    }
}
