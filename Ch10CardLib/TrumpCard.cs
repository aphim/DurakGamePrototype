/*  Project: OOP 4200: Durak Project
 *  Author: Jacky Yuan
 *          Ashok Sasitharan
 *          Andre Agrippa
 *          Roshan Persaud
 *          Manthan Amitkumar Shah
 *          
 *  Desc:  This class is used for the trumpcard in CardLib.
 * 
 */
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

        /// <summary>
        /// get the suit of the trump card
        /// </summary>
        /// <returns>suit of trump card</returns>
        public Suit getTrumpSuit()
        {
            return suit;
        }

        /// <summary>
        /// gets the rank of the trump card
        /// </summary>
        /// <returns>rank of the trump card</returns>
        public Rank getTrumpRank()
        {
            return rank;
        }

        /// <summary>
        /// overwritten tostring method for the trump card
        /// </summary>
        /// <returns>formatted message for the trump card</returns>
        public override string ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }

    }
}
