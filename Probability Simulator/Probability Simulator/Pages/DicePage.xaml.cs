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
    public sealed partial class DicePage : Page
    {
        int numHistory = 0; //number of items in history list
        public DicePage()
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

        private async void DiceRoll_Click(object sender, RoutedEventArgs e)
        {
            if (_6Check.IsChecked == true)
            {
                int numRoll = 0;
                Int32.TryParse(numDiceBox.Text, out numRoll);

                if (Int32.TryParse(numDiceBox.Text, out numRoll) != false && numRoll <= 1000)
                {
                    diceRoll(numRoll);
                }
                else if (Int32.TryParse(numDiceBox.Text, out numRoll) != false && numRoll > 1000 && numRoll < 0)
                {
                    var messageDialog = new MessageDialog("Please enter a number between 0 and 1000.");

                    // Show the message dialog and wait
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var messageDialog = new MessageDialog("Please enter a real number.");

                    // Show the message dialog and wait
                    await messageDialog.ShowAsync();
                }
            }

        }

        public void diceRoll(int numRoll)
        {

            Random random = new Random();
            int result = 0;

            int numOne = Int32.Parse(_1Box.Text);
            int numTwo = Int32.Parse(_2Box.Text);
            int numThree = Int32.Parse(_3Box.Text);
            int numFour = Int32.Parse(_4Box.Text);
            int numFive = Int32.Parse(_5Box.Text);
            int numSix = Int32.Parse(_6Box.Text);

            for (int i = 0; i < numRoll; i++)
            {
                result = random.Next(0, 6);
                result++;
                switch (result)
                {
                    case 1:
                        numOne++;
                        historyList.Children.Add(new TextBlock() { Text = "One" });
                        break;
                    case 2:
                        numTwo++;
                        historyList.Children.Add(new TextBlock() { Text = "Two" });
                        break;
                    case 3:
                        numThree++;
                        historyList.Children.Add(new TextBlock() { Text = "Three" });
                        break;
                    case 4:
                        numFour++;
                        historyList.Children.Add(new TextBlock() { Text = "Four" });
                        break;
                    case 5:
                        numFive++;
                        historyList.Children.Add(new TextBlock() { Text = "Five" });
                        break;
                    default:
                        numSix++;
                        historyList.Children.Add(new TextBlock() { Text = "Six" });
                        break;
                }
            }
            _1Box.Text = numOne.ToString();
            _2Box.Text = numTwo.ToString();
            _3Box.Text = numThree.ToString();
            _4Box.Text = numFour.ToString();
            _5Box.Text = numFive.ToString();
            _6Box.Text = numSix.ToString();

            historyList.Children.Add(new TextBlock() { Text = "  " });
            historyScroll.UpdateLayout();
            historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);
        }
    }
}
