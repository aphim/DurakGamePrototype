/* OOP 4200 Lab 2 - textbook examples
 * This codes is made based in the example from the textbook.
 * @Author:     Jacky Yuan, 100520106
 * @Date:       01/19/2021      
 * @Version:    1.0
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    /// <summary>
    /// Deck class
    /// </summary>
    public class Deck
    {
        //private card array
        public static int cardsInDeck = 36;
        private ArrayList cards = new ArrayList(cardsInDeck);

        /// <summary>
        /// Deck constructor that initializes a deck of cards
        /// </summary>
        public Deck()
        {
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 10; rankVal++)
                {
                    cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
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
                return (Card)cards[cardNum];
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
                newDeck[destCard] = (Card)cards[i]; 
            }
            cards = new ArrayList(newDeck);
        }

        /// <summary>
        /// Gets the number of remaining cards in the deck
        /// </summary>
        /// <returns>number of cards in the current deck</returns>
        public int getCardsRemaining()
        {
            int cardsRemaining = cards.Count;
            return cardsRemaining;
        }

        /// <summary>
        /// Gets the trump card from the deck.
        /// </summary>
        /// <returns>Pulls the bottom card and sets it as the trump card</returns>
        public Card getTrumpcard()
        {
            Card trumpCard = new Card((Card)cards[0]);
            cards.RemoveAt(0);

            return trumpCard;
        }

        /// <summary>
        /// Draws a card from the deck
        /// </summary>
        /// <returns>the top card from the deck as the drawn card</returns>
        public Card drawCard()
        {
            Card drawnCard = new Card((Card)cards[cards.Count-1]);

            cards.RemoveAt(cards.Count - 1);
            
            return drawnCard;
        }

       public void displayDeck(Deck myDeck)
        {
            for (int i = 0; i < myDeck.getCardsRemaining(); i++)
            {
                //displays the current card
                Card tempCard = myDeck.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != myDeck.getCardsRemaining() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

    

    }
}