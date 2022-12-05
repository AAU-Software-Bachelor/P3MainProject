using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Versioning;
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

namespace Project2
{
    /// <summary>
    /// Interaction logic for Abilities.xaml
    /// </summary>
    public partial class AbilitiesPage : Page
    {
        private ObservableCollection<majorTrait> newability;
        public AbilitiesPage(config currentConfig)
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
            newability = new ObservableCollection<majorTrait>();
            foreach (majorTrait ability in CurrentConfig.AbilList)
            {
                newability.Add(ability);
            }

            lstAbility.ItemsSource = newability;
        }
        public config CurrentConfig { get; set; }

        private void AbilitiesMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            MainWindow mainWindow = new MainWindow(CurrentConfig);
            Application.Current.MainWindow.Content = mainWindow;
        }

        private void btnAbility_ClickAdd(object sender, RoutedEventArgs e)
        {
            int i = newability.Count + 1;
            newability.Add(new majorTrait(CurrentConfig.newUID("Ability")) { Name = "new ability" });
        }

        private void btnAbility_ClickDelete(object sender, RoutedEventArgs e)
        {
            var index = lstAbility.SelectedIndex;
            if (lstAbility.SelectedIndex >= 0)

            {
                newability.RemoveAt(index);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }



        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {


        }

        private object and = "AND";
        public string or = "OR";
        private string RaceC = "Race";
        private string AbilityC = "Ability";
        public string[] names = { "Race", "Ability" };
        private void OnClickAddResourcesList(object sender, RoutedEventArgs e)
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
            foreach (resourceTrait res in CurrentConfig.ResList)
            {
                comboBox.Items.Add(res);
            }
            ComboBox comboBoxAO = new ComboBox();
            comboBoxAO.IsReadOnly = true;
            comboBoxAO.IsDropDownOpen = true;
            comboBoxAO.Margin = new Thickness(5, 5, 0, 0);
            comboBoxAO.Height = 26;
            comboBoxAO.Width = 50;
            comboBoxAO.SelectionChanged += ComboBox1_SelectionChanged;
            comboBoxAO.Items.Add(or);


            this.ListResources.Items.Add(comboBoxAO);
            this.ListResources.Items.Add(comboBox);

        }

        private void OnClickAddRequirmentsList(object sender, RoutedEventArgs e)
        {
            this.InitializeComponent();
            //StackPanel stackPanel = new StackPanel();
            //stackPanel.Orientation = Orientation.Horizontal;

            ComboBox comboBox = new ComboBox();
            comboBox.IsReadOnly = true;
            comboBox.IsDropDownOpen = true;
            comboBox.Margin = new Thickness(5, 5, 0, 0);
            comboBox.Height = 24;
            comboBox.Width = 185;
            comboBox.VerticalAlignment = VerticalAlignment.Top;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            comboBox.DisplayMemberPath = "Name";



            ComboBox comboBoxAO = new ComboBox();
            comboBoxAO.IsReadOnly = true;
            comboBoxAO.IsDropDownOpen = true;
            comboBoxAO.Margin = new Thickness(5, 5, 0, 0);
            comboBoxAO.Height = 26;
            comboBoxAO.Width = 50;
            comboBoxAO.VerticalAlignment = VerticalAlignment.Top;
            comboBoxAO.SelectionChanged += ComboBox1_SelectionChanged;
            comboBoxAO.Items.Add(and);
            comboBoxAO.Items.Add(or);


            //stackPanel.Children.Add(comboBoxAO);
            //stackPanel.Children.Add(comboBox);


            if (ListRequirments.SelectedValue == ReqAO1)
            {
                if (Combobox1.SelectedItem == "Race")
                {
                    foreach (majorTrait rac in CurrentConfig.RacList)
                    {
                        comboBox.Items.Add(rac);
                    }
                }
                if (Combobox1.SelectedItem == "Ability")
                {
                    foreach (majorTrait abi in CurrentConfig.AbilList)
                    {
                        comboBox.Items.Add(abi);
                    }
                }
                if (Combobox1.SelectedItem == "Resources")
                {
                    foreach (resourceTrait res in CurrentConfig.ResList)
                    {
                        comboBox.Items.Add(res);
                    }
                }
                if (Combobox1.SelectedItem == "Career")
                {
                    foreach (majorTrait car in CurrentConfig.CarList)
                    {
                        comboBox.Items.Add(car);
                    }
                }
                if (Combobox1.SelectedItem == "Religion")
                {
                    foreach (majorTrait rel in CurrentConfig.RelList)
                    {
                        comboBox.Items.Add(rel);
                    }
                }

                ReqAO1.Children.Add(comboBoxAO);
                ReqAO1.Children.Add(comboBox);

            }
            if (ListRequirments.SelectedValue == ReqAO2)
            {
                if (Combobox2.SelectedItem == "Race")
                {
                    foreach (majorTrait rac in CurrentConfig.RacList)
                    {
                        comboBox.Items.Add(rac);
                    }
                }
                if (Combobox2.SelectedItem == "Ability")
                {
                    foreach (majorTrait abi in CurrentConfig.AbilList)
                    {
                        comboBox.Items.Add(abi);
                    }
                }
                if (Combobox2.SelectedItem == "Resources")
                {
                    foreach (resourceTrait res in CurrentConfig.ResList)
                    {
                        comboBox.Items.Add(res);
                    }
                }
                if (Combobox2.SelectedItem == "Career")
                {
                    foreach (majorTrait car in CurrentConfig.CarList)
                    {
                        comboBox.Items.Add(car);
                    }
                }
                if (Combobox2.SelectedItem == "Religion")
                {
                    foreach (majorTrait rel in CurrentConfig.RelList)
                    {
                        comboBox.Items.Add(rel);
                    }
                }
                ReqAO2.Children.Add(comboBoxAO);
                ReqAO2.Children.Add(comboBox);

            }
            if (ListRequirments.SelectedValue == ReqAO3)
            {
                if (Combobox3.SelectedItem == "Race")
                {
                    foreach (majorTrait rac in CurrentConfig.RacList)
                    {
                        comboBox.Items.Add(rac);
                    }
                }
                if (Combobox3.SelectedItem == "Ability")
                {
                    foreach (majorTrait abi in CurrentConfig.AbilList)
                    {
                        comboBox.Items.Add(abi);
                    }
                }
                if (Combobox3.SelectedItem == "Resources")
                {
                    foreach (resourceTrait res in CurrentConfig.ResList)
                    {
                        comboBox.Items.Add(res);
                    }
                }
                if (Combobox3.SelectedItem == "Career")
                {
                    foreach (majorTrait car in CurrentConfig.CarList)
                    {
                        comboBox.Items.Add(car);
                    }
                }
                if (Combobox3.SelectedItem == "Religion")
                {
                    foreach (majorTrait rel in CurrentConfig.RelList)
                    {
                        comboBox.Items.Add(rel);
                    }
                }
                ReqAO3.Children.Add(comboBoxAO);
                ReqAO3.Children.Add(comboBox);

            }
            if (ListRequirments.SelectedValue == ReqAO4)
            {
                if (Combobox4.SelectedItem == "Race")
                {
                    foreach (majorTrait rac in CurrentConfig.RacList)
                    {
                        comboBox.Items.Add(rac);
                    }
                }
                if (Combobox4.SelectedItem == "Ability")
                {
                    foreach (majorTrait abi in CurrentConfig.AbilList)
                    {
                        comboBox.Items.Add(abi);
                    }
                }
                if (Combobox4.SelectedItem == "Resources")
                {
                    foreach (resourceTrait res in CurrentConfig.ResList)
                    {
                        comboBox.Items.Add(res);
                    }
                }
                if (Combobox4.SelectedItem == "Career")
                {
                    foreach (majorTrait car in CurrentConfig.CarList)
                    {
                        comboBox.Items.Add(car);
                    }
                }
                if (Combobox4.SelectedItem == "Religion")
                {
                    foreach (majorTrait rel in CurrentConfig.RelList)
                    {
                        comboBox.Items.Add(rel);
                    }
                }

                ReqAO4.Children.Add(comboBoxAO);
                ReqAO4.Children.Add(comboBox);

            }
            if (ListRequirments.SelectedValue == ReqAO5)
            {
                if (Combobox5.SelectedItem == "Race")
                {
                    foreach (majorTrait rac in CurrentConfig.RacList)
                    {
                        comboBox.Items.Add(rac);
                    }
                }
                if (Combobox5.SelectedItem == "Ability")
                {
                    foreach (majorTrait abi in CurrentConfig.AbilList)
                    {
                        comboBox.Items.Add(abi);
                    }
                }
                if (Combobox5.SelectedItem == "Resources")
                {
                    foreach (resourceTrait res in CurrentConfig.ResList)
                    {
                        comboBox.Items.Add(res);
                    }
                }
                if (Combobox5.SelectedItem == "Career")
                {
                    foreach (majorTrait car in CurrentConfig.CarList)
                    {
                        comboBox.Items.Add(car);
                    }
                }
                if (Combobox5.SelectedItem == "Religion")
                {
                    foreach (majorTrait rel in CurrentConfig.RelList)
                    {
                        comboBox.Items.Add(rel);
                    }
                }
                ReqAO5.Children.Add(comboBoxAO);
                ReqAO5.Children.Add(comboBox);

            }
            if (ListRequirments.SelectedValue == ReqAO6)
            {
                if (Combobox6.SelectedItem == "Race")
                {
                    foreach (majorTrait rac in CurrentConfig.RacList)
                    {
                        comboBox.Items.Add(rac);
                    }
                }
                if (Combobox6.SelectedItem == "Ability")
                {
                    foreach (majorTrait abi in CurrentConfig.AbilList)
                    {
                        comboBox.Items.Add(abi);
                    }
                }
                if (Combobox6.SelectedItem == "Resources")
                {
                    foreach (resourceTrait res in CurrentConfig.ResList)
                    {
                        comboBox.Items.Add(res);
                    }
                }
                if (Combobox6.SelectedItem == "Career")
                {
                    foreach (majorTrait car in CurrentConfig.CarList)
                    {
                        comboBox.Items.Add(car);
                    }
                }
                if (Combobox6.SelectedItem == "Religion")
                {
                    foreach (majorTrait rel in CurrentConfig.RelList)
                    {
                        comboBox.Items.Add(rel);
                    }
                }
                ReqAO6.Children.Add(comboBoxAO);
                ReqAO6.Children.Add(comboBox);

            }
            if (ListRequirments.SelectedValue == ReqAO7)
            {
                if (Combobox7.SelectedItem == "Race")
                {
                    foreach (majorTrait rac in CurrentConfig.RacList)
                    {
                        comboBox.Items.Add(rac);
                    }
                }
                if (Combobox7.SelectedItem == "Ability")
                {
                    foreach (majorTrait abi in CurrentConfig.AbilList)
                    {
                        comboBox.Items.Add(abi);
                    }
                }
                if (Combobox7.SelectedItem == "Resources")
                {
                    foreach (resourceTrait res in CurrentConfig.ResList)
                    {
                        comboBox.Items.Add(res);
                    }
                }
                if (Combobox7.SelectedItem == "Career")
                {
                    foreach (majorTrait car in CurrentConfig.CarList)
                    {
                        comboBox.Items.Add(car);
                    }
                }
                if (Combobox7.SelectedItem == "Religion")
                {
                    foreach (majorTrait rel in CurrentConfig.RelList)
                    {
                        comboBox.Items.Add(rel);
                    }
                }
                ReqAO7.Children.Add(comboBoxAO);
                ReqAO7.Children.Add(comboBox);

            }

        }

        private void Combobox1_Loaded(object sender, RoutedEventArgs e)
        {
            Combobox1.Items.Add("Race"); Combobox1.Items.Add("Ability"); Combobox1.Items.Add("Resources"); Combobox1.Items.Add("Career"); Combobox1.Items.Add("Religion");
        }
        private void Combobox2_Loaded(object sender, RoutedEventArgs e)
        {
            Combobox2.Items.Add("Race"); Combobox2.Items.Add("Ability"); Combobox2.Items.Add("Resources"); Combobox2.Items.Add("Career"); Combobox2.Items.Add("Religion");
        }

        private void Combobox3_Loaded(object sender, RoutedEventArgs e)
        {
            Combobox3.Items.Add("Race"); Combobox3.Items.Add("Ability"); Combobox3.Items.Add("Resources"); Combobox3.Items.Add("Career"); Combobox3.Items.Add("Religion");
        }

        private void Combobox4_Loaded(object sender, RoutedEventArgs e)
        {
            Combobox4.Items.Add("Race"); Combobox4.Items.Add("Ability"); Combobox4.Items.Add("Resources"); Combobox4.Items.Add("Career"); Combobox4.Items.Add("Religion");
        }

        private void Combobox5_Loaded(object sender, RoutedEventArgs e)
        {
            Combobox5.Items.Add("Race"); Combobox5.Items.Add("Ability"); Combobox5.Items.Add("Resources"); Combobox5.Items.Add("Career"); Combobox5.Items.Add("Religion");
        }

        private void Combobox6_Loaded(object sender, RoutedEventArgs e)
        {
            Combobox6.Items.Add("Race"); Combobox6.Items.Add("Ability"); Combobox6.Items.Add("Resources"); Combobox6.Items.Add("Career"); Combobox6.Items.Add("Religion");
        }

        private void Combobox7_Loaded(object sender, RoutedEventArgs e)
        {
            Combobox7.Items.Add("Race"); Combobox7.Items.Add("Ability"); Combobox7.Items.Add("Resources"); Combobox7.Items.Add("Career"); Combobox7.Items.Add("Religion");
        }
        private void OnClickAddNewRequirments(object sender, RoutedEventArgs e)
        {

            /*StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            ComboBox ComboxType = new ComboBox();
            ComboxType.IsReadOnly = true;
            ComboxType.IsDropDownOpen = true;
            ComboxType.Margin = new Thickness(5, 5, 0, 0);
            ComboxType.Height = 24;
            ComboxType.Width = 185;
            ComboxType.VerticalAlignment = VerticalAlignment.Top;
            ComboxType.SelectionChanged += ComboBox_SelectionChanged;
            ComboxType.Items.Add("Race");
            ComboxType.Items.Add("Ability");
            ComboxType.Items.Add("Items");
            ComboxType.Items.Add("Career");

            Button button = new Button();
            button.IsEnabled = true;
            button.Margin = new Thickness(5, 5, 0, 0);
            button.Height = 26;
            button.Width = 50;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.Content = "Click me";
            button.Click += OnClickAddRequirmentsList;


            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Name = "RequirmentsAO";
            ComboBox comboBox = new ComboBox();
            comboBox.IsReadOnly = true;
            comboBox.IsDropDownOpen = true;
            comboBox.Margin = new Thickness(5, 5, 0, 0);
            comboBox.Height = 24;
            comboBox.Width = 185;
            comboBox.VerticalAlignment = VerticalAlignment.Top;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            comboBox.DisplayMemberPath = "Name";
            foreach (resourceTrait res in CurrentConfig.ResList)
            {
                comboBox.Items.Add(res);
            }

            stackPanel.Children.Add(ComboxType);
            stackPanel.Children.Add(button);*/



        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void OnClickDeleteAbilityList(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


        }
        private void OnClickSaveAblity(object sender, string UID, RoutedEventArgs e)
        {


            majorTrait currentMT = CurrentConfig.GetTrait(UID);
            currentMT.deleteContent();


            string name = (this.FindName("nameBox") as TextBox).Text;
            string playerReq = (this.FindName("playerReqBox") as TextBox).Text;
            string desc = (this.FindName("descBox") as TextBox).Text;

            foreach (StackPanel PANEL in (this.FindName("ListStarterResources") as ListView).Items)
            {
                foreach (ComboBox box in PANEL.Children)
                {
                    string TempUID = box.SelectedValue as string;
                    currentMT.CostTypes.Add(TempUID);
                }

            }


        }


    }
}
