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
        bool[] called = new bool[75];   //store whether each of the possible 75 numbers was called
        int numCalled = 0;
        int[,] card = new int[5,5]; //store the values of the bingo card; [row, column]
        TextBlock[,] cardText = new TextBlock[5, 5]; //store the textBox [row, column]
        bool[,] cardCalled = new bool[5, 5];   //bingo card represented by whether or not the number has been called; [row, column]
        Image[,] chipArray = new Image[5, 5]; //link to each of the bingo chip image in the bingo card; [row, column]
        bool gameEnd;   //if game has ended

        public BingoPage()
        {
            this.InitializeComponent();

            cardCalled[2, 2] = true;    //Set free space to true

            fillCardText();  //link cardBox to proper textBox
            generateCard(); //generate the Bingo Card
            linkChip(); //link the chip image to the proper row and column of chipArray[,]
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

        private void generateCard()
        {
            bool[] used = new bool[75]; //store whether each of the 75 possible number for the bingo card is used
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
                            generated = random.Next(16, 31);
                        }
                        else if (j == 2)
                        {
                            if (i == 2)
                            {
                                generated = random.Next(46, 61);
                                j++;
                            }
                            else
                            {
                                generated = random.Next(31, 46);
                            }
                        }
                        else if (j == 3)
                        {
                            generated = random.Next(46, 61);
                        }
                        else
                        {
                            generated = random.Next(61, 76);
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
                    cardText[i, j].Text = card[i, j].ToString();
                }
            }
        }

        private void fillCardText()  //link cardBox to its respective textBox
        {
            cardText[0, 0] = text0_0;
            cardText[0, 1] = text0_1;
            cardText[0, 2] = text0_2;
            cardText[0, 3] = text0_3;
            cardText[0, 4] = text0_4;

            cardText[1, 0] = text1_0;
            cardText[1, 1] = text1_1;
            cardText[1, 2] = text1_2;
            cardText[1, 3] = text1_3;
            cardText[1, 4] = text1_4;

            cardText[2, 0] = text2_0;
            cardText[2, 1] = text2_1;
            //cardText[2, 2] = text2_2; //Free space, no value
            cardText[2, 3] = text2_3;
            cardText[2, 4] = text2_4;

            cardText[3, 0] = text3_0;
            cardText[3, 1] = text3_1;
            cardText[3, 2] = text3_2;
            cardText[3, 3] = text3_3;
            cardText[3, 4] = text3_4;

            cardText[4, 0] = text4_0;
            cardText[4, 1] = text4_1;
            cardText[4, 2] = text4_2;
            cardText[4, 3] = text4_3;
            cardText[4, 4] = text4_4;
        }

        private void linkChip()
        {
            chipArray[0, 0] = Chip0_0;
            chipArray[0, 1] = Chip0_1;
            chipArray[0, 2] = Chip0_2;
            chipArray[0, 3] = Chip0_3;
            chipArray[0, 4] = Chip0_4;

            chipArray[1, 0] = Chip1_0;
            chipArray[1, 1] = Chip1_1;
            chipArray[1, 2] = Chip1_2;
            chipArray[1, 3] = Chip1_3;
            chipArray[1, 4] = Chip1_4;

            chipArray[2, 0] = Chip2_0;
            chipArray[2, 1] = Chip2_1;
            chipArray[2, 2] = Chip2_2;
            chipArray[2, 3] = Chip2_3;
            chipArray[2, 4] = Chip2_4;

            chipArray[3, 0] = Chip3_0;
            chipArray[3, 1] = Chip3_1;
            chipArray[3, 2] = Chip3_2;
            chipArray[3, 3] = Chip3_3;
            chipArray[3, 4] = Chip3_4;

            chipArray[4, 0] = Chip4_0;
            chipArray[4, 1] = Chip4_1;
            chipArray[4, 2] = Chip4_2;
            chipArray[4, 3] = Chip4_3;
            chipArray[4, 4] = Chip4_4;
        }

        private async void callB_Click(object sender, RoutedEventArgs e)
        {
            int numCall = 0;
            Int32.TryParse(numCallBox.Text, out numCall);
            if (Int32.TryParse(numCallBox.Text, out numCall) != false && numCall <= 1000)   //if input is valid 
            {
                    DrawNumber(numCall);
            }

            else if (Int32.TryParse(numCallBox.Text, out numCall) != false && (numCall > 1000 || numCall < 0))  //if input is out of bound
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
            int numCall;    //total number of call to do
            int callNum = 0 ;    //current number of call
            Int32.TryParse(numCallBox.Text, out numCall);
            int[] resultList = new int[numCall];
            for (int i = 0; i < numDraw; i++)
            {
                numCalled++;
                if (numCalled > 75)
                {
                    var messageDialog = new MessageDialog("All numbers have been drawned");
                    messageDialog.Title = "Out of Numbers";
                    if (gameEnd)    //if do not check, when rolling a high amount at once, it will display the "out of number with reset button" that don't work
                    {
                        messageDialog.Commands.Add(new UICommand(
                        "Reset List",
                        new UICommandInvokedHandler(this.CommandInvokedHandler)));

                        messageDialog.Commands.Add(new UICommand(
                        "Close"));

                        // Set the command that will be invoked by default
                        messageDialog.DefaultCommandIndex = 1;

                        // Set the command to be invoked when escape is pressed
                        messageDialog.CancelCommandIndex = 1;
                    }

                    else
                    {
                        messageDialog.Commands.Add(new UICommand(
                        "Close"));

                        // Set the command that will be invoked by default
                        messageDialog.DefaultCommandIndex = 0;

                        // Set the command to be invoked when escape is pressed
                        messageDialog.CancelCommandIndex = 0;
                    }
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

                resultList[callNum] = result;
                callNum++;

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

            if (!gameEnd)
            {
                updateCard(resultList);
            }
        }

        private void updateCard(int[] resultList)   //get a list of result and update cardCalled and display the card accordingly
        {
            for (int i = 0; i < resultList.Length; i++) //compare against resultList[] instead of called[] because only need to check the changes and thus faster
            {
                int checkColumn = (resultList[i] - 1) / 15; //set checkColumn to one of the value {0, 1, 2, 3, 4}, checkColumn being the column to check
                for (int j = 0; j < 5; j++)
                {
                    if (card[j, checkColumn] == resultList[i])
                    {
                        cardCalled[j, checkColumn] = true;
                        chipArray[j, checkColumn].Visibility = Visibility.Visible;

                        bool win = checkWin(); //check if you have won
                        
                        //exit all loop if won
                        if (win)    
                        {
                            i = resultList.Length;
                            j = 5;
                        }
                    }
                }
            }
        }

        private bool checkWin()
        {
            bool win = false;
            bool rowClear;
            bool columnClear;
            bool diagClear;

            //check for clear by row
            for (int r = 0; r < 5; r++)
            {
                rowClear = true;
                for (int c = 0; c < 5; c++)
                {
                    if (!cardCalled[r, c])
                    {
                        rowClear = false;
                        break;
                    }
                }
                if (rowClear)
                {
                    win = true;
                    gameEnd = true;
                    youWin();
                    break;
                }
            }

            if (!win)
            {
                //check for clear by column
                for (int c = 0; c < 5; c++)
                {
                    columnClear = true;
                    for (int r = 0; r < 5; r++)
                    {
                        if (!cardCalled[r, c])
                        {
                            columnClear = false;
                            break;
                        }
                    }
                    if (columnClear)
                    {
                        win = true;
                        gameEnd = true;
                        youWin();
                        break;
                    }
                }
            }

            if (!win)
            {
                //check for top left to bottom right diagonal
                diagClear = true;
                
                for (int c = 0; c < 5; c++)
                {
                    if (!cardCalled[c, c])
                    {
                        diagClear = false;
                        break;
                    }
                }
                if (diagClear)
                {
                    win = true;
                    gameEnd = true;
                    youWin();
                }
            }
            
            if(!win)    
            {
                //check for bottom left to top right diagonal
                diagClear = true;

                for (int c = 0; c < 5; c++)
                {
                    if (!cardCalled[4 - c, c])
                    {
                        diagClear = false;
                        break;
                    }
                }
                if (diagClear)
                {
                    win = true;
                    gameEnd = true;
                    youWin();
                }
            }
            return win;
        }

        private async void youWin()
        {
            var messageDialog = new MessageDialog("Bingo!");

            messageDialog.Commands.Add(new UICommand(
            "Continue Calling"));

            messageDialog.Commands.Add(new UICommand(
            "Reset List",
            new UICommandInvokedHandler(this.CommandInvokedHandler)));

            messageDialog.Commands.Add(new UICommand(
            "Close"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 2;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 2;

            // Show the message dialog and wait
            await messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)  //Clear history list and reset Bingo card
        {
            for (int i = 0; i < 75; i++)
            {
                called[i] = false;
            }
            numCalled = 0;
            historyList.Children.Clear();

            //Reset variables
            called = new bool[75];
            numCalled = 0;
            card = new int[5, 5];
            cardCalled = new bool[5, 5];
            gameEnd = false;

            generateCard(); //generate the Bingo Card

            //Reset chip
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (j == 2)
                    {
                        if (i == 2)
                        {
                            continue;
                        }
                    }
                    chipArray[i, j].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void RestartAppBarB_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
