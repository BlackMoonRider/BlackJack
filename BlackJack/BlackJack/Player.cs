using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    abstract class Player
    {
        public string Name { get; private set; }
        public List<Card> Hand { get; set; }
        public int Score
        {
            get
            {
                int sum = 0;

                foreach (Card card in Hand)
                {
                    sum += card;
                }

                return sum;
            }
        }
        public bool StopDeal { get; set; }

        public Player()
        {
            Hand = new List<Card>();
        }

        public void CheckStopDeal()
        {
            if (Score >= 21)
                StopDeal = true;
        }

        public void Fold()
        {
            Hand.Clear();
        }

        abstract public void GetDecision();
    }
}
