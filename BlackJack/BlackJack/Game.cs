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
        List<Player> winners;
        Random random;
        int round = 0;
        int tie = 0;
        bool bothLost = false;

        public Game(string playerName, Random random)
        {
            this.players = new List<Player>();
            this.random = random;
            players.Add(new Player("Bender", PlayerType.Computer, random));
            players.Add(new Player(playerName, PlayerType.User, random));
            stock = GetNewDeck(random);
        }

        public void ResetPlayers()
        {
            foreach (Player player in players)
            {
                player.Fold();
                player.Ready = false;
            }
        }

        public void DealInitialCards()
        {
            foreach (Player player in players)
            {
                player.Hand.Add(stock.Deal());
                player.Hand.Add(stock.Deal());
            }
        }

        public void UpdateGameScreen(UpdateScreenOptions option)
        {
            Console.Clear();
            string output = String.Empty;

            output += "##############################################\r\n";
            output += "############## -= Black Jack =- ##############\r\n";
            output += "##############################################\r\n";
            output += Environment.NewLine;

            if (option == UpdateScreenOptions.EndOfRound)
            {
                string winner;
                if (bothLost)
                    winner = "Both lost!\r\n\r\n";
                else
                    winner = winners.Count > 1 ? "It's a tie!\r\n\r\n" : $"{winners[0].Name} wins!\r\n\r\n";

                output += "\t\t" + winner;
            }

            output += $"| Round: {round} | ";


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
                output += $"{player.Print(option)}";
            }

            output += Environment.NewLine;
            output += Environment.NewLine;
            output += "----------------------------------------------\r\n";
            output += Environment.NewLine;
            output += Environment.NewLine;

            switch (option)
            {
                case UpdateScreenOptions.InGame:
                    output += "OPTIONS: [A] or [ENTER]: ASK ANOTHER CARD | [E]: ENOUGH CARDS";
                    break;
                case UpdateScreenOptions.EndOfRound:
                    output += "OPTIONS: | [N] or [ENTER]: NEXT ROUND | [R]: RESTART GAME | [X]: EXIT GAME |"; 
                    break;
                default:
                    break;
            }

            Console.WriteLine(output);
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

        public static UserResponse ValidateUserInput(string parameter)
        {
            string p = parameter.ToLower();
            p.Trim();

            UserResponse userResponse;

            switch (parameter)
            {
                case "":
                case "a":
                    userResponse = new UserResponse(true, false, false, false);
                    break;
                case "e":
                    userResponse = new UserResponse(false, false, false, false);
                    break;
                case "r":
                    userResponse = new UserResponse(false, true, false, false);
                    break;
                case "x":
                    userResponse = new UserResponse(false, false, true, false);
                    break;
                case "n":
                    userResponse = new UserResponse(false, false, false, true);
                    break;
                default:
                    userResponse = new UserResponse(true);
                    break;
            }

            return userResponse;
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

        public bool ContinueRound()
        {
            foreach (Player player in players)
            {
                if (!player.IsFullHand)
                    return true;
            }
            return false;
        }


        private Deck GetNewDeck(Random random)
        {
            return new Deck(random);
        }




        public void GetWinner()
        {
            int maxPoints = 0;
            winners = new List<Player>();

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
            {
                tie++;
            }
            if (winners.Count == 0)
            {
                bothLost = true;
            }
            else
            {
                winners[0].Score++;
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


            if (stock.Cards.Count < 15)
                stock = GetNewDeck(random);

            PrintWinners();
        }

        public void PrintWinners()
        {
            UpdateGameScreen(UpdateScreenOptions.EndOfRound);

            string output = String.Empty;
            if (winners.Count == 1)
                output += $"Winner: {winners[0].Name}";
            else
                output += "It's a draw!";

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
