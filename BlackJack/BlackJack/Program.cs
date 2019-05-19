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
            game.MakeComputerDecision();

            game.ResetPlayers();
            game.DealInitialCards();


            while (game.ContinueRound())
            {
                game.MakeComputerDecision();
                game.UpdateGameScreen(UpdateScreenOptions.InGame);


                string input = String.Empty;
                UserResponse userResponse = new UserResponse(true);
                while (userResponse.IsResponseInvalid)
                {
                    input = Console.ReadLine();
                    userResponse = Game.ValidateUserInput(input);
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

            Console.ReadKey();
                           
        }
    }
}
