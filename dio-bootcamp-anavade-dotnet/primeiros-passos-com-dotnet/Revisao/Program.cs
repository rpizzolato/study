using System;

namespace Revisao
{
    class Program
    {
        static void Main(string[] args)
        {
            Aluno[] alunos = new Aluno[5];
            string opcaoUsuario = ObterOpcaoUsuario();
            int indiceAluno = 0;

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        Console.WriteLine("Informe o nome do aluno:");
                        Aluno aluno = new Aluno();
                        aluno.Nome = Console.ReadLine();

                        Console.WriteLine("Informe a nota do aluno:");
                        if (decimal.TryParse(Console.ReadLine(), out decimal nota)) {
                            aluno.Nota = nota;
                        } else {
                            throw new ArgumentException("Valor da nota deve ser decimal");
                        }

                        alunos[indiceAluno] = aluno;
                        indiceAluno++;
                        break;
                    case "2":
                        Console.WriteLine("Alunos cadastrados");
                        foreach(var a in alunos) {
                            if(!string.IsNullOrEmpty(a.Nome))
                                Console.WriteLine($"ALUNO: {a.Nome} - NOTA: {a.Nota}");
                        }
                        break;
                    case "3":
                        decimal notaTotal = 0;
                        int quantidadeAlunos = 0;

                        //poderia ser feito com for também.
                        foreach (var a in alunos)
                        {
                            if(!string.IsNullOrEmpty(a.Nome)){
                                notaTotal = notaTotal + a.Nota;                                
                                quantidadeAlunos++;
                            }
                        }

                        decimal media = notaTotal / quantidadeAlunos;

                        //conceito de emun
                        Conceito conceitoGeral;

                        if (media < 3) {
                            conceitoGeral = Conceito.C;
                        } else if (media < 7) {
                            conceitoGeral = Conceito.B;
                        } else {
                            conceitoGeral = Conceito.A;
                        }

                        System.Console.WriteLine($"Média Geral: {media} - Conceito {conceitoGeral}");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Opção inválida");
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1. Inserir novo aluno");
            Console.WriteLine("2. Listar alunos");
            Console.WriteLine("3. Calcular média geral");
            Console.WriteLine("X. Sair");
            Console.WriteLine();
            string opcaoUsuario = Console.ReadLine();
            return opcaoUsuario;
        }
    }
}
