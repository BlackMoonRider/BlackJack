using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    static class Settings
    {
        static public GameState CurrentGameState { get; set; }
        static public PlayersDecision CurrentPlayersDecision { get; set; }
    }
}
