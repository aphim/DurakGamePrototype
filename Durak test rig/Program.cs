using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ch10CardLib;

namespace Ch10CardClient
{
    class Program2
    {
        static void Main(string[] args)
        {

            //creates a new deck object
            Deck myDeck = new Deck();
            //create player objects
            Player playerAI = new Player("AI");
            Player player1;

            Console.WriteLine("Please Enter you name");

            string playerName = Console.ReadLine();

            player1 = new Player(playerName);

            //shuffle deck
            myDeck.Shuffle();

            //display shuffled deck
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

            //get the trump card
            TrumpCard trumpCard = myDeck.getTrumpcard();

            Console.WriteLine(trumpCard.ToString());

            //initialize player1's hand
            Console.WriteLine("");
            Console.WriteLine(player1.playerName + "'s Hand:");
            player1.playerHand = new Hand(myDeck);
            player1.playerHand.displayHand(player1.playerHand);

            //initialize player2's hand
            Console.WriteLine("");
            Console.WriteLine(playerAI.playerName + "'s hand:");
            playerAI.playerHand = new Hand(myDeck);
            playerAI.playerHand.displayHand(playerAI.playerHand);

            //initialize the field
            Field playingField = new Field();

        }
    }
}