using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class Hand
    {
        
        public static int defaultHandSize = 6;
        ArrayList hand = new ArrayList(defaultHandSize);

        public Hand(Deck deck)
        {

            for (int i=0; i< defaultHandSize; i++)
            {
                hand.Add(deck.drawCard());
            }
        }

        private Hand()
        {
        }

        public Card GetCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= hand.Count-1)
            {
                return (Card)hand[cardNum];
            }
            else
            {
                throw (new System.ArgumentOutOfRangeException("cardNum", cardNum, "Value must be between 0 and handsize"));
            }
        }

        public int gethandSize()
        {
            int handSize = hand.Count;
            return handSize;
        }

        public Card playCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= hand.Count - 1)
            {
                Card playedCard = (Card)hand[cardNum];

                hand.RemoveAt(cardNum);

                return playedCard;
            }
            else
            {
                throw (new System.ArgumentOutOfRangeException("cardNum", cardNum, "Value must be between 0 and handsize"));
            }
        }

        public void addCard(Card card)
        {
            hand.Add(card);
        }

    }
}
