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
            Player playerAI = new Player("AI", (PlayerStatus)1);
            Player player1;

            Console.WriteLine("Please Enter you name");

            string playerName = Console.ReadLine();

            player1 = new Player(playerName, (PlayerStatus)0);

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
            Card trumpCard = myDeck.getTrumpcard();
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
            Console.WriteLine(player1.playerName+"'s Hand:");
            player1.playerHand = new Hand(myDeck);
            player1.playerHand.displayHand();
            //for (int i = 0; i < player1.playerHand.gethandSize(); i++)
            //{
            //    //displays the current card
            //    Card tempCard = player1.playerHand.GetCard(i);
            //    Console.Write(tempCard.ToString());
            //    if (i != player1.playerHand.gethandSize() - 1)
            //    {
            //        Console.Write(", ");
            //    }
            //    else
            //    {
            //        Console.WriteLine();
            //    }
            //}
            Console.WriteLine("");
            Console.WriteLine(playerAI.playerName+ "'s hand:");
            playerAI.playerHand = new Hand(myDeck);
            playerAI.playerHand.displayHand();
            
            //for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
            //{
            //    //displays the current card
            //    Card tempCard = playerAI.playerHand.GetCard(i);
            //    Console.Write(tempCard.ToString());
            //    if (i != playerAI.playerHand.gethandSize() - 1)
            //    {
            //        Console.Write(", ");
            //    }
            //    else
            //    {
            //        Console.WriteLine();
            //    }
            //}
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
            playingField.cardPlayed(playerAI.playerHand.playCard(3));

            Console.WriteLine("");
            Console.WriteLine("new hand 2:");
            for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = playerAI.playerHand.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != playerAI.playerHand.gethandSize() - 1)
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
            playerAI.playerHand.addCard(myDeck.drawCard());

            for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = playerAI.playerHand.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != playerAI.playerHand.gethandSize() - 1)
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
            myDeck.displayDeck(myDeck);

            //for (int i = 0; i < myDeck.getCardsRemaining(); i++)
            //{
            //    //displays the current card
            //    Card tempCard = myDeck.GetCard(i);
            //    Console.Write(tempCard.ToString());
            //    if (i != myDeck.getCardsRemaining() - 1)
            //    {
            //        Console.Write(", ");
            //    }
            //    else
            //    {
            //        Console.WriteLine();
            //    }
            //}

            Console.WriteLine("");
            Console.WriteLine("Play card from hand1, current hand 1");
            playingField.cardPlayed(player1.playerHand.playCard(3));
            for (int i = 0; i < player1.playerHand.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = player1.playerHand.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != player1.playerHand.gethandSize() - 1)
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
            playingField.displayField();
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
                player1.playerHand.addCard((Card)tempList[i]);
            }
            //new hand 1
            for (int i = 0; i < player1.playerHand.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = player1.playerHand.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != player1.playerHand.gethandSize() - 1)
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
            playingField.cardPlayed(player1.playerHand.playCard(1));
            playingField.cardPlayed(player1.playerHand.playCard(1));

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
            playingField.displayDiscarded();
            //for (int i = 0; i < playingField.getDiscard().Count; i++)
            //{
            //    //displays the current card
            //    Card tempCard = (Card)playingField.getDiscard()[i];
            //    Console.Write(tempCard.ToString());
            //    if (i != playingField.getDiscard().Count - 1)
            //    {
            //        Console.Write(", ");
            //    }
            //    else
            //    {
            //        Console.WriteLine();
            //    }
            //}

            Console.WriteLine("");
            Console.WriteLine("Final hand 1");
            for (int i = 0; i < player1.playerHand.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = player1.playerHand.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != player1.playerHand.gethandSize() - 1)
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
            for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
            {
                //displays the current card
                Card tempCard = playerAI.playerHand.GetCard(i);
                Console.Write(tempCard.ToString());
                if (i != playerAI.playerHand.gethandSize() - 1)
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
