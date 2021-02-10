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
using System.Threading.Tasks;
using Ch10CardLib;

namespace Ch10CardClient
{

    class Program
    {
        static void Main(string[] args)
        {
            //creates a new deck object
            Deck myDeck = new Deck();
            //shuffles the deck object
            myDeck.Shuffle();
            //loops through the deck
            for (int i = 0; i < myDeck.getCardsRemaining(); i++)
            {
                //displays the current card
                Card tempCard = myDeck.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != myDeck.getCardsRemaining()-1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            TrumpCard trumpCard = myDeck.getTrumpcard();
            Console.WriteLine(trumpCard.ToString());
            Console.WriteLine("");
            for (int i = 0; i < myDeck.getCardsRemaining(); i++)
            {
                //displays the current card
                Card tempCard = myDeck.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != myDeck.getCardsRemaining()-1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine(trumpCard.getTrumpSuit().ToString());
            Console.WriteLine(trumpCard.getTrumpRank().ToString());
            Console.WriteLine(trumpCard.ToString());

            Console.ReadKey();
        }
    }
}
