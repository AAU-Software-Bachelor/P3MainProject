using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using static Project2.majorTrait;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Media.TextFormatting;
using System.Security.Cryptography;
using System.Collections;

namespace Project2.classes
{
	internal class Functionality
	{
		//Handles the basic- and input-logic for the deletion of a trait
        public static void deleteMajorTrait(config currentConfig, ListView uiList, ObservableCollection<majorTrait>? collection = null, ObservableCollection<resourceTrait>? resourceCollection = null)
		{
			var index = uiList.SelectedIndex;
			List<string> RequiredBy;
			if(collection != null){
                RequiredBy = GetDependencyReferences(currentConfig, collection[index].UID);
            } else if (resourceCollection != null) {
                RequiredBy = GetDependencyReferences(currentConfig, resourceCollection[index].UID);
            } else
			{
				throw new ArgumentNullException("Only one of resourceCollection and collection can be null");
			}
            
			if (index >= 0)
			{
				if (RequiredBy.Count() > 0)
				{
					List<string> names = new List<string>();
					foreach (string uid in RequiredBy)
					{
						names.Add(currentConfig.GetTrait(uid).Name);
					}
					MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
					MessageBoxResult result = MessageBox.Show("Required by:\n" + string.Join("\n", names) + "\nDo you want to remove this as dependencies for everything? RECOMMENDED: YES(Pressing no will break everything atm)", "This resource is required by other abilities", buttons, MessageBoxImage.Error);

					if (result.Equals(MessageBoxResult.Yes))
					{
                        if (collection != null)
                        {
                            DeleteDependencies(currentConfig, collection[index].UID, RequiredBy);
                        }
                        else 
                        {
                            DeleteDependencies(currentConfig, resourceCollection![index].UID, RequiredBy);
                        }
                        
					}
					else if (result.Equals(MessageBoxResult.Cancel))
					{
						return;
					}
				}

                //gets the ability to be deleteted via GetTrait while it deletes it, and deletes its counterpart in AbilityCollection
                if (collection != null)
                {
                    collection.Remove(currentConfig.GetTrait(collection[index].UID, true));
                    uiList.SelectedIndex = collection.Count - 1;
                }
                else
                {
                    resourceCollection!.Remove(currentConfig.GetTrait(resourceCollection![index].UID, true));
                    uiList.SelectedIndex = resourceCollection!.Count - 1;
                }
				
			}
		}

		//Deletes all references to the string toBeDeleted in different ways according to the runTimeType of T
		//Loops from end to avoid skips
        static void delete<T>(List<T> list, string toBeDeleted)
		{
			List<List<string>>? temp = list as List<List<string>>;
			if(temp != null)
			{
                foreach (List<string> orList in temp)
                {
                    for (int i = orList.Count() - 1; i >= 0; i--)
                    {
                        if (orList[i] == toBeDeleted)
                        {
                            orList.RemoveAt(i);
                        }
					}
				}
                for (int i = 0; i < temp.Count(); i++)
                {
                    if (temp[i].Count() < 1) { temp.RemoveAt(i); }
                }
            }

			List<string>? stringTemp = list as List<string> ;
			if(stringTemp != null)
			{
                for (int i = stringTemp.Count() - 1; i >= 0; i--)
                {
                    if (stringTemp[i] == toBeDeleted)
                    {
                        stringTemp.RemoveAt(i);
                    }
                }
            }

			List<AmountUID>? amountUIDs = list as List<AmountUID>;
			if(amountUIDs != null)
			{
                for (int i = amountUIDs.Count() - 1; i >= 0; i--)
                {
                    if (amountUIDs[i].UID == toBeDeleted)
                    {
                        amountUIDs.RemoveAt(i);
                    }
                }
            }
        }

