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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Probability_Simulator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CoinPage : Page
    {
        public CoinPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void coinFlipB_Click(object sender, RoutedEventArgs e)
        {
            int numCard = 0;
            Int32.TryParse(numCoinBox.Text, out numCard);
            if (Int32.TryParse(numCoinBox.Text, out numCard) != false && numCard <= 1000)   //if input is valid 
            {
                
                    coinFlip(numCard);
            }

            else if (Int32.TryParse(numCoinBox.Text, out numCard) != false && (numCard > 1000 || numCard < 0))  //if input is out of bound
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

        private void coinFlip(int numFlip)
        {
            Random random = new Random();

            int result = 0;

            for (int i = 0; i < numFlip; i++)
            {
                result = random.Next(0, 2);
                if (result == 1)
                {
                    historyList.Children.Add(new TextBlock() { Text = "Tail" });
                }
                else
                {
                    historyList.Children.Add(new TextBlock() { Text = "Head" });
                }
            }

            historyList.Children.Add(new TextBlock() { Text = "  " });
            historyScroll.UpdateLayout();
            historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);
        }
    }
}
