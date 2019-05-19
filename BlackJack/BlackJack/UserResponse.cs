using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class UserResponse
    {
        public string Message { get; }
        public bool IsAskingForCard { get; }
        public bool IsAskingForRestart { get; }
        public bool IsAskingToExit { get; }
        public bool IsResponseInvalid { get; set; }

        public UserResponse(string m, bool c, bool r, bool e, bool i)
        {
            Message = m;
            IsAskingForCard = c;
            IsAskingForRestart = r;
            IsAskingToExit = e;
            IsResponseInvalid = i;
        }
    }
}
