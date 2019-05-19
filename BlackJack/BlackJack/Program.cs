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
        beginning:

            Random random = new Random();

            Game game = new Game("Player", random);

        start:

            game.UpdateRoundCounter();
            game.ResetRoundData();
            game.ResetPlayers();
            game.DealInitialCards();
            game.MakeComputerDecision();

            string input = String.Empty;
            UserResponse userResponse;

            while (game.ContinueRound())
            {
                game.MakeComputerDecision();
                game.UpdateGameScreen(UpdateScreenOptions.InGame);

                userResponse = new UserResponse(true);
                while (userResponse.IsResponseInvalid)
                {
                    input = Console.ReadLine();
                    userResponse = Game.ValidateUserInput(input, UpdateScreenOptions.InGame);
                    if (userResponse.IsResponseInvalid)
                    {
                        game.UpdateGameScreen(UpdateScreenOptions.InGame);
                        Console.WriteLine(Environment.NewLine);
                        Console.WriteLine("> Input is invalid. Please try again:");
                    }
                }

                game.PlayUser(userResponse);
                game.PlayComputer();

                game.UpdateGameScreen(UpdateScreenOptions.InGame);
            }

            game.GetWinner();
            game.UpdateGameScreen(UpdateScreenOptions.EndOfRound);

            game.UpdateStockIfNeeded();

            userResponse = new UserResponse(true);
            while (userResponse.IsResponseInvalid)
            {
                input = Console.ReadLine();
                userResponse = Game.ValidateUserInput(input, UpdateScreenOptions.EndOfRound);
                if (userResponse.IsResponseInvalid)
                {
                    game.UpdateGameScreen(UpdateScreenOptions.InGame);
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("> Input is invalid. Please try again:");
                }
            }

            if (userResponse.IsAskingForNextRound)
                goto start;

            if (userResponse.IsAskingForRestart)
                goto beginning;

            if (userResponse.IsAskingToExit)
            {
                Console.WriteLine("> Are you sure you want to exit the game? \r\n  (Type capital 'N' to continue the game or press any other key to exit):");
                if ((ConsoleKey)Console.Read() == ConsoleKey.N)
                    goto start;
            }

            //Console.ReadKey();
                           
        }
    }
}