		//After clicking ok to the prompt this function uses the list dependers to delete all references
        private static void DeleteDependencies(config currentConfig, string toBeDeleted, List<string> dependers)
		{
			foreach (string depender in dependers)
			{
				majorTrait? major = currentConfig.GetTrait(depender) as majorTrait;
				if (major != null)
				{
					delete(major.Dependencies, toBeDeleted);
                    delete(major.CostTypes, toBeDeleted);
                    delete(major.AffectedResources, toBeDeleted);
                    delete(major.Discounts, toBeDeleted);
                    delete(major.FreeAbilities, toBeDeleted);
                    delete(major.Exclusions, toBeDeleted);
                }
			}
		}
		//Returns a distinct list of UID's that would be affected by the removal of RequirementUid
		private static List<string> GetDependencyReferences(config currentConfig, string RequirementUId)
		{
			List<string> requiredByList = new List<string>();
			requiredByList.AddRange(GetDependencyReferencesPartTwo(RequirementUId, currentConfig.AbiList));
			requiredByList.AddRange(GetDependencyReferencesPartTwo(RequirementUId, currentConfig.CarList));
			requiredByList.AddRange(GetDependencyReferencesPartTwo(RequirementUId, currentConfig.RacList));
			requiredByList.AddRange(GetDependencyReferencesPartTwo(RequirementUId, currentConfig.RelList));
            requiredByList.AddRange(GetDependencyReferencesPartTwo(RequirementUId, currentConfig.IteList));
			//Remove duplicates
            requiredByList = requiredByList.Distinct().ToList();
			return requiredByList;
		}

		//Makes a list of the affected traits
        static List<string> GetDependencyReferencesPartTwo(string toBeDeleted, List<majorTrait> list)
        {
            List<string> requiredByList = new List<string>();
            foreach (majorTrait major in list)
            {
				//Using AddRange instead of add so we can't add an empty entry.
                requiredByList.AddRange(GetDependencyReferencesPartThree(major.Dependencies, toBeDeleted, major.UID));                
                requiredByList.AddRange(GetDependencyReferencesPartThree(major.CostTypes, toBeDeleted, major.UID));                
                requiredByList.AddRange(GetDependencyReferencesPartThree(major.AffectedResources, toBeDeleted, major.UID));                
                requiredByList.AddRange(GetDependencyReferencesPartThree(major.Discounts, toBeDeleted, major.UID));                
                requiredByList.AddRange(GetDependencyReferencesPartThree(major.FreeAbilities, toBeDeleted, major.UID));
                requiredByList.AddRange(GetDependencyReferencesPartThree(major.Exclusions, toBeDeleted, major.UID));
            }

            return requiredByList;
        }

		//Returns a list of length 0 or 1. If length is 1, the majorUID trait would affected by the removal of the uid trait
        static List<string> GetDependencyReferencesPartThree<T>(T list, string uid, string majorUID)
		{
			List<string> result = new List<string>();
            if (majorUID == "asdklasdklmasklmdakmdls") Debug.WriteLine("Found krigerpræst");
            List<List<string>>? requirements = list as List<List<string>>;
			if(requirements != null)
			{
                foreach (List<string> orList in requirements)
                {
                    foreach (string dependency in orList)
                    {
                        if (dependency == uid)
                        {
                            result.Add(majorUID);
                        }
                    }
                }
            }
            Debug.WriteLineIf(uid == "AbiList-/b29ec719-ffde-46c8-8588-9d64c6d37e68", "HP-3 Cost");
            List<string>? stringList = list as List<string>;
			if(stringList != null)
			{
                Debug.WriteLineIf(uid == "AbiList-/b29ec719-ffde-46c8-8588-9d64c6d37e68", "HP-3 Cost");
                foreach (string cost in stringList)
                {
                    Debug.WriteLineIf(uid == "AbiList-/b29ec719-ffde-46c8-8588-9d64c6d37e68", "HP-3");
                    Debug.WriteLineIf(uid == "AbiList-/b29ec719-ffde-46c8-8588-9d64c6d37e68", cost);
                    if (uid == cost)
                    {
                        result.Add(majorUID);
                    }
                }
            }

			List<AmountUID>? amountUIDs = list as List<AmountUID>;
			if(amountUIDs != null)
			{
                foreach (AmountUID discount in amountUIDs)
                {
                    if (uid == discount.UID)
                    {
                        result.Add(majorUID);
                    }
                }
            }
			return result;
		}
		//Opens a new menu
		public static void MainMenu(config CurrentConfig)
		{
			MainWindow mainWindow = new MainWindow(CurrentConfig);
			Application.Current.MainWindow.Content = mainWindow;
		}

