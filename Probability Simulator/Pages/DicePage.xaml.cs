﻿using System;
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
    public sealed partial class DicePage : Probability_Simulator.Common.LayoutAwarePage
    {
        public DicePage()
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

        private void DiceRollB_Click(object sender, RoutedEventArgs e)
        {
            CheckInput();   //check input is in a seperate function so other event handler can call it
        }

        public async void CheckInput()  //check input and bring up notification if incorrect or call dice rolling function if correct
        {
            int numRoll = 0;
            Int32.TryParse(numDiceBox.Text, out numRoll);
            if (Int32.TryParse(numDiceBox.Text, out numRoll) != false && (numRoll <= 1000 && numRoll >= 0))   //if input is valid 
            {
                int[] numberOfResult;   //store the number of times n was a result in n-1 index
                if (_4Check.IsChecked == true)
                    numberOfResult = diceRoll(numRoll, 4);

                else if (_6Check.IsChecked == true)
                    numberOfResult = diceRoll(numRoll, 6);

                else if (_8Check.IsChecked == true)
                    numberOfResult = diceRoll(numRoll, 8);

                else if (_10Check.IsChecked == true)
                    numberOfResult = diceRoll(numRoll, 10);

                else if (_12Check.IsChecked == true)
                    numberOfResult = diceRoll(numRoll, 12);

                else
                    numberOfResult = diceRoll(numRoll, 20);

                updateResult(numberOfResult);   //update the result total and history list given a list of result
            }

            else if (Int32.TryParse(numDiceBox.Text, out numRoll) != false && (numRoll > 1000 || numRoll < 0))  //if input is out of bound
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

        public int[] diceRoll(int numRoll, int numFace)
        {

            Random random = new Random();
            int result = 0;

            int[] numTotal = new int[20];   //store the number of times n was a result in n-1 index

            for (int i = 0; i < numRoll; i++)
            {
                result = random.Next(0, numFace);
                result++;
                switch (result)
                {
                    case 1:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "One" });
                        break;
                    case 2:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Two" });
                        break;
                    case 3:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Three" });
                        break;
                    case 4:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Four" });
                        break;
                    case 5:
                       numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Five" });
                        break;
                    case 6:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Six" });
                        break;
                    case 7:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Seven" });
                        break;
                    case 8:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Eight" });
                        break;
                    case 9:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Nine" });
                        break;
                    case 10:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Ten" });
                        break;
                    case 11:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Eleven" });
                        break;
                    case 12:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Twelve" });
                        break;
                    case 13:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Thirteen" });
                        break;
                    case 14:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Fourteen" });
                        break;
                    case 15:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Fifteen" });
                        break;
                    case 16:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Sixteen" });
                        break;
                    case 17:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Seventeen" });
                        break;
                    case 18:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Eighteen" });
                        break;
                    case 19:
                        numTotal[result - 1]++;
                        historyList.Children.Add(new TextBlock() { Text = "Nineteen" });
                        break;
                    default:
                        numTotal[19]++;
                        historyList.Children.Add(new TextBlock() { Text = "Twenty" });
                        break;
                }
            }
            return numTotal;
            
        }

        public void updateResult(int[] numTotal)
        {
            _1Box.Text = (Int32.Parse(_1Box.Text) + numTotal[0]).ToString();
            _2Box.Text = (Int32.Parse(_2Box.Text) + numTotal[1]).ToString();
            _3Box.Text = (Int32.Parse(_3Box.Text) + numTotal[2]).ToString();
            _4Box.Text = (Int32.Parse(_4Box.Text) + numTotal[3]).ToString();
            _5Box.Text = (Int32.Parse(_5Box.Text) + numTotal[4]).ToString();
            _6Box.Text = (Int32.Parse(_6Box.Text) + numTotal[5]).ToString();
            _7Box.Text = (Int32.Parse(_7Box.Text) + numTotal[6]).ToString();
            _8Box.Text = (Int32.Parse(_8Box.Text) + numTotal[7]).ToString();
            _9Box.Text = (Int32.Parse(_9Box.Text) + numTotal[8]).ToString();
            _10Box.Text = (Int32.Parse(_10Box.Text) + numTotal[9]).ToString();
            _11Box.Text = (Int32.Parse(_11Box.Text) + numTotal[10]).ToString();
            _12Box.Text = (Int32.Parse(_12Box.Text) + numTotal[11]).ToString();
            _13Box.Text = (Int32.Parse(_13Box.Text) + numTotal[12]).ToString();
            _14Box.Text = (Int32.Parse(_14Box.Text) + numTotal[13]).ToString();
            _15Box.Text = (Int32.Parse(_15Box.Text) + numTotal[14]).ToString();
            _16Box.Text = (Int32.Parse(_16Box.Text) + numTotal[15]).ToString();
            _17Box.Text = (Int32.Parse(_17Box.Text) + numTotal[16]).ToString();
            _18Box.Text = (Int32.Parse(_18Box.Text) + numTotal[17]).ToString();
            _19Box.Text = (Int32.Parse(_19Box.Text) + numTotal[18]).ToString();
            _20Box.Text = (Int32.Parse(_20Box.Text) + numTotal[19]).ToString();

            historyList.Children.Add(new TextBlock() { Text = "  " });
            historyScroll.UpdateLayout();
            historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);
        }
    }
}
