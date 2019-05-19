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
        public bool FullHand { get; private set; }
        private int handLimit = 5;
        public PlayerType Type { get; }
        public bool Showdown { get; set; }
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

            if (Type == PlayerType.User || Showdown == true)
            {
                output += $"{Name} has {Score} points:\r\n";
                foreach (Card card in Hand)
                {
                    output += $"\t{card}\r\n";
                }
            }
            else
            {
                output += $"{Name} has {Hand.Count} cards:\r\n";
                foreach (Card card in Hand)
                {
                    output += $"\t##### of #####\r\n";
                }
            }

            return output;
        }

        public Player(string name, PlayerType type)
        {
            Name = name;
            Type = type;
            Fold();
            FullHand = false;
        }

        public void RecieveCard(Card card)
        {
            if (Hand.Count <= handLimit)
            {
                Hand.Add(card);
                FullHand = false;
            }
            else
                FullHand = true;
        }

        public void Fold()
        {
            Hand = new List<Card>();
        }
    }
}