		//Filters the searchBar
		public static void searchbarMT(TextBox searchbar, ObservableCollection<majorTrait> MTCollection, List<majorTrait> MTLst, ListView lstTraits)
		{
			if (searchbar.Text != "")
			{
				MTCollection.Clear();
				foreach (majorTrait ability in MTLst) //adds all races to ObservableCollection TraitCollection
				{
					if (ability.Name.ToLower().Contains(searchbar.Text.ToLower()))
					{
						MTCollection.Add(ability);
					}
				}
			}
			else
			{
				MTCollection.Clear();
				foreach (majorTrait ability in MTLst) //adds all races to ObservableCollection TraitCollection
				{
					MTCollection.Add(ability);
				}
			}
			lstTraits.SelectedIndex = 0;
		}

        public static void MTCopy(ListView lstTraits, ObservableCollection<majorTrait> TraitCollection, config CurrentConfig, string type)
        {
            majorTrait? trait = lstTraits.SelectedItem as majorTrait;
            if (trait != null)
            {
                majorTrait tempMT = new majorTrait(CurrentConfig.newUID(type));
                tempMT.AffectedResources = trait.AffectedResources;
                tempMT.Dependencies = trait.Dependencies;
                tempMT.Exclusions = trait.Exclusions;
                tempMT.Discounts = trait.Discounts;
                tempMT.Name = trait.Name;
                tempMT.Description = trait.Description;
                tempMT.Cost = trait.Cost;
                tempMT.FreeAbilities = trait.FreeAbilities;
                tempMT.CostTypes = trait.CostTypes;

                CurrentConfig.saveToList(tempMT);
                TraitCollection.Add(tempMT);
                lstTraits.SelectedIndex = TraitCollection.Count - 1;
            }
        }
        public static void AddFreeAbilities(ListView lstTraits, ListView ListFreeAbilities, config CurrentConfig)
        {
            int SelIndex = lstTraits.SelectedIndex;  //saves selected index so it is not lost
            ComboBox comboBox = new ComboBox();
            comboBox.IsReadOnly = true;
            comboBox.IsDropDownOpen = false;
            comboBox.Margin = new Thickness(5, 5, 0, 0);
            comboBox.Height = 24;
            comboBox.Width = 185;
            comboBox.DisplayMemberPath = "Name";
            foreach (majorTrait abi in CurrentConfig.AbiList)   //adds all abilities from CurrentConfig to the combobox
            {
                if (abi != lstTraits.SelectedItem)
                {
                    comboBox.Items.Add(abi);
                }
            }

            ListFreeAbilities.Items.Add(comboBox);
            lstTraits.SelectedIndex = SelIndex;  //applies saved index selection
        }
        /// <summary>
        /// adds a row to hold a or requrement
        /// </summary>
        public static void AddRequirmentsList(ListView ListRequirements)
		{
			StackPanel ReqStackPanel = new StackPanel();
			ReqStackPanel.Height = 36;
			ReqStackPanel.Orientation = Orientation.Horizontal;
			ListRequirements.Items.Add(ReqStackPanel);
			ListRequirements.SelectedIndex = ListRequirements.Items.Count - 1;
		}
		/// <summary>
		/// Adds a and requrement to the selected row
		/// </summary>
		public static void AddRequirment(ListView lstTraits, ListView ListRequirements, ComboBox RequireTypeBox, config CurrentConfig)
		{
			if (ListRequirements.SelectedValue != null)
			{
				int count = (ListRequirements.SelectedValue as StackPanel).Children.Count;
				ComboBox comboBoxSelection = new ComboBox();
				comboBoxSelection.IsReadOnly = true;
				comboBoxSelection.IsDropDownOpen = false;
				comboBoxSelection.Margin = new Thickness(5, 5, 0, 0);
				comboBoxSelection.Height = 26;
				comboBoxSelection.Width = 185;
				comboBoxSelection.VerticalAlignment = VerticalAlignment.Top;
				comboBoxSelection.DisplayMemberPath = "Name";


				switch (RequireTypeBox.SelectedItem.ToString())
				{
					case "Race":
						foreach (majorTrait MT in CurrentConfig.RacList)
						{
							if (MT != lstTraits.SelectedItem)
							{
								comboBoxSelection.Items.Add(MT);
							}
						}
						break;

					case "Ability":
						foreach (majorTrait MT in CurrentConfig.AbiList)
						{
							if (MT != lstTraits.SelectedItem)
							{
								comboBoxSelection.Items.Add(MT);
							}
						}
						break;

					case "Career":
						foreach (majorTrait MT in CurrentConfig.CarList)
						{
							if (MT != lstTraits.SelectedItem)
							{
								comboBoxSelection.Items.Add(MT);
							}
						}
						break;

					case "Religion":
						foreach (majorTrait MT in CurrentConfig.RelList)
						{
							if (MT != lstTraits.SelectedItem)
							{
								comboBoxSelection.Items.Add(MT);
							}
						}
						break;
				}

				Label and = new Label();
				and.Height = 26;
				and.VerticalAlignment = VerticalAlignment.Top;

				if (count == 0)
				{
					and.Width = 30;
					and.Margin = new Thickness(0, 5, 0, 0);
					and.Content = "OR";
				}
				else
				{
					and.Width = 35;
					and.Margin = new Thickness(5, 5, 0, 0);
					and.Content = "AND";
				}

				(ListRequirements.SelectedValue as StackPanel).Children.Add(and);
				(ListRequirements.SelectedValue as StackPanel).Children.Add(comboBoxSelection);
			}
		}
		public static void AddCostType(ListView lstTraits, ListView ListCosts, config CurrentConfig)
		{
			int SelIndex = lstTraits.SelectedIndex;  //saves selected index so it is not lost

			ComboBox comboBoxOne = new ComboBox();  //starts on the currency combobox
			comboBoxOne.Text = "Select currency";
			comboBoxOne.IsReadOnly = true;
			comboBoxOne.IsDropDownOpen = false;
			comboBoxOne.Margin = new Thickness(5, 5, 0, 0);
			comboBoxOne.Height = 24;
			comboBoxOne.Width = 185;

			comboBoxOne.DisplayMemberPath = "Name";
			foreach (resourceTrait res in CurrentConfig.ResList)
			{
				if (res != lstTraits.SelectedItem)
				{
					comboBoxOne.Items.Add(res);
				}
			}

			ListCosts.Items.Add(comboBoxOne);   //makes the combobox a child of the stackpanel
			ListCosts.SelectedIndex = ListCosts.Items.Count - 1;
			lstTraits.SelectedIndex = SelIndex;    //applies saved index selection
		}
		public static void AddAffectedResources(ListView lstTraits, ListView ListAffectedResources, config CurrentConfig)
		{
			int SelIndex = lstTraits.SelectedIndex;  //saves selected index so it is not lost
			StackPanel stackPanel = new StackPanel();
			stackPanel.Orientation = Orientation.Horizontal;

			ComboBox comboBoxOne = new ComboBox();  //starts on the recouse combobox
			comboBoxOne.Text = "Select Stat";
			comboBoxOne.IsReadOnly = true;
			comboBoxOne.IsDropDownOpen = false;
			comboBoxOne.Margin = new Thickness(5, 5, 0, 0);
			comboBoxOne.Height = 24;
			comboBoxOne.Width = 185;

			comboBoxOne.DisplayMemberPath = "Name";
			foreach (resourceTrait res in CurrentConfig.ResList)
			{
				if (res != lstTraits.SelectedItem)
				{
					comboBoxOne.Items.Add(res);
				}
			}


			stackPanel.Children.Add(comboBoxOne);   //makes the combobox a child of the stackpanel

			TextBox textBox = new TextBox();    //starts on the number only textbox
			textBox.Margin = new Thickness(15, 13, 0, 0);
			textBox.Width = 40;
			textBox.Height = 24;
			textBox.VerticalAlignment = VerticalAlignment.Top;
			textBox.TextChanged += Functionality.NumberValidationTextBox;

			stackPanel.Children.Add(textBox);   //makes the textbox a child of the stackpanel


			ListAffectedResources.Items.Add(stackPanel);
			ListAffectedResources.SelectedIndex = ListAffectedResources.Items.Count - 1;
			lstTraits.SelectedIndex = SelIndex;  //applies saved index selection
		}
		/// <summary>
		/// deletes selected Affected Resources 
		/// </summary>
		public static void AddExclusion(ListView lstTraits, ListView ListExclusion, config CurrentConfig, object sender)
		{
			int SelIndex = lstTraits.SelectedIndex;  //saves selected index so it is not lost

			ComboBox comboBoxOne = new ComboBox();  //starts on the recouse combobox
			comboBoxOne.Text = "Select Stat";
			comboBoxOne.IsReadOnly = true;
			comboBoxOne.IsDropDownOpen = false;
			comboBoxOne.Margin = new Thickness(5, 5, 0, 0);
			comboBoxOne.Height = 24;
			comboBoxOne.Width = 185;
			comboBoxOne.DisplayMemberPath = "Name";
			string[] name = ((sender as Button).Content.ToString()).Split(' ');
			if (name[1] == "CAREER")
			{
				foreach (majorTrait MT in CurrentConfig.CarList)
				{
					if (MT != lstTraits.SelectedItem)
					{
						comboBoxOne.Items.Add(MT);
					}
				}
			}
			else if (name[1] == "ABILITY")
			{
				foreach (majorTrait MT in CurrentConfig.AbiList)
				{
					if (MT != lstTraits.SelectedItem)
					{
						comboBoxOne.Items.Add(MT);
					}
				}
			}
			else if (name[1] == "RACE")
			{
				foreach (majorTrait MT in CurrentConfig.RacList)
				{
					if (MT != lstTraits.SelectedItem)
					{
						comboBoxOne.Items.Add(MT);
					}
				}
			}

			ListExclusion.Items.Add(comboBoxOne);
			ListExclusion.SelectedIndex = ListExclusion.Items.Count - 1;
			lstTraits.SelectedIndex = SelIndex;  //applies saved index selection
		}
		public static void Deleteselected(ListView List)
		{
			var index = List.SelectedIndex;
			if (index >= 0)
			{
				List.Items.RemoveAt(index);
				List.SelectedIndex = List.Items.Count - 1;
			}
		}
		public static void AddDiscount(config CurrentConfig, ListView lstTraits, ListView ListDiscounts, object sender)
		{
			int SelIndex = lstTraits.SelectedIndex;  //saves selected index so it is not lost
			StackPanel stackPanelDiscounts = new StackPanel();
			stackPanelDiscounts.Orientation = Orientation.Horizontal;

			ComboBox comboBoxOne = new ComboBox();  //starts on the recouse combobox
			comboBoxOne.Text = "Select Stat";
			comboBoxOne.IsReadOnly = true;
			comboBoxOne.IsDropDownOpen = false;
			comboBoxOne.Margin = new Thickness(5, 5, 0, 0);
			comboBoxOne.Height = 24;
			comboBoxOne.Width = 185;
			comboBoxOne.DisplayMemberPath = "Name";
			string[] name = (sender as Button).Content.ToString().Split(' ');
			if (name[1] == "CAREER")
			{
				foreach (majorTrait MT in CurrentConfig.CarList)
				{
					if (MT != lstTraits.SelectedItem)
					{
						comboBoxOne.Items.Add(MT);
					}
				}
			}
			else if (name[1] == "ABILITY")
			{
				foreach (majorTrait MT in CurrentConfig.AbiList)
				{
					if (MT != lstTraits.SelectedItem)
					{
						comboBoxOne.Items.Add(MT);
					}
				}
			}
			else if (name[1] == "RACE")
			{
				foreach (majorTrait MT in CurrentConfig.RacList)
				{
					if (MT != lstTraits.SelectedItem)
					{
						comboBoxOne.Items.Add(MT);
					}
				}
			}

			stackPanelDiscounts.Children.Add(comboBoxOne);   //makes the combobox a child of the stackpanel

			TextBox textBox = new TextBox();    //starts on the number only textbox
			textBox.Margin = new Thickness(15, 13, 0, 0);
			textBox.Width = 40;
			textBox.Height = 24;
			textBox.VerticalAlignment = VerticalAlignment.Top;
			textBox.TextChanged += NumberValidationTextBox;

			stackPanelDiscounts.Children.Add(textBox);   //makes the textbox a child of the stackpanel


			ListDiscounts.Items.Add(stackPanelDiscounts);
			ListDiscounts.SelectedIndex = ListDiscounts.Items.Count - 1;
			lstTraits.SelectedIndex = SelIndex;  //applies saved index selection
		}
		/// <summary>
		/// Validates input in "amount" textbox to only allow integers.
		/// </summary>
		public static void NumberValidationTextBox(object sender, EventArgs e)
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
		public static void SaveMTrait(config CurrentConfig, List<majorTrait> MTList, majorTrait trait, ListView lstTraits, TextBox nameBox, TextBox descBox, TextBox? costBox = null, ListView? ListAffectedResources = null, ListView? ListFreeAbilities = null, ListView? ListCosts = null, ListView? ListDiscounts = null, ListView? ListExclusion = null, ListView? ListRequirements = null, TextBox? playerReqBox = null)
		{
			if (trait == null)
			{
				trait = new majorTrait("");
			}
			int SelIndex = lstTraits.SelectedIndex;  //saves selected index so it is not lost
			int index = 0;
			if (trait.UID.Equals(""))    //is true when funtion is called via a button
			{
				if (lstTraits.SelectedIndex >= 0)
				{
					index = MTList.FindIndex(i => string.Equals(i.UID, (lstTraits.SelectedItem as majorTrait).UID));
				}
			}
			else
			{
				index = MTList.FindIndex(i => string.Equals(i.UID, trait.UID));
			}
			string UID = MTList[index].UID; //uses the given index to find the wanted UID
			majorTrait currentMT = CurrentConfig.GetTrait(UID);
			currentMT.deleteContent();

			currentMT.Name = nameBox.Text;
			currentMT.Description = descBox.Text;
			if (playerReqBox != null)
			{
				currentMT.PlayerReq = playerReqBox.Text;
			}

			if (ListAffectedResources != null)
			{
				foreach (StackPanel PANEL in ListAffectedResources.Items)
				{
					foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
					{
						foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
						{
							if (box.SelectedIndex >= 0 && textBox.Text != "")
							{
								string TempUID = (box.SelectedItem as resourceTrait).UID;  //gets the affected rescource
								int TempVal = int.Parse(textBox.Text);  //gets the value
								currentMT.AffectedResources.Add(new AmountUID(TempUID, TempVal));   //saves the affected rescources and their values
							}
						}
					}
				}
			}
			if (ListFreeAbilities != null)
			{
				foreach (ComboBox BOX in ListFreeAbilities.Items)
				{
					if (BOX.SelectedIndex >= 0)
					{
						string TempUID = (BOX.SelectedItem as majorTrait).UID;
						currentMT.FreeAbilities.Add(TempUID); // saves the free abilities
					}
				}
			}
			if (costBox != null && ListCosts != null && ListDiscounts != null)
			{
				string textcost = costBox.Text;
				if (textcost != "")
				{
					currentMT.Cost = int.Parse(textcost);
				}
				else
				{
					currentMT.Cost = 0;
				}
				foreach (ComboBox BOX in ListCosts.Items)
				{
					if (BOX.SelectedIndex >= 0)
					{
						string TempUID = (BOX.SelectedItem as resourceTrait).UID;
						currentMT.CostTypes.Add(TempUID);
					}
				}
				foreach (StackPanel PANEL in ListDiscounts.Items)
				{
					foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
					{
						foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
						{
							if (box.SelectedItem != null && textBox.Text != "")
							{
								currentMT.Discounts.Add(new AmountUID((box.SelectedItem as majorTrait).UID, int.Parse(textBox.Text)));
							}
						}
					}
				}
			}
			if (ListExclusion != null && ListRequirements != null)
			{
				foreach (ComboBox box in ListExclusion.Items.OfType<ComboBox>())
				{
					if (box.SelectedItem != null)
					{
						currentMT.Exclusions.Add((box.SelectedItem as majorTrait).UID);
					}
				}

				int ind = 0;
				foreach (StackPanel PANEL in ListRequirements.Items)
				{
					currentMT.Dependencies.Add(new List<string>());
					foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
					{
						if (box.SelectedItem != null)
						{
							currentMT.Dependencies[ind].Add((box.SelectedItem as majorTrait).UID);
						}
					}
					if (currentMT.Dependencies[ind].Count == 0)
					{
						currentMT.Dependencies.RemoveAt(ind);
						ind--;
					}
					ind++;
				}
			}

            MTList[index] = currentMT;
			object sender = new object();
			EventArgs e = new EventArgs();
			lstTraits.SelectedIndex = SelIndex;  //applies saved index selection
		}
		public static void MTChange(config CurrentConfig, List<majorTrait> MTlist, majorTrait LastSelected, ListView lstTraits, TextBox nameBox, TextBox descBox, TextBox? costBox = null, ListView? ListAffectedResources = null, ListView? ListFreeAbilities = null, ListView? ListCosts = null, ListView? ListDiscounts = null, ListView? ListExclusion = null, ListView? ListRequirements = null, ComboBox? RequireTypeBox = null, TextBox? playerReqBox = null)
		{
			if (lstTraits.SelectedIndex >= 0)    //lstTraits.SelectedIndex returns -1 if nothing is selected
			{
				if (!LastSelected.UID.Equals(""))
				{
					Functionality.SaveMTrait(CurrentConfig, MTlist, LastSelected, lstTraits, nameBox, descBox, costBox, ListAffectedResources, ListFreeAbilities, ListCosts, ListDiscounts, ListExclusion, ListRequirements);
					nameBox.Text = "";
					descBox.Text = "";
				}
				LastSelected = lstTraits.SelectedItem as majorTrait;
				majorTrait currentMT = lstTraits.SelectedItem as majorTrait; //gets the trait to be loaded
				nameBox.Text = currentMT.Name; //sets text to the name from the current MajorTrait object
				descBox.Text = currentMT.Description;  //sets text to the description from the current MajorTrait object

                if (playerReqBox != null)
                {
					playerReqBox.Text = currentMT.PlayerReq;
                }
                if (ListAffectedResources != null)
                {
                    ListAffectedResources.Items.Clear();
                    foreach (AmountUID AffRes in currentMT.AffectedResources)
					{
						AddAffectedResources(lstTraits, ListAffectedResources, CurrentConfig);
					}
					int ind = 0;
					foreach (StackPanel PANEL in ListAffectedResources.Items)
					{
						foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
						{
							foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
							{
								AmountUID tempAffRes = currentMT.AffectedResources[ind];
								box.SelectedItem = CurrentConfig.GetTrait(tempAffRes.UID); //selects the starter resources in the comboboxes
								textBox.Text = tempAffRes.Amount.ToString(); //sets the right amounts in the textboxes
							}
						}
						ind++;
					}
				}

				if (costBox != null && ListCosts != null && ListDiscounts != null)
                {
                    ListDiscounts.Items.Clear();
                    ListCosts.Items.Clear();
                    costBox.Text = "";
                    costBox.Text = currentMT.Cost.ToString();
					foreach (string CostType in currentMT.CostTypes)
					{
						AddCostType(lstTraits, ListCosts, CurrentConfig);
					}
					int ind = 0;
                    string TempUID;
                    foreach (ComboBox BOX in ListCosts.Items)
					{
						TempUID = currentMT.CostTypes[ind];
						BOX.SelectedItem = CurrentConfig.GetTrait(TempUID);
						ind++;
					}
                    foreach (AmountUID Disc in currentMT.Discounts)
                    {
                        string[] Condui = Disc.UID.Split("-/");
                        if (Condui[0] == "RacList")
                        {
                            Button nsender = new Button() { Content = "ADD RACE" };
                            AddDiscount(CurrentConfig, lstTraits, ListDiscounts, nsender);
                        }
                        else if (Condui[0] == "AbiList")

                        {
                            Button nsender = new Button() { Content = "ADD ABILITY" };
                            AddDiscount(CurrentConfig, lstTraits, ListDiscounts, nsender);
                        }
                        else if (Condui[0] == "CarList")
                        {
                            Button nsender = new Button() { Content = "ADD CAREER" };
                            AddDiscount(CurrentConfig, lstTraits, ListDiscounts, nsender);
                        }

                    }
                    ind = 0;
                    foreach (StackPanel PANEL in ListDiscounts.Items)
                    {
                        foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
                        {
                            foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
                            {
                                AmountUID tempDisc = currentMT.Discounts[ind];
                                box.SelectedItem = CurrentConfig.GetTrait(tempDisc.UID);
                                textBox.Text = tempDisc.Amount.ToString();  //sets the right amounts in the textboxes
                            }
                        }
                        ind++;
                    }
                }
				if (ListFreeAbilities != null)
                {
                    ListFreeAbilities.Items.Clear();
                    foreach (string FreeAbil in currentMT.FreeAbilities)    //makes the needed comboboxes to hold the free abilities
					{
						AddFreeAbilities(lstTraits, ListFreeAbilities, CurrentConfig);
					}
					int ind = 0;
					string TempUID;
					foreach (ComboBox BOX in ListFreeAbilities.Items)
					{
						TempUID = currentMT.FreeAbilities[ind];
						BOX.SelectedItem = CurrentConfig.GetTrait(TempUID);    //selects the free abilities in the comboboxes
						ind++;
					}
				}
				if (ListExclusion != null && ListRequirements != null && RequireTypeBox != null)
                {
                    ListRequirements.Items.Clear();
                    ListExclusion.Items.Clear();
                    foreach (string str in currentMT.Exclusions)
					{
						string[] typeid = str.Split("-/");
						if (typeid[0] == "RacList")
						{
							Button nsender = new Button() { Content = "ADD RACE" };
							AddExclusion(lstTraits, ListExclusion, CurrentConfig, nsender);
						}
						else if (typeid[0] == "AbiList")

						{
							Button nsender = new Button() { Content = "ADD ABILITY" };
							AddExclusion(lstTraits, ListExclusion, CurrentConfig, nsender);
						}
						else if (typeid[0] == "CarList")
						{
							Button nsender = new Button() { Content = "ADD CAREER" };
							AddExclusion(lstTraits, ListExclusion, CurrentConfig, nsender);
						}
					}
					int ind = 0;
					foreach (ComboBox box in ListExclusion.Items)
					{
						box.SelectedItem = CurrentConfig.GetTrait(currentMT.Exclusions[ind]);
						ind++;
					}
					string[] id;
					ind = 0;
					foreach (List<string> list in currentMT.Dependencies)
					{
						AddRequirmentsList(ListRequirements);
						for (int i = 0; i < list.Count; i++)
						{
							id = currentMT.Dependencies[ind][i].Split("-/");
							switch (id[0])
							{
								case "RacList":
									RequireTypeBox.SelectedIndex = 0;
									break;

								case "RelList":
									RequireTypeBox.SelectedIndex = 3;
									break;

								case "CarList":
									RequireTypeBox.SelectedIndex = 2;
									break;

								case "AbiList":
									RequireTypeBox.SelectedIndex = 1;
									break;
							}
							AddRequirment(lstTraits, ListRequirements, RequireTypeBox, CurrentConfig);
						}
						ind++;
					}

					ind = 0;
					int ind2 = 0;
					foreach (StackPanel PANEL in ListRequirements.Items)
					{
						foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
						{
							try
							{
								box.SelectedItem = CurrentConfig.GetTrait(currentMT.Dependencies[ind][ind2]);
							}
							catch { }
							ind2++;
						}
						ind2 = 0;
						ind++;
					}
				}
			}
			else
			{
				LastSelected = new majorTrait("");
                if (playerReqBox != null)
                {
                    playerReqBox.Text = "";
                }
                if (ListRequirements != null && ListExclusion != null)
                {
                    ListRequirements.Items.Clear();
                    ListExclusion.Items.Clear();
                }
                if (ListAffectedResources != null)
                {
                    ListAffectedResources.Items.Clear();
                }
                if (ListDiscounts != null && ListCosts != null && costBox != null)
                {
                    ListDiscounts.Items.Clear();
                    ListCosts.Items.Clear();
                    costBox.Text = "";
                }
                if (ListFreeAbilities != null)
                {
                    ListFreeAbilities.Items.Clear();
                }
                nameBox.Text = "";
                descBox.Text = "";
            }
		}
	}
}
