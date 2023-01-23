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
        /// <summary>
        ///	Saves the current Item (if it exists) via SaveItem() and loads up the new one that was clicked.
        /// </summary>
        private void OnItemChanged(object sender, RoutedEventArgs e)
        {
            Functionality.MTChange(CurrentConfig, CurrentConfig.IteList, LastSelected, lstTraits, nameBox, descBox, ListAffectedResources: ListAffectedResources, ListFreeAbilities: ListStarterAbilities);
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
            int SelIndex = lstTraits.SelectedIndex;  //saves selected index so it is not lost

            string UID = "";
            if (index == -1)    //is true when funtion is called via a button
            {
                if (lstTraits.SelectedIndex >= 0)
                {
                    UID = CurrentConfig.IteList[lstTraits.SelectedIndex].UID;    //uses the selected index to find the wanted UID
                    index = CurrentConfig.IteList.FindIndex(i => string.Equals(i.UID, UID));
                }
            }
            else
            {
                UID = CurrentConfig.IteList[index].UID; //uses the given index to find the wanted UID
            }
            if (UID != "")
            {
                majorTrait currentMT = CurrentConfig.GetTrait(UID);
                currentMT.deleteContent();

                currentMT.Name = (this.FindName("nameBox") as TextBox).Text;
                currentMT.Description = (this.FindName("descBox") as TextBox).Text;

                foreach (ComboBox BOX in (this.FindName("ListStarterAbilities") as ListView).Items)
                {
                    if (BOX.SelectedIndex >= 0)
                    {
                        string TempUID = CurrentConfig.AbiList[BOX.SelectedIndex].UID;
                        currentMT.FreeAbilities.Add(TempUID); // saves the free abilities
                    }
                }

                foreach (StackPanel PANEL in (this.FindName("ListStarterResources") as ListView).Items)
                {
                    foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
                    {
                        foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
                        {
                            if (box.SelectedIndex >= 0 & textBox.Text != "")
                            {
                                string TempUID = CurrentConfig.ResList[box.SelectedIndex].UID;  //gets the affected rescource
                                int TempVal = int.Parse(textBox.Text);  //gets the value
                                currentMT.AffectedResources.Add(new AmountUID(TempUID, TempVal));  //saves the affected rescources and their values
                            }
                        }
                    }
                }
                CurrentConfig.IteList[index] = currentMT;
                TraitCollection.Clear(); // clears the list
                foreach (majorTrait Item in CurrentConfig.IteList)  //rewrites the list.
                {
                    TraitCollection.Add(Item);
                }
                lstTraits.SelectedIndex = SelIndex;  //applies saved index selection
            }
        }

        /// <summary>
        /// Validates input in "amount" textbox to only allow integers.
        /// </summary>
        private void NumberValidationTextBox(object sender, EventArgs e)
        {
            try
            {
                int.Parse((sender as TextBox).Text); //if Parse is unsuccessful, text is something other than integer
            }
            catch
            {
                (sender as TextBox).Text = "";
            }
        }

        private void searchbar_KeyUp(object sender, KeyEventArgs e)
        {
            string searchText = (this.FindName("searchbar") as TextBox).Text;
            if (searchText != "")
            {
                TraitCollection.Clear();
                foreach (majorTrait item in CurrentConfig.IteList) //adds all races to ObservableCollection RaceCollection
                {
                    if (item.Name.ToLower().Contains(searchText.ToLower()))
                    {
                        TraitCollection.Add(item);
                    }
                }
                lstTraits.SelectedIndex = 0;
            }
            else
            {
                TraitCollection.Clear();
                foreach (majorTrait item in CurrentConfig.IteList) //adds all races to ObservableCollection RaceCollection
                {
                    TraitCollection.Add(item);
                }
                lstTraits.SelectedIndex = 0;
            }
        }

        private void ChangeIcon_click(object sender, RoutedEventArgs e)
        {

        }

    }
}
