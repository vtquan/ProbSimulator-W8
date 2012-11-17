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

        public CardPage()
        {
            this.InitializeComponent();
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

            int result = 0;
            String suit = "";
            String value = "";
            String cardString = "";
            for (int i = 0; i < numCard; i++)
            {
                result = random.Next(0, 4);
                result++;
                switch (result)
                {
                    case 1:
                        suit = "Hearts";
                        break;
                    case 2:
                        suit = "Clubs";
                        break;
                    case 3:
                        suit = "Diamonds";
                        break;
                    default:
                        suit = "Spades";
                        break;
                }
                result = random.Next(0, 13);
                result++;
                switch (result)
                {
                    case 1:
                        value = "Ace";
                        break;
                    case 2:
                        value = "2";
                        break;
                    case 3:
                        value = "3";
                        break;
                    case 4:
                        value = "4";
                        break;
                    case 5:
                        value = "5";
                        break;
                    case 6:
                        value = "6";
                        break;
                    case 7:
                        value = "7";
                        break;
                    case 8:
                        value = "8";
                        break;
                    case 9:
                        value = "9";
                        break;
                    case 10:
                        value = "10";
                        break;
                    case 11:
                        value = "Jack";
                        break;
                    case 12:
                        value = "Queen";
                        break;
                    default:
                        value = "King";
                        break;
                }
                cardString = value + " of " + suit;
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
            String suit = "";
            String value = "";
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
                        result1 = random.Next(0, 4);
                        result2 = random.Next(0, 13);
                    } while (drawned[result1, result2] == true);

                    drawned[result1, result2] = true;

                    result1++;
                    result2++;

                    switch (result1)
                    {
                        case 1:
                            suit = "Hearts";
                            break;
                        case 2:
                            suit = "Clubs";
                            break;
                        case 3:
                            suit = "Diamonds";
                            break;
                        default:
                            suit = "Spades";
                            break;
                    }

                    switch (result2)
                    {
                        case 1:
                            value = "Ace";
                            break;
                        case 2:
                            value = "2";
                            break;
                        case 3:
                            value = "3";
                            break;
                        case 4:
                            value = "4";
                            break;
                        case 5:
                            value = "5";
                            break;
                        case 6:
                            value = "6";
                            break;
                        case 7:
                            value = "7";
                            break;
                        case 8:
                            value = "8";
                            break;
                        case 9:
                            value = "9";
                            break;
                        case 10:
                            value = "10";
                            break;
                        case 11:
                            value = "Jack";
                            break;
                        case 12:
                            value = "Queen";
                            break;
                        default:
                            value = "King";
                            break;
                    }
                    //if (value.Equals("Ace") || value.Equals("8"))
                    //{
                    //    cardString = "An " + value + " of " + suit;
                    //    //lastDrawBox.Text = cardString;
                    //}
                    //else
                    //{
                    //    cardString = "A " + value + " of " + suit;
                    //    //lastDrawBox.Text = cardString;
                    //}
                    cardString = value + " of " + suit;
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
    }
}
