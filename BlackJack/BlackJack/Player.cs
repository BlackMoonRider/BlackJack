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
        public List<Card> Hand { get; set; }
        public PlayerType Type { get; }

        private bool isFullHand;
        public bool IsFullHand { get { return Hand.Count >= 5 || Points >= 21 || Ready; } set { isFullHand = value; } }
        private int handLimit = 5;

        public int Score { get; set; } = 0;
        // public bool IsShowdown { get; set; }
        public bool Ready = false;
        public bool IsAnotherCardNeeded { get; private set; }
        public void MakeDecision()
        {
            if (!Ready)
            {
                DecisionMaker decisionMaker = new DecisionMaker(Points, random);
                IsAnotherCardNeeded = decisionMaker.MakeDecision();
            }
            if (!IsAnotherCardNeeded)
                Ready = true;
        }
        public void Unlock() => Ready = false;
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

            if (Type == PlayerType.User || Type == PlayerType.Computer && IsAnotherCardNeeded == false)
            {
                output += $"{Name} has {Points} points:\r\n";
                foreach (Card card in Hand)
                {
                    output += $"\t{card}\r\n";
                }
            }
            if (Type == PlayerType.Computer && IsAnotherCardNeeded == true)
            {
                output += $"{Name} has {Hand.Count} cards:\r\n";
                foreach (Card card in Hand)
                {
                    output += $"\t##### of #####\r\n";
                }
            }

            // Debug purposes
            output += IsAnotherCardNeeded ? "I need another card." : "That's enough";

            return output;
        }

        public Player(string name, PlayerType type, Random random)
        {
            Name = name;
            Type = type;
            this.random = random;
            Fold();
            IsFullHand = false;
        }

        public void RecieveCard(Card card)
        {
            if (Hand.Count <= handLimit)
            {
                Hand.Add(card);
                IsFullHand = false;
            }
            else
                IsFullHand = true;
        }

        public void Fold()
        {
            Hand = new List<Card>();
        }
    }
}
