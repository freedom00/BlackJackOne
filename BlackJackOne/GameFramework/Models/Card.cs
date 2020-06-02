namespace BlackJackOne.GameFramework.Models
{
    public enum Suit
    {
        Diamonds, Spades, Clubs, Hearts
    }

    public enum Rank
    {
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public bool IsCardUp { get; set; }

        public Card(Suit suit, Rank rank, bool isCardUp)
        {
            Suit = suit;
            Rank = rank;
            IsCardUp = isCardUp;
        }

        public override string ToString()
        {
            return "The" + Rank.ToString() + "of" + Suit.ToString();
        }
    }
}