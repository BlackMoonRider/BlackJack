using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck
    {
        private List<Card> cards;
        public List<Card> Cards
        {
            get
            {
                if (cards.Count < 10)
                    CreateDeck();
                return cards;
            }
            private set
            {
                cards = value;
            }
        }

        public Deck()
        {
            CreateDeck();
        }

        private void CreateDeck()
        {
            List<Card> newCards = new List<Card>();

            for (int suit = 0; suit <= 3; suit++)
                for (int value = 2; value <= 14; value++)
                    newCards.Add(new Card((Value)value, (Suit)suit));

            newCards = Shuffle(newCards, Utility.Random);

            Cards = newCards;
        }

        private List<Card> Shuffle(List<Card> cards, Random random)
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
            return returnCard;
        }
    }
}
