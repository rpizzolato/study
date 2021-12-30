using System;
using src.Entities;

namespace mentoria03
{
    class Program
    {
        static void Main(string[] args)
        {
            Heroi arus = new Heroi("Arus", "Mago");
            arus.PontosDeVida = 100;
            arus.PontosDeMagia = 5;

            Console.WriteLine("O nome do herói é: " + arus.Nome);
        }
    }
}
