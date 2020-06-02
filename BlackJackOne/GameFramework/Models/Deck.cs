using System;
using System.Collections.Generic;

namespace BlackJackOne.GameFramework.Models
{
    public class Deck
    {
        private List<Card> cards = new List<Card>();

        public Deck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank, true));
                }
            }
        }

        public Card Draw()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        public void Shuffle()
        {
            Random random = new Random();
            for (int i = 0; i < cards.Count; i++)
            {
                int index1 = i;
                int index2 = random.Next(cards.Count);
                SwapCard(index1, index2);
            }
        }

        private void SwapCard(int index1, int index2)
        {
            Card card = cards[index1];
            cards[index1] = cards[index2];
            cards[index2] = card;
        }
    }
}