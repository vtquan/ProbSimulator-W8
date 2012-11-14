using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

using Attack = Probability_Simulator.Common.RPG.Attack;
using Monster = Probability_Simulator.Common.RPG.Monster;
using Player = Probability_Simulator.Common.RPG.Player;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Probability_Simulator.Pages.RPG
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AxalfFightPage : Probability_Simulator.Common.LayoutAwarePage
    {
        Monster Enemy = new Monster("Axalf", "Dire Dimunitive Drawf", 50, 10, 1);
        Player You = new Player("You", 100, 10, 2);

        Random random = new Random();
        bool fled = false;  //check if the flee button has been pressed

        //store HP and MP bar starting width
        double monsterHPBarWidth;
        double playerHPBarWidth;
        double monsterMPBarWidth;
        double playerMPBarWidth;

        public AxalfFightPage()
        {
            this.InitializeComponent();

            //Idle Animation Begin
            IdleAnimation.Begin();

            //Display Monster and Player value
            pageTitle.Text = Enemy.getSubtitle() + ": " + Enemy.getName();
            monsterName.Text = Enemy.getName();
            monsterHPText.Text = Enemy.getHPStart().ToString();
            monsterMPText.Text = Enemy.getMPStart().ToString();
            yourName.Text = You.getName();
            yourHPText.Text = You.getHPStart().ToString();
            yourMPText.Text = You.getMPStart().ToString();
            Enemy.getMoveList()[0] = new Attack("Fire", 1, 5, 13);
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

        private async void AttackB_Click(object sender, RoutedEventArgs e)
        {
            disableButton();

            if (Enemy.getHP() <= 0 || You.getHP() <= 0 || fled)
            {
                battleEndMessage();  //bring up notification if you win, lose, or fled
                return;
            }
            double damage = You.attack(ref Enemy);
            ActionLogList.Children.Add(new TextBlock() { Text = "You did " + (int)damage + " damage to " + Enemy.getName() });

            //Flinch Animation for Monster 
            FlinchAnimation.Begin();
            await Task.Delay(300);
            IdleAnimation.Begin();

            if (Enemy.getHP() <= 0)
            {
                youWin();
                return;
            }
            else
            {
                monsterHPBar.Width = monsterHPBarWidth * Enemy.getHP() / Enemy.getHPStart();
                monsterHPText.Text = Enemy.getHP().ToString();
            }
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
            monsterAttack();

        }

        private void SpellB_Click(object sender, RoutedEventArgs e)
        {
            disableButton();

            if (Enemy.getHP() <= 0 || You.getHP() <= 0 || fled)
            {
                battleEndMessage();  //bring up notification if you win, lose, or fled
                return;
            }

            if (Enemy.getHP() <= 0)
            {
                youWin();
                return;
            }

            monsterAttack();

        }

        private void DefendB_Click(object sender, RoutedEventArgs e)
        {
            disableButton();

            if (Enemy.getHP() <= 0 || You.getHP() <= 0 || fled)
            {
                battleEndMessage();  //bring up notification if you win, lose, or fled
                return;
            }
            KeyValuePair<double, int> result = Enemy.attackDefended(ref You);

            ActionLogList.Children.Add(new TextBlock() { Text = Enemy.getName() + " uses " + Enemy.getMoveList()[result.Value].getName() + " and did " + (int)result.Key + " damage to you" });
           
            if (You.getHP() <= 0)
            {
                youLose();
                return;
            }
            else
            {
                yourHPBar.Width = 177 * You.getHP() / You.getHPStart();
                yourHPText.Text = You.getHP().ToString();
            }

            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom

            enableButton();
        }

        private void ItemB_Click(object sender, RoutedEventArgs e)
        {
            disableButton();

            if (Enemy.getHP() <= 0 || You.getHP() <= 0 || fled)
            {
                battleEndMessage();  //bring up notification if you win, lose, or fled
                return;
            }

            if (You.getHP() <= 0)
            {
                youLose();
                return;
            }

            if (Enemy.getHP() <= 0)
            {
                youWin();
                return;
            }

            monsterAttack();
        }
        
        private void FleeB_Click(object sender, RoutedEventArgs e)
        {
            disableButton();

            if (Enemy.getHP() <= 0 || You.getHP() <= 0 || fled)
            {
                battleEndMessage();  //bring up notification if you win, lose, or fled
                return;
            }

            if (!fled)
            {
                youFlee();
            }
            else
            {
                fled = true;
                battleEndMessage();
            }
        }

        private void monsterAttack()
        {
            if (Enemy.getHP() <= 0 || You.getHP() <= 0 || fled)
            {
                battleEndMessage();  //bring up notification if you win, lose, or fled
                return;
            }
            KeyValuePair<double, int> result = Enemy.attack(ref You);

            ActionLogList.Children.Add(new TextBlock() { Text = Enemy.getName() + " uses " + Enemy.getMoveList()[result.Value].getName() + " and did " + (int)result.Key + " damage to you" });
           
            if (You.getHP() <= 0)
            {
                battleEndMessage();
                yourHPBar.Width = 0;
                yourHPText.Text = "0";
                return;
            }
            else
            {
                yourHPBar.Width = playerHPBarWidth * You.getHP() / You.getHPStart();
                yourHPText.Text = You.getHP().ToString();
            }

            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom

            enableButton();
        }

        private void youWin()
        {
            ActionLogList.Children.Add(new TextBlock() { Text = "You Win!" });
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Music/Chrono Trigger Music - Lucca's Theme.mp3");

            monsterHPBar.Width = 0;
            monsterHPText.Text = "0";

            battleEndMessage();
        }   

        private void youLose()
        {
            ActionLogList.Children.Add(new TextBlock() { Text = "You Lose!" });
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Music/Chrono Trigger Music - Game Over.mp3");

            battleEndMessage();
        }

        private void youFlee()
        {
            ActionLogList.Children.Add(new TextBlock() { Text = "You run away!" });
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Music/Chrono Trigger Music - Game Over.mp3");

            battleEndMessage();
        }

        private async void battleEndMessage()   //display message of battle outcome
        {
            var messageDialog = new MessageDialog("");

            if (fled)
            {
                messageDialog = new MessageDialog("You fled!");
                messageDialog.Title = "Run Away";
            }
            else if (Enemy.getHP() <= 0)
            {
                messageDialog = new MessageDialog("You won!");
                messageDialog.Title = "Victory";
            }

            if (You.getHP() <= 0)
            {
                messageDialog = new MessageDialog("You lost!");
                messageDialog.Title = "Game Over";
            }

            messageDialog.Commands.Add(new UICommand(
            "Retry",
            new UICommandInvokedHandler(this.CommandInvokedHandler)));

            messageDialog.Commands.Add(new UICommand(
            "Close"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog and wait
            await messageDialog.ShowAsync();

            enableButton();
        }

        private void CommandInvokedHandler(IUICommand command)  //clear action log and reset hp, mp and music
        {
            restartGame();
        }

        private void RestartB_Click(object sender, RoutedEventArgs e)   //clear action log and reset hp, mp and music
        {
            restartGame();
        }

        private void PauseB_Click(object sender, RoutedEventArgs e) //pause background music
        {
            if (BackgroundMusic.Volume == 0)
            {
                BackgroundMusic.Volume = 1;
            }
            else
            {
                BackgroundMusic.Volume = 0;
            }
        }

        private void disableButton()    //disable button functionality to prevent rapid clicking
        {
            AttackB.IsEnabled = false;
            SpellB.IsEnabled = false;
            DefendB.IsEnabled = false;
            ItemB.IsEnabled = false; 
            FleeB.IsEnabled = false; 
        }

        private void enableButton() //enable button functionality
        {
            AttackB.IsEnabled = true;  
            SpellB.IsEnabled = true; 
            DefendB.IsEnabled = true; 
            ItemB.IsEnabled = true; 
            FleeB.IsEnabled = true;  
        }

        private void restartGame()  //clear action log and reset hp, mp and music
        {
            Enemy.setHP(Enemy.getHPStart());
            monsterHPBar.Width = 177;
            monsterHPText.Text = Enemy.getHPStart().ToString();
            You.setHP(You.getHPStart());
            yourHPBar.Width = 177;
            yourHPText.Text = You.getHPStart().ToString();
            ActionLogList.Children.Clear();
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Music/Chrono Trigger Music - Battle Theme.mp3");

            enableButton();
        }

        private void MonsterInfoGrid_SizeChanged(object sender, SizeChangedEventArgs e) //update monster info bar size, will be call when page is loaded 
        {
            monsterHPBarWidth = monsterFullHPBar.ActualWidth;
            monsterMPBarWidth = monsterFullMPBar.ActualWidth;

            if (Enemy.getHP() <= 0)
            {
                monsterHPBar.Width = 0;
                monsterHPText.Text = "0";
            }
            else
            {
                monsterHPBar.Width = monsterHPBarWidth * Enemy.getHP() / Enemy.getHPStart();
                monsterHPText.Text = Enemy.getHP().ToString();
                monsterMPBar.Width = monsterMPBarWidth * Enemy.getMP() / Enemy.getMPStart();
                monsterMPText.Text = Enemy.getMP().ToString();
            }
            
        }

        private void PlayerInfoGrid_SizeChanged(object sender, SizeChangedEventArgs e)  //update player info bar size, will be call when page is loaded 
        {
            playerHPBarWidth = yourFullMPBar.ActualWidth;
            playerMPBarWidth = yourFullMPBar.ActualWidth;

            if (You.getHP() <= 0)
            {
                yourHPBar.Width = 0;
                yourHPText.Text = "0";
            }
            else
            {
                yourHPBar.Width = playerHPBarWidth * You.getHP() / You.getHPStart();
                yourHPText.Text = You.getHP().ToString();
                yourMPBar.Width = playerMPBarWidth * You.getMP() / You.getMPStart();
                yourMPText.Text = You.getMP().ToString();
            }
        }
    }
}
