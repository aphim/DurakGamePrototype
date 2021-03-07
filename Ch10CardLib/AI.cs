using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class AI : Player
    {
        public AI (string name, PlayerStatus status) : base(name, status)
        {
            playerName = name;
            playerStatus = status;
        }
    }
}
