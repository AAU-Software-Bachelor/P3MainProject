﻿using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;

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
    /// Interaction logic for Religion.xaml
    /// </summary>
    public partial class Religion : Page
    {

        public Religion(config currentConfig)
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
            newreligion = new ObservableCollection<newReligion>(){
            new newReligion(){Name = "New Religion"}
            };
            lstReligion.ItemsSource = newreligion;
        }
        public config CurrentConfig { get; set; }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ReligionMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

        private ObservableCollection<newReligion> newreligion;

        public class newReligion
        {
            public string Name { get; set; }
        }

        private void btnReligion_ClickAdd(object sender, RoutedEventArgs e)
        {
            int i = newreligion.Count + 1;
            newreligion.Add(new newReligion() { Name = "New Religion" });
        }

        private void btnReligion_ClickDelete(object sender, RoutedEventArgs e)
        {
            var index = lstReligion.SelectedIndex;
            if (lstReligion.SelectedIndex >= 1)
            {
                newreligion.RemoveAt(index);
            }
        }
        private void OnClickAddAffectedResources(object sender, RoutedEventArgs e)
        {
            this.InitializeComponent();
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            ComboBox comboBoxOne = new ComboBox();
            comboBoxOne.Text = "Select Stat";
            comboBoxOne.IsReadOnly = true;
            comboBoxOne.IsDropDownOpen = false;
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

            this.ListAffectedResources.Items.Add(stackPanel);

        }

        private void OnClickDeleteAffectedResources(object sender, RoutedEventArgs e)
        {
            var index = ListAffectedResources.SelectedIndex;

            if (index >= 0)
            {
                ListAffectedResources.Items.RemoveAt(index);
            }

        }

        /*private void ListStarterAbilities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }*/

        /*private void OnClickSaveRace(object sender, string UID, RoutedEventArgs e)
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
            majorTrait currentMT = CurrentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])];
            currentMT.deleteContent();

            string name = (this.FindName("nameBox") as TextBox).Text;
            string playerReq = (this.FindName("playerReqBox") as TextBox).Text;
            string desc = (this.FindName("descBox") as TextBox).Text;

            currentMT.name = name;
            currentMT.description = playerReq + "\n\n" + desc;



            currentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])] = currentMT;
        }*/

    }
}


