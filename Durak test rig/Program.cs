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
            bool playAgain = false;

            do
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

                //get the trump card
                Card trumpCard = myDeck.getTrumpcard();

                Console.WriteLine("Trump Card");
                Console.WriteLine(trumpCard.ToString());

                //initialize player1's hand
                Console.WriteLine("");
                Console.WriteLine(player1.playerName + "'s Hand:");
                player1.playerHand = new Hand(myDeck);
                //player1.playerHand.displayHand(player1.playerHand);

                //initialize player2's hand
                Console.WriteLine("");
                Console.WriteLine(playerAI.playerName + "'s Hand:");
                playerAI.playerHand = new Hand(myDeck);
                //playerAI.playerHand.displayHand(playerAI.playerHand);

                //initialize the field
                Field playingField = new Field();
                //Console.WriteLine("\nPlay the 4th card");
                //playingField.cardPlayed(playerAI.playerHand.playCard(3));
                //Console.WriteLine("\nDisplay the field");
                //playingField.displayField(playingField);



                //player 1's initial turn (attacker)
                Console.WriteLine("");
                Console.WriteLine(player1.playerName + " goes first.");

                player1.playerHand.displayHand(player1.playerHand);

                int selectedCard;

                int.TryParse(Console.ReadLine(), out selectedCard);

                playingField.cardPlayed(player1.playerHand.playCard(selectedCard));

               

                //player 2's initial turn (defender)
                Console.WriteLine("");
                Console.WriteLine("AI player's turn.");

                playerAI.playerHand.displayHand(playerAI.playerHand);

                int.TryParse(Console.ReadLine(), out selectedCard);

                playingField.cardPlayed(playerAI.playerHand.playCard(selectedCard));

                playingField.displayField(playingField);




















                //for looping the game
                Console.WriteLine("");
                Console.WriteLine("Play again? Y or N");

                string flag = Console.ReadLine();
                if (flag == "Y" || flag == "y")
                {
                    playAgain = true;
                }
                else
                {
                    playAgain = false;
                }

            } while (playAgain);
        }
    }
}