using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace Project2
{
    /// <summary>
    /// Interaction logic for Race.xaml
    /// </summary>
    public partial class Race : Page
    {

        public Race(config currentConfig)
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
        }
        config CurrentConfig;

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
            int i = 1;
            string y = i.ToString();
            string f = "New Race";
            ListForRaces.Items.Add(new ListViewItem
            {
                Uid = Guid.NewGuid().ToString(),
                Content = f
            }) ;
            i++;
        }

        private void OnClickDeleteRaceList(object sender, EventArgs e)
        {
            var itemsender = sender as Button;
            var data = itemsender.DataContext;
            
            

            foreach (ListViewItem item in ListForRaces.Items)
            {
                if(itemsender.Uid == item.Uid)
                {
                    ListForRaces.Items.Remove(data);
                }
             /*
                if (ListForRaces.Items.Count == 1)
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
            this.InitializeComponent();
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            ComboBox comboBoxOne = new ComboBox();
            comboBoxOne.Text = "Select Stat";
            comboBoxOne.IsReadOnly = true;
            comboBoxOne.IsDropDownOpen = true;
            comboBoxOne.Margin = new Thickness(5, 5, 0, 0);
            comboBoxOne.Height = 24;
            comboBoxOne.Width = 185;
            comboBoxOne.SelectionChanged += ComboBox_SelectionChanged;

            stackPanel.Children.Add(comboBoxOne);

            TextBox textBox = new TextBox();
            textBox.Margin = new Thickness(15, 13, 0, 0);
            textBox.Width = 40;
            textBox.Height = 24;
            textBox.VerticalAlignment = VerticalAlignment.Top;

            stackPanel.Children.Add(textBox);

            StackPanel childStackPanel = new StackPanel();
            childStackPanel.Orientation = Orientation.Vertical;

            Image imagePlus = new Image();
            imagePlus.Height = 20;
            imagePlus.Width = 20;
            imagePlus.Margin = new Thickness(1, 5, 0, 0);
            imagePlus.Stretch = Stretch.Fill;
            childStackPanel.Children.Add(imagePlus);

            Image imageMinus = new Image();
            imageMinus.Height = 20;
            imageMinus.Width = 20;
            imageMinus.Margin = new Thickness(1, 0, 0, 0);
            imageMinus.Stretch = Stretch.Fill;
            childStackPanel.Children.Add(imageMinus);
            
            stackPanel.Children.Add(childStackPanel);

            this.ListStarterResources.Items.Add(stackPanel);

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

            string UID = "2-001";
            string[] id = UID.Split('-');
            majorTrait currentMT = CurrentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])];
            //currentMT.deleteContent();

            string name = (this.FindName("nameBox") as TextBox).Text;
            string playerReq = (this.FindName("playerReqBox") as TextBox).Text;
            string desc = (this.FindName("descBox") as TextBox).Text;

            (this.FindName("nameBox") as TextBox).Text = Guid.NewGuid().ToString(); //testing

            //currentMT.name = name;
           // currentMT.description = playerReq + "\n\n" + desc;



            CurrentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])] = currentMT;
        }

    }
}
