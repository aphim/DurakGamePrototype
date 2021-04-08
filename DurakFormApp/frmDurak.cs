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
using System.IO;

namespace DurakFormApp
{
    public partial class frmDurak : Form
    {
        //Global variables used to store a number of properties used in the game
        private List<CardBox.CardBox> cards = new List<CardBox.CardBox>();
        private List<CardBox.CardBox> cardsAI = new List<CardBox.CardBox>();
        private List<CardBox.CardBox> fieldCards = new List<CardBox.CardBox>();
        //create a deck object
        private Deck myDeck = new Deck();
        // create player objects
        private AI playerAI = new AI("AI");
        private Player player1;
        //create an attacker and a defender
        Player attacker;
        Player defender;
        //create a variable for the current player
        Player currentPlayer;
        //setup counters and flags for the game logic
        int turnCounter = 0;
        bool endGame = false;
        bool perevodnoyFlag = false;
        PassFlag playerPassed = new PassFlag();
        //initialize the field
        Field playingField = new Field();
        //declare a trump card object
        Card trumpCard;
        //Constants used to determine which part of the round it is currently
        const string ATTACKINITIAL = "initialTurn";
        const string ATTACKERTURN = "attacker";
        const string DEFENDERTURN = "defender";
        //initialize the round variable
        string round = ATTACKINITIAL;
        //initialize a match flag
        bool matchFlag = false;
        int playerCardIndex=0;
        bool showAIHand = false;
        int playerOffset = 215;
        int aiOffset = 215;
        DateTime today = DateTime.Now;
        const int MAXATTACKCHAIN = 6;

        int AICardIndex = 0;

        string filePath = @"../../GameLog.txt";

        public frmDurak()
        {
            InitializeComponent();
            //initiate variables
            //bool playAgain = false;
            playerPassed.passFlag = false;
            playerPassed.attackerWin = false;
            playerPassed.defenderWin = false;
            btnSkipTurn.Enabled = false;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Application has started");
                writer.WriteLine("Game Started At: " + today.ToString("F"));
            }
            

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
        /// On form load, displays cards values in card box objects and various variables that we may need
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

            //enables and disables buttons
            btnPlayCard.Enabled = false;
            btnDiscardPile.Enabled = false;
            btnSkipTurn.Enabled = false;
            btnStart.Visible = true;
            txtNameInput.Visible = true;
            chkAIHandToggle.Enabled = false;
           
        }

        /// <summary>
        /// Start game button that initializes the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            myDeck = new Deck();
            turnCounter = 0;
            endGame = false;
            cards = new List<CardBox.CardBox>();
            cardsAI = new List<CardBox.CardBox>();
            fieldCards = new List<CardBox.CardBox>();
            round = ATTACKINITIAL;
            matchFlag = false;
            pnPlayerHand.Controls.Clear();
            pnAIHand.Controls.Clear();
            cards.Clear();
            playingField = new Field();
            pnPlayingField.Controls.Clear();
            cbTrumpCard.Visible = true;
            cardBox1.Visible = true;
            chkAIHandToggle.Enabled = true;
         

            //shuffle deck
            myDeck.Shuffle();

            if(String.IsNullOrEmpty(txtNameInput.Text))
            {
               player1 = new Player("Player 1");
            }
            else
            {
                player1 = new Player(txtNameInput.Text);
            }


          /*  using (StreamWriter writer = new StreamWriter(filePath,true))
            {
                writer.WriteLine("Name has been set to " + player1.playerName);
            }*/

            //Reset variables for a new game
            player1.playerHand = new Hand(myDeck);
            playerAI.playerHand = new Hand(myDeck);


            //get the trump card
            trumpCard = myDeck.getTrumpcard();
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();
            this.cbTrumpCard.Card = trumpCard;

