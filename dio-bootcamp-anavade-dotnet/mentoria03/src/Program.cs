using System;
using src.Entities;

namespace mentoria03
{
    class Program
    {
        static void Main(string[] args)
        {
            Heroi arus = new Heroi();
            arus.Nome = "Arus";
            arus.PontosDeVida = 100;
            arus.PontosDeMagia = 5;
            Console.WriteLine("Hello World");
        }
    }
}
