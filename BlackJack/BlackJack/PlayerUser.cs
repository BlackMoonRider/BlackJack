using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class PlayerUser : Player
    {
        public override void GetDecision()
        {
            bool inputIsValid = true;
            string playerInput;
            PlayersDecision playersDecision = PlayersDecision.None;
            do
            {
                playerInput = Console.ReadLine().Trim().ToLower();
                inputIsValid = ValidateUserInput(playerInput, ref playersDecision);
            } while (!inputIsValid);
            Settings.CurrentPlayersDecision = playersDecision;
        }

        private bool ValidateUserInput(string playerInput, ref PlayersDecision playersDecision)
        {
            if (Settings.CurrentGameState == GameState.PlayingRound)
            {
                switch (playerInput)
                {
                    case "a":
                    case "":
                        playersDecision = PlayersDecision.AskCard;
                        return true;
                    case "e":
                        playersDecision = PlayersDecision.EnoughCards;
                        Settings.CurrentGameState = GameState.PlayersChoiceIsMade;
                        StopDeal = true;
                        return true;
                    default:
                        return false;
                }
            }

            else if (Settings.CurrentGameState == GameState.PlayersChoiceIsMade)
            {
                switch (playerInput)
                {
                    case "n":
                    case "":
                        playersDecision = PlayersDecision.Next;
                        StopDeal = true;
                        return true;
                    default:
                        return false;
                }
            }

            else // GameState.RoundFinished
            {
                switch (playerInput)
                {
                    case "n":
                    case "":
                        playersDecision = PlayersDecision.NextRound;
                        return true;
                    case "r":
                        playersDecision = PlayersDecision.RestartGame;
                        return true;
                    case "x":
                        playersDecision = PlayersDecision.ExitGame;
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}
