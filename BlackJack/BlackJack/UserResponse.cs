using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class UserResponse
    {
        //public string Message { get; }
        public bool IsAskingForCard { get; }
        public bool IsAskingForRestart { get; }
        public bool IsAskingToExit { get; }
        public bool IsAskingForNextRound { get; }
        public bool IsResponseInvalid { get; }

        public UserResponse(bool a, bool r, bool x, bool n)
        {
            //Message = m;
            IsAskingForCard = a;
            IsAskingForRestart = r;
            IsAskingToExit = x;
            IsAskingForNextRound = n;
        }
        public UserResponse(bool i)
        {
            IsResponseInvalid = i;
        }
    }
}
