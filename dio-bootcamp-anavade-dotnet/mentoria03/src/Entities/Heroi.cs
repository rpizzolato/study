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

        public int VAlorUltimoAtaque { get; set; }

        public override string ToString()
        {
            return "Meu nome é " + this.Nome + "\n"
                + "Nível: " + this.Nivel + "\n"
                + "Classe: " + this.ClasseFantastica + "\n"
                + "Ponto de vida: " + this.PontosDeVida + "\n"
                + "Ponto de magia: " + this.PontosDeMagia + "\n";
        }

        public virtual string Atacar() {
            Random dado = new Random();
            int forcaDoAtaque = this.Nivel + dado.Next(1,20);
            this.VAlorUltimoAtaque = forcaDoAtaque;
            return this.Nome + " Ataca com a sua espada e dá " +
                    forcaDoAtaque + " de dano.";
        }

        public void ReceberDano(int danoRecebido) {
            this.PontosDeVida -= danoRecebido;
        }
    }
}