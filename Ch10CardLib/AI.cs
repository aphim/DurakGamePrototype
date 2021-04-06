using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class AI : Player
    {
        //constructor for making an instance of an AI object
        public AI (string name) : base(name)
        {
            playerName = name;
        }


        public int AITurnCycle(Card TrumpCard, Field PlayingField, string round)
        {
            const string ATTACKINITIAL = "initialTurn";
            const string ATTACKERTURN = "attacker";
            const string DEFENDERTURN = "defender";
            if (round == ATTACKINITIAL)
            {
                return AIAttackerInitialTurn(TrumpCard);
            }
            else if(round == ATTACKERTURN)
            {
                return AIAttackerTurn(PlayingField, TrumpCard);
            }
            else if( round == DEFENDERTURN )
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// Method used to control the AI's decisions while making the initial attack (Will play the lowest value card)
        /// </summary>
        /// <param name="playingField"></param>
        /// <param name="handAI"></param>
        /// <param name="trumpCard"></param>
        public int AIAttackerInitialTurn(Card trumpCard)
        {

            ////////// CHECKS FOR THE LOWEST "VALUE" CARD IN THE HAND WITH TRUMP SUIT CARDS GETTING MORE VALUE THAN OTHER CARDS ///////////
            
            //sets the first card as the current lowest card 
            Card lowestCard = playerHand.GetCard(0);
            int lowestcardIndex = 0;

            //initialize the lowestcard value
            int lowestCardValue = lowestCard.value;

            //checks to see if the current card is of the trump suit and raises its value if it is
            if (lowestCard.suit.Equals(trumpCard.suit))
            {
                lowestCardValue = lowestCard.value + 36;
            }

            //loops through the hand
            for (int i = 1; i < playerHand.gethandSize(); i++)
            {
                //create a variable for the next card
                Card nextCard = playerHand.GetCard(i);

                //initialize the nextCardValue;
                int nextCardValue = nextCard.value;

                //checks to see if the next card is of the trump suit and raises its value if it is
                if (nextCard.suit.Equals(trumpCard.suit))
                {
                    nextCardValue = nextCard.value + 36;
                }

                //checks if the value of the new card is lower than the current card
                if (lowestCardValue > nextCardValue)
                {
                    //sets the lowest card to new card if new card is lower than current card
                    lowestCard = nextCard;
                    lowestCardValue = nextCardValue;
                    lowestcardIndex = i;
                }
            }

            //////////////////// Plays the lowest value card ///////////////////////////////

            //play the card onto the field (removing it from the hand)'
            //playingField.cardPlayed(playerHand.playCard(lowestcardIndex));
            return lowestcardIndex;

        }

        /// <summary>
        /// Method used to control the AI's decisions while making a standard attack turn.
        /// </summary>
        /// <param name="playingField"></param>
        /// <param name="passFlag"></param>
        /// <param name="trumpCard"></param>
        /// <param name="handAI"></param>
        public int AIAttackerTurn(Field playingField, Card trumpCard)
        {
            //check the playingfield
            ArrayList validCards = new ArrayList();
            ArrayList validCardIndex = new ArrayList();
            bool passFlag = false;

            //loop through the hand of the AI
            for (int i = 0;  i < playerHand.gethandSize(); i++ )
            {
                //declare a temp card as the current card
                Card tempCard = (Card)playerHand.GetCard(i);
                int tempCardIndex = i;

                //loop through the field
                for (int j = 0; j < playingField.getField().Count; j++)
                {
                    //declares a card as the currently viewed field card
                    Card fieldCard = (Card)playingField.getField()[j];

                    //checks to see if the card is the same rank
                    if (fieldCard.isSameRank(tempCard))
                    {
                        //if the card is of the same rank, adds it to the validCards arrayList and breaks out of the current loop
                        validCards.Add(tempCard);
                        validCardIndex.Add(tempCardIndex);
                        break;
                    }

                }
            }
             
            ////////// Skips the turn if no cards are valid ////////////////////////

            //If there are no valid cards, setup bypass flag and defender win flag
            if (validCards.Count == 0)
            {
                passFlag = true;
                
            }
            

            /////// If moves are valid //////////////////////////////////////////

            //checks to see if there are any moves available
            if (passFlag == false)
            {
                //initialize some arraylists
                ArrayList trumpSuits = new ArrayList();
                ArrayList trumpSuitIndex = new ArrayList();

                ///////Sorts the valid Cards into trump cards and non trump cards//////////
                
                //loops through the vaild cards and finds the cards that are trump suits and the 
                //indexes assocaiated with them
                for (int i = 0; i < validCards.Count; i++)
                {
                    Card tempCard = (Card)validCards[i];
                    int tempIndex = (int)validCardIndex[i];

                    //if they are of the trump suits, move them to the trump suits arraylist
                    if (tempCard.suit == trumpCard.suit)
                    {
                        trumpSuits.Add(tempCard);
                        trumpSuitIndex.Add(tempIndex);
                        validCards.RemoveAt(i);
                        validCardIndex.RemoveAt(i);
                    }
                }

                //initialize the variables
                Card cardSelected = null;
                int indexSelected = 0;

                //////////Selects the to play first looking through the non trump cards and selecting the lowest//////
                //////////before looking at the trump cards and selecting the lowest.  ///////////////////////////////

                //if the validCards arraylist is greater than 0, looks for the the lowest card
                if (validCards.Count > 0)
                {
                    //sets the first card as the current lowest card 
                    cardSelected = (Card)validCards[0];
                    indexSelected = (int)validCardIndex[0];

                    //loops through the hand
                    for (int i = 0; i < validCards.Count; i++)
                    {
                        //checks if the value of the new card is lower than the current card
                        if (cardSelected.value > ((Card)validCards[i]).value)
                        {
                            //sets the lowest card to new card if new card is lower than current card 
                            cardSelected = (Card)validCards[i];
                            indexSelected = (int)validCardIndex[i];
                        }
                    }
                }
                //otherwise, checks the trumpsuits arraylist to find the lowest card there
                else if (trumpSuits.Count > 0)
                {
                    //sets the first card as the current lowest card 
                    cardSelected = (Card)trumpSuits[0];
                    indexSelected = (int)trumpSuitIndex[0];

                    //loops through the hand
                    for (int i = 0; i < trumpSuits.Count; i++)
                    {
                        //checks if the value of the new card is lower than the current card
                        if (cardSelected.value > ((Card)trumpSuits[i]).value)
                        {
                            //sets the lowest card to new card if new card is lower than current card
                            cardSelected = (Card)trumpSuits[i];
                            indexSelected = (int)trumpSuitIndex[i];
                        }
                    }
                }

                //play the card onto the field 
               // playingField.cardPlayed(playerHand.playCard(indexSelected));
                return indexSelected;
            }
            else
            {
                return -1;
            }
        }

        
        /// <summary>
        /// This method controls the AI's descisions on a defender turn
        /// </summary>
        /// <param name="playingField"></param>
        /// <param name="trumpCard"></param>
        /// <param name="passFlag"></param>
        public override void DefenderTurn(Field playingField, Card trumpCard, PassFlag passFlag)
        {
            this.playerHand.displayHand();
            ArrayList validCards = new ArrayList();
            ArrayList validCardIndex = new ArrayList();
            bool equalsTrump = false;
            bool noTrump = false;
            bool lose = false;
            //Find the last card played on the field by the attacker
            Card lastCard = playingField.getCurrentCard();
            bool validCard = false;

            //check if the last placed card on the field is the same suit as the trump suit
            if (lastCard.suit == trumpCard.suit)
            {
                equalsTrump = true;
            }
            else
            {
                equalsTrump = false;
            }

            //loop through ai hand
            for (int i = 0; i < playerHand.gethandSize(); i++)
            {
                //declare a temp card as the current card
                Card tempCard = (Card)playerHand.GetCard(i);
                int tempCardIndex = i;


                //Run if the last placed card on the field is of the trump suit
                if (equalsTrump)
                {

                    //check if the suit of the current card in the hand has the same suit as the trump card and is higher in value than the last placed card
                    if (tempCard.suit == trumpCard.suit && tempCard.value > lastCard.value)
                    {

                        //adds the card to the validCards arrayList and breaks out of the current loop
                        validCards.Add(tempCard);
                        validCardIndex.Add(tempCardIndex);
                        break;
                    }

                }
                else//run if the last placed card was not a trump suit
                {
                    //run if the tempCard's suit is the same as the last placed card's suit OR if the suit is the same as the trump suit 
                    //and is greater in value compared to the last placed card
                    if (tempCard.suit == lastCard.suit || tempCard.suit == trumpCard.suit && tempCard.value > lastCard.value)
                    {
                        //adds the card to the validCards arrayList and breaks out of the current loop
                        validCards.Add(tempCard);
                        validCardIndex.Add(tempCardIndex);
                        break;
                    }
                }
            }

            ////////// Ends the turn if no cards are valid ////////////////////////

            //If there are no valid cards, setup bypass flag and Attacker win flag
            if (validCards.Count == 0)
            {
                Console.WriteLine("YOU LOSE");
                lose = true;
                /*  passFlag.passFlag = true;
                  passFlag.attackerWin = true;*/

            }


            /////// If moves are valid //////////////////////////////////////////

            //checks to see if there are any moves available
            if (passFlag.passFlag == false)
            {
                //initialize some arraylists
                ArrayList trumpSuits = new ArrayList();
                ArrayList trumpSuitIndex = new ArrayList();

                ///////Sorts the valid Cards into trump cards and non trump cards//////////

                //initialize the variables
                Card cardSelected = null;
                int indexSelected = 0;

                //If the last card played is a trump card
                if (equalsTrump)
                {

                    //run if the validCards arraylist is greater than 0
                    if (validCards.Count > 0)
                    {
                        //sets cardSelected as the first card in the arrayList
                        cardSelected = (Card)validCards[0];
                        indexSelected = (int)validCardIndex[0];

                        //loops through the hand
                        for (int i = 0; i < validCards.Count; i++)
                        {

                            //checks if the value of the cardSelected is greater than the value of the next card in the array
                            if (cardSelected.value > ((Card)validCards[i]).value)
                            {
                                //checks if cardSelected's value is greater than the last placed card's value
                                if (cardSelected.value > lastCard.value)
                                {
                                    //set cardSelected to the current card in the arrayList
                                    cardSelected = (Card)validCards[i];
                                    indexSelected = (int)validCardIndex[i];

                                }

                            }
                        }

                    }

                }
                else// run if the last card played was not a trump card
                {
                    validCard = false;
                    //run if the validCards arrayList is not empty
                    if (validCards.Count > 0)
                    {
                        //sets the first card in the array as cardSelected
                        cardSelected = (Card)validCards[0];
                        indexSelected = (int)validCardIndex[0];

                        //loops through the hand
                        for (int i = 0; i < validCards.Count; i++)
                        {

                            //checks if the value of the cardSelected is lower than the next card in the array and if the cardSelected's value is greater than the last placed card
                            if (cardSelected.value < ((Card)validCards[i]).value && cardSelected.value > lastCard.value)
                            {


                                //checks is the suit of the last card is the same as the selected card
                                if (cardSelected.suit == lastCard.suit)
                                {
                                    //set cardSelected to the current card in the array

                                    cardSelected = (Card)validCards[i];
                                    indexSelected = (int)validCardIndex[i];
                                    validCard = false;


                                }
                                else //set validCard to true if there is no valid non trump suit cards to play
                                {
                                    validCard = true;
                                }


                            }
                        }

                        //Make the AI find the appropriate trump card to play if the last placed card was not a trump card but no cards of the same suit that are greater exist in the hand
                        if (validCard)
                        {
                            //loop through the hand
                            for (int i = 0; i < playerHand.gethandSize(); i++)
                            {
                                //set tempCard as the current card in the array
                                Card tempCard = (Card)playerHand.GetCard(i);
                                int tempCardIndex = i;

                                
                                //if the tempCard's suit is the same as the trump suit and the value is greater than the last placed card's value
                                if (tempCard.suit == trumpCard.suit && tempCard.value > lastCard.value)
                                {

                                    //add the card to the valid cards array
                                    validCards.Add(tempCard);
                                    validCardIndex.Add(tempCardIndex);
                                    break;
                                }
                              


                            }
                            //sets the first card as the current selected card 
                            cardSelected = (Card)validCards[0];
                            indexSelected = (int)validCardIndex[0];
                            //loops through the hand
                            for (int i = 0; i < validCards.Count; i++)
                            {
                                //if the value of the selected card is greater than the next card
                                if (cardSelected.value > ((Card)validCards[i]).value)
                                {
                                   
                                    //if the card selected has the same suit as the trump card and if the card selected's value is graeter than the 
                                    //last placed cards value and it the card selected's value is less than the next cards value
                                    if (cardSelected.suit == trumpCard.suit && cardSelected.value > lastCard.value && cardSelected.value < ((Card)validCards[i + 1]).value)
                                    {
                                        //set the curruent card as cardSelected
                                        cardSelected = (Card)validCards[i];
                                        indexSelected = (int)validCardIndex[i];


                                    }

                                }
                            }
                        }

                    }

                }
                //If the lose flag was not triggered play the card
                if (!lose)
                {
                    //play the card onto the field (removing it from the hand)
                    playingField.cardPlayed(playerHand.playCard(indexSelected));
                }
                else// output lose message. (Change to end turn)
                {
                    Console.WriteLine("YOU HAVE LOST!!!");
                }

            }



        }//END OF DEFENDER METHOD







    }
}
