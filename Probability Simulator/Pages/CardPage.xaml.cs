using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Probability_Simulator.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CardPage : Probability_Simulator.Common.LayoutAwarePage
    {
        bool[,] drawned = new bool[4, 13];
        int numDrawned = 0;

        Dictionary<int, string> cardSuit = new Dictionary<int, string>();
        Dictionary<int, string> cardValue = new Dictionary<int, string>();

        public CardPage()
        {
            this.InitializeComponent();

            setupDict();    
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void drawCardB_Click(object sender, RoutedEventArgs e)
        {
            checkInput();
        }

        private async void checkInput()
        {
            int numCard = 0;
            Int32.TryParse(numCardBox.Text, out numCard);
            if (Int32.TryParse(numCardBox.Text, out numCard) != false && numCard <= 1000)   //if input is valid 
            {
                if (CardReturn.IsChecked == true)
                {
                    //reset all drawned card list
                    drawned = new bool[4, 13];
                    numDrawned = 0;
                    cardDrawReturn(numCard);
                }
                else
                    cardDrawNoReturn(numCard);
            }

            else if (Int32.TryParse(numCardBox.Text, out numCard) != false && (numCard > 1000 || numCard < 0))  //if input is out of bound
            {
                var messageDialog = new MessageDialog("Please enter a number between 0 and 1000.");
                messageDialog.Title = "Invalid Input";

                // Show the message dialog and wait
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog = new MessageDialog("Please enter a real number.");   //if input is not an integer
                messageDialog.Title = "Invalid Input";

                // Show the message dialog and wait
                await messageDialog.ShowAsync();
            }
        }

        private void cardDrawReturn(int numCard)
        {
            Random random = new Random();

            int suit;
            int value;
            String cardString = "";
            for (int i = 0; i < numCard; i++)
            {
                //get suit
                suit = random.Next(0, 4);
                suit++;
                //get value
                value = random.Next(0, 13);
                value++;

                cardString = cardValue[value] + " of " + cardSuit[suit];
                historyList.Children.Add(new TextBlock() { Text = cardString });
            }
            historyList.Children.Add(new TextBlock() { Text = "  " });
            historyScroll.UpdateLayout();
            historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);
        }

        private async void cardDrawNoReturn(int numCard)
        {
            Random random = new Random();


            int result1 = 0;
            int result2 = 0;
            int suit;
            int value;
            String cardString = "";
            for (int i = 0; i < numCard; i++)
            {
                numDrawned++;
                if (numDrawned > 52)
                {
                    var messageDialog = new MessageDialog("All cards have been drawned");
                    messageDialog.Title = "Out of Cards";

                    messageDialog.Commands.Add(new UICommand(
                    "Reset List",
                    new UICommandInvokedHandler(this.CommandInvokedHandler)));

                    messageDialog.Commands.Add(new UICommand(
                    "Close"));

                    // Set the command that will be invoked by default
                    messageDialog.DefaultCommandIndex = 1;

                    // Set the command to be invoked when escape is pressed
                    messageDialog.CancelCommandIndex = 1;

                    // Show the message dialog and wait
                    await messageDialog.ShowAsync();
                    break;
                }
                else
                {
                    do
                    {
                        //get suit
                        suit = random.Next(0, 4);
                        suit++;
                        //get value
                        value = random.Next(0, 13);
                        value++;
                    } while (drawned[result1, result2] == true);

                    cardString = cardValue[value] + " of " + cardSuit[suit];
                    historyList.Children.Add(new TextBlock() { Text = cardString });

                    if (i == numCard - 1)   //add spacing at the end only when there are no problem
                    {
                        historyList.Children.Add(new TextBlock() { Text = "  " });
                    }
                }
                historyScroll.UpdateLayout();
                historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);
            }

        }


        private void CommandInvokedHandler(IUICommand command)  //clear history list and reset card deck
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 13; j++)
                    drawned[i, j] = false;
            numDrawned = 0;
            historyList.Children.Clear();
        }
    
        private void setupDict()
        {
            cardSuit.Add(1, "Spades");
            cardSuit.Add(2, "Hearts");
            cardSuit.Add(3, "Diamonds");
            cardSuit.Add(4, "Clubs");

            cardValue.Add(1, "Ace");
            cardValue.Add(2, "2");
            cardValue.Add(3, "3");
            cardValue.Add(4, "4");
            cardValue.Add(5, "5");
            cardValue.Add(6, "6");
            cardValue.Add(7, "7");
            cardValue.Add(8, "8");
            cardValue.Add(9, "9");
            cardValue.Add(10, "10");
            cardValue.Add(11, "Jack");
            cardValue.Add(12, "Queen");
            cardValue.Add(13, "King");
        }
    }

    
}
