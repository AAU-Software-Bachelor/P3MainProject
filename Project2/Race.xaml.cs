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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void OnClickAddRaceList(object sender, RoutedEventArgs e)
        {
            ListForRaces.Items.Add("New Race");
        }

        private void OnClickDeleteRaceList(object sender, EventArgs e)
        {
            foreach (ListViewItem itemSelected in ListForRaces.SelectedItems)
            {
                ListForRaces.Items.Remove(itemSelected);
                /*if (ListForRaces.Items.Count == 1)
                {
                    throw new ArgumentOutOfRangeException("Du kan ikke slette denne race.");
                }
                else
                {
                    ListForRaces.Items.Remove(itemSelected);
                }*/
            }

        }
        private void OnClickAddStarterAbilities(object sender, RoutedEventArgs e)
        {
            this.InitializeComponent();
            ComboBox comboBox = new ComboBox();
            comboBox.Text = "Select Ability";
            comboBox.IsReadOnly = true;
            comboBox.IsDropDownOpen = true;
            comboBox.Margin = new Thickness(5, 5, 0, 0);
            comboBox.Height = 24;
            comboBox.Width = 185;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            this.ListStarterAbilities.Items.Add(comboBox);

        }

        private void OnClickDeleteStarterAbilities(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var item = button.Tag;

            ListStarterAbilities.Items.Remove(item);

        }


        private void OnClickAddStarterResources(object sender, RoutedEventArgs e)
        {
            //starter resources
            //hent starter resources
            this.InitializeComponent();

        }

        private void OnClickDeleteStarterResources(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var item = button.Tag;

            ListStarterAbilities.Items.Remove(item);

        }

        private void OnClickSaveRace(object sender, RoutedEventArgs e)
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
            string[] id = UID.Split('-');
            majorTrait currentMT = currentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])];
            currentMT.deleteContent();

            string name = (this.FindName("nameBox") as TextBox).Text;
            string playerReq = (this.FindName("playerReqBox") as TextBox).Text;
            string desc = (this.FindName("descBox") as TextBox).Text;

            currentMT.name = name;
            currentMT.description = playerReq + "\n\n" + desc;
            


            currentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])] = currentMT;
        }
        
    }
}
