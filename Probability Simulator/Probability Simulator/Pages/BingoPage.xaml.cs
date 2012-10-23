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

namespace Probability_Simulator.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BingoPage : Page
    {
        bool[] drawned = new bool[75];
        int numDrawned = 0;

        public BingoPage()
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

        private async void drawCardB_Click(object sender, RoutedEventArgs e)
        {
            int numDraw = 0;
            Int32.TryParse(numDrawBox.Text, out numDraw);
            if (Int32.TryParse(numDrawBox.Text, out numDraw) != false && numDraw <= 1000)   //if input is valid 
            {
                    DrawNumber(numDraw);
            }

            else if (Int32.TryParse(numDrawBox.Text, out numDraw) != false && (numDraw > 1000 || numDraw < 0))  //if input is out of bound
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

        private async void DrawNumber(int numDraw)
        {
            Random random = new Random();

            int result = 0;
            for (int i = 0; i < numDraw; i++)
            {
                numDrawned++;
                if (numDrawned > 75)
                {
                    var messageDialog = new MessageDialog("All numbers have been drawned");
                    messageDialog.Title = "Out of Numbers";

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
                do
                {
                    result = random.Next(0, 75);
                } while (drawned[result] == true);

                drawned[result] = true;
                result++;

                if(result <16)
                    historyList.Children.Add(new TextBlock() { Text = "B-"+result });
                else if (result < 31)
                    historyList.Children.Add(new TextBlock() { Text = "I-" + result });
                else if (result < 46)
                    historyList.Children.Add(new TextBlock() { Text = "N-" + result });
                else if (result < 61)
                    historyList.Children.Add(new TextBlock() { Text = "G-" + result });
                else 
                    historyList.Children.Add(new TextBlock() { Text = "O-" + result });

                if (i == numDraw - 1)   //add spacing at the end only when there are no problem
                {
                    historyList.Children.Add(new TextBlock() { Text = "  " });
                }
            }
            historyScroll.UpdateLayout();
            historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);
        }

        private void CommandInvokedHandler(IUICommand command)  //clear history list and reset number list
        {
            for (int i = 0; i < 75; i++)
            {
                    drawned[i] = false;
            }
            numDrawned = 0;
            historyList.Children.Clear();
        }
    }
}
