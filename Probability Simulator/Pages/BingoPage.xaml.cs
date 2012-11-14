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
    public sealed partial class BingoPage : Probability_Simulator.Common.LayoutAwarePage
    {
        Random random = new Random();
        bool[] called = new bool[75];
        int numCalled = 0;
        int[,] card = new int[5,5]; //store the values of the bingo card
        TextBox[,] cardBox = new TextBox[5,5];

        public BingoPage()
        {
            this.InitializeComponent();
            fillCardBox();  //link cardBox to proper textBox
            generateCard(); //generate the Bingo Card
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

        private async void callB_Click(object sender, RoutedEventArgs e)
        {
            int numDraw = 0;
            Int32.TryParse(numCallBox.Text, out numDraw);
            if (Int32.TryParse(numCallBox.Text, out numDraw) != false && numDraw <= 1000)   //if input is valid 
            {
                    DrawNumber(numDraw);
            }

            else if (Int32.TryParse(numCallBox.Text, out numDraw) != false && (numDraw > 1000 || numDraw < 0))  //if input is out of bound
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
            int result = 0;
            for (int i = 0; i < numDraw; i++)
            {
                numCalled++;
                if (numCalled > 75)
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
                } while (called[result] == true);

                called[result] = true;
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
                    called[i] = false;
            }
            numCalled = 0;
            historyList.Children.Clear();
        }

        private void generateCard()
        {
            bool[] used = new bool[75]; //store whether each of the 75 possible number for the bingo card is used
            int repeated = 0;
            int generated = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    while (card[i, j] == 0)
                    {
                        if (j == 0)
                        {
                            generated = random.Next(1, 16);
                        }
                        else if (j == 1)
                        {
                            generated = random.Next(15, 31);
                        }
                        else if (j == 2)
                        {
                             generated = random.Next(30, 46);
                        }
                        else if (j == 3)
                        {
                            generated = random.Next(45, 61);
                        }
                        else
                        {
                            generated = random.Next(60, 76);
                        }

                        if (!used[generated - 1])
                        {
                            card[i, j] = generated;
                            used[generated - 1] = true;
                        }
                    }
                }
            }
            displayCard();
        }

        private void displayCard()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (j == 2)
                    {
                            if (i == 2) //don't need to display number for free space
                            {
                                j++;
                            }
                    }
                    cardBox[i, j].Text = card[i, j].ToString();
                }
            }
            //box1_0.Text = card[0, 0].ToString();
            //box1_1.Text = card[0, 1].ToString();
            //box1_2.Text = card[0, 2].ToString();
            //box1_3.Text = card[0, 3].ToString();
            //box1_4.Text = card[0, 4].ToString();
            //box2_0.Text = card[1, 0].ToString();
            //box2_1.Text = card[1, 1].ToString();
            //box2_2.Text = card[1, 2].ToString();  
            //box2_3.Text = card[1, 3].ToString();
            //box2_4.Text = card[1, 4].ToString();
            //box3_0.Text = card[2, 0].ToString();
            //box3_1.Text = card[2, 1].ToString();
            ////box3_2.Text = card[2, 2].ToString();  //Free Space; not needed
            //box3_3.Text = card[2, 3].ToString();
            //box3_4.Text = card[2, 4].ToString();
            //box4_0.Text = card[3, 0].ToString();
            //box4_1.Text = card[3, 1].ToString();
            //box4_2.Text = card[3, 2].ToString();
            //box4_3.Text = card[3, 3].ToString();
            //box4_4.Text = card[3, 4].ToString();
            //box5_0.Text = card[4, 0].ToString();
            //box5_1.Text = card[4, 1].ToString();
            //box5_2.Text = card[4, 2].ToString();
            //box5_3.Text = card[4, 3].ToString();
            //box5_4.Text = card[4, 4].ToString();
        }

        private void fillCardBox()  //link cardBox to its respective textBox
        {
            cardBox[0, 0] = box1_0;
            cardBox[0, 1] = box1_1;
            cardBox[0, 2] = box1_2;
            cardBox[0, 3] = box1_3;
            cardBox[0, 4] = box1_4;

            cardBox[1, 0] = box2_0;
            cardBox[1, 1] = box2_1;
            cardBox[1, 2] = box2_2;
            cardBox[1, 3] = box2_3;
            cardBox[1, 4] = box2_4;

            cardBox[2, 0] = box3_0;
            cardBox[2, 1] = box3_1;
            cardBox[2, 2] = box3_2;
            cardBox[2, 3] = box3_3;
            cardBox[2, 4] = box3_4;

            cardBox[3, 0] = box4_0;
            cardBox[3, 1] = box4_1;
            cardBox[3, 2] = box4_2;
            cardBox[3, 3] = box4_3;
            cardBox[3, 4] = box4_4;

            cardBox[4, 0] = box5_0;
            cardBox[4, 1] = box5_1;
            cardBox[4, 2] = box5_2;
            cardBox[4, 3] = box5_3;
            cardBox[4, 4] = box5_4;
        }

        private void updateCard()
        {
        }
    }
}
