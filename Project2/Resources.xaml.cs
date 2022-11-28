using System;
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
    /// Interaction logic for Recources.xaml
    /// </summary>
    public partial class Resources : Page
    {   
        public Resources(config currentConfig)
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
            newresource = new ObservableCollection<newResource>(){
            new newResource(){Name = "New Resource", ResourceType = "HP/Stat Bar" }
            };
            lstResources.ItemsSource = newresource;
        }
        config CurrentConfig;

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ResourceMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Content = mainWindow;
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }*/

        private ObservableCollection<newResource> newresource;

        public class newResource
        {
            public string Name { get; set; }
            public string ResourceType { get; set; }
        }

        private void btnResource_ClickAdd(object sender, RoutedEventArgs e)
        {
            int i = newresource.Count + 1;
            newresource.Add(new newResource() { Name = "New Resource", ResourceType = "HP/Stat Bar" });
        }

        private void btnResource_ClickDelete(object sender, RoutedEventArgs e)
        {
            var index = lstResources.SelectedIndex;
            if (lstResources.SelectedIndex >= 1)
            {
                newresource.RemoveAt(index);
            }
        }

        private void AssignResourceTypeRadioButton(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            var content = rb.Content;

            var index = lstResources.SelectedIndex;

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

