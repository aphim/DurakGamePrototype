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

            bool endGame = false;

            do
            {
                //creates a new deck object
                Deck myDeck = new Deck();

                //create player objects
                Player playerAI = new Player("AI", (PlayerStatus)1);
                Player player1;

                Player attacker;
                Player defender;

                Console.WriteLine("Please Enter you name");

                string playerName = Console.ReadLine();

                player1 = new Player(playerName, (PlayerStatus)0);


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


                ////Determine who goes first /////////////////////////////////////////////////////////////////
                Card lowestCard = player1.playerHand.GetCard(0);
                for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                {

                    if (lowestCard.value > player1.playerHand.GetCard(i).value)
                    {
                        lowestCard = player1.playerHand.GetCard(i);
                    }
                }

                Card lowestCard2 = playerAI.playerHand.GetCard(0);
                for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
                {
                    if (lowestCard2.value > playerAI.playerHand.GetCard(i).value)
                    {
                        lowestCard2 = playerAI.playerHand.GetCard(i);
                    }

                }

                //set the starting player based on who has the lowest card (poker suits)
                if (lowestCard < lowestCard2)
                {
                    //initialize attacker and defenders
                    attacker = player1;
                    defender = playerAI;
                }
                else
                {
                    defender = player1;
                    attacker = playerAI;
                }

                bool turnFlag = true; //flag for the below do while loop
                do ////Loop for the different turns 
                {
                    ////////////////////////////////// attacker's initial turn  /////////////////////////////////////////////////////////////

                    Console.WriteLine("");
                    attacker.AttackerInitialTurn(playingField, trumpCard);
                    Console.WriteLine("");
                    Console.WriteLine("The cards on the field:");
                    playingField.displayField();

                    turnCounter++;

                    //checks to see if the player wins during the endgame
                    if (endGame)
                    {
                        if (attacker.playerHand.gethandSize() == 0)
                        {
                            Console.WriteLine(attacker.playerName + "Wins!");
                            playerPassed.passFlag = true;
                            turnFlag = false;
                        }
                    }


                    do ///Loop for the attack and defence chain rounds
                    {
                        /////////////////////////////////////// (defender) turn /////////////////////////////////////////////////////////////

                        if (!playerPassed.passFlag)
                        {
                            Console.WriteLine("");
                            defender.DefenderTurn(playingField, trumpCard, playerPassed);
                            Console.WriteLine("");
                            Console.WriteLine("The cards on the field are:");
                            playingField.displayField();
                        }

                        //checks to see if the player wins during the endgame
                        if (endGame)
                        {
                            if (defender.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(defender.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                        }

                        turnCounter++;
                        if (turnCounter <= 6)
                        {

                            /////////////////////////////////////// Attacker's standard turn /////////////////////////////////////////////////////////////

                            if (!playerPassed.passFlag)
                            {
                                Console.WriteLine("");
                                attacker.AttackerTurn(playingField, playerPassed, trumpCard);
                                Console.WriteLine("");
                                Console.WriteLine("The cards on the field are:");
                                playingField.displayField();

                                //checks to see if the player wins during the endgame
                                if (endGame)
                                {
                                    if (attacker.playerHand.gethandSize() == 0)
                                    {
                                        Console.WriteLine(attacker.playerStatus + "Wins!");
                                        playerPassed.passFlag = true;
                                        turnFlag = false;
                                    }
                                }

                            }
                        }



                    } while (!playerPassed.passFlag || turnCounter < 7);




                    //////////////////////////////// end of round logic ///////////////////////////////////////////////////////////////////////
                    //flags to be placed in the proper places later


                    if (playerPassed.attackerWin)
                    {
                        //defender picks up all the field cards

                        ArrayList cardsToBePickedUp = playingField.pickupField();

                        for (int i = 0; i < cardsToBePickedUp.Count; i++)
                        {
                            defender.playerHand.addCard((Card)cardsToBePickedUp[i]);
                        }

                        playerAI = defender;
                        player1 = attacker;


                        /////DRAW CARDS/////
                        //draws back up to 6 cards in hand if necessary/possible attackers first

                        //loop until minimum hand size is reached for attacker (*Note attackers draw first)

                        player1.DrawCards(myDeck);


                        //sets the endgame flag
                        if (myDeck.getCardsRemaining() == 0)
                        {
                            endGame = true;
                        }

                        //checks to see if attacker wins during the endgame
                        if (endGame)
                        {
                            if (player1.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(player1.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                            else if (playerAI.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(playerAI.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                        }


                        //loop until minimum hand size is reached for defender (*Note defender always draws second)
                        playerAI.DrawCards(myDeck);


                        //sets the endgame flag
                        if (myDeck.getCardsRemaining() == 0)
                        {
                            endGame = true;
                        }

                        //checks to see if attacker wins during the endgame
                        if (endGame)
                        {
                            if (playerAI.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(playerAI.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                            else if (player1.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(player1.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                        }


                        //resets the loop and attacker is the same player
                        playerPassed.passFlag = false;
                        playerPassed.attackerWin = false;
                        turnCounter = 0;

                        attacker = player1;
                        defender = playerAI;

                        Console.Clear();


                        Console.WriteLine("");
                        Console.WriteLine(player1.playerName + "'s new hands is:");
                        player1.playerHand.displayHand();
                        Console.WriteLine("");
                        Console.WriteLine(playerAI.playerName + "'s new hands is:");
                        playerAI.playerHand.displayHand();
                    }


                    if (playerPassed.defenderWin)
                    {
                        /////Discard Field Cards //////
                        //field cards get discarded
                        playingField.discardField();

                        playerAI = defender;
                        player1 = attacker;


                        /////DRAW CARDS/////
                        //draws back up to 6 cards in hand if necessary/possible attackers first

                        //loop until minimum hand size is reached for attacker (*Note attackers draw first)
                        player1.DrawCards(myDeck);


                        //sets the endgame flag
                        if (myDeck.getCardsRemaining() == 0)
                        {
                            endGame = true;
                        }

                        //checks to see if attacker wins during the endgame
                        if (endGame)
                        {
                            if (player1.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(player1.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                            else if (playerAI.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(playerAI.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                        }

                        //loop until minimum hand size is reached for defender (*Note defender always draws second)
                        playerAI.DrawCards(myDeck);


                        //sets the endgame flag
                        if (myDeck.getCardsRemaining() == 0)
                        {
                            endGame = true;
                        }

                        //checks to see if attacker wins during the endgame
                        if (endGame)
                        {
                            if (playerAI.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(playerAI.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                            else if (player1.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(player1.playerName + "Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                        }


                        //defender is the new attacker
                        playerPassed.passFlag = false;
                        playerPassed.defenderWin = false;
                        turnCounter = 0;

                        player1.playerStatus = ((PlayerStatus)1);
                        playerAI.playerStatus = ((PlayerStatus)0);

                        defender = player1;
                        attacker = playerAI;

                        Console.Clear();

                        Console.WriteLine("");
                        Console.WriteLine(player1.playerName + "'s new hands is:");
                        player1.playerHand.displayHand();
                        Console.WriteLine("");
                        Console.WriteLine(playerAI.playerName + "'s new hands is:");
                        playerAI.playerHand.displayHand();
                    }



                } while (turnFlag);




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