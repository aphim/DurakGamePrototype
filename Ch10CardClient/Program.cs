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
            //myDeck.Shuffle();
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
            Console.WriteLine("");
            Console.WriteLine("Number of cards remaining:");
            Console.WriteLine(myDeck.getCardsRemaining());
            TrumpCard trumpCard = myDeck.getTrumpcard();
            Console.WriteLine("");
            Console.WriteLine("Trump Card:");
            Console.WriteLine(trumpCard.ToString());
            Console.WriteLine("");
            Console.WriteLine("remaining cards in deck:");
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
            Console.WriteLine("");
            Console.WriteLine("Trump card info");
            Console.WriteLine(trumpCard.getTrumpSuit().ToString());
            Console.WriteLine(trumpCard.getTrumpRank().ToString());
            Console.WriteLine(trumpCard.ToString());

            Console.WriteLine(myDeck.getCardsRemaining());

            Console.WriteLine("");
            Console.WriteLine("Hand 1:");
            Hand hand1 = new Hand(myDeck);
            for (int i = 0; i < hand1.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = hand1.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != hand1.gethandSize() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("hand 2:");
            Hand hand2 = new Hand(myDeck);
            for (int i = 0; i < hand2.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = hand2.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != hand2.gethandSize() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("remaining cards in deck");
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

            Console.WriteLine("");

            Console.WriteLine("play 4th card from hand 2:");
            Field playingField = new Field();
            playingField.cardPlayed(hand2.playCard(3));

            Console.WriteLine("");
            Console.WriteLine("new hand 2:");
            for (int i = 0; i < hand2.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = hand2.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != hand2.gethandSize() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("draw card into hand 2:");
            hand2.addCard(myDeck.drawCard());

            for (int i = 0; i < hand2.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = hand2.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != hand2.gethandSize() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("new remaining cards in deck:");
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

            Console.WriteLine("");
            Console.WriteLine("Play card from hand1, current hand 1");
            playingField.cardPlayed(hand1.playCard(3));
            for (int i = 0; i < hand1.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = hand1.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != hand1.gethandSize() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Cards on field");

            for (int i = 0; i < playingField.getField().Count; i++)
            {
                //displays the current card
                Card tempCard = (Card)playingField.getField()[i];
                Console.Write(tempCard.ToString());
                if (i != playingField.getField().Count - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Pickup cards on field into hand 1");
            ArrayList tempList = playingField.pickupField();
            Console.WriteLine(tempList.Count);
            for (int i = 0; i < tempList.Count; i++)
            {
                //displays the current card
                hand1.addCard((Card)tempList[i]);
            }
            //new hand 1
            for (int i = 0; i < hand1.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = hand1.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != hand1.gethandSize() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("New field");
            for (int i = 0; i < playingField.getField().Count; i++)
            {
                //displays the current card
                Card tempCard = (Card)playingField.getField()[i];
                Console.Write(tempCard.ToString());
                if (i != playingField.getField().Count - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Play some cards from hand2");
            playingField.cardPlayed(hand1.playCard(1));
            playingField.cardPlayed(hand1.playCard(1));

            Console.WriteLine("");
            Console.WriteLine("Cards on field");

            for (int i = 0; i < playingField.getField().Count; i++)
            {
                //displays the current card
                Card tempCard = (Card)playingField.getField()[i];
                Console.Write(tempCard.ToString());
                if (i != playingField.getField().Count - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Discard field");
            playingField.discardField();

            for (int i = 0; i < playingField.getField().Count; i++)
            {
                //displays the current card
                Card tempCard = (Card)playingField.getField()[i];
                Console.Write(tempCard.ToString());
                if (i != playingField.getField().Count - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Discard pile");
            for (int i = 0; i < playingField.getDiscard().Count; i++)
            {
                //displays the current card
                Card tempCard = (Card)playingField.getDiscard()[i];
                Console.Write(tempCard.ToString());
                if (i != playingField.getDiscard().Count - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Final hand 1");
            for (int i = 0; i < hand1.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = hand1.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != hand1.gethandSize() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Final hand 2");
            for (int i = 0; i < hand2.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = hand2.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != hand2.gethandSize() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine("final deck");
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
            Console.WriteLine("");
            Console.WriteLine("Trump Card");
            Console.WriteLine(trumpCard.ToString());

            Console.ReadKey();

        }
    }
}
