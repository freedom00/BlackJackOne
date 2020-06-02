using System;
using System.Collections.Generic;

namespace BlackJackOne.GameFramework.Models
{
    public class Hand
    {
        // Creates a list of cards
        protected List<Card> cards = new List<Card>();

        public int NumCards { get { return cards.Count; } }
        public List<Card> Cards { get { return cards; } }

        public bool ContainsCard(Rank item)
        {
            foreach (Card c in cards)
            {
                if (c.Rank == item)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class BlackJackHand : Hand
    {
        public int CompareRankue(object otherHand)
        {
            BlackJackHand aHand = otherHand as BlackJackHand;
            if (aHand != null)
            {
                return this.GetSumOfHand().CompareTo(aHand.GetSumOfHand());
            }
            else
            {
                throw new ArgumentException("Argument is not a Hand");
            }
        }

        public int GetSumOfHand()
        {
            int val = 0;
            int numAces = 0;

            foreach (Card c in cards)
            {
                if (c.Rank == Rank.Ace)
                {
                    numAces++;
                    val += 11;
                }
                else if (c.Rank == Rank.Jack || c.Rank == Rank.Queen || c.Rank == Rank.King)
                {
                    val += 10;
                }
                else
                {
                    val += (int)c.Rank;
                }
            }

            while (val > 21 && numAces > 0)
            {
                val -= 10;
                numAces--;
            }

            return val;
        }
    }
}