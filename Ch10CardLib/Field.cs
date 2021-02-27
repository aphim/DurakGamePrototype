using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch10CardLib
{
    public class Field
    {
        private ArrayList field = new ArrayList();
        private ArrayList discard = new ArrayList();

        public Field()
        {
        }

        public ArrayList getField()
        {
            ArrayList fieldCards = new ArrayList();
            if (field.Count > 0)
            {

               for (int i = 0; i < field.Count; i++)
               {
                  fieldCards.Add((Card)field[i]);
               }


            }
            else
            {
                Console.WriteLine("Field is empty.");
            }

            return fieldCards;
        }

        public void cardPlayed(Card card)
        {
            field.Add(card);
        }

        //get current card in the field
        public Card getCurrentCard()
        {
            Card currentCard;

            currentCard = ((Card)field[field.Count-1]);

            return currentCard;
        }

        //get current card in the field
        public Card getIndexCard(int index)
        {
            Card indexCard;

            indexCard = ((Card)field[index]);

            return indexCard;
        }


        public ArrayList getDiscard()
        {

            ArrayList discardCards = new ArrayList();
            if (discard.Count > 0)
            {

                for (int i = 0; i < discard.Count; i++)
                {
                    discardCards.Add((Card)discard[i]);
                }

            }
            else
            {
                Console.WriteLine("discard is empty.");
            }
            return discardCards;
        }

        public void discardField()
        {
            if (field.Count > 0)
            {
                for (int i = 0; i < field.Count; i++)
                {
                    discard.Add((Card)field[i]);
                }

            }
            else
            {
                Console.WriteLine("Field is empty.");
            }
            field.Clear();
        }

        public ArrayList pickupField()
        {
            ArrayList pickupCards = new ArrayList();

            if (field.Count > 0)
            {
                for (int i = 0; i < field.Count; i++)
                {
                    pickupCards.Add((Card)field[i]);
                }
            }
            else
            {
                Console.WriteLine("Field is empty.");
            }
            field.Clear();
            return pickupCards;

        }

        public void displayField(Field playingField)
        {
            for (int i = 0; i < playingField.getField().Count; i++)
            {
                //displays the current card
                Card tempCard = (Card)playingField.getField()[i];
                Console.Write(tempCard.ToString());
                if (i != playingField.getField().Count - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        public void displayDiscarded(Field playingField)
        {
            for (int i = 0; i < playingField.getDiscard().Count; i++)
            {
                //displays the current card
                Card tempCard = (Card)playingField.getDiscard()[i];
                Console.Write(tempCard.ToString());
                if (i != playingField.getDiscard().Count - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

    }
}
