using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    struct Card
    {
        public Value Value { get; }
        public Suit Suit { get; }

        public Card(Value value, Suit suit)
        {
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

        public static int operator +(int cardSum, Card card)
        {
            int result;

            int cardValue = (int)card.Value > 11 ? 10 : (int)card.Value;

            result = cardSum + cardValue;

            if (result > 21 && card.Value == Value.Ace)
                result -= 10;

            return result;
        }
    }
}
