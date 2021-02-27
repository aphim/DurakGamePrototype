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

                //get the trump card
                Card trumpCard = myDeck.getTrumpcard();

                Console.WriteLine("Trump Card");
                Console.WriteLine(trumpCard.ToString());

                //initialize player1's hand
                Console.WriteLine("");
                Console.WriteLine(player1.playerName + "'s Hand:");
                player1.playerHand = new Hand(myDeck);
                player1.playerHand.displayHand(player1.playerHand);

                //initialize player2's hand
                Console.WriteLine("");
                Console.WriteLine(playerAI.playerName + "'s Hand:");
                playerAI.playerHand = new Hand(myDeck);
                playerAI.playerHand.displayHand(playerAI.playerHand);

                //initialize the field
                Field playingField = new Field();


                ////////////////////////////////// player 1's initial turn (attacker) /////////////////////////////////////////////////////////////

              
                Console.WriteLine("");
                player1.AttackerInitialTurn(playingField);
              


                /////////////////////////////////////// player 2's turn (defender) /////////////////////////////////////////////////////////////

                //ASK THE USER IF THEY WANT TO PLAY OR SKIP
                //IF THEY WANT TO PLAY THEN DO BELOW CODE OTHERWISE ADD FEILD CARDS TO HAND

                Console.WriteLine("");
                playerAI.DefenderTurn(playingField, trumpCard);
                

                playingField.displayField();


                /////////////////////////////////////// player1"s second turn /////////////////////////////////////////////////////////////
                Console.WriteLine("");
        
                player1.AttackerTurn(playingField);
                

                //////////////////////////////// end of round logic ///////////////////////////////////////////////////////////////////////
                //flags to be placed in the proper places later
                bool attackerWin = false;
                bool defenderWin = false;

                if (attackerWin)
                {
                    //defender picks up all the field cards

                    ArrayList cardsToBePickedUp = playingField.pickupField();

                    for (int i = 0; i < cardsToBePickedUp.Count; i++)
                    {
                        player1.playerHand.addCard((Card)cardsToBePickedUp[i]);
                    }



                    /////DRAW CARDS/////
                    //draws back up to 6 cards in hand if necessary/possible attackers first
                    bool attackerDraw = true;
                    bool defenderDraw = true;
                    //loop until minimum hand size is reached for attacker (*Note attackers draw first)
                    while (attackerDraw)
                    {
                        //check the remaining deck size
                        if (myDeck.getCardsRemaining() > 0)
                        {
                            //check if the attacker's hand is greater than 6 (standard hand size)
                            int attackerHandSize = player1.playerHand.gethandSize();
                            if (attackerHandSize < 6)
                            {
                                player1.playerHand.addCard(myDeck.drawCard());
                            }
                            else
                            {
                                attackerDraw = false;
                            }
                        }
                    }
                    //loop until minimum hand size is reached for defender (*Note defender always draws second)
                    while (defenderDraw)
                    {
                        //check the remaining deck size
                        if (myDeck.getCardsRemaining() > 0)
                        {
                            //check if the attacker's hand is greater than 6 (standard hand size)
                            int defenderHandSize = playerAI.playerHand.gethandSize();
                            if (defenderHandSize < 6)
                            {
                                playerAI.playerHand.addCard(myDeck.drawCard());
                            }
                            else
                            {
                                defenderDraw = false;
                            }
                        }
                    }





                    //resets the loop and attacker is the same player

                }

                if (defenderWin)
                {
                    /////Discard Field Cards //////
                    //field cards get discarded
                    playingField.discardField();


                    /////DRAW CARDS/////
                    //draws back up to 6 cards in hand if necessary/possible attackers first
                    bool attackerDraw = true;
                    bool defenderDraw = true;
                    //loop until minimum hand size is reached for attacker (*Note attackers draw first)
                    while (attackerDraw)
                    {
                        //check the remaining deck size
                        if (myDeck.getCardsRemaining() > 0)
                        {
                            //check if the attacker's hand is greater than 6 (standard hand size)
                            int attackerHandSize = player1.playerHand.gethandSize();
                            if (attackerHandSize < 6)
                            {
                                player1.playerHand.addCard(myDeck.drawCard());
                            }
                            else
                            {
                                attackerDraw = false;
                            }
                        }
                    }
                    //loop until minimum hand size is reached for defender (*Note defender always draws second)
                    while (defenderDraw)
                    {
                        //check the remaining deck size
                        if (myDeck.getCardsRemaining() > 0)
                        {
                            //check if the attacker's hand is greater than 6 (standard hand size)
                            int defenderHandSize = playerAI.playerHand.gethandSize();
                            if (defenderHandSize < 6)
                            {
                                playerAI.playerHand.addCard(myDeck.drawCard());
                            }
                            else
                            {
                                defenderDraw = false;
                            }
                        }
                    }


                    //defender is the new attacker





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