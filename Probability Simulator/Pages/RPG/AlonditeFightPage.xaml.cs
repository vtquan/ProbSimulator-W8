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
using Spell = Probability_Simulator.Common.RPG.Spell;
using Item = Probability_Simulator.Common.RPG.Item;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Probability_Simulator.Pages.RPG
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AlonditeFightPage : Probability_Simulator.Common.LayoutAwarePage
    {
        Monster Enemy;
        Player You;

        Button[] itemButton;
        Button[] spellButton;

        Random random = new Random();
        bool fled = false;  //check if the flee button has been pressed

        //store HP and MP bar starting width
        double monsterHPBarWidth;
        double playerHPBarWidth;
        double monsterMPBarWidth;
        double playerMPBarWidth;

        public AlonditeFightPage()
        {
            this.InitializeComponent();

            setUpPage();    //initialyze the page with proper information
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

        private void setUpPage()
        {
            //Idle Animation Begin
            IdleAnimation.Begin();

            //Create Monster and Player value
            Enemy = new Monster("Alondite", "Elder Lord", 200, 20);
            You = new Player("You", 100, 100);

            //Creating and adding attacks to monster
            Attack Smash = new Attack("Smash", 1, 10, 15);
            Attack Crush = new Attack("Crush", 2, 10, 20);
            Attack Obliterate = new Attack("Obliterate", 1, 20, 20);
            Attack Claw = new Attack("Claw", 3, 10, 15);
            
            Enemy.addAttack(Smash);
            Enemy.addAttack(Crush);
            Enemy.addAttack(Obliterate);
            Enemy.addAttack(Claw);

            //Creating and adding items to player
            Item Potion = new Item("Potion", 20);
            Item MegaPotion = new Item("Mega Potion", 50);
            Item UltraPotion = new Item("Ultra Potion", 100);

            You.addItem(Potion);
            You.addItem(Potion);
            You.addItem(Potion);
            You.addItem(MegaPotion);
            You.addItem(MegaPotion);
            You.addItem(UltraPotion);
            You.addItem(UltraPotion);

            //Creating and adding spells  to player
            Spell Fire = new Spell("Fire", 2, 15, 25, 15);
            Spell Water = new Spell("Water", 3, 20, 30, 20);
            Spell Earth = new Spell("Earth", 3, 25, 35, 25);
            Spell Air = new Spell("Air", 3, 30, 40, 30);

            You.addSpell(Fire);
            You.addSpell(Water);
            You.addSpell(Earth);
            You.addSpell(Air);

            //Change player default attack
            Attack defaultAttack = new Attack("Attack", 3, 10, 20);
            You.setDefaultAttack(defaultAttack);

            //Display Monster and Player value
            pageTitle.Text = Enemy.getSubtitle() + ": " + Enemy.getName();
            monsterName.Text = Enemy.getName();
            monsterHPText.Text = Enemy.getHPStart().ToString();
            monsterMPText.Text = Enemy.getMPStart().ToString();
            yourName.Text = You.getName();
            yourHPText.Text = You.getHPStart().ToString();
            yourMPText.Text = You.getMPStart().ToString();
        }

        private async void spellClick(object sender, RoutedEventArgs e)
        {
            //Display Action Log list
            ActionLogLabel.Visibility = Visibility.Visible;
            ActionLogScroll.Visibility = Visibility.Visible;
            SpellListLabel.Visibility = Visibility.Collapsed;
            SpellListScroll.Visibility = Visibility.Collapsed;

            //Find item and store item values for easy display
            int spellIndex = You.findSpell(((Button)sender).Content.ToString().Substring(0, ((Button)sender).Content.ToString().Length - 7));    //get "Spell" instead of "Spell - 10mp"
            string spellName = You.getSpellList()[spellIndex].getName();
            int spellDamage;

            //Use selected item
            KeyValuePair<bool, int[]> result = You.useSpell(spellName, ref Enemy);
            spellDamage = result.Value[0]; 
            if (!result.Key)
            {
                ActionLogList.Children.Add(new TextBlock() { Text = "You do not have enough mp" });
                enableButton();
            }
            else
            {
                //Flinch Animation for Monster 
                FlinchAnimation.Begin();
                await Task.Delay(300);
                IdleAnimation.Begin();

                ActionLogList.Children.Add(new TextBlock() { Text = "You use " + spellName + " and did " + spellDamage + " damage" });

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


                monsterHPBar.Width = monsterHPBarWidth * Enemy.getHP() / Enemy.getHPStart();
                monsterHPText.Text = Enemy.getHP().ToString();
                monsterMPBar.Width = monsterMPBarWidth * Enemy.getMP() / Enemy.getMPStart();
                monsterMPText.Text = Enemy.getMP().ToString();

                monsterAttack();
            }
        }

        void itemClick(object sender, RoutedEventArgs e)
        {       
            //Display Action Log list
            ActionLogLabel.Visibility = Visibility.Visible;
            ActionLogScroll.Visibility = Visibility.Visible;
            ItemListLabel.Visibility = Visibility.Collapsed;
            ItemListScroll.Visibility = Visibility.Collapsed;

            //Find item and store item values for easy display
            int itemIndex = You.findItem(((Button)sender).Content.ToString().Substring(0, ((Button)sender).Content.ToString().Length - 3));
            string itemName = You.getItemList()[itemIndex].getName();   //get "Item" instead of "Item xNumItem"
            int itemHeal = You.getItemList()[itemIndex].getHeal();

            //Use selected item
            bool used = You.useItem(itemName);    
            if (!used)
            {
                ActionLogList.Children.Add(new TextBlock() { Text = "You don't have that item!" });
                enableButton();
            }
            else
            {
                ActionLogList.Children.Add(new TextBlock() { Text = "You use " + itemName + " and recover " + itemHeal.ToString() + "hp" }); 

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
                monsterMPBar.Width = monsterMPBarWidth * Enemy.getMP() / Enemy.getMPStart();
                monsterMPText.Text = Enemy.getMP().ToString();
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

            //Set up spell list
            SpellList.Children.Clear();
            spellButton = new Button[You.getSpellList().Length];
            for (int i = 0; i < You.getNumSpells(); i++)
            {
                if (You.getSpellList()[i] == null)
                {
                    break;
                }
                spellButton[i] = new Button() { Content = You.getSpellList()[i].getName() + " - " +You.getSpellList()[i].getMPCost()+"mp" };     //new button with content = "Spell - MPCostmp";
                spellButton[i].Click += spellClick;
                SpellList.Children.Add(spellButton[i]);
            }

            //Display item list
            ActionLogLabel.Visibility = Visibility.Collapsed;
            ActionLogScroll.Visibility = Visibility.Collapsed;

            SpellListLabel.Visibility = Visibility.Visible;
            SpellListScroll.Visibility = Visibility.Visible;

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
                yourHPBar.Width = playerHPBarWidth * You.getHP() / You.getHPStart();
                yourHPText.Text = You.getHP().ToString();
                yourMPBar.Width = playerHPBarWidth * You.getMP() / You.getMPStart();
                yourMPText.Text = You.getMP().ToString();
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

            //Set up item list
            ItemList.Children.Clear();
            itemButton = new Button[You.getItemList().Length];
            for (int i = 0; i < You.getItemList().Length; i++)
            {
                if (You.getItemList()[i] == null)
                {
                    break;
                }
                itemButton[i] = new Button() { Content = You.getItemList()[i].getName() + " x" + You.getNumItemList()[i] };     //new button with content = "Item xNumItem";
                itemButton[i].Click += itemClick;
                ItemList.Children.Add(itemButton[i]);
            }

            //Display item list
            ActionLogLabel.Visibility = Visibility.Collapsed;
            ActionLogScroll.Visibility = Visibility.Collapsed;

            ItemListLabel.Visibility = Visibility.Visible;
            ItemListScroll.Visibility = Visibility.Visible;
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
                youLose();
                yourHPBar.Width = 0;
                yourHPText.Text = "0";
                return;
            }
            else
            {
                yourHPBar.Width = playerHPBarWidth * You.getHP() / You.getHPStart();
                yourHPText.Text = You.getHP().ToString();
                yourMPBar.Width = playerHPBarWidth * You.getMP() / You.getMPStart();
                yourMPText.Text = You.getMP().ToString();
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
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Musics/Chrono Trigger Music - Lucca's Theme.mp3");

            monsterHPBar.Width = 0;
            monsterHPText.Text = "0";

            //Dying animation for Enemy
            DeadAnimation.Begin();

            battleEndMessage();
        }   

        private void youLose()
        {
            ActionLogList.Children.Add(new TextBlock() { Text = "You Lose!" });
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Musics/Chrono Trigger Music - Game Over.mp3");

            battleEndMessage();
        }

        private void youFlee()
        {
            ActionLogList.Children.Add(new TextBlock() { Text = "You run away!" });
            ActionLogScroll.UpdateLayout();   //make sure historyScroll is update to include the added element
            ActionLogScroll.ScrollToVerticalOffset(ActionLogList.ActualHeight);     //scroll to bottom
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Musics/Chrono Trigger Music - Game Over.mp3");
            fled = true;

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
            viewbox.Opacity = 100;
            Enemy.setHP(Enemy.getHPStart());
            monsterHPBar.Width = monsterFullHPBar.ActualWidth;
            monsterMPBar.Width = monsterFullMPBar.ActualWidth;
            monsterHPText.Text = Enemy.getHPStart().ToString();
            You.setHP(You.getHPStart());
            yourHPBar.Width = yourFullHPBar.ActualWidth;
            yourMPBar.Width = yourFullMPBar.ActualWidth;
            yourHPText.Text = You.getHPStart().ToString();
            ActionLogList.Children.Clear();
            BackgroundMusic.Source = new Uri(this.BaseUri, "ms-appx:///Assets/Musics/Chrono Trigger Music - Battle Theme.mp3");

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
