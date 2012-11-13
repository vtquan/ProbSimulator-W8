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

using Attack = Probability_Simulator.Common.Attack;
using Monster = Probability_Simulator.Common.Monster;
using Player = Probability_Simulator.Common.Player;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Probability_Simulator.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    
    //public class MeleeAttack : Attack
    //{
    //}

    //public class MagicAttack : Attack
    //{
    //}

    
    

    

    public sealed partial class EasterEggPage : Probability_Simulator.Common.LayoutAwarePage
    {
        Monster Enemy = new Monster("Axalf", 50, 10, 1);
        Player You = new Player("You", 100, 10, 2);

        Random random = new Random();

        

        public EasterEggPage()
        {
            this.InitializeComponent();


            //Idle Animation Begin
            IdleAnimation.Begin();

            //Load Enemy and You
            monsterName.Text = Enemy.getName();
            monsterHealthText.Text = Enemy.getHPStart().ToString();
            yourName.Text = You.getName();
            yourHealthText.Text = You.getHPStart().ToString();
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
            if (Enemy.getHP() <= 0 || You.getHP() <= 0)
            {
                var messageDialog = new MessageDialog("");
                if (Enemy.getHP() <= 0)
                {
                    messageDialog = new MessageDialog("You won!");
                    messageDialog.Title = "Victory";
                }

                if (You.getHP() <= 0)
                {
                    messageDialog = new MessageDialog("You lose!");
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
                return;
            }
            double damage = 0;
            damage = random.Next(1, 15);

            AttackedAnimation.Begin();
            await Task.Delay(300);
            IdleAnimation.Begin();

            Enemy.setHP(Enemy.getHP() - (int)damage);
            ActionLogList.Children.Add(new TextBlock() { Text = "You did " + (int)damage + " damage to Enemy" });

            if (Enemy.getHP() <= 0)
            {
                youWin();
                monsterHealthBar.Width = 0;
                monsterHealthText.Text = "0";
            }
            else
            {
                monsterHealthBar.Width = 177 * Enemy.getHP() / Enemy.getHPStart();
                monsterHealthText.Text = Enemy.getHP().ToString();
            }
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
            monsterAttack();

        }

        private async void DefendB_Click(object sender, RoutedEventArgs e)
        {
            if (Enemy.getHP() <= 0 || You.getHP() <= 0)
            {
                var messageDialog = new MessageDialog("");
                if(Enemy.getHP() <= 0)
                {
                    messageDialog = new MessageDialog("You won!");
                    messageDialog.Title = "Victory";
                }

                if (You.getHP() <= 0)
                {
                    messageDialog = new MessageDialog("You lose!");
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
                return;
            }
            double damage;
            int moveChosen = random.Next(0, Enemy.getNumAttacks()); //choose from one of the possible attacks
            bool crit;
            crit = (Enemy.getMoveList()[moveChosen].getCritPercent() == random.Next(1, 11));  //if crit == 1, monster do a critical attack
            damage = random.Next((int)Enemy.getMoveList()[moveChosen].getMinDamage(), (int)Enemy.getMoveList()[moveChosen].getMaxDamage());
            if (crit)
            {
                damage = damage * 1.5;
            }
            damage = damage / 2;
            You.setHP(You.getHP() - (int)damage);
            ActionLogList.Children.Add(new TextBlock() { Text = Enemy.getName() +" use " + Enemy.getMoveList()[moveChosen].getName() + " and did " + (int)damage + " damage to you" });

            if (You.getHP() <= 0)
            {
                youLose();
                yourHealthBar.Width = 0;
                yourHealthText.Text = "0";
            }
            else
            {
                yourHealthBar.Width = 177 * You.getHP() / You.getHPStart();
                yourHealthText.Text = You.getHP().ToString();
            }

            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom

        }

        private void FleeB_Click(object sender, RoutedEventArgs e)
        {
            ActionLogList.Children.Add(new TextBlock() { Text = "You run away!" });
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
        }

        private async void monsterAttack()
        {
            if (Enemy.getHP() <= 0 || You.getHP() <= 0)
            {
                var messageDialog = new MessageDialog("");
                if (Enemy.getHP() <= 0)
                {
                    messageDialog = new MessageDialog("You won!");
                    messageDialog.Title = "Victory";
                }

                if (You.getHP() <= 0)
                {
                    messageDialog = new MessageDialog("You lose!");
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
                return;
            }
            double damage;
            int moveChosen = random.Next(0, Enemy.getNumAttacks()); //choose from one of the possible attacks
            bool crit;
            crit = (Enemy.getMoveList()[moveChosen].getCritPercent() == random.Next(1, 11));  //if crit == 1, monster do a critical attack
            damage = random.Next((int)Enemy.getMoveList()[moveChosen].getMinDamage(), (int)Enemy.getMoveList()[moveChosen].getMaxDamage());
            if (crit)
            {
                damage = damage * 1.5;
            }
            You.setHP(You.getHP() - (int)damage);
            ActionLogList.Children.Add(new TextBlock() { Text = Enemy.getName() +" uses "+ Enemy.getMoveList()[moveChosen].getName() + " and did " + (int)damage + " damage to you" });

            if (You.getHP() <= 0)
            {
                youLose();
                yourHealthBar.Width = 0;
                yourHealthText.Text = "0";
            }
            else
            {
                yourHealthBar.Width = 177 * You.getHP() / You.getHPStart();
                yourHealthText.Text = You.getHP().ToString();
            }

            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
        }

        private void youLose()
        {
            ActionLogList.Children.Add(new TextBlock() { Text = "You Lose!" });
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Music/Chrono Trigger Music - Game Over.mp3");
        }

        private void youWin()
        {
            ActionLogList.Children.Add(new TextBlock() { Text = "You Win!" });
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom

            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Music/Chrono Trigger Music - Lucca's Theme.mp3");
            
        }

        private void CommandInvokedHandler(IUICommand command)  //clear action log and reset hp and mp
        {
            Enemy.setHP(Enemy.getHPStart());
            monsterHealthBar.Width = 177;
            monsterHealthText.Text = Enemy.getHPStart().ToString();
            You.setHP(You.getHPStart());
            yourHealthBar.Width = 177;
            yourHealthText.Text = You.getHPStart().ToString();
            ActionLogList.Children.Clear();
        }
    }
}
