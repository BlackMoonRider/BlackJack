using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class DecisionMaker
    {
        private Random random;
        private int points;


        public DecisionMaker(int points, Random random)
        {
            this.random = random;
            this.points = points;
        }

        public bool MakeDecision()
        {
            bool decisionToTake;

            switch (points)
            {
                case 16:
                    decisionToTake = random.NextDouble() > 0.1 ? true : false;
                    break;
                case 17:
                    decisionToTake = random.NextDouble() > 0.3 ? true : false;
                    break;
                case 18:
                    decisionToTake = random.NextDouble() > 0.5 ? true : false;
                    break;
                case 19:
                    decisionToTake = random.NextDouble() > 0.8 ? true : false;
                    break;
                case 20:
                    decisionToTake = random.NextDouble() > 0.98 ? true : false;
                    break;
                default:
                    if (points <= 15)
                        decisionToTake = true;
                    else
                        decisionToTake = false;
                    break;
            }

            return decisionToTake;
        }
    }
}
