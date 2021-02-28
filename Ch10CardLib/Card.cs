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

        public readonly int value;

        public static Suit trump ;
        public static Rank trumpRank;
        /// <summary>
        /// parameterized constructor: Sets the suit and rank for the card object.
        /// </summary>
        /// <param name="newSuit">stores the suit of the card</param>
        /// <param name="newRank">stores the rank of the card</param>
        public Card(Suit newSuit, Rank newRank, int newValue)
        {
            suit = newSuit;
            rank = newRank;
            value = newValue;
        }

        public Card(Card card)
        {
            suit = card.suit;
            rank = card.rank;
            value = card.value;
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



        //OPERATOR OVERLAODS

        /// <summary>
        /// Flag for trump usage If true, trumps are value higher thant cards of other suits
        /// </summary>
        /// 
        public static bool useTrumps = false;



        /// <summary>
        /// Overriden GetHashCode()
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 13 * (int)suit + (int)rank;
        }

        public static void setTrumpSuit(TrumpCard trumpCard) 
        {
             Suit trump = trumpCard.suit;
        
        }
        public Suit getTrumpSuit()
        {
            return trump;
        }

        public static void setTrumpRank(TrumpCard trumpCard)
        {
            Rank trumpRank = trumpCard.rank;

        }
        public Rank getTrumpRank()
        {
            return trumpRank;
        }


        public bool isSameRank(Card card1)
        {
            if (card1.rank == this.rank)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public static bool isAceHigh = true;

        //OPERATOR OVERLOADS AND OVERRIDES
        /// <summary>
        /// Overriden == operator
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator ==(Card card1, Card card2)
        {
            return (card1.suit == card2.suit) && (card1.rank == card2.rank);
        }


        /// <summary>
        /// Overriden != operator
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator !=(Card card1, Card card2)
        {
            return !(card1 == card2);
        }

        /// <summary>
        /// Overriden Equals()
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public override bool Equals(object card)
        {
            return this == (Card)card;
        }




        /// <summary>
        /// Overloaded > operator
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator >(Card card1, Card card2)
        {
            if (card1.suit == card2.suit)
            {
                if (isAceHigh)
                {
                    if (card1.rank == Rank.Ace)
                    {
                        if (card2.rank == Rank.Ace)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (card2.rank == Rank.Ace)
                        {
                            return false;
                        }
                        else
                        {
                            return (card1.rank > card2.rank);
                        }
                    }
                }
                else
                {
                    return (card1.rank > card2.rank);
                }
            }
            else
            {
                if (useTrumps && (card2.suit == Card.trump))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        /// <summary>
        /// Overloaded < operator
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator <(Card card1, Card card2)
        {
            return !(card1 >= card2);
        }

        /// <summary>
        /// Overloaded >= operator
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator >=(Card card1, Card card2)
        {
            if (card1.suit == card2.suit)
            {
                if (isAceHigh)
                {
                    if (card1.rank == Rank.Ace)
                    {
                        return true;
                    }
                    else
                    {
                        if (card2.rank == Rank.Ace)
                        {
                            return false;
                        }
                        else
                        {
                            return (card1.rank >= card2.rank);
                        }
                    }
                }
                else
                {
                    return (card1.rank >= card2.rank);
                }
            }
            else
            {
                if (useTrumps && (card2.suit == Card.trump))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Overloaded <= operator
        /// </summary>
        /// <param name="card1"></param>
        /// <param name="card2"></param>
        /// <returns></returns>
        public static bool operator <=(Card card1, Card card2)
        {
            return !(card1 > card2);
        }

    }
}