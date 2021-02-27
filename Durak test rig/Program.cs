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
                Console.WriteLine(player1.playerName + "'s turn");

                player1.playerHand.displayHand(player1.playerHand);

                int selectedCard;

                int.TryParse(Console.ReadLine(), out selectedCard);
                selectedCard = selectedCard - 1;

                playingField.cardPlayed(player1.playerHand.playCard(selectedCard));



                playingField.displayField(playingField);


                /////////////////////////////////////// player 2's turn (defender) /////////////////////////////////////////////////////////////
                Console.WriteLine("");
                Console.WriteLine("AI player's turn.");

                playerAI.playerHand.displayHand(playerAI.playerHand);

                int.TryParse(Console.ReadLine(), out selectedCard);
                selectedCard = selectedCard - 1;

                Card currentCard = playingField.getCurrentCard();

                Card cardSelected = playerAI.playerHand.selectCard(selectedCard);

                while (true)
                {
                    //checks the card in hand is equals to the trump suit
                    if (cardSelected.suit.Equals(trumpCard.suit))
                    {
                        if (currentCard.suit.Equals(trumpCard.suit))
                        {
                            //if the selected card rank is higher than the current card rank
                            if (cardSelected.rank > currentCard.rank)
                            {
                                playingField.cardPlayed(playerAI.playerHand.playCard(selectedCard));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("rank is too low.");
                                int.TryParse(Console.ReadLine(), out selectedCard);
                                selectedCard = selectedCard - 1;
                                cardSelected = playerAI.playerHand.selectCard(selectedCard);
                            }
                        }
                        else
                        {
                            playingField.cardPlayed(playerAI.playerHand.playCard(selectedCard));
                            break;
                        }
                    }
                    //checks to see if played card suit is the field card suit
                    else if (cardSelected.suit == currentCard.suit)
                    {
                        //checks to see if played card rank is higher than field card rank
                        if (cardSelected.rank > currentCard.rank)
                        {
                            playingField.cardPlayed(playerAI.playerHand.playCard(selectedCard));
                            break;
                        }
                        else
                        {
                            Console.WriteLine("rank is too low.");
                            int.TryParse(Console.ReadLine(), out selectedCard);
                            selectedCard = selectedCard - 1;
                            cardSelected = playerAI.playerHand.selectCard(selectedCard);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You can only play the same suit or the trump suit.");
                        int.TryParse(Console.ReadLine(), out selectedCard);
                        selectedCard = selectedCard - 1;
                        cardSelected = playerAI.playerHand.selectCard(selectedCard);
                    }
                }

                playingField.displayField(playingField);


                /////////////////////////////////////// player1"s second turn /////////////////////////////////////////////////////////////
                Console.WriteLine("");
                Console.WriteLine(player1.playerName + "'s turn");

                player1.playerHand.displayHand(player1.playerHand);

                int.TryParse(Console.ReadLine(), out selectedCard);
                selectedCard = selectedCard - 1;

                Card tempCard;
                cardSelected = player1.playerHand.selectCard(selectedCard);

                //GET ALL CARDS ON FIELD
                bool matchFlag = true;
                while (matchFlag)
                {
                    for (int i = 0; i < playingField.getField().Count; i++)
                    {
                        tempCard = (Card)playingField.getField()[i];

                        if (tempCard.isSameRank(cardSelected))
                        {

                            matchFlag = false;
                        }

                    }

                    if (matchFlag == false)
                    {
                        playingField.cardPlayed(player1.playerHand.playCard(selectedCard));
                        playingField.displayField(playingField);
                    }
                    else
                    {
                        Console.WriteLine("You can only play a card of the same rank as the cards on the field");
                        int.TryParse(Console.ReadLine(), out selectedCard);
                        selectedCard = selectedCard - 1;
                        cardSelected = player1.playerHand.selectCard(selectedCard);
                    }
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