using BlackJackOne.GameFramework.Controllers;
using BlackJackOne.GameFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJackOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Obsolete]
        private BlackJackGame game = new BlackJackGame(int.Parse(System.Configuration.ConfigurationSettings.AppSettings["InitBalance"]));

        private Image[] playerCards;
        private Image[] dealerCards;

        [Obsolete]
        public MainWindow()
        {
            InitializeComponent();
            LoadCardSkinImages();
            SetUpNewGame();
        }

        private void LoadCardSkinImages()
        {
            playerCards = new Image[] { imgPlayerCard1, imgPlayerCard2, imgPlayerCard3, imgPlayerCard4, imgPlayerCard5, imgPlayerCard6 };
            dealerCards = new Image[] { imgDealerCard1, imgDealerCard2, imgDealerCard3, imgDealerCard4, imgDealerCard5, imgDealerCard6 };
        }

        [Obsolete]
        private void Bet(decimal betValue)
        {
            try
            {
                // Update the bet amount
                game.CurrentPlayer.IncreaseBet(betValue);

                // Update the "My Bet" and "My Account" values
                ShowBankValue();
            }
            catch (Exception NotEnoughMoneyException)
            {
                MessageBox.Show(NotEnoughMoneyException.Message);
            }
        }

        [Obsolete]
        private void ShowBankValue()
        {
            // Update the "My Account" value
            tbMyAccount.Text = "$" + game.CurrentPlayer.Balance.ToString();
            tbMyBet.Text = "$" + game.CurrentPlayer.Bet.ToString();
        }

        [Obsolete]
        private void UpdateUIPlayerCards()
        {
            // Update the value of the hand
            lblPlayerTotal.Content = game.CurrentPlayer.Hand.GetSumOfHand().ToString();

            List<Card> pcards = game.CurrentPlayer.Hand.Cards;
            for (int i = 0; i < pcards.Count; i++)
            {
                // Load each card from file
                LoadCard(playerCards[i], pcards[i]);
                playerCards[i].Visibility = Visibility.Visible;
                //playerCards[i].BringToFront();
            }

            List<Card> dcards = game.Dealer.Hand.Cards;
            for (int i = 0; i < dcards.Count; i++)
            {
                LoadCard(dealerCards[i], dcards[i]);
                dealerCards[i].Visibility = Visibility.Visible;
                //dealerCards[i].BringToFront();
            }
        }

        [Obsolete]
        private void EndGame(EndResult endState)
        {
            switch (endState)
            {
                case EndResult.DealerBust:
                    tbGameOver.Text = "Dealer Bust!";
                    game.PlayerWin();
                    break;

                case EndResult.DealerBlackJack:
                    tbGameOver.Text = "Dealer BlackJack!";
                    game.PlayerLose();
                    break;

                case EndResult.DealerWin:
                    tbGameOver.Text = "Dealer Won!";
                    game.PlayerLose();
                    break;

                case EndResult.PlayerBlackJack:
                    tbGameOver.Text = "BlackJack!";
                    game.CurrentPlayer.Balance += (game.CurrentPlayer.Bet * (decimal)2.5);
                    game.CurrentPlayer.Wins += 1;
                    break;

                case EndResult.PlayerBust:
                    tbGameOver.Text = "You Bust!";
                    game.PlayerLose();
                    break;

                case EndResult.PlayerWin:
                    tbGameOver.Text = "You Won!";
                    game.PlayerWin();
                    break;

                case EndResult.Push:
                    tbGameOver.Text = "Push";
                    game.CurrentPlayer.Push += 1;
                    game.CurrentPlayer.Balance += game.CurrentPlayer.Bet;
                    break;
            }
            // Update the "My Record" values
            tbWins.Text = game.CurrentPlayer.Wins.ToString();
            tbLosses.Text = game.CurrentPlayer.Losses.ToString();
            //tiesTextBox.Text = game.CurrentPlayer.Push.ToString();
            SetUpNewGame();
            ShowBankValue();
            tbGameOver.Visibility = Visibility.Visible;
            // Check if the current player is out of money
            if (game.CurrentPlayer.Balance == 0)
            {
                MessageBox.Show("Out of Money.  Please create a new game to play again.");
                this.Close();
            }
        }

        [Obsolete]
        private void SetUpNewGame()
        {
            btDeal.IsEnabled = true;
            btStand.IsEnabled = false;
            btHit.IsEnabled = false;
            btClear.IsEnabled = true;
            btOne.IsEnabled = true;
            btTen.IsEnabled = true;
            btTwentyFive.IsEnabled = true;
            btFifty.IsEnabled = true;
            tbGameOver.Visibility = Visibility.Hidden;
            lblPlayerTotal.Visibility = Visibility.Hidden;
            ShowBankValue();
        }

        private void SetUpGameInPlay()
        {
            btOne.IsEnabled = false;
            btTen.IsEnabled = false;
            btTwentyFive.IsEnabled = false;
            btFifty.IsEnabled = false;
            btClear.IsEnabled = false;
            btStand.IsEnabled = true;
            btHit.IsEnabled = true;
            tbGameOver.Visibility = Visibility.Hidden;
            lblPlayerTotal.Visibility = Visibility.Visible;
            btDeal.IsEnabled = false;
        }

        [Obsolete]
        private void LoadCard(Image pb, Card c)
        {
            try
            {
                StringBuilder image = new StringBuilder();

                switch (c.Suit)
                {
                    case Suit.Diamonds:
                        image.Append("di");
                        break;

                    case Suit.Hearts:
                        image.Append("he");
                        break;

                    case Suit.Spades:
                        image.Append("sp");
                        break;

                    case Suit.Clubs:
                        image.Append("cl");
                        break;
                }

                switch (c.Rank)
                {
                    case Rank.Ace:
                        image.Append("1");
                        break;

                    case Rank.King:
                        image.Append("k");
                        break;

                    case Rank.Queen:
                        image.Append("q");
                        break;

                    case Rank.Jack:
                        image.Append("j");
                        break;

                    case Rank.Ten:
                        image.Append("10");
                        break;

                    case Rank.Nine:
                        image.Append("9");
                        break;

                    case Rank.Eight:
                        image.Append("8");
                        break;

                    case Rank.Seven:
                        image.Append("7");
                        break;

                    case Rank.Six:
                        image.Append("6");
                        break;

                    case Rank.Five:
                        image.Append("5");
                        break;

                    case Rank.Four:
                        image.Append("4");
                        break;

                    case Rank.Three:
                        image.Append("3");
                        break;

                    case Rank.Two:
                        image.Append("2");
                        break;
                }

                image.Append(System.Configuration.ConfigurationSettings.AppSettings["CardGameImageExtension"]);
                string cardGameImagePath = System.Configuration.ConfigurationSettings.AppSettings["CardGameImagePath"];
                string cardGameImageSkinPath = System.Configuration.ConfigurationSettings.AppSettings["CardGameImageSkinPath"];
                image.Insert(0, cardGameImagePath);
                //check to see if the card should be faced down or up;
                if (!c.IsCardUp)
                    image.Replace(image.ToString(), cardGameImageSkinPath);

                pb.Source = new BitmapImage(new Uri(image.ToString(), UriKind.RelativeOrAbsolute));
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Card images are not loading correctly.  Make sure all card images are in the right location.");
            }
        }

        [Obsolete]
        private EndResult GetGameResult()
        {
            EndResult endState;
            // Check for blackjack
            if (game.Dealer.Hand.NumCards == 2 && game.Dealer.HasBlackJack())
            {
                endState = EndResult.DealerBlackJack;
            }
            // Check if the dealer has bust
            else if (game.Dealer.HasBust())
            {
                endState = EndResult.DealerBust;
            }
            else if (game.Dealer.Hand.CompareRankue(game.CurrentPlayer.Hand) > 0)
            {
                //dealer wins
                endState = EndResult.DealerWin;
            }
            else if (game.Dealer.Hand.CompareRankue(game.CurrentPlayer.Hand) == 0)
            {
                // push
                endState = EndResult.Push;
            }
            else
            {
                // player wins
                endState = EndResult.PlayerWin;
            }
            return endState;
        }

        private void ClearTable()
        {
            for (int i = 0; i < 6; i++)
            {
                dealerCards[i].Source = null;
                dealerCards[i].Visibility = Visibility.Hidden;

                playerCards[i].Source = null;
                playerCards[i].Visibility = Visibility.Hidden;
            }
        }

        [Obsolete]
        private void btOne_Click(object sender, RoutedEventArgs e) => Bet(1);

        [Obsolete]
        private void btTen_Click(object sender, RoutedEventArgs e) => Bet(10);

        [Obsolete]
        private void btTwentyFive_Click(object sender, RoutedEventArgs e) => Bet(25);

        [Obsolete]
        private void btFifty_Click(object sender, RoutedEventArgs e) => Bet(50);

        [Obsolete]
        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            game.CurrentPlayer.ClearBet();
            ShowBankValue();
        }

        [Obsolete]
        private void btHit_Click(object sender, RoutedEventArgs e)
        {
            // Hit once and update UI cards
            game.CurrentPlayer.Hit();
            UpdateUIPlayerCards();

            // Check to see if player has bust
            if (game.CurrentPlayer.HasBust())
            {
                EndGame(EndResult.PlayerBust);
            }
        }

        [Obsolete]
        private void btStand_Click(object sender, RoutedEventArgs e)
        {
            game.DealerPlay();
            UpdateUIPlayerCards();

            // Check who won the game
            EndGame(GetGameResult());
        }

        [Obsolete]
        private void btDeal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // If the current bet is equal to 0, ask the player to place a bet
                if ((game.CurrentPlayer.Bet == 0) && (game.CurrentPlayer.Balance > 0))
                {
                    MessageBox.Show(this, "You must place a bet before the dealer deals.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Place the bet
                    game.CurrentPlayer.PlaceBet();
                    ShowBankValue();

                    // Clear the table, set up the UI for playing a game, and deal a new game
                    ClearTable();
                    SetUpGameInPlay();
                    game.DealNewGame();
                    UpdateUIPlayerCards();

                    // Check see if the current player has blackjack
                    if (game.CurrentPlayer.HasBlackJack())
                    {
                        EndGame(EndResult.PlayerBlackJack);
                    }
                }
            }
            catch (Exception NotEnoughMoneyException)
            {
                MessageBox.Show(NotEnoughMoneyException.Message);
            }
        }
    }
}