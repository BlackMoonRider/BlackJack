using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Player
    {
        Random random;
        public string Name { get; set; }
        public List<Card> Hand { get; private set; }
        public bool FullHand { get; private set; }
        private int handLimit = 5;
        public PlayerType Type { get; }
        public bool Showdown { get; set; }
        public bool NeedAnotherCard
        {
            get
            {
                DecisionMaker decisionMaker = new DecisionMaker(Points, random);
                return decisionMaker.MakeDecision();
            }
        }
        public int Points
        {
            get
            {
                int result = 0;
                List<Card> hand = new List<Card>(Hand);
                List<Card> aces = new List<Card>();

                for (int i = 0; i < hand.Count; i++)
                {
                    if (hand[i].Value == Value.Ace)
                    {
                        aces.Add(hand[i]);
                        hand.RemoveAt(i);
                    }
                }

                foreach (Card card in hand)
                {
                    result += card; 
                }

                foreach (Card card in aces)
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
                output += $"{Name} has {Points} points:\r\n";
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

            // Debug purposes
            output += NeedAnotherCard ? "I need another card." : "That's enough";

            return output;
        }

        public Player(string name, PlayerType type, Random random)
        {
            Name = name;
            Type = type;
            this.random = random;
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
