using Project2.classes;
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
using static Project2.majorTrait;

namespace Project2
{
    /// <summary>
    /// Interaction logic for Religion.xaml
    /// </summary>
    public partial class ReligionPage : Page
    {

        public ReligionPage(config currentConfig)
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
            TraitCollection = new ObservableCollection<majorTrait>();
            foreach (majorTrait religion in CurrentConfig.RelList) //adds all religion to ObservableCollection TraitCollection
            {
                TraitCollection.Add(religion);
            }
            lstTraits.ItemsSource = TraitCollection;
            LastSelected = new majorTrait("");  //keeps track of what object to save
            lstTraits.SelectedIndex = 0;
        }
        public config CurrentConfig { get; set; }
        public majorTrait LastSelected { get; set; }
        private ObservableCollection<majorTrait> TraitCollection;

        private void ReligionMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Functionality.MainMenu(CurrentConfig);
        }

        private void btnReligion_ClickAdd(object sender, RoutedEventArgs e)
        {
            majorTrait tempReligion = new majorTrait(CurrentConfig.newUID("RelList")) { Name = "new religion" };   //makes the new religion object
            CurrentConfig.saveToList(tempReligion);
            TraitCollection.Add(tempReligion);
            lstTraits.SelectedIndex = TraitCollection.Count - 1;
        }
        private void btnReligion_ClickDelete(object sender, RoutedEventArgs e)
        {
            Functionality.deleteMajorTrait(CurrentConfig, lstTraits, TraitCollection);
        }
        private void btnReligion_ClickCopy(object sender, RoutedEventArgs e)
        {
            Functionality.MTCopy(lstTraits, TraitCollection, CurrentConfig, "RelList");
        }
        private void OnClickAddAffectedResources(object sender, RoutedEventArgs e)
        {
            Functionality.AddAffectedResources(lstTraits, ListAffectedResources, CurrentConfig);
        }

        private void OnClickDeleteAffectedResources(object sender, RoutedEventArgs e)
        {
            Functionality.Deleteselected(ListAffectedResources);
        }

        private void OnReligionChanged(object sender, RoutedEventArgs e)
        {
            Functionality.MTChange(CurrentConfig, CurrentConfig.RelList, LastSelected, lstTraits, nameBox, descBox, ListAffectedResources: ListAffectedResources);
        }

        private void OnClickSaveReligion(object sender, RoutedEventArgs e)
        {
            SaveReligion();
        }

        private void SaveReligion(int index = -1)
        {
            Functionality.SaveMTrait(CurrentConfig, CurrentConfig.RelList, LastSelected, lstTraits, nameBox, descBox, ListAffectedResources: ListAffectedResources);
        }
        /// <summary>
		/// Validates input in "amount" textbox to only allow integers.
		/// </summary>
		private void NumberValidationTextBox(object sender, EventArgs e)
        {
            Functionality.NumberValidationTextBox(sender, e);
        }

        private void searchbar_KeyUp(object sender, KeyEventArgs e)
        {
            Functionality.searchbarMT(searchbar, TraitCollection, CurrentConfig.RelList, lstTraits);
        }

        private void ChangeIcon_click(object sender, RoutedEventArgs e)
        {

        }
    }
}


