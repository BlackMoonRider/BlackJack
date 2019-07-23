using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class ScreenRenderer
    {
        public void TitleScreen()
        {
            string titleScreen = @" .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
| |   ______     | || |   _____      | || |      __      | || |     ______   | || |  ___  ____   | |
| |  |_   _ \    | || |  |_   _|     | || |     /  \     | || |   .' ___  |  | || | |_  ||_  _|  | |
| |    | |_) |   | || |    | |       | || |    / /\ \    | || |  / .'   \_|  | || |   | |_/ /    | |
| |    |  __'.   | || |    | |   _   | || |   / ____ \   | || |  | |         | || |   |  __'.    | |
| |   _| |__) |  | || |   _| |__/ |  | || | _/ /    \ \_ | || |  \ `.___.'\  | || |  _| |  \ \_  | |
| |  |_______/   | || |  |________|  | || ||____|  |____|| || |   `._____.'  | || | |____||____| | |
| |              | || |              | || |              | || |              | || |              | |
| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
 '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 
           .----------------.  .----------------.  .----------------.  .----------------.           
          | .--------------. || .--------------. || .--------------. || .--------------. |          
          | |     _____    | || |      __      | || |     ______   | || |  ___  ____   | |          
          | |    |_   _|   | || |     /  \     | || |   .' ___  |  | || | |_  ||_  _|  | |          
          | |      | |     | || |    / /\ \    | || |  / .'   \_|  | || |   | |_/ /    | |          
          | |   _  | |     | || |   / ____ \   | || |  | |         | || |   |  __'.    | |          
          | |  | |_' |     | || | _/ /    \ \_ | || |  \ `.___.'\  | || |  _| |  \ \_  | |          
          | |  `.___.'     | || ||____|  |____|| || |   `._____.'  | || | |____||____| | |          
          | |              | || |              | || |              | || |              | |          
          | '--------------' || '--------------' || '--------------' || '--------------' |          
           '----------------'  '----------------'  '----------------'  '----------------'           

                                          In memory of goto...

                                        PRESS ANY KEY TO START";

            Console.WriteLine(titleScreen);
        }

        public void GameplayScreen(int roundNumber, int playerPoints, int computerPoints, int numberOfTies, int numberOfBothLost, List<Card> playerUserHand, int playerUserScore, List<Card> playerComputerHand, int playerComputerScore)
        {
            Console.Clear();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Round: {roundNumber}");
            stringBuilder.AppendLine($"Player: {playerPoints}");
            stringBuilder.AppendLine($"Computer: {computerPoints}");
            stringBuilder.AppendLine($"Ties: {numberOfTies}");
            stringBuilder.AppendLine($"Both lost: {numberOfBothLost}");
            stringBuilder.AppendLine(Environment.NewLine);
            stringBuilder.AppendLine($"Computer: {(Settings.CurrentGameState == GameState.RoundFinished ? playerComputerScore.ToString() : "##")}");

            foreach (Card card in playerComputerHand)
            {
                if (Settings.CurrentGameState == GameState.RoundFinished)
                    stringBuilder.AppendLine($"\t{card}");
                else
                    stringBuilder.AppendLine($"\t####### ## #####");
            }

            stringBuilder.AppendLine(Environment.NewLine);
            stringBuilder.AppendLine($"Player: {playerUserScore}");

            foreach (Card card in playerUserHand)
            {
                stringBuilder.AppendLine($"\t{card}");
            }

            stringBuilder.AppendLine(Environment.NewLine);

            switch (Settings.CurrentGameState)
            {
                case GameState.PlayingRound:
                    stringBuilder.AppendLine("ASK CARD: A, ENTER | ENOUGH CARDS: E");
                    break;
                case GameState.PlayersChoiceIsMade:
                    stringBuilder.AppendLine("NEXT: N, ENTER");
                    break;
                case GameState.RoundFinished:
                    stringBuilder.AppendLine("NEXT ROUND: N, ENTER | RESTART GAME: R | EXIT GAME: X");
                    break;
                default:
                    break;
            }

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
