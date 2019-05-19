using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Game
    {
        private List<Player> players;
        private Deck stock;
        List<Player> winners = new List<Player>();
        Random random;
        int round = 0;
        int tie = 0;

        public Game(string playerName, Random random)
        {
            this.players = new List<Player>();
            this.random = random;
            players.Add(new Player("Bender", PlayerType.Computer, random));
            players.Add(new Player(playerName, PlayerType.User, random));
            stock = GetNewDeck(random);
            DealInitialCards();

        }

        private Deck GetNewDeck(Random random)
        {
            return new Deck(random);
        }

        private void DealInitialCards()
        {
            foreach (Player player in players)
            {
                player.Hand.Add(stock.Deal());
                player.Hand.Add(stock.Deal());
            }
        }

        public void MakeComputerDecision()
        {
            foreach (Player player in players)
            {
                if (player.Type == PlayerType.Computer)
                {
                    player.MakeDecision();
                }
            }
        }

        public void PlayComputer()
        {
            foreach (Player player in players)
            {
                if (player.Type == PlayerType.Computer)
                {
                    if (player.IsAnotherCardNeeded)
                        player.Hand.Add(stock.Deal());
                }
            }
        }

        public static UserResponse GetUserResponse(string parameter)
        {
            string output = String.Empty;
            string p = parameter.ToLower();
            p.Trim();

            switch (parameter)
            {
                case "":
                case "a":
                    string ma = "Player asks for another card";
                    return new UserResponse(ma, true, false, false, false);
                case "e":
                    string me = "Player doesn't need an extra card";
                    return new UserResponse(me, false, false, false, false);
                case "r":
                    string mr = "Player wants to restart the game";
                    return new UserResponse(mr, false, true, false, false);
                case "x":
                    string mx = "Player wants to exit the game";
                    return new UserResponse(mx, false, false, true, false);
                default:
                    string mi = "The parameter is invalid";
                    return new UserResponse(mi, false, false, false, true);
            }
        }

        public void PlayUser(UserResponse response)
        {
            if (response.IsAskingForCard)
                foreach (Player player in players)
                {
                    if (player.Type == PlayerType.User && !player.IsFullHand)
                    {
                        player.Hand.Add(stock.Deal());
                    }
                }

            if (!response.IsAskingForCard)
            {

                foreach (Player player in players)
                {
                    if (player.Type == PlayerType.User)
                    {
                        player.Ready = true;
                    }
                }
            }

            
        }

        public void EndRound()
        {
            foreach (Player player in players)
            {
                if (!player.IsFullHand)
                    return;
            }

            
            int maxPoints = 0;

            foreach (Player player in players)
            {
                if (player.Points == maxPoints)
                    winners.Add(player);
                if (player.Points > maxPoints && player.Points < 22)
                {
                    maxPoints = player.Points;
                    winners.Clear();
                    winners.Add(player);
                }
            }

            if (winners.Count > 1)
                tie++;
            else
                winners[0].Score++;


            if (stock.Cards.Count < 11)
                stock = GetNewDeck(random);

            PrintWinners();
        }

        public void PrintWinners()
        {
            UpdateGameScreen(open: true);

            string output = String.Empty;
            if (winners.Count == 1)
                output += $"Winner: {winners[0].Name}";
            else
                output += "It's a draw!";

            Console.WriteLine(output);
        }

        public void ResetPlayers()
        {
            foreach (Player player in players)
            {
                player.Fold();
                player.Ready = false;
            }

            DealInitialCards();

        }

        public void UpdateGameScreen(bool open = false)
        {
            Console.Clear();
            string output = String.Empty;

            output += "##############################################\r\n";
            output += "############## -= Black Jack =- ##############\r\n";
            output += "##############################################\r\n";
            output += Environment.NewLine;
            output += "\t| ";

            foreach (Player player in players)
            {
                output += $"{player.Name}: {player.Score} | ";
            }

            output += $"Tie: {tie} |";

            foreach (Player player in players)
            {
                output += Environment.NewLine;
                output += Environment.NewLine;
                output += "----------------------------------------------\r\n";
                output += Environment.NewLine;
                output += $"{player.Print(open)}";
            }

            output += Environment.NewLine;
            output += Environment.NewLine;
            output += "----------------------------------------------\r\n";
            output += Environment.NewLine;
            output += Environment.NewLine;
            output += "OPTIONS: [A] or [ENTER]: ASK ANOTHER CARD | [E]: ENOUGH CARDS | [R]: RESTART GAME | [X]: EXIT GAME";

            Console.WriteLine(output);
        }

        public void PrintRoundNumber()
        {
            round++;
            Console.WriteLine($"Round {round}. Get ready.");
        }

        public void PlayOneRound()
        {
            DealInitialCards();
        }
    }
}
