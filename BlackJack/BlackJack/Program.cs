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
            Random random = new Random();

            Game game = new Game("Player", random);

            while (true)
            {
                //game.PrintRoundNumber();
                //Console.ReadKey();

                game.MakeComputerDecision();
                game.UpdateGameScreen();
                game.PlayUser(Game.GetUserResponse(Console.ReadLine()));
                game.PlayComputer();
                game.UpdateGameScreen();
                game.EndRound();
                //game.PrintWinners();
                //Console.ReadKey();
                game.ResetPlayers();
            }

            Console.ReadKey();

            Card card1 = new Card(Value.Jack, Suit.Clubs);
            Card card2 = new Card(Value.Queen, Suit.Clubs);
            Card card3 = new Card(Value.Ace, Suit.Clubs);
            Console.WriteLine(card1 + card2 + card3);

            Deck deck = new Deck(random);
            foreach (Card card in deck.Cards)
            {
                Console.Write($"{card} | ");
            }

            Console.WriteLine(Environment.NewLine);


            Player player = new Player("Test Player", PlayerType.User, random);
            player.RecieveCard(deck.Deal());
            Console.WriteLine(player);
            Console.WriteLine(Environment.NewLine);

            player.RecieveCard(deck.Deal());
            Console.WriteLine(player);
            Console.WriteLine(Environment.NewLine);

            player.RecieveCard(deck.Deal());
            Console.WriteLine(player);
            Console.WriteLine(Environment.NewLine);

            player.RecieveCard(deck.Deal());
            Console.WriteLine(player);
            Console.WriteLine(Environment.NewLine);

            player.RecieveCard(deck.Deal());
            Console.WriteLine(player);
            Console.WriteLine(Environment.NewLine);

            foreach (Card card in deck.Cards)
            {
                Console.Write($"{card} | ");
            }

            Console.ReadKey();
        }
    }
}
