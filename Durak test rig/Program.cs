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
                Player playerAI = new Player("AI", (PlayerStatus)0);
                Player player1;

                Console.WriteLine("Please Enter you name");

                string playerName = Console.ReadLine();

                player1 = new Player(playerName, (PlayerStatus)1);

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

                bool turnFlag = true;

                //Determine who goes first
                Card lowestCard = player1.playerHand.GetCard(0);
                for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                {
                    if (lowestCard.rank > player1.playerHand.GetCard(i).rank)
                    {
                        lowestCard = player1.playerHand.GetCard(i);
                    }

                }

                Card lowestCard2 = playerAI.playerHand.GetCard(0);
                for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
                {
                    if (lowestCard2.rank > playerAI.playerHand.GetCard(i).rank)
                    {
                        lowestCard2 = playerAI.playerHand.GetCard(i);
                    }

                }

                Console.WriteLine("P1 Lowest: " + lowestCard + " P2 Lowest: " + lowestCard2);

                //Player one has lower rank
                if (lowestCard.rank < lowestCard2.rank)
                {
                    Console.WriteLine("Player 1 Goes lower rank");
                }
                //If ranks are equal to each other 
                else if (lowestCard.rank == lowestCard2.rank)
                {
                    //If P1 has same suit as trump card
                    if (lowestCard.suit == trumpCard.suit)
                    {
                        Console.WriteLine("Player 1 Goes, equal to trump suit");
                    }
                    //If P2 has same suit as trump card
                    else if (lowestCard2.suit == trumpCard.suit)
                    {
                        Console.WriteLine("Player 2 Goes, equal to trump suit");
                    }
                    //If P1 and P2 do not have same suit as trump card, but same rank
                    else
                    {
                        int p1TrumpCount = 0;
                        int p2TrumpCount = 0;
                        bool trumpFound = false;

                        //Do they have trump suit in either deck, count if so
                        for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                        {
                            if (trumpCard.suit == player1.playerHand.GetCard(i).suit)
                            {
                                p1TrumpCount += 1;
                                trumpFound = true;
                            }
                            if (trumpCard.suit == playerAI.playerHand.GetCard(i).suit)
                            {
                                p2TrumpCount += 1;
                                trumpFound = true;
                            }
                        }
                        // If a trump card is found in either deck
                        if (trumpFound)
                        {
                            //If p1 deck has more trump suits than p2 deck
                            if (p1TrumpCount > p2TrumpCount)
                            {
                                Console.WriteLine("P1 goes has more trump suits than P2");
                            }
                            //If p2 deck has more trump suits than p1 deck
                            else
                            {
                                Console.WriteLine("P2 goes has more trump suits than P1");
                            }
                        }
                        //If they do not have trump suit in either deck, poker style lowest-highest (diamonds, clubs, hearts ,spades)
                        else
                        {
                            Console.WriteLine("Poker Style");
                            int p1SuitWins = 0;
                            int p2SuitWins = 0;
                            for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                            {
                                if (player1.playerHand.GetCard(i).suit > playerAI.playerHand.GetCard(i).suit)
                                {
                                    p1SuitWins += 1;
                                }
                                else
                                {
                                    p2SuitWins += 1;
                                }
                            }

                            if (p1SuitWins > p2SuitWins)
                            {
                                Console.WriteLine("P1 goes Poker Style");
                            }
                            else
                            {
                                Console.WriteLine("P2 goes Poker Style");
                            }

                        }
                    }
                }
                //P2 has lower rank
                else
                {
                    Console.WriteLine("Player 2 Goes lower rank");
                }


                do ////Loop for the different turns 
                {
                    ////////////////////////////////// player 1's initial turn (attacker) /////////////////////////////////////////////////////////////




                    Console.WriteLine("");
                    player1.AttackerInitialTurn(playingField, trumpCard);
                    turnCounter++;



                    do ///Loop for the attack and defence chain rounds
                    {
                        /////////////////////////////////////// player 2's turn (defender) /////////////////////////////////////////////////////////////
                        Console.WriteLine("");
                        if (!playerPassed.passFlag)
                        {

                            playerAI.DefenderTurn(playingField, trumpCard, playerPassed);
                            playingField.displayField();
                        }

                        turnCounter++;
                        if (turnCounter <= 2)
                        {

                            /////////////////////////////////////// player1"s second turn /////////////////////////////////////////////////////////////
                            Console.WriteLine("");
                            if (!playerPassed.passFlag)
                            {
                                player1.AttackerTurn(playingField, playerPassed, trumpCard);
                                playingField.displayField();

                            }
                        }



                    } while (!playerPassed.passFlag || turnCounter < 3);




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
                        Console.WriteLine("");
                        Console.WriteLine("New hands are:");
                        player1.playerHand.displayHand();
                        playerAI.playerHand.displayHand();
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
                        Console.WriteLine("");
                        Console.WriteLine("New hands are:");
                        player1.playerHand.displayHand();
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