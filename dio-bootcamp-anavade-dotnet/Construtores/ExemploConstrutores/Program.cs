using System;
using ExemploConstrutores.Models;

namespace ExemploConstrutores
{
    class Program
    {
        public delegate void Operacao(int x, int y);

        static void Main(string[] args)
        {
            Matematica m = new Matematica(10, 20);
            m.Somar();

            //há duas maneiras de chamar o delegate
            //Operacao op = Calculadora.Somar;
            // Operacao op = new Operacao(Calculadora.Somar);

            // op += Calculadora.Subtrair;

            //duas formas de chamar o delegate também
            // op.Invoke(10, 10);
            //op(10, 10);

            // const double pi = 3.14;
            // System.Console.WriteLine(pi);

            // Data data = new Data();
            //data.setMes(2);
            // data.Mes = 22;
            // System.Console.WriteLine(data.Mes);
            // data.ApresentarMes();
            
            // Aluno al = new Aluno("Rodrigo", "Pizzolato", "Português");
            // al.Apresentar();

            // Log log = Log.GetInstance();
            // log.PropriedadeLog = "Teste instancia";

            // Log log2 = Log.GetInstance();
            // System.Console.WriteLine(log2.PropriedadeLog);

            // Pessoa p1 = new Pessoa("Rodrigo", "Pizzolato");
            // p1.Apresentar();
            
        }
    }
}
