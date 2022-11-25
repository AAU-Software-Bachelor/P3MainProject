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
    /// Interaction logic for Abilities.xaml
    /// </summary>
    public partial class And_Or_Select : Page
    {
        public And_Or_Select()
        {
            InitializeComponent();
        }

        private void AbilitiesMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            this.Content = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        public string and = "and";
        private string or = "or";
        private ObservableCollection<string> _andor;

        // private readonly ObservableCollection<string> andor = _andor;

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string[] andor = new[] { and, or };
            ComboxAndOr.ItemsSource = andor;
           

        }

        private And_Or_Select(object sender, SelectionChangedEventArgs e)
        {
            _andor = new ObservableCollection<string>();
            _andor.Add("and");
            _andor.Add(or);

           /* if (_andor == "and")
            {
                //Combox1selected
                //Comboxselected2

            }
            else if (andor = or)
            {
                //comboxselected

            }*/
        }


    }
}
