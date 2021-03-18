using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ch10CardLib;


namespace DurakFormApp
{
    public partial class frmDurak : Form
    {
        private List<CardBox.CardBox> cards = new List<CardBox.CardBox>();
        private List<CardBox.CardBox> fieldCards = new List<CardBox.CardBox>();
        private Deck myDeck = new Deck();
        private AI playerAI = new AI("AI");
        private Player player1;
        // create player objects
        Player attacker;
        Player defender;
        int turnCounter = 0;
        bool endGame = false;
        PassFlag playerPassed = new PassFlag();
        //initialize the field
        Field playingField = new Field();

        Card trumpCard;

        public frmDurak()
        {
            InitializeComponent();
            //initiate variables
            //bool playAgain = false;
            playerPassed.passFlag = false;
            playerPassed.attackerWin = false;
            playerPassed.defenderWin = false;


        }


        /// <summary>
        /// Event that triggers when user clicks how to play menu option - displays Durak rules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Deck of 36 cards – 6, 7, 8, 9, 10, J, Q, K, A (Deck is shuffled)" + "\n" +
                            "2. Players are dealt six cards in their hand. " + "\n" +
                            "3. The bottom card of the deck is removed from the deck and flipped face up. " + "\n" +
                            "   a. This card is removed from play and is shown to all players." + "\n" +
                            "   b. The suit of this card is the trump suit for this match (The value does not matter much)" + "\n" +
                            "4. The player with the lowest card in their hand is the first attacker." + "\n" +
                            "5. The attacker begins the round by playing a card from their hand to begin the attack." + "\n" +
                            "6.The defender can, if they choose to, defend against the card by playing a card from their hand that is " +
                            "either of the same suit but of a higher value or of a card from the trump suit. (If the attacker attacks with a " +
                            "card of the trump suit, the defender must defend with a card of the trump suit but of a higher value)" + "\n" +
                            "7. If the defender defends the attack, the attacker may choose to chain another attack by playing a card that is" +
                            " if one of the values on the field. (EG: if the attacker plays a 6 of hearts to attack, and defender defends with a " +
                            "8 of hearts. The attacker can attack again this round using either a 6 or an 8) This can go up to 6 attacks in a round. " +
                            "(The game does not end even if the attack uses up their entire hand unless the deck has been exhausted)" + "\n" +
                            "8. If the defender cannot defend against the attack, or if they choose to pass on defending, they must pickup all the " +
                            "cards involved in the attack." + "\n" +
                            "9. Both players draw from the deck back up to a minimum of 6 cards if necessary. (if there is not enough cards, draw whatever is left " +
                            "with attacker drawing first)" + "\n" +
                            "10. If the defender successfully defends against the attack (attack cannot chain anymore attacks or 6 attacks have been made) then all " +
                            "the cards involved in the attack is discarded and the next round begins." + "\n" +
                            "11. If the attacker is successful in attacking, they continue as attacker in the next round. If they are not, the defender " +
                            "becomes the new attacker." + "\n" +
                            "12. Rounds continue until the deck runs out of cards." + "\n" +
                            "13. When the deck runs out of cards, the rounds continue but players no longer draw cards. " + "\n" +
                            "14. Players leave the game when they run out of cards and the deck is exhausted. (can be attacker or defender) The last person with " +
                            "cards remaining loses the game."
                            );
        }
        /// <summary>
        /// If the exit buttonis clciked, close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// On form load, displays cards values in card box objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDurak_Load(object sender, EventArgs e)
        {
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();
            Card theCard = new Card(Suit.Clubs, Rank.Six, 1);
            theCard.FaceUp = false;
            Card theCard2 = new Card(Suit.Clubs, Rank.Seven, 2);
            theCard2.FaceUp = false;
            this.cardBox1.Card = theCard;
            this.cbTrumpCard.Card = theCard2;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //shuffle deck
            myDeck.Shuffle();

            btnStart.Visible = false;

            player1 = new Player("Player1");


            player1.playerHand = new Hand(myDeck);
            playerAI.playerHand = new Hand(myDeck);


            //get the trump card
            trumpCard = myDeck.getTrumpcard();
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();
            this.cbTrumpCard.Card = trumpCard;

            CreateControls();
            DisplayControls();
            DetermineStartingPlayer();

            if (attacker == player1)
            {
                lblPlayerTurn.Text = player1.playerName + "'s turn.";
            }
            else
            {
                lblPlayerTurn.Text = playerAI.playerName + "'s turn.";
            }

            lblAIhand.Text = playerAI.playerHand.displayHandGUI();

        }

        private void CreateControls()
        {
            for (int i = 0; i < player1.playerHand.gethandSize(); i++)
            {
                CardBox.CardBox newCardBox = new CardBox.CardBox(player1.playerHand.GetCard(i));
                cards.Add(newCardBox);
            }
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();
        }

        private void DisplayControls()
        {
            for (int i = player1.playerHand.gethandSize() - 1; i >= 0; i--)
            {
                cards[i].Left = (i * 20) + 100;
                this.pnPlayerHand.Controls.Add(cards[i]);

            }
        }

        private void DisplayPlayingField()
        {
            ArrayList cardsToAdd = playingField.getField();
            for (int i = cardsToAdd.Count - 1; i >= 0; i--)
            {
                fieldCards[i].Left = (i * 20) + 100;
                this.pnPlayingField.Controls.Add(fieldCards[i]);

            }
        }

        /// <summary>
        /// This function is used to determine the starting player
        /// </summary>
        private void DetermineStartingPlayer()
        {
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
            if (lowestCard.value < lowestCard2.value)
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

        }


