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
    public sealed partial class BasicPage1 : Probability_Simulator.Common.LayoutAwarePage
    {
        public BasicPage1()
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
