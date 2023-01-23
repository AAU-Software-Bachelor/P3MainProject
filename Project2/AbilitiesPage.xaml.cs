using ControlzEx.Standard;
using Project2.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using static Project2.majorTrait;

namespace Project2
{
	/// <summary>
	/// Interaction logic for Abilities.xaml
	/// </summary>
	public partial class AbilitiesPage : Page
	{
		public AbilitiesPage(config currentConfig) //bility window constructor
		{
			CurrentConfig = currentConfig;
			InitializeComponent();
			RequireTypeBox.Items.Add("Race");
			RequireTypeBox.Items.Add("Ability");
			RequireTypeBox.Items.Add("Career");
			RequireTypeBox.Items.Add("Religion");
			RequireTypeBox.SelectedIndex = 0;
			TraitCollection = new ObservableCollection<majorTrait>();
			foreach (majorTrait ability in CurrentConfig.AbiList) //adds all abilities to ObservableCollection TraitCollection
			{
				TraitCollection.Add(ability);
			}
			lstTraits.ItemsSource = TraitCollection;
			LastSelected = new majorTrait("");  //keeps track of what object to save
			lstTraits.SelectedIndex = 0;
		}
		public config CurrentConfig { get; set; }
		public majorTrait LastSelected { get; set; }
		private ObservableCollection<majorTrait> TraitCollection;   //itemSource for lstTraits ListVeiw


		private void MainMenu_MouseClick(object sender, MouseButtonEventArgs e)
		{
			Functionality.MainMenu(CurrentConfig);
		}

		private void btnAbility_ClickAdd(object sender, EventArgs e)
		{
			majorTrait tempAbility = new majorTrait(CurrentConfig.newUID("AbiList")) { Name = "new ability" };   //makes the new ability object
			CurrentConfig.saveToList(tempAbility);
			TraitCollection.Add(tempAbility);
			lstTraits.SelectedIndex = TraitCollection.Count - 1;
		}

		private void btnAbility_ClickDelete(object sender, EventArgs e)
		{
			Functionality.DeleteRes(CurrentConfig, lstTraits, TraitCollection);
		}
		private void btnAbility_ClickCopy(object sender, EventArgs e)
		{
			Functionality.MTCopy(lstTraits, TraitCollection, CurrentConfig, "AbiList");
		}
		private void OnClickAddFreeAbilities(object sender, EventArgs e)
		{
			Functionality.AddFreeAbilities(lstTraits, ListFreeAbilities, CurrentConfig);
		}
		private void OnClickDeleteFreeAbilities(object sender, RoutedEventArgs e)
		{
			Functionality.Deleteselected(ListFreeAbilities);
		}
		private void OnClickAddRequirmentsList(object sender, EventArgs e)
		{
			Functionality.AddRequirmentsList(ListRequirements);
		}
		private void OnClickDeleteRequirmentsList(object sender, RoutedEventArgs e)
		{
			Functionality.Deleteselected(ListRequirements);
		}
		private void OnClickAddRequirment(object sender, EventArgs e)
		{
			Functionality.AddRequirment(lstTraits, ListRequirements, RequireTypeBox, CurrentConfig);
		}
		private void OnClick_AddCostType(object sender, EventArgs e)
		{
			Functionality.AddCostType(lstTraits, ListCosts, CurrentConfig);
		}
		private void OnClick_DeleteCostType(object sender, EventArgs e)
        {
            Functionality.Deleteselected(ListCosts);
        }
		private void OnClick_AddAffectedResources(object sender, EventArgs e)
		{
			Functionality.AddAffectedResources(lstTraits, ListAffectedResources, CurrentConfig);
		}
		private void OnClick_DeleteAffectedResources(object sender, EventArgs e)
		{
			Functionality.Deleteselected(ListAffectedResources);
		}
		private void OnClick_AddExclusion(object sender, EventArgs e)
		{
			Functionality.AddExclusion(lstTraits, ListExclusion, CurrentConfig, sender);
		}
		private void OnClick_DeleteExclusion(object sender, EventArgs e)
		{
			Functionality.Deleteselected(ListExclusion);
		}
		private void OnClick_AddDiscount(object sender, EventArgs e)
        {
			Functionality.AddDiscount(CurrentConfig, lstTraits, ListDiscounts, sender);
        }
		private void OnClick_DeleteDiscount(object sender, RoutedEventArgs e)
		{
			Functionality.Deleteselected(ListDiscounts);
		}

		bool amworkingonchange = false;
		private void OnAbilityChanged(object sender, RoutedEventArgs e)
		{
			if (amworkingonchange == false)
			{
				amworkingonchange = true;
				Functionality.MTChange(CurrentConfig, CurrentConfig.AbiList, LastSelected, lstTraits, nameBox, descBox, costBox, ListAffectedResources, ListFreeAbilities, ListCosts, ListDiscounts, ListExclusion, ListRequirements, RequireTypeBox);
				amworkingonchange = false;
			}
		}
		private void OnClickSaveAbility(object sender, EventArgs e)
		{
			SaveAbility();
		}
		private void SaveAbility(majorTrait? trait = null)
		{
			Functionality.SaveMTrait(CurrentConfig, CurrentConfig.AbiList, trait, lstTraits, nameBox,descBox,costBox,ListAffectedResources,ListFreeAbilities,ListCosts,ListDiscounts,ListExclusion,ListRequirements);
            Functionality.searchbarMT(searchbar, TraitCollection, CurrentConfig.AbiList, lstTraits);
        }
        private void NumberValidationTextBox(object sender, EventArgs e)
        {
			Functionality.NumberValidationTextBox(sender, e);
        }
        private void searchbar_KeyUp(object sender, EventArgs e)
		{
			Functionality.searchbarMT(searchbar, TraitCollection, CurrentConfig.AbiList, lstTraits);			
		}
	}
}
