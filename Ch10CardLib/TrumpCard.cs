using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class TrumpCard
    {
        public readonly Suit trumpSuit;
        public readonly Rank trumpRank;

        public TrumpCard (Card card)
        {
            trumpSuit = card.suit;
            trumpRank = card.rank;
        }

        public Suit getTrumpSuit()
        {
            return trumpSuit;
        }

        public Rank getTrumpRank()
        {
            return trumpRank;
        }

        public override string ToString()
        {
            return "The " + trumpRank + " of " + trumpSuit + "s";
        }

    }
}
