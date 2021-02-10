using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class TrumpCard : Card
    {

        public TrumpCard (Card card): base(card)
        {

        }

        public Suit getTrumpSuit()
        {
            return suit;
        }

        public Rank getTrumpRank()
        {
            return rank;
        }

        public override string ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }

    }
}
