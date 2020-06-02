using System;
using System.Collections.Generic;

namespace BlackJackOne.GameFramework.Models
{
    public class Player
    {
        private List<Card> cards = new List<Card>();

        public string Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Push { get; set; }
        public decimal Bet { get; set; }
        public decimal Balance { get; set; }
        public BlackJackHand Hand { get; set; }
        public Deck Deck { get; set; }

        public Player() : this(-1)
        {
        }

        public Player(int newBalance)
        {
            Hand = new BlackJackHand();
            Balance = newBalance;
        }

        public void IncreaseBet(decimal amt)
        {
            if ((Balance - (Bet + amt)) >= 0)
            {
                Bet += amt;
            }
            else
            {
                throw new Exception("You do not have enough money to make this bet.");
            }
        }

        public void PlaceBet()
        {
            if ((Balance - Bet) >= 0)
            {
                Balance = Balance - Bet;
            }
            else
            {
                throw new Exception("You do not have enough money to place this bet.");
            }
        }

        public BlackJackHand NewHand()
        {
            Hand = new BlackJackHand();
            return Hand;
        }

        public bool HasBlackJack() => 21 == Hand.GetSumOfHand();

        public bool HasBust() => Hand.GetSumOfHand() > 21;

        public void ClearBet() => Bet = 0;

        public void Hit() => Hand.Cards.Add(Deck.Draw());
    }
}