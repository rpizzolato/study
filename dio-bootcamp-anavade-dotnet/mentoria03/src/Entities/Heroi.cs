using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entities
{
    public class Heroi
    {
        public Heroi(string Nome, string ClasseFantastica)
        {
            this.Nome = Nome;
            this.ClasseFantastica = ClasseFantastica;
            this.Nivel = 1;
            this.PontosDeVida = 50;
            this.PontosDeMagia = 50;
        }

        public string Nome { get; set; }
        public int Nivel { get; set; }
        public int PontosDeVida { get; set; }
        public int PontosDeMagia { get; set; }
        public string ClasseFantastica { get; set; }
    }
}