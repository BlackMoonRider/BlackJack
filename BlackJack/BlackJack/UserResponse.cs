using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    struct UserResponse
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
            IsResponseInvalid = false;
        }
        public UserResponse(bool i)
        {
            IsAskingForCard = false;
            IsAskingForRestart = false;
            IsAskingToExit = false;
            IsAskingForNextRound = false;
            IsResponseInvalid = i;
        }
    }
}
