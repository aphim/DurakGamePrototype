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
            PassFlag playerPassed = new PassFlag();
            playerPassed.passFlag = false;
            playerPassed.attackerWin = false;
            playerPassed.defenderWin = false;
            int turnCounter = 0;

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

                //get the trump card
                Card trumpCard = myDeck.getTrumpcard();

                Console.WriteLine("Trump Card");
                Console.WriteLine(trumpCard.ToString());

                //initialize player1's hand
                Console.WriteLine("");
                Console.WriteLine(player1.playerName + "'s Hand:");
                player1.playerHand = new Hand(myDeck);
                player1.playerHand.displayHand();

                //initialize player2's hand
                Console.WriteLine("");
                Console.WriteLine(playerAI.playerName + "'s Hand:");
                playerAI.playerHand = new Hand(myDeck);
                playerAI.playerHand.displayHand();

                //initialize the field
                Field playingField = new Field();


                ////////////////////////////////// player 1's initial turn (attacker) /////////////////////////////////////////////////////////////

              
                Console.WriteLine("");
                player1.AttackerInitialTurn(playingField);
                turnCounter++;


                
                do
                {
                    /////////////////////////////////////// player 2's turn (defender) /////////////////////////////////////////////////////////////
                    Console.WriteLine("");
                    if (!playerPassed.passFlag)
                    {
                     
                        playerAI.DefenderTurn(playingField, trumpCard, playerPassed);
                        playingField.displayField();
                    }

                    turnCounter++;
                    if(turnCounter <=2)
                    {
                      
                        /////////////////////////////////////// player1"s second turn /////////////////////////////////////////////////////////////
                        Console.WriteLine("");
                        if (!playerPassed.passFlag)
                        {
                            player1.AttackerTurn(playingField, playerPassed,trumpCard);
                            playingField.displayField();

                        }
                    }
                    
                      
                    
                } while (!playerPassed.passFlag || turnCounter<3);

         

               



                //////////////////////////////// end of round logic ///////////////////////////////////////////////////////////////////////
                //flags to be placed in the proper places later


                if (playerPassed.attackerWin)
                {
                    //defender picks up all the field cards

                    ArrayList cardsToBePickedUp = playingField.pickupField();

                    for (int i = 0; i < cardsToBePickedUp.Count; i++)
                    {
                        playerAI.playerHand.addCard((Card)cardsToBePickedUp[i]);
                    }



                    /////DRAW CARDS/////
                    //draws back up to 6 cards in hand if necessary/possible attackers first
         
                    //loop until minimum hand size is reached for attacker (*Note attackers draw first)
                   
                        player1.DrawCards(myDeck);


                    //loop until minimum hand size is reached for defender (*Note defender always draws second)
                    playerAI.DrawCards(myDeck);


                    //resets the loop and attacker is the same player
                    playerPassed.passFlag = false;
                    playerPassed.attackerWin = false;
                    turnCounter = 0;
                   player1.playerHand.displayHand();
                   playerAI.playerHand.displayHand();
                    playingField.displayField();

                }

                if (playerPassed.defenderWin)
                {
                    /////Discard Field Cards //////
                    //field cards get discarded
                    playingField.discardField();


                    /////DRAW CARDS/////
                    //draws back up to 6 cards in hand if necessary/possible attackers first
             
                    //loop until minimum hand size is reached for attacker (*Note attackers draw first)
                    player1.DrawCards(myDeck);


                    //loop until minimum hand size is reached for defender (*Note defender always draws second)
                    playerAI.DrawCards(myDeck);



                    //defender is the new attacker
                    playerPassed.passFlag = false;
                    playerPassed.defenderWin = false;
                    turnCounter = 0;
                    player1.playerHand.displayHand();
                    playerAI.playerHand.displayHand();

                    playingField.displayField();
                    playingField.displayDiscarded();


                }





                //for looping the game
                Console.WriteLine("");
                Console.WriteLine("Play again? Y or N");

                string flag = Console.ReadLine();
                if (flag == "Y" || flag == "y")
                {
                    playAgain = true;
                    Console.Clear();
                }
                else
                {
                    playAgain = false;
                }

            } while (playAgain);
        }
    }
}