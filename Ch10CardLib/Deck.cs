/* OOP 4200 Lab 2 - textbook examples
 * This codes is made based in the example from the textbook.
 * @Author:     Jacky Yuan, 100520106
 * @Date:       01/19/2021      
 * @Version:    1.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch10CardLib
{
    /// <summary>
    /// Deck class
    /// </summary>
    public class Deck
    {
        //private card array
        private Card[] cards;
        public int cardsInDeck = 36;

        /// <summary>
        /// Deck constructor that initializes a deck of cards
        /// </summary>
        public Deck()
        {
            //creates an array fo 52 cards looping through the suits and ranks
            cards = new Card[cardsInDeck];
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 10; rankVal++)
                {
                    cards[suitVal * 9 + rankVal - 1] = new Card((Suit)suitVal, (Rank)rankVal);
                }
            }
        }

        /// <summary>
        /// Method for getting the value of a card
        /// </summary>
        /// <param name="cardNum">holds the value of a particular card</param>
        /// <returns>the value of the card</returns>
        public Card GetCard(int cardNum)
        {
            if(cardNum >= 0 && cardNum <= cardsInDeck-1)
            {
                return cards[cardNum];
            }
            else
            {
                throw (new System.ArgumentOutOfRangeException("cardNum", cardNum, "Value must be between 0 and 36."));
            }
        }

        /// <summary>
        /// Method used to shuffle the deck of cards
        /// </summary>
        public void Shuffle()
        {
            //creates arrays for the shuffle method
            Card[] newDeck = new Card[cardsInDeck];
            bool[] assigned = new bool[cardsInDeck];
            Random sourceGen = new Random();
            //for loop shuffling the cards
            for(int i =0; i< cardsInDeck; i++)
            {
                int destCard = 0;
                bool foundCard = false;
                while (foundCard == false)
                {
                    destCard = sourceGen.Next(cardsInDeck);
                    if(assigned[destCard] == false)
                    {
                        foundCard = true;
                    }
                }
                assigned[destCard] = true;
                newDeck[destCard] = cards[i]; 
            }
            newDeck.CopyTo(cards, 0);
        }

        public int getCardsRemaining()
        {
            int cardsRemaining = cards.Length;
            return cardsRemaining;
        }

        public TrumpCard getTrumpcard()
        {
            TrumpCard trumpCard = new TrumpCard(cards[0]);
            cards = cards.Skip(1).ToArray();

            return trumpCard;
        }

        public Card drawCard()
        {
            Card drawnCard = new Card(cards[cards.Length-1]);

            Array.Resize(ref cards, cards.Length - 1);
            
            return drawnCard;
        }

    }
}