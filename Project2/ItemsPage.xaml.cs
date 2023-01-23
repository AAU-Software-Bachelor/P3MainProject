using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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
using static Project2.majorTrait;
using static Project2.ItemPage;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;
using Project2.classes;

namespace Project2
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class ItemPage : Page
    {

        public ItemPage(config currentConfig) //Item window constructor
        {
            CurrentConfig = currentConfig;
            InitializeComponent();
            TraitCollection = new ObservableCollection<majorTrait>();
            foreach (majorTrait Item in CurrentConfig.IteList) //adds all Items to ObservableCollection TraitCollection
            {
                TraitCollection.Add(Item);
            }
            lstTraits.ItemsSource = TraitCollection;
            LastSelected = new majorTrait("");  //keeps track of what object to save
            lstTraits.SelectedIndex = 0;
        }
        public config CurrentConfig { get; set; }
        public majorTrait LastSelected { get; set; }
        private ObservableCollection<majorTrait> TraitCollection;   //itemSource for lstTraits ListVeiw

        /// <summary>
        /// sets page to MainWindow
        /// </summary>
        private void ItemMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Functionality.MainMenu(CurrentConfig);
        }
        /// <summary>
        /// adds a new empty Item to Current config and newItem
        /// </summary>
        private void btnItems_ClickAdd(object sender, RoutedEventArgs e)
        {
            majorTrait tempItem = new majorTrait(CurrentConfig.newUID("IteList")) { Name = "new Item" };   //makes the new Item object
            CurrentConfig.saveToList(tempItem);
            TraitCollection.Add(tempItem);
            lstTraits.SelectedIndex = TraitCollection.Count - 1;

        }
        /// <summary>
		/// deletes Item from both CurrentConfig and newItem
		/// </summary>
        private void btnItems_ClickDelete(object sender, RoutedEventArgs e)
        {
            Functionality.DeleteRes(CurrentConfig, lstTraits, TraitCollection);
        }
        private void btnItems_ClickCopy(object sender, RoutedEventArgs e)
        {
            Functionality.MTCopy(lstTraits, TraitCollection, CurrentConfig, "IteList");
        }

        /// <summary>
        /// adds a combobox with all Abilities fron CurrentConfig as selectables
        /// </summary>
        private void OnClickAddStarterAbilities(object sender, RoutedEventArgs e)
        {
            Functionality.AddFreeAbilities(lstTraits, ListStarterAbilities, CurrentConfig);
        }
        /// <summary>
        /// Deletes selected starterAbility
        /// </summary>
        private void OnClickDeleteStarterAbilities(object sender, RoutedEventArgs e)
        {
            Functionality.Deleteselected(ListStarterAbilities);
        }

        /// <summary>
        /// add a combobox with all Resources fron CurrentConfig as selectables, together with a numer only textbox
        /// </summary>
        private void OnClickAddStarterResources(object sender, RoutedEventArgs e)
        {
            Functionality.AddAffectedResources(lstTraits, ListAffectedResources, CurrentConfig);
        }

        /// <summary>
        /// deletes selected Starter Resources 
        /// </summary>
        private void OnClickDeleteStarterResources(object sender, RoutedEventArgs e)
        {
            Functionality.Deleteselected(ListAffectedResources);
        }
        bool amworkingonchange = false;
        /// <summary>
        ///	Saves the current Item (if it exists) via SaveItem() and loads up the new one that was clicked.
        /// </summary>
        private void OnItemChanged(object sender, RoutedEventArgs e)
        {
            if (amworkingonchange == false)
            {
                amworkingonchange = true;
                Functionality.MTChange(CurrentConfig, CurrentConfig.IteList, LastSelected, lstTraits, nameBox, descBox, ListAffectedResources: ListAffectedResources, ListFreeAbilities: ListStarterAbilities);
                amworkingonchange = false;
            }
        }
        /// <summary>
        /// works as a middle man between butons and SaveItem 
        /// </summary>
        private void OnClickSaveItem(object sender, RoutedEventArgs e)
        {
            SaveItem();
        }

        /// <summary>
        /// Saves everything in the indexed Item to the current config. if no index is given then it saves the currently selected Item.
        /// </summary>
        private void SaveItem(int index = -1)
        {
            Functionality.SaveMTrait(CurrentConfig, CurrentConfig.IteList, LastSelected, lstTraits, nameBox, descBox, ListAffectedResources:ListAffectedResources, ListFreeAbilities:ListStarterAbilities);
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
            Functionality.searchbarMT(searchbar, TraitCollection, CurrentConfig.IteList, lstTraits);
        }

        private void ChangeIcon_click(object sender, RoutedEventArgs e)
        {

        }

    }
}
