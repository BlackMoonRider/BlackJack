using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class PlayerComputer : Player
    {
        public override void GetDecision()
        {
            bool decisionToStop = false;

            if (!StopDeal)
            {
                switch (Score)
                {
                    case 16:
                        decisionToStop = Utility.Random.NextDouble() < 0.1;
                        break;
                    case 17:
                        decisionToStop = Utility.Random.NextDouble() < 0.3;
                        break;
                    case 18:
                        decisionToStop = Utility.Random.NextDouble() < 0.5;
                        break;
                    case 19:
                        decisionToStop = Utility.Random.NextDouble() < 0.9;
                        break;
                    case 20:
                        decisionToStop = Utility.Random.NextDouble() < 0.997;
                        break;
                    default:
                        decisionToStop = Score > 15;
                        break;
                }
            }

            StopDeal = decisionToStop;
        }
    }
}
