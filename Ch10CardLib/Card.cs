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
    /// Card class
    /// </summary>
    public class Card
    {
        //calls the suit and rank enumerations
        public readonly Suit suit;
        public readonly Rank rank;

        /// <summary>
        /// parameterized constructor: Sets the suit and rank for the card object.
        /// </summary>
        /// <param name="newSuit">stores the suit of the card</param>
        /// <param name="newRank">stores the rank of the card</param>
        public Card(Suit newSuit, Rank newRank)
        {
            suit = newSuit;
            rank = newRank;
        }

        public Card(Card card)
        {
            suit = card.suit;
            rank = card.rank;
        }

        /// <summary>
        /// Defualt constructor for cards.
        /// </summary>
        protected Card()
        {

        }

        /// <summary>
        /// Overrides the tostring method for formatting the output of a card object.
        /// </summary>
        /// <returns>string displaying a card object</returns>
        public override string ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }
    }
}