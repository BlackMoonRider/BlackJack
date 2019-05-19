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

        public static int operator +(Card card1, Card card2)
        {
            int result;

            int card1Value = (int)card1.Value > 11 ? 10 : (int)card1.Value;
            int card2Value = (int)card2.Value > 11 ? 10 : (int)card2.Value;

            result = card1Value + card2Value;

            if (result > 21 && card1.Value == Value.Ace)
                result -= 10;
            if (result > 21 && card2.Value == Value.Ace)
                result -= 10;

            return result;
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