/// /////////////////////////////////////////////// This Section will be used for the turn cycling ///////////////////////////////////////////////


        //initialize counter for counting number of rounds ///////////TODO////////////
        int counter = 0;
        //Constants used to determine which part of the round it is currently
        const string ATTACKINITIAL = "initialTurn";
        const string ATTACKERTURN = "attacker";
        const string DEFENDERTURN = "defender";
        //initialize the round variable
        string round = ATTACKINITIAL;
        //initialize a match flag
        bool matchFlag = false;



        /// <summary>
        /// Event handler for on click of the playcard button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayCard_Click(object sender, EventArgs e)
        {
            //update the deck size
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();

            //////////////////////////// The attacking player's initial turn (no restrictions on playable cards) /////////////////////////////
            if (round == ATTACKINITIAL)
            {
                //Creates a new cardbox object that will be set to the card the player selects
                CardBox.CardBox newCardBox = new CardBox.CardBox(attacker.playerHand.GetCard(int.Parse(txtHandInput.Text)));

                //The card is played from the player's hand (removed) and played onto the field (added)
                playingField.cardPlayed(attacker.playerHand.playCard(int.Parse(txtHandInput.Text)));

                //checks if the attacker is the player1 or player AI
                if (attacker == player1)
                {
                    //removes the card from the list that displays the hand of the player
                    cards.RemoveAt(int.Parse(txtHandInput.Text));

                    //resets the list to remove the existing display
                    this.pnPlayerHand.Controls.Clear();

                    //loops through the player's new hand
                    for (int i = attacker.playerHand.gethandSize() - 1; i >= 0; i--)
                    {
                        //adds an offset to each card
                        cards[i].Left = (i * 20) + 100;
                        //displays the hand in the picturebox
                        this.pnPlayerHand.Controls.Add(cards[i]);

                    }
                }
                else
                {
                    //If it is player AI's turn, refeshes the text displaying their hand (TEMP FOR 2P testing) 
                    lblAIhand.Text = attacker.playerHand.displayHandGUI();
                }

                //Cards that are played are added to the field output
                fieldCards.Add(newCardBox);

                //The display for the field is refreshed to update new cards added
                DisplayPlayingField();
                //changes the message to reflect the next player's turn
                lblPlayerTurn.Text = defender.playerName + "'s turn.";
                //sets the next turn in the round
                round = DEFENDERTURN;
            }
            //////////////////////////////// The defender's turn (can only play cards of the same suit higher rank or trump suit on non-trumps)////////////////////////////////////////////
            else if (round == DEFENDERTURN)
            {
                //Sets a variable for the currently selected card used for comparsions
                Card cardSelected = defender.playerHand.GetCard(int.Parse(txtHandInput.Text));
                //Sets a variable for the current card in the playing field
                Card currentCard = playingField.getCurrentCard();

                //checks the card in hand is equals to the trump suit
                if (cardSelected.suit.Equals(trumpCard.suit))
                {
                    //checks to see if the current card on the field is of the trump suit
                    if (currentCard.suit.Equals(trumpCard.suit))
                    {
                        //if the selected card rank is higher than the current card rank
                        if (cardSelected.rank > currentCard.rank)
                        {
                            //card is played and leaves the loop (hand card is trump, field card is trump, hand card higher rank)
                            CardBox.CardBox newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(int.Parse(txtHandInput.Text)));

                            //the card is played from the hand onto the field 
                            playingField.cardPlayed(defender.playerHand.playCard(int.Parse(txtHandInput.Text)));

                            //checks if defender is player 1
                            if (defender == player1)
                            {
                                //removes the card from the hand display
                                cards.RemoveAt(int.Parse(txtHandInput.Text));

                                //reset the display
                                this.pnPlayerHand.Controls.Clear();

                                //loops through the current hand
                                for (int i = defender.playerHand.gethandSize() - 1; i >= 0; i--)
                                {
                                    //sets an offset
                                    cards[i].Left = (i * 20) + 100;
                                    //displays the new hand in the output
                                    this.pnPlayerHand.Controls.Add(cards[i]);

                                }
                            }
                            else
                            {
                                //refreshes the text for the AI's hand
                                lblAIhand.Text = defender.playerHand.displayHandGUI();
                            }

                            //adds the cards to the field display object
                            fieldCards.Add(newCardBox);

                            //refreshes the field to display the new card
                            DisplayPlayingField();
                            //sets the message for the next player's turn
                            lblPlayerTurn.Text = attacker.playerName + "'s turn.";
                            //add 1 to the counter (for counting 6 rounds)
                            counter++;
                            //sets the attacker as the next turn
                            round = ATTACKERTURN;
                            //empty error message
                            lblErrorMsg.Text = "";
                        }
                        //otherwise the selected card's rank is too low, break out of this loop
                        //(hand card is trump, field card is trump, field card higher rank)
                        else
                        {
                            lblErrorMsg.Text = "rank is too low.(trump suit)";
                        }
                    }
                    //the card is played (hand card is trump, field card is not trump) leaves the loop
                    else
                    {
                        //Sets a cardbox object for the selected card
                        CardBox.CardBox newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(int.Parse(txtHandInput.Text)));
                        //the card is played from the hand onto the field
                        playingField.cardPlayed(defender.playerHand.playCard(int.Parse(txtHandInput.Text)));

                        //check if the defender is player 1 
                        if (defender == player1)
                        {
                            //removes the card from the hand display list
                            cards.RemoveAt(int.Parse(txtHandInput.Text));

                            //refreshes the hand display
                            this.pnPlayerHand.Controls.Clear();

                            //loops through the hand
                            for (int i = defender.playerHand.gethandSize() - 1; i >= 0; i--)
                            {
                                //sets an offset
                                cards[i].Left = (i * 20) + 100;
                                //adds the cards into the display
                                this.pnPlayerHand.Controls.Add(cards[i]);

                            }
                        }
                        else
                        {
                            //refreshes the text for AI hand (TESTING PURPOSES ONLY)
                            lblAIhand.Text = defender.playerHand.displayHandGUI();
                        }

                        //adds the cards to the new field display list
                        fieldCards.Add(newCardBox);

                        //Refreshes the field display to add the new cards
                        DisplayPlayingField();
                        //sets the message to the next player's turn
                        lblPlayerTurn.Text = attacker.playerName + "'s turn.";
                        //increment the counter
                        counter++;
                        //sets the next turn
                        round = ATTACKERTURN;
                        //reset the error message
                        lblErrorMsg.Text = "";
                    }
                }
                //checks to see if played card suit is the field card suit
                else if (cardSelected.suit == currentCard.suit)
                {
                    //checks to see if played card rank is higher than field card rank
                    if (cardSelected.rank > currentCard.rank)
                    {
                        //plays the card and leaves the loop (hand card matches the field card suit but is higher rank)
                        CardBox.CardBox newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(int.Parse(txtHandInput.Text)));

                        //plays the card from the hand onto the field
                        playingField.cardPlayed(defender.playerHand.playCard(int.Parse(txtHandInput.Text)));

                        //checks if the defender is player1
                        if (defender == player1)
                        {
                            //removes the card played from the hand display list
                            cards.RemoveAt(int.Parse(txtHandInput.Text));

                            //resets the hand display
                            this.pnPlayerHand.Controls.Clear();

                            //loops through the hand
                            for (int i = defender.playerHand.gethandSize() - 1; i >= 0; i--)
                            {
                                //sets an offset
                                cards[i].Left = (i * 20) + 100;
                                //adds the cards to the hand display
                                this.pnPlayerHand.Controls.Add(cards[i]);

                            }
                        }
                        else
                        {
                            //refreshes the text for the AI hand (TEMP)
                            lblAIhand.Text = defender.playerHand.displayHandGUI();
                        }

                        //Adds the new cards into the field display list
                        fieldCards.Add(newCardBox);

                        //refreshes the field display to display the new cards
                        DisplayPlayingField();
                        //sets the message for the next player's turn
                        lblPlayerTurn.Text = attacker.playerName + "'s turn.";
                        //increments the counter
                        counter++;
                        //sets the next round
                        round = ATTACKERTURN;
                        //resets error message
                        lblErrorMsg.Text = "";
                    }
                    //displays an error message and leaves the loop (hand card matches the field card suit but rank too low)
                    else
                    {
                        lblErrorMsg.Text = "rank is too low.(non-trump)";
                    }
                }
                //displays an error message and leaves the loop (hand card is not field card suit or trump suit)
                else
                {
                    lblErrorMsg.Text = "You can only play the same suit or the trump suit.";
                }

            }
            ///////////////////////////////////ATTACKER STANDARD TURN///////////////////////////////////////////
            else if (round == ATTACKERTURN)
            {
                //creates a variable that holds the currently selected card
                Card cardSelected = attacker.playerHand.GetCard(int.Parse(txtHandInput.Text));

                //creates a temperary card variable
                Card tempCard;

                //loops through all the cards on the field
                for (int i = 0; i < playingField.getField().Count; i++)
                {
                    //sets the temp card equal to the card on the field
                    tempCard = (Card)playingField.getField()[i];

                    //checks to see if the card is the same rank
                    if (tempCard.isSameRank(cardSelected))
                    {
                        //sets the match flag to equal true
                        matchFlag = true;
                    }

                }
                // if a card has been matched, plays the selected card
                if (matchFlag == true)
                {
                    //sets a cardbox object to be the selected card
                    CardBox.CardBox newCardBox = new CardBox.CardBox(attacker.playerHand.GetCard(int.Parse(txtHandInput.Text)));

                    //plays the card from the hand to the field
                    playingField.cardPlayed(attacker.playerHand.playCard(int.Parse(txtHandInput.Text)));

                    //checks to see if the attacker is player 1
                    if (attacker == player1)
                    {
                        //removes the card from the hand display list
                        cards.RemoveAt(int.Parse(txtHandInput.Text));

                        //resets the hand display
                        this.pnPlayerHand.Controls.Clear();

                        //loops through the hand 
                        for (int i = attacker.playerHand.gethandSize() - 1; i >= 0; i--)
                        {
                            //sets an offset on the hand
                            cards[i].Left = (i * 20) + 100;
                            //adds the cards to the display
                            this.pnPlayerHand.Controls.Add(cards[i]);

                        }
                    }
                    else
                    {
                        //refreshes the text for the player AI's hand (TEMP)
                        lblAIhand.Text = attacker.playerHand.displayHandGUI();
                    }

                    //adds the cards to the field cards list
                    fieldCards.Add(newCardBox);

                    //refreshes the field display to add the new card
                    DisplayPlayingField();
                    //sets the message to the next player's turn
                    lblPlayerTurn.Text = defender.playerName + "'s turn.";
                    //sets the next roung
                    round = DEFENDERTURN;
                    //resets the match flag
                    matchFlag = false;
                    //resets the error message
                    lblErrorMsg.Text = "";
                }
                //otherwise displays a message telling them the cards cannot be played and breaks out of this loop
                else
                {
                    lblErrorMsg.Text = "You can only play a card of the same rank as the cards on the field";

                }

            }

            //////////////////////////END OF ROUND LOGIC STARTS HERE ///////////////////////////////////////////////

        }

    }
}
