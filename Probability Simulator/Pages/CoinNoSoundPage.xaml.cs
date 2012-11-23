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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Probability_Simulator.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CoinNoSoundPage : Probability_Simulator.Common.LayoutAwarePage
    {
        float numHead = 0;
        float numTail = 0;
        float numTotal = 0;

        public CoinNoSoundPage()
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

        private void coinFlipB_Click(object sender, RoutedEventArgs e)
        {
            testInput();
        }

        private async void testInput()    //test if input is correct
        {
            int numCoin = 0;
            Int32.TryParse(numCoinBox.Text, out numCoin);
            if (Int32.TryParse(numCoinBox.Text, out numCoin) != false && (numCoin <= 1000 && numCoin >= 0))   //if input is valid 
            {

                coinFlip(numCoin);
            }

            else if (Int32.TryParse(numCoinBox.Text, out numCoin) != false && (numCoin > 1000 || numCoin < 0))  //if input is out of bound
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

            //roll as many time as input
            for (int i = 0; i < numFlip; i++)
            {
                result = random.Next(0, 2);     //0 is head, 1 is tail
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

            //update image

            //how to change canvas background
            //BitmapImage coinHeadBrush = new BitmapImage(new Uri("ms-appx:///Assets/penny-head-trans.png", UriKind.RelativeOrAbsolute));
            //BitmapImage coinTailBrush = new BitmapImage(new Uri("ms-appx:///Assets/penny-tail-trans.png", UriKind.RelativeOrAbsolute));
            //ImageBrush headBrush = new ImageBrush();
            //headBrush.ImageSource = coinHeadBrush;
            //ImageBrush tailBrush = new ImageBrush();
            //tailBrush.ImageSource = coinTailBrush;
            //coinImage.Background = headBrush;
            //coinImage.Background = tailBrush;

            BitmapImage coinHeadBrush = new BitmapImage(new Uri("ms-appx:///Assets/penny-head-trans.png", UriKind.RelativeOrAbsolute));
            BitmapImage coinTailBrush = new BitmapImage(new Uri("ms-appx:///Assets/penny-tail-trans.png", UriKind.RelativeOrAbsolute));

            if (result == 1)    //tail
            {
                CoinImage.Source = coinTailBrush;
            }
            else     //head
            {
                CoinImage.Source = coinHeadBrush;
            }

            //update history list
            historyList.Children.Add(new TextBlock() { Text = "  " });
            historyScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);     //scroll to bottom

            updateGraph();
        }

        private void updateGraph()  //update the graph to match probability
        {
            numTotal = numHead + numTail;   //total number of result

            if (numHead == 0 && numTail == 0)   //prevent 0/0
            {
                headBar.Height = 0;
                tailBar.Height = 0;
            }
            else if (numHead == 0)  //prevent numTail/0
            {
                headBar.Height = 0;
                tailBar.Height = graphBox.ActualHeight; //actualHeight so that proportion stay the same across resolution
            }
            else if (numTail == 0)  //prevent numHead/0
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
            updateGraph();
        }

        private void CoinImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            testInput();
        }
    }
}
