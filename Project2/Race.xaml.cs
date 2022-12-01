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
using System.Xml.Linq;
using static Project2.Race;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
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
            newrace = new ObservableCollection<majorTrait>();
            foreach (majorTrait race in CurrentConfig.RacList)
            {
                newrace.Add(race);
            }

            lstRaces.ItemsSource = newrace;
        }
        public config CurrentConfig { get; set; }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RaceMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            MainWindow mainWindow = new MainWindow(CurrentConfig);
            Application.Current.MainWindow.Content = mainWindow;
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }*/


        private ObservableCollection<majorTrait> newrace;

        private void btnRaces_ClickAdd(object sender, RoutedEventArgs e)
        {
            int i = newrace.Count + 1;
            newrace.Add(new majorTrait(CurrentConfig.newUID("Race"))  { Name = "new race" });
        }

        private void btnRaces_ClickDelete(object sender, RoutedEventArgs e)
        {
            var index = lstRaces.SelectedIndex;
            if (lstRaces.SelectedIndex >= 0)

            {
                newrace.RemoveAt(index);
            }
        }


        private void OnClickAddStarterAbilities(object sender, RoutedEventArgs e)
        {
            this.InitializeComponent();
            ComboBox comboBox = new ComboBox();
            comboBox.IsReadOnly = true;
            comboBox.IsDropDownOpen = true;
            comboBox.Margin = new Thickness(5, 5, 0, 0);
            comboBox.Height = 24;
            comboBox.Width = 185;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            comboBox.DisplayMemberPath = "Name";
            foreach (majorTrait abi in CurrentConfig.AbilList)
            {
                comboBox.Items.Add(abi);
            }

            this.ListStarterAbilities.Items.Add(comboBox);

        }

        private void OnClickDeleteStarterAbilities(object sender, RoutedEventArgs e)
        {
            var index = ListStarterAbilities.SelectedIndex;

            if(index >= 0) 

            {
                ListStarterAbilities.Items.RemoveAt(index);
            }
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

            comboBoxOne.DisplayMemberPath = "Name";
            foreach (majorTrait abi in CurrentConfig.AbilList)
            {
                comboBoxOne.Items.Add(abi);
            }


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
            var index = ListStarterResources.SelectedIndex;
            if (index >= 0)
            {
                ListStarterResources.Items.RemoveAt(index);
            }
        }

        private void ListStarterAbilities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnClickSaveRace(object sender, string UID, RoutedEventArgs e)
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

            
            majorTrait currentMT = CurrentConfig.GetTrait(UID);
            currentMT.deleteContent();
            

            string name = (this.FindName("nameBox") as TextBox).Text;
            string playerReq = (this.FindName("playerReqBox") as TextBox).Text;
            string desc = (this.FindName("descBox") as TextBox).Text;

            foreach (ComboBox BOX in (this.FindName("ListStarterAbilities") as ListView).Items)
            {
                string TempUID =  BOX.SelectedValue as string;
                currentMT.freeAbilities.Add(TempUID);
            }

            foreach (StackPanel PANEL in (this.FindName("ListStarterResources") as ListView).Items)
            {
                foreach (ComboBox box in PANEL.Children)
                {
                    foreach (TextBox textBox in PANEL.Children)
                    {
                        string TempUID = box.SelectedValue as string;
                        int TempVal = int.Parse(textBox.Text);
                        currentMT.addAffectedResources(TempUID, TempVal);
                    }
                }

            }




            //CurrentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])] = currentMT;
        }

    }
}
