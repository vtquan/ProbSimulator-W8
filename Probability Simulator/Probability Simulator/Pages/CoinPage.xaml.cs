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
    public sealed partial class CoinPage : Probability_Simulator.Common.LayoutAwarePage
    {
        float numHead = 0;
        float numTail = 0;
        float numTotal = 0;

        public CoinPage()
        {
            this.InitializeComponent();
        }

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
                    numTail++;
                    historyList.Children.Add(new TextBlock() { Text = "Tail" });
                }
                else
                {
                    numHead++;
                    historyList.Children.Add(new TextBlock() { Text = "Head" });
                }
            }

            historyList.Children.Add(new TextBlock() { Text = "  " });
            historyScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);     //scroll to bottom

            updateGraph();
        }

        private void updateGraph()  //update the graph to match probability
        {
            numTotal = numHead + numTail;   //total number of result
            if (numHead == 0)
            {
                headBar.Height = 0;
                tailBar.Height = graphBox.ActualHeight; //actualHeight so that proportion stay the same across resolution
            }
            else if (numTail == 0)
            {
                headBar.Height = graphBox.ActualHeight;
                tailBar.Height = 0;
            }
            else
            {
                headBar.Height = (graphBox.ActualHeight * (numHead / numTotal));
                tailBar.Height = (graphBox.ActualHeight * (numTail / numTotal));
            }
        }

        private void graphBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            headBar.Height = (graphBox.ActualHeight * (numHead / numTotal));
            tailBar.Height = (graphBox.ActualHeight * (numTail / numTotal));
        }


    
    }
}
