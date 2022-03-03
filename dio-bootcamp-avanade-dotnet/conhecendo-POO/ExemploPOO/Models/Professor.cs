using System;

namespace ExemploPOO.Models
{
    public class Professor : Pessoa
    {
        public double Salario { get; set; }

        public override void Apresentar() {
            Console.WriteLine($"Olá, meu nome é {Nome}. Sou um professor, ganho salário de {Salario}");
        }
    }
}