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
    public sealed partial class RandomNumPage : Probability_Simulator.Common.LayoutAwarePage
    {
        public RandomNumPage()
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

        private void generateNumB_Click(object sender, RoutedEventArgs e)
        {
            checkInput();
        }

        private async void checkInput()
        {
            int numNumber = 0;
            Int32.TryParse(numNumberBox.Text, out numNumber);
            if (Int32.TryParse(numNumberBox.Text, out numNumber) != false && numNumber <= 1000)   //if input is valid 
            {
                checkInputRange();
            }

            else if (Int32.TryParse(numNumberBox.Text, out numNumber) != false && (numNumber > 1000 || numNumber < 0))  //if input is out of bound
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

        private async void checkInputRange()
        {
            int numNumber = 0;
            Int32.TryParse(numNumberBox.Text, out numNumber);

            int minNumber = 0;
            Int32.TryParse(numNumberBox.Text, out minNumber);

            if (Int32.TryParse(numNumberBox.Text, out minNumber) != false && minNumber <= 2147483647)   //if input is valid 
            {
                Int32 maxNumber = 0;
                Int32.TryParse(numNumberBox.Text, out maxNumber);

                if (Int32.TryParse(numNumberBox.Text, out maxNumber) != false && maxNumber <= 2147483647)   //if input is valid 
                {
                    if (minNumber <= maxNumber)
                    {
                        generateNumber(numNumber);
                    }
                }

                else if (Int32.TryParse(numNumberBox.Text, out maxNumber) != false && (maxNumber > 2147483647 || maxNumber < 0))  //if input is out of bound
                {
                    var messageDialog = new MessageDialog("Please enter a number between 0 and 2147483647 for maximum number.");
                    messageDialog.Title = "Invalid Input";

                    // Show the message dialog and wait
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var messageDialog = new MessageDialog("Please enter a real number for maximum number.");   //if input is not an integer
                    messageDialog.Title = "Invalid Input";

                    // Show the message dialog and wait
                    await messageDialog.ShowAsync();
                }
            }

            else if (Int32.TryParse(numNumberBox.Text, out minNumber) != false && (minNumber > 2147483647 || minNumber < 0))  //if input is out of bound
            {
                var messageDialog = new MessageDialog("Please enter a number between 0 and 4294967295 for minimum number.");
                messageDialog.Title = "Invalid Input";

                // Show the message dialog and wait
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog = new MessageDialog("Please enter a real number for minimum number.");   //if input is not an integer
                messageDialog.Title = "Invalid Input";

                // Show the message dialog and wait
                await messageDialog.ShowAsync();
            }

            
        }

        private void generateNumber(int numNumber)
        {
            Random random = new Random();

            int minNumber = 0;
            Int32.TryParse(MinNumBox.Text, out minNumber);
            int maxNumber = 0;
            Int32.TryParse(MaxNumBox.Text, out maxNumber);

            for (int i = 0; i < numNumber; i++)
            {
                historyList.Children.Add(new TextBlock() { Text = random.Next(minNumber, maxNumber + 1).ToString() });
            }
            historyList.Children.Add(new TextBlock() { Text = "  " });
            historyScroll.UpdateLayout();
            historyScroll.ScrollToVerticalOffset(historyList.ActualHeight);
        }
    }
}
