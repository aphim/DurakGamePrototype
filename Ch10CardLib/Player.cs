using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class Player
    {
        /// <summary>
        /// getter and setter for the playername attribute
        /// </summary>
        public string playerName { get; set; }

        /// <summary>
        /// getter and setter for the playerhand attribute
        /// </summary>
        public Hand playerHand { get; set; }


        /// <summary>
        /// Constructor for the player class, entering his name
        /// </summary>
        /// <param name="name"></param>
        public Player(string name)
        {
            playerName = name;
        }
        
        ///// <summary>
        ///// Method used to initialize the first attacker turn
        ///// </summary>
        ///// <param name="playingField"></param>
        ///// <param name="trumpCard"></param>
        //public virtual void AttackerInitialTurn(Field playingField, Card trumpCard)
        //{
        //    //displays message
        //    Console.WriteLine("It is " + this.playerName + "'s Turn.");

        //    //displays this player's hand
        //    this.playerHand.displayHand();

        //    //initialize some variables
        //    int selectedCard = 0;
        //    bool validCard = false;
        //    string inputString;

        //    //input loop
        //    do
        //    {
        //        //recieves input
        //        inputString = Console.ReadLine();
        //        //if input is "d" or "D"
        //        if (inputString.Equals("D") || inputString.Equals("d"))
        //        {
        //            //displays the discard pile and restart the loop
        //            Console.WriteLine("");
        //            Console.WriteLine("The discard pile is:");
        //            playingField.displayDiscarded();
        //            continue;

        //        }
        //        //if the input is "t" or "T"
        //        if (inputString.Equals("T") || inputString.Equals("t"))
        //        {
        //            //displays the trump card and restarts the loop
        //            Console.WriteLine("");
        //            Console.WriteLine("The trump card is:");
        //            Console.WriteLine(trumpCard.ToString());
        //            continue;
        //        }

        //        //tries the parse the selected card
        //        int.TryParse(inputString, out selectedCard);
        //        selectedCard = selectedCard - 1;
                
        //        //if the selected card is not in the hand size, prints out an error message
        //        if (selectedCard > this.playerHand.gethandSize() || selectedCard < 0)
        //        {
        //            Console.WriteLine("Invalid option please pick a card.");
        //        }
        //        //if selection is valid, breaks out of the loop
        //        else
        //        {
        //            validCard = true;
        //        }
        //    } while (!validCard);

        //    //play the card selected onto the field
        //    playingField.cardPlayed(this.playerHand.playCard(selectedCard));

        //}

        ///// <summary>
        ///// Method used to initialize the first attacker turn
        ///// </summary>
        ///// <param name="playingField"></param>
        ///// <param name="trumpCard"></param>
        //public virtual void AttackerInitialTurnForm(Field playingField, Card trumpCard, string input)
        //{
        //    //displays message
        //    //Console.WriteLine("It is " + this.playerName + "'s Turn.");

        //    //displays this player's hand
        //    //this.playerHand.displayHand();

        //    //initialize some variables
        //    int selectedCard = 0;
        //    bool validCard = false;
        //    string inputString;

        //    //input loop
        //    do
        //    {
        //        ///////////////TODO IMPLEMENT THE ADDITIONAL FUNCTIONALITY ////////////////////////////////// 
        //        //recieves input
        //        inputString = input;
        //        ////if input is "d" or "D"
        //        //if (inputString.Equals("D") || inputString.Equals("d"))
        //        //{
        //        //    //displays the discard pile and restart the loop
        //        //    Console.WriteLine("");
        //        //    Console.WriteLine("The discard pile is:");
        //        //    playingField.displayDiscarded();
        //        //    continue;

        //        //}
        //        ////if the input is "t" or "T"
        //        //if (inputString.Equals("T") || inputString.Equals("t"))
        //        //{
        //        //    //displays the trump card and restarts the loop
        //        //    Console.WriteLine("");
        //        //    Console.WriteLine("The trump card is:");
        //        //    Console.WriteLine(trumpCard.ToString());
        //        //    continue;
        //        //}

        //        //tries the parse the selected card
        //        int.TryParse(inputString, out selectedCard);
        //        selectedCard = selectedCard - 1;
        //        selectedCard = 1;

        //        //if the selected card is not in the hand size, prints out an error message
        //        if (selectedCard > this.playerHand.gethandSize() || selectedCard < 0)
        //        {
        //            Console.WriteLine("Invalid option please pick a card.");
        //        }
        //        //if selection is valid, breaks out of the loop
        //        else
        //        {
        //            validCard = true;
        //        }
        //    } while (!validCard);

        //    //play the card selected onto the field
        //    playingField.cardPlayed(this.playerHand.playCard(selectedCard));

        //}


        ///// <summary>
        ///// Method used to produce an attacker's standard turn
        ///// </summary>
        ///// <param name="playingField"></param>
        ///// <param name="passFlag"></param>
        ///// <param name="trumpCard"></param>
        //public virtual void AttackerTurn(Field playingField, Card trumpCard)
        //{
        //    //initialize variables
        //    int selectedCard=0;
        //    bool validCard = false;
        //    string inputString;
        //    Console.WriteLine("It is " + this.playerName + "'s Turn.");
        //    this.playerHand.displayHand();

        //    //input validations loop
        //    do
        //    {

        //        inputString = Console.ReadLine();
        //        //checks to see if input is a d or D and displays the discard pile if so. Restarts the loop
        //        if (inputString.Equals("D") || inputString.Equals("d"))
        //        {
        //            Console.WriteLine("");
        //            Console.WriteLine("The discard pile is:");
        //            playingField.displayDiscarded();
        //            continue;
        //        }
        //        //checks to see if the input is t or T and displays the trump card if so. Then restarts the loop
        //        if(inputString.Equals("T") || inputString.Equals("t"))
        //        {
        //            Console.WriteLine("");
        //            Console.WriteLine("The trump card is:");
        //            Console.WriteLine(trumpCard.ToString());
        //            continue;
        //        }
        //        //Checks to see if the input is a q or Q and ends the round. Breaks out of the loop and raises the 
        //        //bypass flag and the defenderwin flag
        //        if (inputString.Equals("Q") || inputString.Equals("q"))
        //        {
        //            passFlag.passFlag = true;
        //            passFlag.defenderWin = true;
                    
        //            break;
        //        }

        //        //checks to see if the selected card is within the player's handsize
        //        int.TryParse(inputString, out selectedCard);
        //        selectedCard = selectedCard - 1;

        //        if (selectedCard > this.playerHand.gethandSize() || selectedCard < 0)
        //        {
        //            Console.WriteLine("Invalid option please pick a card.");
        //        }
        //        else
        //        {
        //            validCard = true;
        //        }


        //    Card tempCard;
        //        //checks to see if passflag has been tripped
        //    if (passFlag.passFlag == false)
        //    {
        //            //select card from the hand
        //        Card cardSelected = this.playerHand.selectCard(selectedCard);

        //        //GET ALL CARDS ON FIELD
        //        bool matchFlag = true;

        //            //checks for matches on the field (Mainly used for chaining attacks)
        //        while (matchFlag)
        //        {
        //            for (int i = 0; i < playingField.getField().Count; i++)
        //            {
        //                tempCard = (Card)playingField.getField()[i];

        //                    //checks to see if the card is the same rank
        //                if (tempCard.isSameRank(cardSelected))
        //                {

        //                    matchFlag = false;
        //                }

        //            }
        //            // if a card has been matched, play the selected card
        //            if (matchFlag == false)
        //            {
        //                playingField.cardPlayed(this.playerHand.playCard(selectedCard));
        //                //playingField.displayField();
        //            }
        //            //otherwise displays a message telling them the cards cannot be played and breaks out of this loop
        //            else
        //            {
        //                Console.WriteLine("You can only play a card of the same rank as the cards on the field");
        //                validCard = false;
        //                break;
        //            }
        //        }
        //    }

        //    } while (!validCard);

        //}

        ///// <summary>
        ///// Method used to produce an attacker's standard turn
        ///// </summary>
        ///// <param name="playingField"></param>
        ///// <param name="passFlag"></param>
        ///// <param name="trumpCard"></param>
        //public virtual void AttackerTurnGUI(Field playingField, PassFlag passFlag, Card trumpCard)
        //{
        //    //initialize variables
        //    int selectedCard = 0;
        //    bool validCard = false;
        //    string inputString;
        //    this.playerHand.displayHand();

        //    //input validations loop
        //    do
        //    {

        //        inputString = Console.ReadLine();
        //        //checks to see if input is a d or D and displays the discard pile if so. Restarts the loop
        //        if (inputString.Equals("D") || inputString.Equals("d"))
        //        {
        //            Console.WriteLine("");
        //            Console.WriteLine("The discard pile is:");
        //            playingField.displayDiscarded();
        //            continue;
        //        }
        //        //checks to see if the input is t or T and displays the trump card if so. Then restarts the loop
        //        if (inputString.Equals("T") || inputString.Equals("t"))
        //        {
        //            Console.WriteLine("");
        //            Console.WriteLine("The trump card is:");
        //            Console.WriteLine(trumpCard.ToString());
        //            continue;
        //        }
        //        //Checks to see if the input is a q or Q and ends the round. Breaks out of the loop and raises the 
        //        //bypass flag and the defenderwin flag
        //        if (inputString.Equals("Q") || inputString.Equals("q"))
        //        {
        //            passFlag.passFlag = true;
        //            passFlag.defenderWin = true;

        //            break;
        //        }

        //        //checks to see if the selected card is within the player's handsize
        //        int.TryParse(inputString, out selectedCard);
        //        selectedCard = selectedCard - 1;

        //        if (selectedCard > this.playerHand.gethandSize() || selectedCard < 0)
        //        {
        //            Console.WriteLine("Invalid option please pick a card.");
        //        }
        //        else
        //        {
        //            validCard = true;
        //        }


        //        Card tempCard;
        //        //checks to see if passflag has been tripped
        //        if (passFlag.passFlag == false)
        //        {
        //            //select card from the hand
        //            Card cardSelected = this.playerHand.selectCard(selectedCard);

        //            //GET ALL CARDS ON FIELD
        //            bool matchFlag = true;

        //            //checks for matches on the field (Mainly used for chaining attacks)
        //            while (matchFlag)
        //            {
        //                for (int i = 0; i < playingField.getField().Count; i++)
        //                {
        //                    tempCard = (Card)playingField.getField()[i];

        //                    //checks to see if the card is the same rank
        //                    if (tempCard.isSameRank(cardSelected))
        //                    {

        //                        matchFlag = false;
        //                    }

        //                }
        //                // if a card has been matched, play the selected card
        //                if (matchFlag == false)
        //                {
        //                    playingField.cardPlayed(this.playerHand.playCard(selectedCard));
        //                    //playingField.displayField();
        //                }
        //                //otherwise displays a message telling them the cards cannot be played and breaks out of this loop
        //                else
        //                {
        //                    Console.WriteLine("You can only play a card of the same rank as the cards on the field");
        //                    validCard = false;
        //                    break;
        //                }
        //            }
        //        }

        //    } while (!validCard);

        //}


        ///// <summary>
        ///// Method used for the main logic of a defender's turn
        ///// </summary>
        ///// <param name="playingField"></param>
        ///// <param name="trumpCard"></param>
        ///// <param name="passFlag"></param>
        //public virtual void DefenderTurn(Field playingField, Card trumpCard, PassFlag passFlag)
        //{
        //    //initialize some variables
        //    Console.WriteLine( "It is " + this.playerName + "'s Turn.");
        //    int selectedCard=0;
        //    bool validCard = false;
        //    string inputString;
        //    this.playerHand.displayHand();

        //    //validation loop
        //    do
        //    {
        //        //checks to see if the input is d or D and displays the discard pile if it is, then restarts the loop
        //        inputString = Console.ReadLine();
        //        if (inputString.Equals("D") || inputString.Equals("d"))
        //        {
        //            Console.WriteLine("");
        //            Console.WriteLine("The discard pile is:");
        //            playingField.displayDiscarded();
        //            continue;
                   
        //        }
        //        //checks to see if the input is a t or T and displays the trump card if it is, then restarts the loop
        //        if (inputString.Equals("T") || inputString.Equals("t"))
        //        {
        //            Console.WriteLine("");
        //            Console.WriteLine("The trump card is:");
        //            Console.WriteLine(trumpCard.ToString());
        //            continue;
        //        }
        //        //checks to see if the input is a q or Q and quits the round. Raises the bypass flag and the attacker win flag
        //        if (inputString.Equals("Q") || inputString.Equals("q"))
        //        {
        //            passFlag.passFlag = true;
        //            passFlag.attackerWin = true;
        //            break;
        //        }
                
        //        //tries to parse the selected card 
        //        int.TryParse(inputString, out selectedCard);
        //        selectedCard = selectedCard - 1;

        //        //checks to see if the selected card is within the hand size
        //        if (selectedCard > this.playerHand.gethandSize() || selectedCard < 0)
        //        {
        //            Console.WriteLine("Invalid option please pick a card.");
        //        }
        //        else
        //        {
        //            validCard = true;
        //        }

        //        //sets the current card
        //    Card currentCard = playingField.getCurrentCard();

        //        //checks to see if the round has been passed
        //    if (passFlag.passFlag == false)
        //    {
        //        Card cardSelected = this.playerHand.selectCard(selectedCard);


        //        while (true)
        //        {

        //            //checks the card in hand is equals to the trump suit
        //            if (cardSelected.suit.Equals(trumpCard.suit))
        //            {
        //                //checks to see if the current card on the field is of the trump suit
        //                if (currentCard.suit.Equals(trumpCard.suit))
        //                {
        //                    //if the selected card rank is higher than the current card rank
        //                    if (cardSelected.rank > currentCard.rank)
        //                    {
        //                        // card is played and leaves the loop (hand card is trump, field card is trump, hand card higher rank)
        //                        playingField.cardPlayed(this.playerHand.playCard(selectedCard));
        //                        break;
        //                    }
        //                        //otherwise the selected card's rank is too low, break out of this loop
        //                        //(hand card is trump, field card is trump, field card higher rank)
        //                        else
        //                        {
        //                        Console.WriteLine("rank is too low.");
        //                        validCard = false;
        //                        break;
        //                    }
        //                }
        //                //the card is played (hand card is trump, field card is not trump) leaves the loop
        //                else
        //                {
        //                    playingField.cardPlayed(this.playerHand.playCard(selectedCard));
        //                    break;
        //                }
        //            }
        //            //checks to see if played card suit is the field card suit
        //            else if (cardSelected.suit == currentCard.suit)
        //            {
        //                //checks to see if played card rank is higher than field card rank
        //                if (cardSelected.rank > currentCard.rank)
        //                {
        //                        //plays the card and leaves the loop (hand card matches the field card suit but is higher rank)
        //                    playingField.cardPlayed(this.playerHand.playCard(selectedCard));
        //                    break;
        //                }
        //                //displays an error message and leaves the loop (hand card matches the field card suit but rank too low)
        //                else
        //                {
        //                    Console.WriteLine("rank is too low.");
        //                    validCard = false;
        //                    break;
        //                }
        //            }
        //            //displays an error message and leaves the loop (hand card is not field card suit or trump suit)
        //            else
        //            {
        //                Console.WriteLine("You can only play the same suit or the trump suit.");
        //                validCard = false;
        //                break;
        //            }
        //        }
        //    }//END OF IF

        //    } while (!validCard);
        //}

        /// <summary>
        /// Method used to draw cards from the deck
        /// </summary>
        /// <param name="myDeck"></param>
        public void DrawCards(Deck myDeck)
        {
            bool attackerDraw = true;
            string filePath = @"../../GameLog.txt";
            string tempString = "";
            //check the remaining deck size

            while (attackerDraw)
            {
                //check to see if there are still cards in the deck
                if (myDeck.getCardsRemaining() > 0)
                {
                    //check if the attacker's hand is greater than 6 (standard hand size)
                    int attackerHandSize = this.playerHand.gethandSize();
                    if (attackerHandSize < 6)
                    {
                        Card drawnCard = myDeck.drawCard();
                        tempString += " " + drawnCard.ToString();
                        //output what card the player drew
                     
                        this.playerHand.addCard(drawnCard);
                    

                    }
                    else
                    {
                        attackerDraw = false;

                    }
                }
                else
                {
                    attackerDraw = false;

                }
            }
            if(tempString != "")
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {

                    writer.WriteLine(this.playerName + " drew:" + tempString);

                }
            }
          

        }


    }
}
