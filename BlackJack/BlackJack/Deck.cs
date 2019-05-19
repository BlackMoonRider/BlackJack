using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck
    {
        public List<Card> Cards;
        private Random random = new Random();
        public bool DeckRunsOut = false;
        private int deckRunsOutLimit = 13;

        public Deck()
        {
            Cards = new List<Card>();

            for (int suit = 0; suit <= 3; suit++)
                for (int value = 2; value <= 14; value++)
                    Cards.Add(new Card((Value)value, (Suit)suit));

            Cards = Shuffle(Cards);
        }

        private List<Card> Shuffle(List<Card> cards)
        {
            List<Card> initial = cards;
            List<Card> shuffled = new List<Card>();

            while (initial.Count > 0)
            {
                int removeIndex = random.Next(initial.Count);
                shuffled.Add(initial[removeIndex]);
                initial.RemoveAt(removeIndex);
            }

            return shuffled;
        }

        public Card Deal()
        {
            Card returnCard = Cards[0];
            Cards.RemoveAt(0);
            DeckRunsOut = Cards.Count < deckRunsOutLimit ? true : false;
            return returnCard;
        }
    }
}
