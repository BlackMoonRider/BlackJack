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
            players.Add(new Player("Computer", PlayerType.Computer, random));
            players.Add(new Player(playerName, PlayerType.User, random));
            stock = GetNewDeck();
        }

        public void UpdateRoundCounter()
        {
            round++;
        }

        public void ResetRoundData()
        {
            bothLost = false;
        }

        public void ResetGameData()
        {
            ResetRoundData();
            stock = GetNewDeck();
            round = 0;
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
            string separator = "------------------------------------------------\r\n";

            string header = "-= Black Jack =-";
            if (option == UpdateScreenOptions.EndOfRound)
            {
                if (bothLost)
                    header = "   Both lost!   ";
                else
                    header = winners.Count > 1 ? "  It's a tie!!  " : $" {winners[0].Name} wins! ";
            }

            if (header.Length == 14)
                header = $" {header} ";

            output += "################################################\r\n";
            output += $"############### {header} ###############\r\n";
            output += "################################################\r\n";
            output += Environment.NewLine;

            output += $"| Round: {round} | ";

            foreach (Player player in players)
            {
                output += $"{player.Name}: {player.Score} | ";
            }

            output += $"Ties: {tie} |";

            foreach (Player player in players)
            {
                output += Environment.NewLine;
                output += Environment.NewLine;
                output += separator;
                output += Environment.NewLine;
                output += $"{player.Print(option)}";
            }

            output += Environment.NewLine;
            output += Environment.NewLine;
            output += separator;
            output += Environment.NewLine;
            output += Environment.NewLine;

            //bool isEnough = false;
            //foreach (Player player in players)
            //{
            //    if (player.Type == PlayerType.User)
            //        isEnough = player.IsFullHand;
            //}

            //if (isEnough)
            //    output += "OPTIONS: [ENTER]: NEXT TURN";

            //else {
            //}

            switch (option)
            {
                case UpdateScreenOptions.InGame:
                    output += "OPTIONS: [A] or [ENTER]: ASK CARD | [E]: ENOUGH CARDS";
                    break;
                //case UpdateScreenOptions.EnoughCards:
                //    output += "OPTIONS: [ENTER]: NEXT TURN";
                //    break;
                case UpdateScreenOptions.EndOfRound:
                    output += "OPTIONS: [N] or [ENTER]: NEXT ROUND | [R]: RESTART GAME | [X]: EXIT GAME";
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

        public static UserResponse ValidateUserInput(string parameter, UpdateScreenOptions options)
        {
            string p = parameter.ToLower();
            p.Trim();

            UserResponse userResponse = new UserResponse(true);

            switch (parameter)
            {
                case "":
                    if (options == UpdateScreenOptions.InGame) userResponse = new UserResponse(true, false, false, false);
                    if (options == UpdateScreenOptions.EndOfRound) userResponse = new UserResponse(false, false, false, true);
                    break;
                case "a":
                    if (options == UpdateScreenOptions.InGame) userResponse = new UserResponse(true, false, false, false);
                    else userResponse = new UserResponse(true);
                    break;
                case "e":
                    if (options == UpdateScreenOptions.InGame) userResponse = new UserResponse(false, false, false, false);
                    else userResponse = new UserResponse(true);
                    break;
                case "r":
                    if (options == UpdateScreenOptions.EndOfRound) userResponse = new UserResponse(false, true, false, false);
                    else userResponse = new UserResponse(true);
                    break;
                case "x":
                    if (options == UpdateScreenOptions.EndOfRound) userResponse = new UserResponse(false, false, true, false);
                    else userResponse = new UserResponse(true);
                    break;
                case "n":
                    if (options == UpdateScreenOptions.EndOfRound) userResponse = new UserResponse(false, false, false, true);
                    else userResponse = new UserResponse(true);
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

        private Deck GetNewDeck()
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
            if (winners.Count == 1)
            {
                winners[0].Score++;
            }
        }

        public void UpdateStockIfNeeded()
        {
            if (stock.Cards.Count < 10)
                stock = GetNewDeck();
        }
        

    }
}
