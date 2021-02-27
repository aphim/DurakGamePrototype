using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class Player
    {
        public string playerName { get; set; }
        public Hand playerHand { get; set; }
        public Player(string name)
        {
            playerName = name;
        }
        
  

        public void AttackerInitialTurn(Field playingField)
        {
            Console.WriteLine("It is " + this.playerName + "'s Turn.");

            this.playerHand.displayHand();

            int selectedCard;
            bool validCard = false;

            do
            {

                int.TryParse(Console.ReadLine(), out selectedCard);
                selectedCard = selectedCard - 1;
                
                if (selectedCard > this.playerHand.gethandSize() || selectedCard < 0)
                {
                    Console.WriteLine("Invalid option please pick a card.");
                }
                else
                {
                    validCard = true;
                }
            } while (!validCard);

            playingField.cardPlayed(this.playerHand.playCard(selectedCard));



            playingField.displayField();

        }

        public void AttackerTurn(Field playingField, PassFlag passFlag)
        {
            int selectedCard=0;
            bool validCard = false;
            string inputString;
            Console.WriteLine("It is " + this.playerName + "'s Turn.");
            this.playerHand.displayHand();

            do
            {

                inputString = Console.ReadLine();
                if (inputString.Equals("D") || inputString.Equals("d"))
                {
                    playingField.displayDiscarded();
                    inputString = Console.ReadLine();

                }
                if (inputString.Equals("Q") || inputString.Equals("q"))
                {
                    passFlag.passFlag = true;
                    passFlag.defenderWin = true;
                    
                    break;
                }


                int.TryParse(inputString, out selectedCard);
                selectedCard = selectedCard - 1;

                if (selectedCard > this.playerHand.gethandSize() || selectedCard < 0)
                {
                    Console.WriteLine("Invalid option please pick a card.");
                }
                else
                {
                    validCard = true;
                }
            } while (!validCard);



            Card tempCard;
            if (passFlag.passFlag == false)
            {
                Card cardSelected = this.playerHand.selectCard(selectedCard);

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
                        playingField.cardPlayed(this.playerHand.playCard(selectedCard));
                        playingField.displayField();
                    }
                    else
                    {
                        Console.WriteLine("You can only play a card of the same rank as the cards on the field");
                        int.TryParse(Console.ReadLine(), out selectedCard);
                        selectedCard = selectedCard - 1;
                        cardSelected = this.playerHand.selectCard(selectedCard);
                    }
                }
            }


        }

        public void DefenderTurn(Field playingField, Card trumpCard, PassFlag passFlag)
        {
            Console.WriteLine( "It is " + this.playerName + "'s Turn.");
            int selectedCard=0;
            bool validCard = false;
            string inputString;
            this.playerHand.displayHand();
            do
            {

                inputString = Console.ReadLine();
                if (inputString.Equals("D") || inputString.Equals("d"))
                {
                    playingField.displayDiscarded();
                    inputString = Console.ReadLine();
                }
                if(inputString.Equals("Q") || inputString.Equals("q"))
                {
                    passFlag.passFlag = true;
                    passFlag.attackerWin = true;
                    break;
                }
                
                
                int.TryParse(inputString, out selectedCard);
                selectedCard = selectedCard - 1;

                if (selectedCard > this.playerHand.gethandSize() || selectedCard < 0)
                {
                    Console.WriteLine("Invalid option please pick a card.");
                }
                else
                {
                    validCard = true;
                }
            } while (!validCard);

            Card currentCard = playingField.getCurrentCard();

            if (passFlag.passFlag == false)
            {
                Card cardSelected = this.playerHand.selectCard(selectedCard);

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
                                playingField.cardPlayed(this.playerHand.playCard(selectedCard));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("rank is too low.");
                                int.TryParse(Console.ReadLine(), out selectedCard);
                                selectedCard = selectedCard - 1;
                                cardSelected = this.playerHand.selectCard(selectedCard);
                            }
                        }
                        else
                        {
                            playingField.cardPlayed(this.playerHand.playCard(selectedCard));
                            break;
                        }
                    }
                    //checks to see if played card suit is the field card suit
                    else if (cardSelected.suit == currentCard.suit)
                    {
                        //checks to see if played card rank is higher than field card rank
                        if (cardSelected.rank > currentCard.rank)
                        {
                            playingField.cardPlayed(this.playerHand.playCard(selectedCard));
                            break;
                        }
                        else
                        {
                            Console.WriteLine("rank is too low.");
                            int.TryParse(Console.ReadLine(), out selectedCard);
                            selectedCard = selectedCard - 1;
                            cardSelected = this.playerHand.selectCard(selectedCard);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You can only play the same suit or the trump suit.");
                        int.TryParse(Console.ReadLine(), out selectedCard);
                        selectedCard = selectedCard - 1;
                        cardSelected = this.playerHand.selectCard(selectedCard);
                    }
                }
            }//END OF IF
           
        }


    }
}
