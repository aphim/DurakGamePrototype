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
                Player playerAI = new Player("AI", (PlayerStatus)1);
                Player player1;

                Player attacker;
                Player defender;

                Console.WriteLine("Please Enter you name");

                string playerName = Console.ReadLine();

                player1 = new Player(playerName, (PlayerStatus)0);

                //initialize attacker and defenders
                attacker = player1;
                defender = playerAI;

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
                    //else
                    //{
                        //int p1TrumpCount = 0;
                        //int p2TrumpCount = 0;
                        //bool trumpFound = false;

                        ////Do they have trump suit in either deck, count if so
                        //for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                        //{
                        //    if (trumpCard.suit == player1.playerHand.GetCard(i).suit)
                        //    {
                        //        p1TrumpCount += 1;
                        //        trumpFound = true;
                        //    }
                        //    if (trumpCard.suit == playerAI.playerHand.GetCard(i).suit)
                        //    {
                        //        p2TrumpCount += 1;
                        //        trumpFound = true;
                        //    }
                        //}
                        //// If a trump card is found in either deck
                        //if (trumpFound)
                        //{
                        //    //If p1 deck has more trump suits than p2 deck
                        //    if (p1TrumpCount > p2TrumpCount)
                        //    {
                        //        Console.WriteLine("P1 goes has more trump suits than P2");
                        //    }
                        //    //If p2 deck has more trump suits than p1 deck
                        //    else
                        //    {
                        //        Console.WriteLine("P2 goes has more trump suits than P1");
                        //    }
                        //}
                        //If they do not have trump suit in either deck, poker style lowest-highest (diamonds, clubs, hearts ,spades)
                        else
                        {
                            Console.WriteLine("Poker Style");
                            int p1DeckSuitValue = 0;
                            int p2DeckSuitValue = 0;

                            Card p1HighestSuit = player1.playerHand.GetCard(0);
                            Card p2HighestSuit = playerAI.playerHand.GetCard(0);

                            for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                            {
                                if(player1.playerHand.GetCard(i).suit == Suit.Diamond)
                                {
                                    p1DeckSuitValue += 4;
                                }
                                if (player1.playerHand.GetCard(i).suit == Suit.Club)
                                {
                                    p1DeckSuitValue += 3;
                                }
                                if (player1.playerHand.GetCard(i).suit == Suit.Heart)
                                {
                                    p1DeckSuitValue += 2;
                                }
                                if (player1.playerHand.GetCard(i).suit == Suit.Spade)
                                {
                                    p1DeckSuitValue += 1;
                                }
                            }
                            for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
                            {
                                if (playerAI.playerHand.GetCard(i).suit == Suit.Diamond)
                                {
                                    p2DeckSuitValue += 4;
                                }
                                if (playerAI.playerHand.GetCard(i).suit == Suit.Club)
                                {
                                    p2DeckSuitValue += 3;
                                }
                                if (playerAI.playerHand.GetCard(i).suit == Suit.Heart)
                                {
                                    p2DeckSuitValue += 2;
                                }
                                if (playerAI.playerHand.GetCard(i).suit == Suit.Spade)
                                {
                                    p2DeckSuitValue += 1;
                                }
                            }
                            Console.WriteLine("P1 DECK VALUE: " + p1DeckSuitValue + "\t" + "P2 DECK VALUE: " + p2DeckSuitValue);
                            if (p1DeckSuitValue > p2DeckSuitValue)
                            {
                                Console.WriteLine("P1 goes Poker Style");
                            }
                            else
                            {
                                Console.WriteLine("P2 goes Poker Style");
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
                    ////////////////////////////////// attacker's initial turn  /////////////////////////////////////////////////////////////




                    Console.WriteLine("");
                    attacker.AttackerInitialTurn(playingField, trumpCard);
                    turnCounter++;



                    do ///Loop for the attack and defence chain rounds
                    {
                        /////////////////////////////////////// (defender) turn /////////////////////////////////////////////////////////////
                        Console.WriteLine("");
                        if (!playerPassed.passFlag)
                        {

                            defender.DefenderTurn(playingField, trumpCard, playerPassed);
                            playingField.displayField();
                        }

                        turnCounter++;
                        if (turnCounter <= 2)
                        {

                            /////////////////////////////////////// Attacker's standard turn /////////////////////////////////////////////////////////////
                            Console.WriteLine("");
                            if (!playerPassed.passFlag)
                            {
                                attacker.AttackerTurn(playingField, playerPassed, trumpCard);
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
                            defender.playerHand.addCard((Card)cardsToBePickedUp[i]);
                        }

                        playerAI = defender;
                        player1 = attacker;


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

                        attacker = player1;
                        defender = playerAI;


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

                        playerAI = defender;
                        player1 = attacker;


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

                        player1.playerStatus = ((PlayerStatus)1);
                        playerAI.playerStatus = ((PlayerStatus)0);

                        defender = player1;
                        attacker = playerAI;

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