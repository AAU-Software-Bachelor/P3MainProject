using Project2.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
using static Project2.RacePage;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;

namespace Project2
{
	/// <summary>
	/// Interaction logic for Race.xaml
	/// </summary>
	public partial class RacePage : Page
	{

		public RacePage(config currentConfig) //Race window constructor
		{
			CurrentConfig = currentConfig;
			InitializeComponent();
			TraitCollection = new ObservableCollection<majorTrait>();
			foreach (majorTrait race in CurrentConfig.RacList) //adds all races to ObservableCollection TraitCollection
			{
				TraitCollection.Add(race);
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
		private void RaceMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Functionality.MainMenu(CurrentConfig);
		}
		/// <summary>
		/// adds a new empty race to Current config and newrace
		/// </summary>
		private void btnRaces_ClickAdd(object sender, RoutedEventArgs e)
		{
			majorTrait tempRace = new majorTrait(CurrentConfig.newUID("RacList")) { Name = "new race" };	//makes the new race object
			CurrentConfig.saveToList(tempRace);
			TraitCollection.Add(tempRace);
			lstTraits.SelectedIndex = TraitCollection.Count-1;

		}
        /// <summary>
		/// deletes race from both CurrentConfig and newrace
		/// </summary>
        private void btnRaces_ClickDelete(object sender, RoutedEventArgs e)
		{
			Functionality.DeleteRes(CurrentConfig, lstTraits, TraitCollection);	
		}
        private void btnRaces_ClickCopy(object sender, RoutedEventArgs e)
        {
			Functionality.MTCopy(lstTraits, TraitCollection, CurrentConfig, "RacList");
        }

        /// <summary>
        /// adds a combobox with all Abilities fron CurrentConfig as selectables
        /// </summary>
        private void OnClickAddStarterAbilities(object sender, RoutedEventArgs e)
		{
			Functionality.AddFreeAbilities(lstTraits,ListStarterAbilities, CurrentConfig);
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
			Functionality.AddAffectedResources(lstTraits, ListStarterResources, CurrentConfig);
        }

		/// <summary>
		/// deletes selected Starter Resources 
		/// </summary>
		private void OnClickDeleteStarterResources(object sender, RoutedEventArgs e)
		{
			Functionality.Deleteselected(ListStarterResources);
		}
		/// <summary>
		///		Saves the current race (if it exists) via SaveRace() and loads up the new one that was clicked.
		/// </summary>
		/// 
		bool amworkingonchange = false;
		private void OnRaceChanged(object sender, RoutedEventArgs e)
		{
			if (amworkingonchange == false)
			{
				amworkingonchange = true;
				Functionality.MTChange(CurrentConfig, CurrentConfig.RacList, LastSelected, lstTraits, nameBox, descBox, ListAffectedResources:ListStarterResources, ListFreeAbilities:ListStarterAbilities, playerReqBox: playerReqBox);
				//Task.Delay(2);
                amworkingonchange = false;
            }

		}

		/// <summary>
		/// works as a middle man between butons and SaveRace 
		/// </summary>
		private void OnClickSaveRace(object sender, RoutedEventArgs e)
		{
			SaveRace();
		}

		/// <summary>
		/// Saves everything in the indexed race to the current config. if no index is given then it saves the currently selected race.
		/// </summary>
		private void SaveRace(int index = -1)
		{
			Functionality.SaveMTrait(CurrentConfig, CurrentConfig.RacList, LastSelected, lstTraits, nameBox, descBox, playerReqBox: playerReqBox);
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

		private void ChangeIcon_click(object sender, RoutedEventArgs e)
		{

		}

		private void searchbar_KeyUp(object sender, KeyEventArgs e)
		{
			Functionality.searchbarMT(searchbar, TraitCollection, CurrentConfig.RacList, lstTraits);
        }

	}
}
