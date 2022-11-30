using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
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

namespace Project2
{
    /// <summary>
    /// Interaction logic for Recources.xaml
    /// </summary>
    public partial class Resources : Page
    {
        public ObservableCollection<resourceTrait> ResourceCollection;
        public Resources(config currentConfig)
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
            ResourceCollection = new ObservableCollection<resourceTrait>()
            {
            new resourceTrait("R-0", 1)
            };
            lstResources.ItemsSource = ResourceCollection;
        }
        config CurrentConfig;
        int lastSelectedIndex;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ResourceMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

        private ObservableCollection<newResource> newresource;

        public class newResource
        {
            public string Name { get; set; }
            public string ResourceType { get; set; }
        }

        private void btnResource_ClickAdd(object sender, RoutedEventArgs e)
        {
      
            newresource.Add(new newResource() { Name = "New Resource", ResourceType = "HP/Stat Bar" });
            lastSelectedIndex = newresource.Count + 1;
        }

        private void btnResource_ClickDelete(object sender, RoutedEventArgs e)
        {
            
            var index = lstResources.SelectedIndex;
            if (lstResources.SelectedIndex == null)
            {
                index = lastSelectedIndex;
            }
            if (lstResources.SelectedIndex >= 1)
            {
                newresource.RemoveAt(index);
            }
        }
     /*   public string getRadioButton()
        {
            *//*string type = newresourc
*//*
            return Type;
        }*/

        private void AssignResourceTypeRadioButton(object sender, RoutedEventArgs e)
        { 
            RadioButton rb = sender as RadioButton;
       
   /*         string content = rb.Content.ToString();*/
            System.Diagnostics.Debug.WriteLine(rb.Content);

            var index = lstResources.SelectedIndex;
            if (lstResources.SelectedIndex == null)
            {
                index = lastSelectedIndex;
            }
            string ResourceType = rb.Content.ToString();
            if (lstResources.SelectedIndex >= 0)
            {
                newresource[index] = new newResource() { Name = newresource[index].Name, ResourceType = ResourceType };
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

