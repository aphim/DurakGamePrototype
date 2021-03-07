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
            //initiate variables
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
                
                //sets the first card as the current lowest card 
                Card lowestCard = player1.playerHand.GetCard(0);
                //loops through the hand
                for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                {
                    //checks if the value of the new card is lower than the current card
                    if (lowestCard.value > player1.playerHand.GetCard(i).value)
                    {
                        //sets the lowest card to new card if new card is lower than current card
                        lowestCard = player1.playerHand.GetCard(i);
                    }
                }

                //sets the first card as the current lowest card 
                Card lowestCard2 = playerAI.playerHand.GetCard(0);
                //loops through the hand
                for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
                {
                    //checks if the value of the new card is lower than the current card
                    if (lowestCard2.value > playerAI.playerHand.GetCard(i).value)
                    {
                        //sets the lowest card to new card if new card is lower than current card
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
                    //calls the attacker initial turn function
                    attacker.AttackerInitialTurn(playingField, trumpCard);
                    Console.WriteLine("");
                    Console.WriteLine("The cards on the field:");
                    playingField.displayField();

                    turnCounter++;

                    //checks to see if the player wins during the endgame
                    if (endGame)
                    {
                        //check player hand size
                        if (attacker.playerHand.gethandSize() == 0)
                        {
                            //declares winner if conditions are met
                            Console.WriteLine(attacker.playerName + " Wins!");
                            playerPassed.passFlag = true;
                            turnFlag = false;
                        }
                    }


                    do ///Loop for the attack and defence chain rounds
                    {
                        /////////////////////////////////////// (defender) turn /////////////////////////////////////////////////////////////

                        //checks if a player has passed their turn
                        if (!playerPassed.passFlag)
                        {
                            Console.WriteLine("");
                            //calls the defender's turn method
                            defender.DefenderTurn(playingField, trumpCard, playerPassed);
                            Console.WriteLine("");
                            Console.WriteLine("The cards on the field are:");
                            playingField.displayField();
                        }

                        //checks to see if the player wins during the endgame
                        if (endGame)
                        {
                            //checks defender's hand size
                            if (defender.playerHand.gethandSize() == 0)
                            {
                                //declares winner if conditions are met
                                Console.WriteLine(defender.playerName + " Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                        }

                        turnCounter++;

                        //checks to see the number of turns that has gone.
                        if (turnCounter <= 6)
                        {

                            /////////////////////////////////////// Attacker's standard turn /////////////////////////////////////////////////////////////

                            //check if a player has passed
                            if (!playerPassed.passFlag)
                            {
                                Console.WriteLine("");
                                //call attackers' standard turn method
                                attacker.AttackerTurn(playingField, playerPassed, trumpCard);
                                Console.WriteLine("");
                                Console.WriteLine("The cards on the field are:");
                                playingField.displayField();

                            }

                            //checks to see if the player wins during the endgame
                            if (endGame)
                            {
                                //checks attacker's hand size
                                if (attacker.playerHand.gethandSize() == 0)
                                {
                                    //declares winner if condition has been met
                                    Console.WriteLine(attacker.playerStatus + " Wins!");
                                    playerPassed.passFlag = true;
                                    turnFlag = false;
                                }
                            }
                        }
                        //loops as long as neither side passes and that it hasn't reached 6 turns
                    } while (!playerPassed.passFlag || turnCounter < 7);




                    //////////////////////////////// end of round logic ///////////////////////////////////////////////////////////////////////

                    //checks if attackerwin flag has been tripped
                    if (playerPassed.attackerWin)
                    {
                        //creates an arraylist with all the field cards
                        ArrayList cardsToBePickedUp = playingField.pickupField();

                        //adds all the cards in the arraylist to the defender's hand
                        for (int i = 0; i < cardsToBePickedUp.Count; i++)
                        {
                            defender.playerHand.addCard((Card)cardsToBePickedUp[i]);
                        }

                        //sets the roles for the next round
                        playerAI = defender;
                        player1 = attacker;


                        /////DRAW CARDS///// (TODO FURTHER TESTING NEEDED FOR EXCEEDING MAXIMUM ROUNDS BUG)
                        //draws back up to 6 cards in hand if necessary/possible attackers first

                        //loop until minimum hand size is reached for attacker (*Note attackers draw first)
                        player1.DrawCards(myDeck);


                        //sets the endgame flag if deck reaches 0 cards
                        if (myDeck.getCardsRemaining() == 0)
                        {
                            endGame = true;
                        }

                        //checks to see if any player wins during the endgame
                        if (endGame)
                        {
                            //checks their hand size
                            if (player1.playerHand.gethandSize() == 0)
                            {
                                //declare winner
                                Console.WriteLine(player1.playerName + " Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                            else if (playerAI.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(playerAI.playerName + " Wins!");
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
                            //checks hand size
                            if (playerAI.playerHand.gethandSize() == 0)
                            {
                                //declare winner
                                Console.WriteLine(playerAI.playerName + " Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                            else if (player1.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(player1.playerName + " Wins!");
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

                    //checks to see if defenderwin flag has been tripped
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
                            //checks player's hand size
                            if (player1.playerHand.gethandSize() == 0)
                            {
                                //declares winner
                                Console.WriteLine(player1.playerName + " Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                            else if (playerAI.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(playerAI.playerName + " Wins!");
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
                            //checks players' handsize
                            if (playerAI.playerHand.gethandSize() == 0)
                            {
                                //declare winner
                                Console.WriteLine(playerAI.playerName + " Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                            else if (player1.playerHand.gethandSize() == 0)
                            {
                                Console.WriteLine(player1.playerName + " Wins!");
                                playerPassed.passFlag = true;
                                turnFlag = false;
                            }
                        }


                        //defender is the new attacker (swap roles)(
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



                } while (turnFlag); //loops the program until the game ends by someone winning




                //for looping the game asks if user wants to play again
                Console.WriteLine("");
                Console.WriteLine("Play again? Y or N");

                //if user enters y or Y, new game is started, otherwise loop is exited and program ends.
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