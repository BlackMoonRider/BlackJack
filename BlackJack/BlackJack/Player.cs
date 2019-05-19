using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; private set; }
        public int Score
        {
            get
            {
                int result = 0;
                foreach (Card card in Hand)
                {
                    result += card; 
                }
                return result;
            }
        }

        public override string ToString()
        {
            string output = String.Empty;

            output += $"{Name} has {Score} points:\r\n";
            foreach (Card card in Hand)
            {
                output += $"\t{card}";
            }

            return output;
        }

        public Player(string name)
        {
            Name = name;
            Fold();
        }

        public void RecieveCard(Card card)
        {
            Hand.Add(card);
        }

        public void Fold()
        {
            Hand = new List<Card>();
        }
    }
}
