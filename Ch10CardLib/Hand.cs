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
        private ArrayList hand = new ArrayList(defaultHandSize);

        /// <summary>
        /// Draws out initial hand from the selected deck.
        /// </summary>
        /// <param name="deck">deck to draw the hand from</param>
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

        /// <summary>
        /// Gets a card using its index from the hand
        /// </summary>
        /// <param name="cardNum">index of the card</param>
        /// <returns>returns selected card</returns>
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

        /// <summary>
        /// returns the size of the current hand
        /// </summary>
        /// <returns>size of the hand</returns>
        public int gethandSize()
        {
            int handSize = hand.Count;
            return handSize;
        }

        /// <summary>
        /// Plays a card from the hand returning the selected card and removing it from the hand
        /// </summary>
        /// <param name="cardNum">index of the card to be played</param>
        /// <returns>returns selected card from the hand</returns>
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

        /// <summary>
        /// adds a card to the hand
        /// </summary>
        /// <param name="card">card to be added</param>
        public void addCard(Card card)
        {
            hand.Add(card);
        }

    }
}
