using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Card card1 = new Card(Value.Jack, Suit.Clubs);
            Card card2 = new Card(Value.Queen, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Clubs);
            Console.WriteLine(card1 + card2 + card3);

            Deck deck = new Deck();
            foreach (Card card in deck.Cards)
            {
                Console.WriteLine(card);
            }



            Console.ReadKey();
        }
    }
}
