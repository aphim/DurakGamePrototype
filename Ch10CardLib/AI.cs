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

        /// <summary>
        /// Method used to control the AI's decisions while making the initial attack (Will play the lowest value card)
        /// </summary>
        /// <param name="playingField"></param>
        /// <param name="handAI"></param>
        /// <param name="trumpCard"></param>
        public override void AttackerInitialTurn(Field playingField, Card trumpCard)
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

            //play the card onto the field (removing it from the hand)
            playingField.cardPlayed(playerHand.playCard(lowestcardIndex));

        }

        /// <summary>
        /// Method used to control the AI's decisions while making a standard attack turn.
        /// </summary>
        /// <param name="playingField"></param>
        /// <param name="passFlag"></param>
        /// <param name="trumpCard"></param>
        /// <param name="handAI"></param>
        public override void AttackerTurn(Field playingField, PassFlag passFlag, Card trumpCard)
        {
            //check the playingfield
            ArrayList validCards = new ArrayList();
            ArrayList validCardIndex = new ArrayList();

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
                passFlag.passFlag = true;
                passFlag.defenderWin = true;
            }
            

            /////// If moves are valid //////////////////////////////////////////

            //checks to see if there are any moves available
            if (passFlag.passFlag == false)
            {
                //initialize some arraylists
                ArrayList trumpSuits = new ArrayList();
                ArrayList trumpSuitIndex = new ArrayList();

                ///////Sorts the valid Cards into trump cards and non trump cards//////////
                
                //loops through the vaild cards and finds the cards that are trump suits and the 
                //indices assocaiated with them
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

                //play the card onto the field (removing it from the hand)
                playingField.cardPlayed(playerHand.playCard(indexSelected));

            }
        }








    }
}
