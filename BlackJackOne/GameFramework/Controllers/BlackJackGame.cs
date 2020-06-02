using BlackJackOne.GameFramework.Models;

namespace BlackJackOne.GameFramework.Controllers
{
    public class BlackJackGame
    {
        private readonly Player dealer;
        private readonly Player player;
        private Deck deck;

        public Player Dealer => dealer;
        public Player CurrentPlayer => player;
        public Deck CurrentDeck => deck;

        public BlackJackGame(int initBalance)
        {
            // Create a dealer and one player with the initial balance.
            dealer = new Player();
            player = new Player(initBalance);
        }

        public void DealerPlay()
        {
            dealer.Hand.Cards[1].IsCardUp = true;

            // Check to see if dealer has a hand that is less than 17
            // If so, dealer should keep hitting until their hand is greater or equal to 17
            if (dealer.Hand.GetSumOfHand() < 17)
            {
                dealer.Hit();
                DealerPlay();
            }
        }

        public void DealNewGame()
        {
            // Create a new deck and then shuffle the deck
            deck = new Deck();
            deck.Shuffle();

            // Reset the player and the dealer's hands in case this is not the first game
            player.NewHand();
            dealer.NewHand();

            // Deal two cards to each person's hand
            for (int i = 0; i < 2; i++)
            {
                Card c = deck.Draw();
                player.Hand.Cards.Add(c);

                Card d = deck.Draw();
                // Set the dealer's second card to be facing down
                if (i == 1)
                    d.IsCardUp = false;

                dealer.Hand.Cards.Add(d);
            }

            // Give the player and the dealer a handle to the current deck
            player.Deck = deck;
            dealer.Deck = deck;
        }

        public void PlayerLose() => player.Losses += 1;

        public void PlayerWin()
        {
            player.Balance += player.Bet * 2;
            player.Wins += 1;
        }
    }
}