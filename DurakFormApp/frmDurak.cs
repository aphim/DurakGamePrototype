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
            bool playAgain = false;
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

        private void InitialAttTurn()
        {

            attacker.AttackerInitialTurnForm(playingField, trumpCard, txtHandInput.Text);


            turnCounter++;

        }

        private void btnPlayCard_Click(object sender, EventArgs e)
        {
            CardBox.CardBox newCardBox = new CardBox.CardBox(player1.playerHand.GetCard(int.Parse(txtHandInput.Text)));

            playingField.cardPlayed(player1.playerHand.playCard(int.Parse(txtHandInput.Text)));

            cards.RemoveAt(int.Parse(txtHandInput.Text));

            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();

            this.pnPlayerHand.Controls.Clear();

            for (int i = player1.playerHand.gethandSize() - 1; i >= 0; i--)
            {
                cards[i].Left = (i * 20) + 100;
                this.pnPlayerHand.Controls.Add(cards[i]);

            }

            fieldCards.Add(newCardBox);
            lblDeckSizeValue.Text = myDeck.getCardsRemaining().ToString();

            //AddToPLayingField();
            DisplayPlayingField();
        }























        ////checks to see if the player wins during the endgame
        //if (endGame)
        //{
        //    //check player hand size
        //    if (attacker.playerHand.gethandSize() == 0)
        //    {
        //        //declares winner if conditions are met
        //        Console.WriteLine(attacker.playerName + " Wins!");
        //        playerPassed.passFlag = true;
        //        turnFlag = false;
        //    }
        //}


        //                do ///Loop for the attack and defence chain rounds
        //                {
        //                    /////////////////////////////////////// (defender) turn /////////////////////////////////////////////////////////////

        //                    //checks if a player has passed their turn
        //                    if (!playerPassed.passFlag)
        //                    {
        //                        Console.WriteLine("");
        //                        //calls the defender's turn method
        //                        defender.DefenderTurn(playingField, trumpCard, playerPassed);
        //                        Console.WriteLine("");
        //                        Console.WriteLine("The cards on the field are:");
        //                        playingField.displayField();
        //                    }

        //                    //checks to see if the player wins during the endgame
        //                    if (endGame)
        //                    {
        //                        //checks defender's hand size
        //                        if (defender.playerHand.gethandSize() == 0)
        //                        {
        //                            //declares winner if conditions are met
        //                            Console.WriteLine(defender.playerName + " Wins!");
        //                            playerPassed.passFlag = true;
        //                            turnFlag = false;
        //                        }
        //                    }

        //                    turnCounter++;

        ////checks to see the number of turns that has gone.
        //if (turnCounter <= 6)
        //{

        //    /////////////////////////////////////// Attacker's standard turn /////////////////////////////////////////////////////////////

        //    //check if a player has passed
        //    if (!playerPassed.passFlag)
        //    {
        //        Console.WriteLine("");
        //        //call attackers' standard turn method
        //        attacker.AttackerTurn(playingField, playerPassed, trumpCard);
        //        Console.WriteLine("");
        //        Console.WriteLine("The cards on the field are:");
        //        playingField.displayField();

        //    }

        //    //checks to see if the player wins during the endgame
        //    if (endGame)
        //    {
        //        //checks attacker's hand size
        //        if (attacker.playerHand.gethandSize() == 0)
        //        {
        //            //declares winner if condition has been met
        //            Console.WriteLine(attacker.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //    }
        //}
        //                    //loops as long as neither side passes and that it hasn't reached 6 turns
        //                } while (!playerPassed.passFlag || turnCounter < 7) ;




        ////////////////////////////////// end of round logic ///////////////////////////////////////////////////////////////////////

        ////checks if attackerwin flag has been tripped
        //if (playerPassed.attackerWin)
        //{
        //    //creates an arraylist with all the field cards
        //    ArrayList cardsToBePickedUp = playingField.pickupField();

        //    //adds all the cards in the arraylist to the defender's hand
        //    for (int i = 0; i < cardsToBePickedUp.Count; i++)
        //    {
        //        defender.playerHand.addCard((Card)cardsToBePickedUp[i]);
        //    }

        //    //sets the roles for the next round
        //    playerAI = (AI)defender;
        //    player1 = attacker;


        //    /////DRAW CARDS///// (TODO FURTHER TESTING NEEDED FOR EXCEEDING MAXIMUM ROUNDS BUG)
        //    //draws back up to 6 cards in hand if necessary/possible attackers first

        //    //loop until minimum hand size is reached for attacker (*Note attackers draw first)
        //    player1.DrawCards(myDeck);


        //    //sets the endgame flag if deck reaches 0 cards
        //    if (myDeck.getCardsRemaining() == 0)
        //    {
        //        endGame = true;
        //    }

        //    //checks to see if any player wins during the endgame
        //    if (endGame)
        //    {
        //        //checks their hand size
        //        if (player1.playerHand.gethandSize() == 0)
        //        {
        //            //declare winner
        //            Console.WriteLine(player1.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //        else if (playerAI.playerHand.gethandSize() == 0)
        //        {
        //            Console.WriteLine(playerAI.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //    }


        //    //loop until minimum hand size is reached for defender (*Note defender always draws second)
        //    playerAI.DrawCards(myDeck);


        //    //sets the endgame flag
        //    if (myDeck.getCardsRemaining() == 0)
        //    {
        //        endGame = true;
        //    }

        //    //checks to see if attacker wins during the endgame
        //    if (endGame)
        //    {
        //        //checks hand size
        //        if (playerAI.playerHand.gethandSize() == 0)
        //        {
        //            //declare winner
        //            Console.WriteLine(playerAI.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //        else if (player1.playerHand.gethandSize() == 0)
        //        {
        //            Console.WriteLine(player1.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //    }


        //    //resets the loop and attacker is the same player
        //    playerPassed.passFlag = false;
        //    playerPassed.attackerWin = false;
        //    turnCounter = 0;

        //    attacker = player1;
        //    defender = playerAI;

        //    Console.Clear();


        //    Console.WriteLine("");
        //    Console.WriteLine(player1.playerName + "'s new hands is:");
        //    player1.playerHand.displayHand();
        //    Console.WriteLine("");
        //    Console.WriteLine(playerAI.playerName + "'s new hands is:");
        //    playerAI.playerHand.displayHand();
        //}

        ////checks to see if defenderwin flag has been tripped
        //if (playerPassed.defenderWin)
        //{
        //    /////Discard Field Cards //////
        //    //field cards get discarded
        //    playingField.discardField();

        //    playerAI = (AI)defender;
        //    player1 = attacker;


        //    /////DRAW CARDS/////
        //    //draws back up to 6 cards in hand if necessary/possible attackers first

        //    //loop until minimum hand size is reached for attacker (*Note attackers draw first)
        //    player1.DrawCards(myDeck);


        //    //sets the endgame flag
        //    if (myDeck.getCardsRemaining() == 0)
        //    {
        //        endGame = true;
        //    }

        //    //checks to see if attacker wins during the endgame
        //    if (endGame)
        //    {
        //        //checks player's hand size
        //        if (player1.playerHand.gethandSize() == 0)
        //        {
        //            //declares winner
        //            Console.WriteLine(player1.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //        else if (playerAI.playerHand.gethandSize() == 0)
        //        {
        //            Console.WriteLine(playerAI.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //    }

        //    //loop until minimum hand size is reached for defender (*Note defender always draws second)
        //    playerAI.DrawCards(myDeck);


        //    //sets the endgame flag
        //    if (myDeck.getCardsRemaining() == 0)
        //    {
        //        endGame = true;
        //    }

        //    //checks to see if attacker wins during the endgame
        //    if (endGame)
        //    {
        //        //checks players' handsize
        //        if (playerAI.playerHand.gethandSize() == 0)
        //        {
        //            //declare winner
        //            Console.WriteLine(playerAI.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //        else if (player1.playerHand.gethandSize() == 0)
        //        {
        //            Console.WriteLine(player1.playerName + " Wins!");
        //            playerPassed.passFlag = true;
        //            turnFlag = false;
        //        }
        //    }


        //    //defender is the new attacker (swap roles)(
        //    playerPassed.passFlag = false;
        //    playerPassed.defenderWin = false;
        //    turnCounter = 0;


        //    defender = player1;
        //    attacker = playerAI;

        //    Console.Clear();

        //    Console.WriteLine("");
        //    Console.WriteLine(player1.playerName + "'s new hands is:");
        //    player1.playerHand.displayHand();
        //    Console.WriteLine("");
        //    Console.WriteLine(playerAI.playerName + "'s new hands is:");
        //    playerAI.playerHand.displayHand();
        //}



        //            } while (turnFlag) ; //loops the program until the game ends by someone winning

        //}


    }
}
