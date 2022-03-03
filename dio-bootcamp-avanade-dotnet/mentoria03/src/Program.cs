using System;
using src.Entities;

namespace mentoria03
{
    class Program
    {
        static void Main(string[] args)
        {
            Heroi arus = new Heroi("Arus", "Guerreiro");
            Mago wedge = new Mago("Wedge", "Mago");
            Inimigo kingMummy = new Inimigo("kingMummy", "Zumbi");

            Console.WriteLine("Arus " + arus.Atacar());
            Console.WriteLine("King Mummy " + kingMummy.Atacar() + "\n");



            if (arus.VAlorUltimoAtaque == kingMummy.VAlorUltimoAtaque)
            {
                Console.WriteLine("Empate, ambos deram dano de " + arus.VAlorUltimoAtaque);
            } else if (arus.VAlorUltimoAtaque > kingMummy.VAlorUltimoAtaque)
            {
                kingMummy.ReceberDano(arus.VAlorUltimoAtaque - kingMummy.VAlorUltimoAtaque);
                Console.WriteLine(arus.Nome + " venceu esse round");
            } else {
                arus.ReceberDano(kingMummy.VAlorUltimoAtaque - arus.VAlorUltimoAtaque);
                Console.WriteLine(kingMummy.Nome + " venceu esse round");
            }
        }
    }
}
