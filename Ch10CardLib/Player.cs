using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class Player
    {
        public string playerName { get; set; }
        public Hand playerHand { get; set; }
        public Player(string name)
        {
            playerName = name;
        }

    }
}
