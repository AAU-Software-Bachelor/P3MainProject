using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project2
{
    /// <summary>
    /// Interaction logic for Race.xaml
    /// </summary>
    public partial class Race : Page
    {
        public Race()
        {
            InitializeComponent();
        }

        private void RaceMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            this.Content = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void OnClick1(object sender, RoutedEventArgs e)
        {
            this.InitializeComponent();
            this.ListForRaces.Items.Add("New Race");

        }

        private void OnClick2(object sender, RoutedEventArgs e)
        {
            foreach (ListViewItem eachItem in ListForRaces.SelectedItems)
            {
                if(ListForRaces.Items.Count == 1)
                {
                    throw new ArgumentOutOfRangeException("Du kan ikke slette denne race.");
                }
                else
                {
                    ListForRaces.Items.Remove(eachItem);
                }
            }

        }
        private void OnClick3(object sender, RoutedEventArgs e)
        {
            this.InitializeComponent();
            //Hent alle abilities
            //this.ListStarterAbilities.Items.Add();
        }

        private void OnClick4(object sender, RoutedEventArgs e)
        {
            //starter resources
            //hent starter resources
            this.InitializeComponent();

        }

        private void OnClick5(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            var item = button.Tag;

            ListStarterAbilities.Items.Remove(item);

        }
        private void OnClick6(object sender, RoutedEventArgs e)
        {
            //majorTrait.deleteContent()
            //get name
            //get image
            //get description
            //get cost
            //for loop through Costtype
            //for loop through freeAbilities
            //for loop through exclusions
            //for loop through dependencies / discounts
            //for loop through affectedResources

        }
    }
}