            //Prints the trump card and players hands to the log file
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Trump Card: " + trumpCard.ToString());
                writer.WriteLine(player1.playerName+ "'s hand:" + player1.playerHand.displayHandGUI());
                writer.WriteLine(playerAI.playerName + "'s hand:" + playerAI.playerHand.displayHandGUI());
            }

            //1.Create cardbox controls 2.display them on the screen 3.Determine the starting player
            CreateControls();
            DisplayControls();
            DetermineStartingPlayer();

            if (attacker == player1)
            {
                lblPlayerTurn.Text = player1.playerName + " is the initial attacker.";
                btnSkipTurn.Enabled = false;
                writeGameLog(player1.playerName + " is the initial attacker.");
            }
            else
            {
               
                AICardIndex = playerAI.AITurnCycle(trumpCard, playingField, round, perevodnoyFlag);
                writeGameLog(playerAI.playerName + " is the initial attacker.");
                TurnCycle();
               
            }

            btnPlayCard.Enabled = true;
            btnDiscardPile.Enabled = true;
            btnStart.Visible = false;
            txtNameInput.Visible = false;
     
            
        }

        /// <summary>
        /// Creates a list of CardBox controls and updates deck size value
        /// </summary>
        private void CreateControls()
        {
            for (int i = 0; i < player1.playerHand.gethandSize(); i++)
            {
                CardBox.CardBox newCardBox = new CardBox.CardBox(player1.playerHand.GetCard(i));
                newCardBox.Click += CardBox_Click;// Wire CardBox_Click
                cards.Add(newCardBox);
            }
            for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
            {
                CardBox.CardBox newCardBox = new CardBox.CardBox(playerAI.playerHand.GetCard(i));
                cardsAI.Add(newCardBox);
            }
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();
        }

        /// <summary>
        /// Displays the CardBox controls for a CardBox list
        /// </summary>
        private void DisplayControls()
        {
            playerOffset = playerOffset - (player1.playerHand.gethandSize() - 6) * 20 / 100;
            aiOffset = aiOffset - (playerAI.playerHand.gethandSize() - 6) * 20 / 100;
            //Decrements because incrementing will overlap cards in a false way 
            for (int i = player1.playerHand.gethandSize() - 1; i >= 0; i--)
            {
                cards[i].Left = (i * 20) + playerOffset;
                this.pnPlayerHand.Controls.Add(cards[i]);
            }
            //Decrements because incrementing will overlap cards in a false way TODO (CHECK THIS)
            for (int i = playerAI.playerHand.gethandSize() - 1; i >= 0; i--)
            {
                cardsAI[i].Left = (i * 20) + aiOffset;
                cardsAI[i].FaceUp = showAIHand;
                this.pnAIHand.Controls.Add(cardsAI[i]);
            }
        }

        /// <summary>
        /// Displays the playing field updating playing field panel
        /// </summary>
        private void DisplayPlayingField()
        {
            ArrayList cardsToAdd = playingField.getField();
            for (int i = cardsToAdd.Count - 1; i >= 0; i--)
            {
                fieldCards[i].Left = (i * 20) + 100;
                fieldCards[i].FaceUp = true;
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
                currentPlayer = attacker;
            }
            else
            {
                defender = player1;
                attacker = playerAI;
                currentPlayer = attacker;
            }

        }


        /////////////////////////////////////////////// This Section will be used for the turn cycling ///////////////////////////////////////////////

        /// <summary>
        /// Event handler for on click of the playcard button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayCard_Click(object sender, EventArgs e)
        {
            TurnCycle();
            playerCardIndex = 0;
            Card aCard = player1.playerHand.GetCard(playerCardIndex);

            lblCardSelected.Text = aCard.ToString();
        }

        /// <summary>
        /// This function holds the logic for the turn cycling of the game
        /// </summary>
        private void TurnCycle()
        {
            //update the deck size
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();

            //////////////////////////// The attacking player's initial turn (no restrictions on playable cards) /////////////////////////////
            if (round == ATTACKINITIAL)
            {
                if (attacker.playerHand.gethandSize() > 0)
                {
                    CardBox.CardBox newCardBox;
                    
                    if (attacker == player1)
                    {
                        //Creates a new cardbox object that will be set to the card the player selects
                        newCardBox = new CardBox.CardBox(attacker.playerHand.GetCard(playerCardIndex));
                        
                        //Prints the card played for player initial attack to the log
                        writeGameLog(attacker.playerName + " attacked with: " + attacker.playerHand.GetCard(playerCardIndex).ToString()); 

                        //The card is played from the player's hand (removed) and played onto the field (added)
                        playingField.cardPlayed(attacker.playerHand.playCard(playerCardIndex));

                        

                    }
                    else
                    {

                        //Creates a new cardbox object that will be set to the card the player selects
                        newCardBox = new CardBox.CardBox(attacker.playerHand.GetCard(AICardIndex));

                        newCardBox.FaceUp = true;


                        //Prints the card played for AI initial attack to the log
                        writeGameLog(attacker.playerName + " attacked with: " + attacker.playerHand.GetCard(AICardIndex).ToString());

                        //The card is played from the player's hand (removed) and played onto the field (added)

                        playingField.cardPlayed(attacker.playerHand.playCard(AICardIndex));

                        


                    }
                   
                    //refreshes attacker's hands and display
                    AttackerHandRefresh();

                    //Cards that are played are added to the field output
                    fieldCards.Add(newCardBox);
                    
                }
              
                DisplayPlayingField();
                
                //sets the endgame flag
                if (myDeck.getCardsRemaining() == 0)
                {
                    endGame = true;
                }

                //checks to see if a player wins during the endgame
                if (endGame)
                {
                    EndGameCheck();
                }

                //sets the next turn in the round
                round = DEFENDERTURN;
                //changes current player to the defender
                currentPlayer = defender;
                perevodnoyFlag = true;
                //changes the message to reflect the next player's turn
                lblPlayerTurn.Text = currentPlayer.playerName + " is defending.";
                writeGameLog(defender.playerName + " is defending.");
                lblPlayerTurn.Refresh();


                if (player1.playerHand.gethandSize() != 0 && playerAI.playerHand.gethandSize() != 0)
                 {
                     //enables the skip turn button
                     btnSkipTurn.Enabled = true;

                 }

            }
            //////////////////////////////// The defender's turn (can only play cards of the same suit higher rank or trump suit on non-trumps)////////////////////////////////////////////
            else if (round == DEFENDERTURN)
            {
                if (defender.playerHand.gethandSize() > 0)
                {
                    Card cardSelected;
                    if (defender == player1)
                    {
                        //Sets a variable for the currently selected card used for comparsions
                        cardSelected = defender.playerHand.GetCard(playerCardIndex);
                    }
                    else
                    {
                        //Sets a variable for the currently selected card used for comparsions
                        cardSelected = defender.playerHand.GetCard(AICardIndex);
                    }
                    //Sets a variable for the current card in the playing field
                    Card currentCard = playingField.getCurrentCard();

                    ///////////////////////////////// Perevodnoy additional rules BELOW//////////////////////////////////////////////////
                    if (turnCounter == 0 && cardSelected.rank == currentCard.rank)
                    {
                        CardBox.CardBox newCardBox;
                        if (defender == player1)
                        {
                            //Creates a new cardbox object that will be set to the card the player selects
                            newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(playerCardIndex));

                            //Prints the card played for player defense to the log
                            writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(playerCardIndex).ToString());
                            //The card is played from the player's hand (removed) and played onto the field (added)
                            playingField.cardPlayed(defender.playerHand.playCard(playerCardIndex));
                        }
                        else
                        {
                            //Creates a new cardbox object that will be set to the card the player selects
                            newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(AICardIndex));

                            newCardBox.FaceUp = true;

                            //Prints the card played for AI defense to the log
                            writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(AICardIndex).ToString());

                            //The card is played from the player's hand (removed) and played onto the field (added)
                            playingField.cardPlayed(defender.playerHand.playCard(AICardIndex));
                        }

                        //refreshes defender hand and display
                        DefenderHandRefresh();

                        //adds the cards to the field display object
                        fieldCards.Add(newCardBox);

                        SwapRoles();

                        
                        //add 1 to the counter (for counting 6 rounds)
                        turnCounter++;

                        //refreshes the field to display the new card
                        DisplayPlayingField();
                        perevodnoyFlag = true;
                        //sets the new defender as the next turn
                        round = DEFENDERTURN;
                        //empty error message
                        lblErrorMsg.Text = "";
                        //changes current player to the new made defender
                        currentPlayer = defender;

                        /*  //sets the message for the next player's turn
                          lblPlayerTurn.Text = currentPlayer.playerName + "'s turn as new defender.";
                          Console.WriteLine(currentPlayer.playerName + "'s turn.");*/
                        lblPlayerTurn.Text = currentPlayer.playerName + " is now defending.";
                        writeGameLog("Roles Swapped");
                        writeGameLog(defender.playerName + " is now defending.");
                        lblPlayerTurn.Refresh();

                    }
                    else if (perevodnoyFlag == true && cardSelected.rank == currentCard.rank)
                    {
                        CardBox.CardBox newCardBox;
                        if (defender == player1)
                        {
                            //Creates a new cardbox object that will be set to the card the player selects
                            newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(playerCardIndex));

                            //Prints the card played for player defense to the log
                            writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(playerCardIndex).ToString());
                            //The card is played from the player's hand (removed) and played onto the field (added)
                            playingField.cardPlayed(defender.playerHand.playCard(playerCardIndex));
                        }
                        else
                        {
                            //Creates a new cardbox object that will be set to the card the player selects
                            newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(AICardIndex));

                            newCardBox.FaceUp = true;

                            //Prints the card played for AI defense to the log
                            writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(AICardIndex).ToString());
                            //The card is played from the player's hand (removed) and played onto the field (added)
                            playingField.cardPlayed(defender.playerHand.playCard(AICardIndex));
                        }


                        //refreshes defender hand and display
                        DefenderHandRefresh();

                        //adds the cards to the field display object
                        fieldCards.Add(newCardBox);

                        perevodnoyFlag = true;

                        SwapRoles();

                      /*  //sets the message for the next player's turn
                        lblPlayerTurn.Text = defender.playerName + "'s turn as new defender.";
                        Console.WriteLine(currentPlayer.playerName + "'s turn.");*/
                        //add 1 to the counter (for counting 6 rounds)
                        turnCounter++;

                        //refreshes the field to display the new card
                        DisplayPlayingField();

                        //sets the new defender as the next turn
                        round = DEFENDERTURN;
                        //empty error message
                        lblErrorMsg.Text = "";
                        //changes current player to the new made defender
                        currentPlayer = defender;

                        lblPlayerTurn.Text = currentPlayer.playerName + " is now defending.";
                        writeGameLog(defender.playerName + " is now defending.");
                        lblPlayerTurn.Refresh();
                    }
                    //////////////////////////////////////////////////Perevodnoy additional rules ABOVE//////////////////////////////////////////////////
                    else
                    {

                        //checks the card in hand is equals to the trump suit
                        if (cardSelected.suit.Equals(trumpCard.suit))
                        {
                            //checks to see if the current card on the field is of the trump suit
                            if (currentCard.suit.Equals(trumpCard.suit))
                            {
                                //if the selected card rank is higher than the current card rank
                                if (cardSelected.rank > currentCard.rank)
                                {
                                    CardBox.CardBox newCardBox;
                                    if (defender == player1)
                                    {
                                        //Creates a new cardbox object that will be set to the card the player selects
                                        newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(playerCardIndex));

                                        //Writes player defense to the log
                                        writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(playerCardIndex).ToString());

                                        //The card is played from the player's hand (removed) and played onto the field (added)
                                        playingField.cardPlayed(defender.playerHand.playCard(playerCardIndex));
                                    }
                                    else
                                    {
                                        //Creates a new cardbox object that will be set to the card the player selects
                                        newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(AICardIndex));

                                        newCardBox.FaceUp = true;

                                        //Writes AI defense to the log
                                        writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(AICardIndex).ToString());

                                        //The card is played from the player's hand (removed) and played onto the field (added)
                                        playingField.cardPlayed(defender.playerHand.playCard(AICardIndex));
                                    }


                                    //refreshes defender hand and display
                                    DefenderHandRefresh();

                                    //adds the cards to the field display object
                                    fieldCards.Add(newCardBox);

                                    //refreshes the field to display the new card
                                    DisplayPlayingField();

                                   /* //sets the message for the next player's turn
                                    lblPlayerTurn.Text = attacker.playerName + "'s turn.";
                                    Console.WriteLine(currentPlayer.playerName + "'s turn.");*/
                                    //add 1 to the counter (for counting 6 rounds)
                                    turnCounter++;
                                    //check if max attack chain has been reached
                                    if (turnCounter >= MAXATTACKCHAIN)
                                    {
                                        currentPlayer = attacker;
                                        DefendersWin();
                                    }
                                    else
                                    {

                                        //sets the attacker as the next turn
                                        round = ATTACKERTURN;
                                        //empty error message
                                        lblErrorMsg.Text = "";
                                        //changes current player to attacker
                                        currentPlayer = attacker;
                                        perevodnoyFlag = false;

                                        lblPlayerTurn.Text = currentPlayer.playerName + " is attacking.";
                                        writeGameLog(attacker.playerName + " is attacking.");
                                        lblPlayerTurn.Refresh();
                                    }

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
                                CardBox.CardBox newCardBox;
                                if (defender == player1)
                                {
                                    //Creates a new cardbox object that will be set to the card the player selects
                                    newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(playerCardIndex));

                                    //Writes Player defense to the log
                                    writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(playerCardIndex).ToString());

                                    //The card is played from the player's hand (removed) and played onto the field (added)
                                    playingField.cardPlayed(defender.playerHand.playCard(playerCardIndex));
                                }
                                else
                                {
                                    //Creates a new cardbox object that will be set to the card the player selects
                                    newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(AICardIndex));

                                    newCardBox.FaceUp = true;
                                    //Writes AI defense to the log
                                    writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(AICardIndex).ToString());
                                    //The card is played from the player's hand (removed) and played onto the field (added)
                                    playingField.cardPlayed(defender.playerHand.playCard(AICardIndex));
                                }

                                //refreshes defender hand and display
                                DefenderHandRefresh();

                                //adds the cards to the new field display list
                                fieldCards.Add(newCardBox);


                                //Refreshes the field display to add the new cards
                                DisplayPlayingField();

                             
                                //increment the counter
                                turnCounter++;
                                //check if max attack chain has been reached
                                if (turnCounter >= MAXATTACKCHAIN)
                                {
                                    currentPlayer = attacker;
                                    DefendersWin();
                                }
                                else
                                {
                                    //sets the attacker as the next turn
                                    round = ATTACKERTURN;
                                    //empty error message
                                    lblErrorMsg.Text = "";
                                    //changes current player to attacker
                                    currentPlayer = attacker;
                                    perevodnoyFlag = false;

                                    lblPlayerTurn.Text = currentPlayer.playerName + " is attacking.";
                                    writeGameLog(attacker.playerName + " is attacking.");
                                    lblPlayerTurn.Refresh();
                                }
                            }
                        }
                        //checks to see if played card suit is the field card suit
                        else if (cardSelected.suit == currentCard.suit)
                        {
                            //checks to see if played card rank is higher than field card rank
                            if (cardSelected.rank > currentCard.rank)
                            {
                                CardBox.CardBox newCardBox;
                                if (defender == player1)
                                {
                                    //Creates a new cardbox object that will be set to the card the player selects
                                    newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(playerCardIndex));
                                    //Writes Player defense to the log
                                    writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(playerCardIndex).ToString());
                                    //The card is played from the player's hand (removed) and played onto the field (added)
                                    playingField.cardPlayed(defender.playerHand.playCard(playerCardIndex));
                                }
                                else
                                {
                                    //Creates a new cardbox object that will be set to the card the player selects
                                    newCardBox = new CardBox.CardBox(defender.playerHand.GetCard(AICardIndex));

                                    newCardBox.FaceUp = true;

                                    //Writes AI defense to the log
                                    writeGameLog(defender.playerName + " defended with: " + defender.playerHand.GetCard(AICardIndex).ToString());

                                    //The card is played from the player's hand (removed) and played onto the field (added)
                                    playingField.cardPlayed(defender.playerHand.playCard(AICardIndex));
                                }

                                //refreshes defender hand and display
                                DefenderHandRefresh();

                                //Adds the new cards into the field display list
                                fieldCards.Add(newCardBox);

                                //refreshes the field display to display the new cards
                                DisplayPlayingField();

                              
                                //increments the counter
                                turnCounter++;
                                //check is max attack chain has been reached
                                if (turnCounter >= MAXATTACKCHAIN)
                                {
                                    currentPlayer = attacker;
                                    DefendersWin();
                                }
                                else
                                {
                                    //sets the attacker as the next turn
                                    round = ATTACKERTURN;
                                    //empty error message
                                    lblErrorMsg.Text = "";
                                    //changes current player to attacker
                                    currentPlayer = attacker;
                                    perevodnoyFlag = false;

                                    lblPlayerTurn.Text = currentPlayer.playerName + " is attacking.";
                                    writeGameLog(attacker.playerName + " is attacking.");
                                    lblPlayerTurn.Refresh();
                                }
                                //sets the message for the next player's turn
                                /*lblPlayerTurn.Text = attacker.playerName + "'s turn.";
                                Console.WriteLine(currentPlayer.playerName + "'s turn.");*/
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
                }

                //sets the endgame flag
                if (myDeck.getCardsRemaining() == 0)
                {
                    endGame = true;
                }

                //checks to see if a player wins during the endgame
                if (endGame)
                {
                    EndGameCheck();
                }


            }
            ///////////////////////////////////ATTACKER STANDARD TURN///////////////////////////////////////////
            else if (round == ATTACKERTURN)
            {
                if (attacker.playerHand.gethandSize() > 0)
                {
                    if (turnCounter < MAXATTACKCHAIN)
                    {
                        //creates a variable that holds the currently selected card
                        Card cardSelected;
                        if (attacker == player1)
                        {
                            cardSelected = attacker.playerHand.GetCard(playerCardIndex);
                        }
                        else
                        {
                            cardSelected = attacker.playerHand.GetCard(AICardIndex);
                        }

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
                            CardBox.CardBox newCardBox;
                            if (attacker == player1)
                            {
                                //Creates a new cardbox object that will be set to the card the player selects
                                newCardBox = new CardBox.CardBox(attacker.playerHand.GetCard(playerCardIndex));

                                //Writes Player attack to the log
                                writeGameLog(attacker.playerName + " attacked with: " + attacker.playerHand.GetCard(playerCardIndex).ToString());

                                //The card is played from the player's hand (removed) and played onto the field (added)
                                playingField.cardPlayed(attacker.playerHand.playCard(playerCardIndex));
                            }
                            else
                            {
                                //Creates a new cardbox object that will be set to the card the player selects
                                newCardBox = new CardBox.CardBox(attacker.playerHand.GetCard(AICardIndex));

                                //Writes Player attack to the log
                                writeGameLog(attacker.playerName + " attacked with: " + attacker.playerHand.GetCard(AICardIndex).ToString());

                                //The card is played from the player's hand (removed) and played onto the field (added)
                                playingField.cardPlayed(attacker.playerHand.playCard(AICardIndex));
                            }

                            //refreshes attacker's hands and display
                            AttackerHandRefresh();

                            //adds the cards to the field cards list
                            fieldCards.Add(newCardBox);

                            //refreshes the field display to add the new card
                            DisplayPlayingField();

                           
                            //sets the next roung
                            round = DEFENDERTURN;

                            /*//sets the message to the next player's turn
                            lblPlayerTurn.Text = currentPlayer.playerName + "'s turn.";*/

                            //resets the match flag
                            matchFlag = false;
                            //resets the error message
                            lblErrorMsg.Text = "";
                            //sets the current player to the defender
                            currentPlayer = defender;
                            perevodnoyFlag = false;

                            //sets the message to the next player's turn
                            lblPlayerTurn.Text = currentPlayer.playerName + " is defending.";
                            writeGameLog(defender.playerName + " is defending.");
                        }
                        //otherwise displays a message telling them the cards cannot be played and breaks out of this loop
                        else
                        {
                            lblErrorMsg.Text = "You can only play a card of the same rank as the cards on the field";

                        }
                    }
                }
                //sets the endgame flag
                if (myDeck.getCardsRemaining() == 0)
                {
                    endGame = true;
                }

                //checks to see if a player wins during the endgame
                if (endGame)
                {
                    EndGameCheck();
                }
            }

            //////////////////////////END OF ROUND LOGIC STARTS HERE ///////////////////////////////////////////////

        }

        private void AttackersWin()
        {

            //creates an arraylist with all the field cards
            ArrayList cardsToBePickedUp = playingField.pickupField();

            //adds the cards to the field display object
            fieldCards.Clear();
            this.pnPlayingField.Controls.Clear();

            DisplayPlayingField();


            /////DRAW CARDS///// (TODO FURTHER TESTING NEEDED FOR EXCEEDING MAXIMUM ROUNDS BUG)
            //draws back up to 6 cards in hand if necessary/possible attackers first

            if (attacker == player1)
            {
                //loop until minimum hand size is reached for attacker (*Note attackers draw first)
                player1.DrawCards(myDeck);

                //reset the display
                this.pnPlayerHand.Controls.Clear();

                //clears the output list for player1 hand
                cards.Clear();

                //loops through and adds the player's hands to the hand display
                for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                {
                    CardBox.CardBox newCardBox = new CardBox.CardBox(player1.playerHand.GetCard(i));
                    newCardBox.Click += CardBox_Click;// Wire CardBox_Click
                    cards.Add(newCardBox);
                }

                DisplayControls();
            }
            else
            {
                //draws cards back to a full hand
                playerAI.DrawCards(myDeck);
                //reset the display
                this.pnAIHand.Controls.Clear();

                //clears the output list for player1 hand
                cardsAI.Clear();

                //loops through and adds the player's hands to the hand display
                for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
                {
                    CardBox.CardBox newCardBox = new CardBox.CardBox(playerAI.playerHand.GetCard(i));
                    cardsAI.Add(newCardBox);
                }

                DisplayControls();
            }

            //sets the endgame flag
            if (myDeck.getCardsRemaining() == 0)
            {
                endGame = true;
            }

            //checks to see if a player wins during the endgame
            if (endGame)
            {
                EndGameCheck();
            }


            if (defender == player1)
            {
                //adds all the cards in the pickup list to the defender's hand
                for (int i = 0; i < cardsToBePickedUp.Count; i++)
                {
                    player1.playerHand.addCard((Card)cardsToBePickedUp[i]);
                }

                //loop until minimum hand size is reached for defender (*Note defender always draws second)
                player1.DrawCards(myDeck);

                //reset the display
                this.pnPlayerHand.Controls.Clear();

                //clears the output list for player1 hand
                cards.Clear();

                //loops through and adds the player's hands to the hand display
                for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                {
                    CardBox.CardBox newCardBox = new CardBox.CardBox(player1.playerHand.GetCard(i));
                    newCardBox.Click += CardBox_Click;// Wire CardBox_Click
                    cards.Add(newCardBox);
                }

                DisplayControls();
            }
            else
            {
                //adds all the cards in the pickup list to the defender's hand
                for (int i = 0; i < cardsToBePickedUp.Count; i++)
                {
                    playerAI.playerHand.addCard((Card)cardsToBePickedUp[i]);
                }
                
                //draws hand back to 6
                playerAI.DrawCards(myDeck);

                //reset the display
                this.pnAIHand.Controls.Clear();

                //clears the output list for player1 hand
                cardsAI.Clear();

                //loops through and adds the player's hands to the hand display
                for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
                {
                    CardBox.CardBox newCardBox = new CardBox.CardBox(playerAI.playerHand.GetCard(i));
                    cardsAI.Add(newCardBox);
                }

                DisplayControls();
            }


            //sets the endgame flag
            if (myDeck.getCardsRemaining() == 0)
            {
                endGame = true;
            }

            //checks to see if a player wins during the endgame
            if (endGame)
            {
                EndGameCheck();
            }

            //check the current roles and swaps them
            if (attacker == player1)
            {
                defender = playerAI;
                attacker = player1;
            }
            else if (attacker == playerAI)
            {
                defender = player1;
                attacker = playerAI;
            }

            //reset counters and attributes
            turnCounter = 0;
            currentPlayer = attacker;
            round = ATTACKINITIAL;
            perevodnoyFlag = false;
            lblPlayerTurn.Text = currentPlayer.playerName + " is still attacker.";
            writeGameLog(currentPlayer.playerName + " is still attacker.");
            //disable the skip turn function
            btnSkipTurn.Enabled = false;
            playerCardIndex = 0;
            lblCardSelected.Text = player1.playerHand.GetCard(playerCardIndex).ToString();

            //update the deck size
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();


        }
        private void DefendersWin()
        {

            /////Discard Field Cards //////
            //field cards get discarded
            playingField.discardField();

            //adds the cards to the field display object
            fieldCards.Clear();
            this.pnPlayingField.Controls.Clear();

            DisplayPlayingField();


            /////DRAW CARDS/////
            //draws back up to 6 cards in hand if necessary/possible attackers first

            if (attacker == player1)
            {
                //loop until minimum hand size is reached for attacker (*Note attackers draw first)
                player1.DrawCards(myDeck);

                //reset the display
                this.pnPlayerHand.Controls.Clear();

                //clears the output list for player1 hand
                cards.Clear();

                //loops through and adds the player's hands to the hand display
                for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                {
                    CardBox.CardBox newCardBox = new CardBox.CardBox(player1.playerHand.GetCard(i));
                    newCardBox.Click += CardBox_Click;// Wire CardBox_Click
                    cards.Add(newCardBox);
                }

                DisplayControls();
            }
            else
            {
                //draws cards
                playerAI.DrawCards(myDeck);

                //reset the display
                this.pnAIHand.Controls.Clear();

                //clears the output list for player1 hand
                cardsAI.Clear();

                //loops through and adds the player's hands to the hand display
                for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
                {
                    CardBox.CardBox newCardBox = new CardBox.CardBox(playerAI.playerHand.GetCard(i));
                    cardsAI.Add(newCardBox);
                }

                DisplayControls();
            }


            //sets the endgame flag
            if (myDeck.getCardsRemaining() == 0)
            {
                endGame = true;
                
            }

            //checks to see if a player wins during the endgame
            if (endGame)
            {
                EndGameCheck();
            }


            if (defender == player1)
            {
                //loop until minimum hand size is reached for defender (*Note defender always draws second)
                player1.DrawCards(myDeck);

                //reset the display
                this.pnPlayerHand.Controls.Clear();

                //clears the output list for player1 hand
                cards.Clear();

                //loops through and adds the player's hands to the hand display
                for (int i = 0; i < player1.playerHand.gethandSize(); i++)
                {
                    CardBox.CardBox newCardBox = new CardBox.CardBox(player1.playerHand.GetCard(i));
                    newCardBox.Click += CardBox_Click;// Wire CardBox_Click
                    cards.Add(newCardBox);
                }

                DisplayControls();
            }
            else
            {
                //draws cards
                playerAI.DrawCards(myDeck);

                //reset the display
                this.pnAIHand.Controls.Clear();

                //clears the output list for player1 hand
                cardsAI.Clear();

                //loops through and adds the player's hands to the hand display
                for (int i = 0; i < playerAI.playerHand.gethandSize(); i++)
                {
                    CardBox.CardBox newCardBox = new CardBox.CardBox(playerAI.playerHand.GetCard(i));
                    cardsAI.Add(newCardBox);
                }

                DisplayControls();
            }


            //sets the endgame flag
            if (myDeck.getCardsRemaining() == 0)
            {
                endGame = true;
            }

            //checks to see if a player wins during the endgame
            if (endGame)
            {
                EndGameCheck();
            }

            //checks the roles and swaps them
            SwapRoles();


            //reset counters and attributes
            turnCounter = 0;
            currentPlayer = attacker;
            round = ATTACKINITIAL;
            perevodnoyFlag = false;
            lblPlayerTurn.Text = currentPlayer.playerName + " is the new attacker.";
            writeGameLog(attacker.playerName + " is the new attacker.");
            //disable the skip turn function
            btnSkipTurn.Enabled = false;

            //update the deck size
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();

        }

        /// <summary>
        /// Function that refreshes the hand and display on an attacker's turn
        /// </summary>
        private void AttackerHandRefresh()
        {
            //checks if the attacker is the player1 or player AI
            if (attacker == player1)
            {
                //removes the card from the list that displays the hand of the player
                cards.RemoveAt(playerCardIndex);


                //resets the list to remove the existing display
                this.pnPlayerHand.Controls.Clear();

                DisplayControls();
            }
            else
            {
                //removes the card from the list that displays the hand of the player
                cardsAI.RemoveAt(AICardIndex);


                //resets the list to remove the existing display
                this.pnAIHand.Controls.Clear();

                DisplayControls();
            }
        }

        /// <summary>
        /// Function that refreshes defender hand and display
        /// </summary>
        private void DefenderHandRefresh()
        {
            if (defender == player1)
            {
                //removes the card from the hand display
                cards.RemoveAt(playerCardIndex);

                //reset the display
                this.pnPlayerHand.Controls.Clear();

                DisplayControls();

            }
            else
            {
                //removes the card from the list that displays the hand of the player
                cardsAI.RemoveAt(AICardIndex);

                //resets the list to remove the existing display
                this.pnAIHand.Controls.Clear();

                DisplayControls();

            }

        }


        /// <summary>
        /// Function for skipping the turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSkipTurn_Click(object sender, EventArgs e)
        {
            //checks the current player and runs the corresponding end of round functions based on whom won
            if(currentPlayer == defender)
            {
                AttackersWin();
            
            }
            else if (currentPlayer == attacker)
            {
                DefendersWin();
            }
            AICardIndex = playerAI.AITurnCycle(trumpCard, playingField, round, perevodnoyFlag);

            TurnCycle();
        }

        /// <summary>
        /// Checks to see if a player has won the game and sends a message box if someone has
        /// </summary>
        public void EndGameCheck()
        {
            //checks the hand size of the AI player to see if it is zero
            if (playerAI.playerHand.gethandSize() == 0)
            {
                //declare winner
                MessageBox.Show("GAME OVER.");
                //lblPlayerTurn.Text = player1.playerName +" loses. Try again?";
                //enables and disables buttons
                btnPlayCard.Enabled = false;
                btnDiscardPile.Enabled = false;
                btnSkipTurn.Enabled = false;
                btnStart.Visible = true;
                txtNameInput.Visible = true;
            }
            //checks the handsize of the player to see if it is zero
            else if (player1.playerHand.gethandSize() == 0)
            {
                //declare the winner
                MessageBox.Show(player1.playerName + " Wins!");
                //lblPlayerTurn.Text = player1.playerName + " wins! Try again?";
                //enables and disables buttons
                btnPlayCard.Enabled = false;
                btnDiscardPile.Enabled = false;
                btnSkipTurn.Enabled = false;
                btnStart.Visible = true;
            }
            cardBox1.Visible = false;
            myDeck = new Deck();
            turnCounter = 0;
            endGame = false;
            cards = new List<CardBox.CardBox>();
            cardsAI = new List<CardBox.CardBox>();
            fieldCards = new List<CardBox.CardBox>();
            round = ATTACKINITIAL;
            matchFlag = false;
            pnPlayerHand.Controls.Clear();
            pnAIHand.Controls.Clear();
            cards.Clear();
            playingField = new Field();
            pnPlayingField.Controls.Clear();
            cbTrumpCard.Visible = true;
            cardBox1.Visible = true;
            chkAIHandToggle.Enabled = true;
        }

        /// <summary>
        /// On click of the discardpile button will send the field object and call a new dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDiscardPile_Click(object sender, EventArgs e)
        {
            //create new isntance of a form object
            frmDiscard frmdiscard = new frmDiscard();
            //send the field to the new form
            frmDiscard.field = playingField;
            //show the form
            frmdiscard.ShowDialog();
        }


        public void SwapRoles()
        {
            //check the current roles and swaps them
            if (defender == player1)
            {
                defender = playerAI;
                attacker = player1;
            }
            else if (defender == playerAI)
            {
                defender = player1;
                attacker = playerAI;
            }
        }

        /// <summary>
        /// Function usd for the click of a cardbox object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CardBox_Click(object sender, EventArgs e)
        {

            // Convert sender to a CardBox
            CardBox.CardBox aCardBox = sender as CardBox.CardBox;

            // If the conversion worked
            if (aCardBox != null)
            {

                for(int i=0; i < player1.playerHand.gethandSize(); i ++)
                {
                    if(aCardBox.Card == player1.playerHand.GetCard(i))
                    {
                        playerCardIndex = i;
                    }
                }
                lblCardSelected.Text = aCardBox.Card.ToString();
                 

            }

        }

        //toggles AI hand to show or hide cards
        private void chkAIHandToggle_CheckedChanged(object sender, EventArgs e)
        {
            //checks if the hand is currently shown or not
            if (showAIHand == false)
            {
                showAIHand = true;
            }
            else
            {
                showAIHand = false;
            }

            //resets the list to remove the existing display
            this.pnAIHand.Controls.Clear();

            //Decrements because incrementing will overlap cards in a false way 
            for (int i = player1.playerHand.gethandSize() - 1; i >= 0; i--)
            {
                cards[i].Left = (i * 20) + playerOffset;
                this.pnPlayerHand.Controls.Add(cards[i]);
            }
            //Decrements because incrementing will overlap cards in a false way 
            for (int i = playerAI.playerHand.gethandSize() - 1; i >= 0; i--)
            {
                cardsAI[i].Left = (i * 20) + aiOffset;
                cardsAI[i].FaceUp = showAIHand;
                this.pnAIHand.Controls.Add(cardsAI[i]);
            }
        }

        /// <summary>
        /// Function that calls an AI turn on the message change of the turns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlayerTurn_TextChanged(object sender, EventArgs e)
        {
            if (currentPlayer == playerAI && round == ATTACKERTURN)
            { 
                if(playerAI.AITurnCycle(trumpCard,playingField,round, perevodnoyFlag) == -1)
                {
                    DefendersWin();
                }
                else
                {
                    perevodnoyFlag = false;
                    AICardIndex = playerAI.AITurnCycle(trumpCard, playingField, round, perevodnoyFlag);
                    TurnCycle();
                }
            }
            if (currentPlayer == playerAI && round == DEFENDERTURN)
            {
                if (playerAI.AITurnCycle(trumpCard, playingField, round, perevodnoyFlag) == -1)
                {
                    //System.Threading.Thread.Sleep(3000);
                    AttackersWin();
                }
                else
                {
                    AICardIndex = playerAI.AITurnCycle(trumpCard, playingField, round, perevodnoyFlag);
                    TurnCycle();
                }
            }
        }

        private void frmDurak_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        public void writeGameLog(string msg)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {

                writer.WriteLine(msg);

            }
        }

    }
}
