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
            ScreenRenderer screenRenderer = new ScreenRenderer();
            screenRenderer.TitleScreen();
            Console.ReadKey();

            Game game = new Game(screenRenderer);
            while (true)
            {
                game.PlayOneStep();
            }
        }
    }
}
