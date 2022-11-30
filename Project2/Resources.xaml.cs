using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;
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
            new resourceTrait(CurrentConfig.newUID("Resource"), 0){Name = "great resource"}
            };
            lstResources.ItemsSource = ResourceCollection;
           
        }
        config CurrentConfig;
        int lastSelectedIndex;
   

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



        private void btnResource_ClickAdd(object sender, RoutedEventArgs e)
        {
            ResourceCollection.Add(new resourceTrait(CurrentConfig.newUID("Resource"),3) { Name = "new resource" });
            lastSelectedIndex = ResourceCollection.Count + 1;
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
                ResourceCollection.RemoveAt(index);
            }
          
        }
        public static void DelayAction(int millisecond, Action action)
        {
            var timer = new DispatcherTimer();
            timer.Tick += delegate

            {
                action.Invoke();
                timer.Stop();
            };

            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }
      

        async void listUpdate_Click(object sender, RoutedEventArgs e)//async, due to await.task
        {
            System.Diagnostics.Debug.WriteLine("you clicked an item in the listview, testvar is : ");
            var item = sender as ListViewItem;
            await Task.Delay(5);  //the "selection" happens AFTER the click on the list, so we await the selection
            if (item != null && item.IsSelected)
            {
                int testvar = lstResources.SelectedIndex;

                System.Diagnostics.Debug.WriteLine(testvar.ToString());
                if (ResourceCollection[testvar].type == 0)
                {
                    HPSTATBARRadioButton.IsChecked = true;
                }
                if (ResourceCollection[testvar].type == 1)
                {
                    XPVARIANTRadioButton.IsChecked = true;
                }
                if (ResourceCollection[testvar].type == 2)
                {
                    CURRENCYRadioButton.IsChecked = true;
                }
                if (ResourceCollection[testvar].type == 3)
                {
                    MISCRadioButton.IsChecked = true;
                }
            }

            
        }
        private void AssignResourceTypeRadioButton(object sender, RoutedEventArgs e)
        { 
            RadioButton rb = sender as RadioButton;

            System.Diagnostics.Debug.WriteLine(rb.Tag);
            int index = lstResources.SelectedIndex;
            
            int typeOfResource = Int16.Parse(rb.Tag.ToString());
    /*        int position = lstResources.
            */
            if (lstResources.SelectedIndex >= 0)
            {
                ResourceCollection[index] = new resourceTrait(ResourceCollection[index].UID, typeOfResource) { Name = ResourceCollection[index].Name, Description = ResourceCollection[index].Description };
                
                
                foreach (resourceTrait i in ResourceCollection) {
                    System.Diagnostics.Debug.WriteLine(i.Name);
                    System.Diagnostics.Debug.WriteLine(i.type);
                    System.Diagnostics.Debug.WriteLine(i.UID);
                    System.Diagnostics.Debug.WriteLine(i.Description);
                }
            }
            lstResources.SelectedIndex=index;
        }

        /*private void ListStarterAbilities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }*/

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


           /* string[] id = UID.Split('-');
            majorTrait currentMT = CurrentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])];
            currentMT.deleteContent();
*/
            string name = (this.FindName("nameBox") as TextBox).Text;
            System.Diagnostics.Debug.WriteLine(name);
            string playerReq = (this.FindName("playerReqBox") as TextBox).Text;
            string desc = (this.FindName("descBox") as TextBox).Text;
/*
            currentMT.name = name;
            currentMT.description = playerReq + "\n\n" + desc;



            currentConfig.MTList[int.Parse(id[0])][int.Parse(id[1])] = currentMT;*/
        }

    }
}

