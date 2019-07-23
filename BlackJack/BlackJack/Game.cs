using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Game
    {
        private readonly List<Player> players;
        private Deck deck;
        private Player winner;
        private bool tie;
        private ScreenRenderer screenRenderer;

        private int roundNumber;
        private int playerPoints;
        private int computerPoints;
        private int numberOfTies;
        private int numberOfBothLost;

        public Game(ScreenRenderer screenRenderer)
        {
            this.screenRenderer = screenRenderer;

            players = new List<Player>();
            players.Add(new PlayerUser());
            players.Add(new PlayerComputer());

            ResetGame();
        }

        public void ResetRound()
        {
            winner = null;
            tie = false;
            foreach (Player player in players)
            {
                player.Fold();
                player.StopDeal = false;
            }
        }

        private void ResetGame()
        {
            roundNumber = 0;
            playerPoints = 0;
            computerPoints = 0;
            numberOfTies = 0;
            numberOfBothLost = 0;

            deck = new Deck();

            ResetRound();
        }

        public void PlayOneStep()
        {
            roundNumber++;
            Settings.CurrentGameState = GameState.PlayingRound;
            Settings.CurrentPlayersDecision = PlayersDecision.AskCard;
            DealInitialCards();
            UpdateScreen();

            while (!StopDealingToEverybody())
            {
                foreach (Player player in players)
                {
                    player.CheckStopDeal();
                }

                if (players[0].StopDeal)
                {
                    Settings.CurrentGameState = GameState.PlayersChoiceIsMade;
                    UpdateScreen();
                }


                players[0].GetDecision();

                if (!players[1].StopDeal)
                    players[1].GetDecision();

                foreach (Player player in players)
                {
                    if (!player.StopDeal)
                        DealOneCard(player);
                }

                foreach (Player player in players)
                {
                    player.CheckStopDeal();
                }

                UpdateScreen();
            }

            Settings.CurrentGameState = GameState.RoundFinished;
            DetermineWinner();
            UpdateStatistics();
            UpdateScreen();
            players[0].GetDecision();

            if (Settings.CurrentPlayersDecision == PlayersDecision.NextRound)
                ResetRound();

            if (Settings.CurrentPlayersDecision == PlayersDecision.RestartGame)
                ResetGame();

            if (Settings.CurrentPlayersDecision == PlayersDecision.ExitGame)
                Environment.Exit(0);
            
        }

        public void DetermineWinner()
        {
            foreach (Player player in players)
            {
                if (player.Score > 21)
                    continue;
                else if (winner != null && player.Score > winner.Score)
                    winner = player;
                else if (winner == null)
                    winner = player;
                else if (player.Score == winner.Score)
                    tie = true;
                else
                    continue;
            }
        }

        public void UpdateStatistics()
        {
            if (tie)
                numberOfTies++;
            else if (winner == null)
                numberOfBothLost++;
            else if (winner is PlayerUser)
                playerPoints++;
            else
                computerPoints++;
        }

        private void UpdateScreen()
        {
            screenRenderer.GameplayScreen(roundNumber, playerPoints, computerPoints, numberOfTies, numberOfBothLost, players[0].Hand, players[0].Score, players[1].Hand, players[1].Score);
        }

        private void DealInitialCards()
        {
            foreach (Player player in players)
            {
                player.Hand.Add(deck.Deal());
                player.Hand.Add(deck.Deal());
            }
        }

        private void DealOneCard(Player player)
        {
            player.Hand.Add(deck.Deal());
        }

        private bool StopDealingToEverybody()
        {
            bool stopDealing = true;

            foreach (Player player in players)
            {
                if (!player.StopDeal)
                {
                    stopDealing = false;
                    break;
                }
            }

            return stopDealing;
        }
    }
}
