using System;
using System.Collections.Generic;
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
                ListForRaces.Items.Remove(eachItem);
            }

        }
    }
}
