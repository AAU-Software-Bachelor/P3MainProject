using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Shell;
using static Project2.majorTrait;

namespace Project2
{
	/// <summary>
	/// Interaction logic for Career.xaml
	/// </summary>
	public partial class CareerPage : Page
	{
		public CareerPage(config currentConfig)
		{
			CurrentConfig = currentConfig;
			InitializeComponent();
			RequireTypeBox.Items.Add("Race");
            RequireTypeBox.Items.Add("Ability");
            RequireTypeBox.Items.Add("Career");
            RequireTypeBox.Items.Add("Religion");
			RequireTypeBox.SelectedIndex= 0;
			CareerCollection = new ObservableCollection<majorTrait>();
			foreach (majorTrait Career in CurrentConfig.CarList) //adds all Career to ObservableCollection CareerCollection
			{
				CareerCollection.Add(Career);
			}
			lstCareer.ItemsSource = CareerCollection;
			CurrentIndex = -1;  //skip the next use of CurrentIndex
			lstCareer.SelectedIndex = 0;

        }
		public config CurrentConfig { get; set; }
		int CurrentIndex { get; set; }  //keeps track of what index to use
		private ObservableCollection<majorTrait> CareerCollection;   //itemSource for lstCareer ListVeiw


		private void CareerMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

			MainWindow mainWindow = new MainWindow(CurrentConfig);
			Application.Current.MainWindow.Content = mainWindow;
		}

		private void btnCareer_ClickAdd(object sender, EventArgs e)
		{
			majorTrait tempCareer = new majorTrait(CurrentConfig.newUID("CarList")) { Name = "new Career" };   //makes the new Career object
			CurrentConfig.saveToList(tempCareer);
			CareerCollection.Add(tempCareer);
			lstCareer.SelectedIndex = CareerCollection.Count - 1;
		}

		private void btnCareer_ClickDelete(object sender, EventArgs e)
		{
			var index = lstCareer.SelectedIndex;
			if (index >= 0)
			{
				CareerCollection.Remove(CurrentConfig.GetTrait(CareerCollection[index].UID, true)); //gets the Career to be deleteted via GetTrait while it deletes it, and deletes its counterpart in CareerCollection
				lstCareer.SelectedIndex = CareerCollection.Count - 1;
			}
		}


		private void OnClickAddStarterAbilities(object sender, EventArgs e)
		{
			int SelIndex = lstCareer.SelectedIndex;  //saves selected caeer so it is not lost
			ComboBox comboBox = new ComboBox();
			comboBox.IsReadOnly = true;
			comboBox.IsDropDownOpen = false;
			comboBox.Margin = new Thickness(5, 5, 0, 0);
			comboBox.Height = 24;
			comboBox.Width = 185;
			comboBox.DisplayMemberPath = "Name";
			foreach (majorTrait abi in CurrentConfig.AbiList)   //adds all abilities from CurrentConfig to the combobox
			{
				comboBox.Items.Add(abi);
			}

			this.ListStarterAbilities.Items.Add(comboBox);
			lstCareer.SelectedIndex = SelIndex;  //applies saved career selection
		}
		/// <summary>
		/// Deletes selected starterAbility
		/// </summary>
		private void OnClickDeleteStarterAbilities(object sender, RoutedEventArgs e)
		{
			var index = ListStarterAbilities.SelectedIndex;
			if (index >= 0)
			{
				ListStarterAbilities.Items.RemoveAt(index);
			}
		}

        private void OnClickAddRequirmentsList(object sender, EventArgs e)
        {
            this.InitializeComponent();
            StackPanel ReqStackPanel = new StackPanel();
            ReqStackPanel.Height = 36;
            ReqStackPanel.Orientation = Orientation.Horizontal;
            ListRequirements.Items.Add(ReqStackPanel);
            ListRequirements.SelectedIndex = ListRequirements.Items.Count - 1;
        }

        private void OnClickDeleteRequirmentsList(object sender, RoutedEventArgs e)
		{
			var index = ListRequirements.SelectedIndex;
			if (index >= 0)
			{
				ListRequirements.Items.RemoveAt(index);
			}
		}

        private void OnClickAddRequirment(object sender, EventArgs e)
        {
            this.InitializeComponent();

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
                            comboBoxSelection.Items.Add(MT);
                        }
                        break;

                    case "Ability":
                        foreach (majorTrait MT in CurrentConfig.AbiList)
                        {
                            comboBoxSelection.Items.Add(MT);
                        }
                        break;

                    case "Career":
                        foreach (majorTrait MT in CurrentConfig.CarList)
                        {
                            comboBoxSelection.Items.Add(MT);
                        }
                        break;

                    case "Religion":
                        foreach (majorTrait MT in CurrentConfig.RelList)
                        {
                            comboBoxSelection.Items.Add(MT);
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

        private void OnClick_AddCostType(object sender, EventArgs e)
		{
			int SelIndex = lstCareer.SelectedIndex;  //saves selected Career so it is not lost
			this.InitializeComponent();

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
				comboBoxOne.Items.Add(res);
			}

			this.ListCosts.Items.Add(comboBoxOne);   //makes the combobox a child of the stackpanel
			lstCareer.SelectedIndex = SelIndex;    //applies saved Career selection
		}

		private void OnClick_DeleteCostType(object sender, EventArgs e)
		{
			var index = ListCosts.SelectedIndex;
			if (index >= 0)
			{
				ListCosts.Items.RemoveAt(index);
			}
		}
		private void OnClick_AddGrantedResouce(object sender, EventArgs e)
		{
			int SelIndex = lstCareer.SelectedIndex;  //saves selected Career so it is not lost
			this.InitializeComponent();
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
				comboBoxOne.Items.Add(res);
			}


			stackPanel.Children.Add(comboBoxOne);   //makes the combobox a child of the stackpanel

			TextBox textBox = new TextBox();    //starts on the number only textbox
			textBox.Margin = new Thickness(15, 13, 0, 0);
			textBox.Width = 40;
			textBox.Height = 24;
			textBox.VerticalAlignment = VerticalAlignment.Top;
			textBox.TextChanged += NumberValidationTextBox;

			stackPanel.Children.Add(textBox);   //makes the textbox a child of the stackpanel


			this.ListGrantedResources.Items.Add(stackPanel);
			lstCareer.SelectedIndex = SelIndex;  //applies saved Career selection
		}
		/// <summary>
		/// deletes selected Starter Resources 
		/// </summary>
		private void OnClick_DeleteGrantedResouce(object sender, EventArgs e)
		{
			var index = ListGrantedResources.SelectedIndex;
			if (index >= 0)
			{
				ListGrantedResources.Items.RemoveAt(index);
			}
		}

		private void OnClick_AddExclusion(object sender, EventArgs e)
		{
			int SelIndex = lstCareer.SelectedIndex;  //saves selected Career so it is not lost
			this.InitializeComponent();

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
				foreach (majorTrait car in CurrentConfig.CarList)
				{
					comboBoxOne.Items.Add(car);
				}
			}
			else if (name[1] == "ABILITY")
			{
				foreach (majorTrait abi in CurrentConfig.AbiList)
				{
					comboBoxOne.Items.Add(abi);
				}
			}
			else if (name[1] == "RACE")
			{
				foreach (majorTrait rac in CurrentConfig.RacList)
				{
					comboBoxOne.Items.Add(rac);
				}
			}

			this.ListExclusion.Items.Add(comboBoxOne);
			ListExclusion.SelectedIndex = ListExclusion.Items.Count - 1;
			lstCareer.SelectedIndex = SelIndex;  //applies saved Career selection
		}

		private void OnClick_DeleteExclusion(object sender, EventArgs e)
		{
			var index = ListExclusion.SelectedIndex;
			if (index >= 0)
			{
				ListExclusion.Items.RemoveAt(index);
				ListExclusion.SelectedIndex = ListExclusion.Items.Count - 1;
			}
		}

		private void OnClick_AddDiscount(object sender, EventArgs e)
		{
			int SelIndex = lstCareer.SelectedIndex;  //saves selected Career so it is not lost
			this.InitializeComponent();
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
			string[] name = ((sender as Button).Content.ToString()).Split(' ');
			if (name[1] == "CAREER")
			{
				foreach (majorTrait car in CurrentConfig.CarList)
				{
					comboBoxOne.Items.Add(car);
				}
			}
			else if (name[1] == "ABILITY")
			{
				foreach (majorTrait abi in CurrentConfig.AbiList)
				{
					comboBoxOne.Items.Add(abi);
				}
			}
			else if (name[1] == "RACE")
			{
				foreach (majorTrait rac in CurrentConfig.RacList)
				{
					comboBoxOne.Items.Add(rac);
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


			this.ListDiscounts.Items.Add(stackPanelDiscounts);
			lstCareer.SelectedIndex = SelIndex;  //applies saved Career selection
		}

		/// <summary>
		/// deletes selected Starter Resources 
		/// </summary>
		private void OnClick_DeleteDiscount(object sender, RoutedEventArgs e)
		{
			var index = ListDiscounts.SelectedIndex;
			if (index >= 0)
			{
				ListDiscounts.Items.RemoveAt(index);
			}
		}

		bool amworkingonchange = false;
		private void OnCareerChanged(object sender, RoutedEventArgs e)
		{
			if (amworkingonchange == false)
			{

				amworkingonchange = true;
				CareerChange(sender, e);

				//Task.Delay(2);
				amworkingonchange = false;
			}

		}


		private void CareerChange(object sender, EventArgs e)
		{
			if (lstCareer.SelectedIndex >= 0)    //lstCareer.SelectedIndex returns -1 if nothing is selected
			{
				if (CurrentIndex >= 0)
				{
					SaveCareer(CurrentIndex);
					ListRequirements.Items.Clear();
					ListGrantedResources.Items.Clear();
					ListDiscounts.Items.Clear();
					ListCosts.Items.Clear();
					ListStarterAbilities.Items.Clear();
                    ListExclusion.Items.Clear();


                }
				CurrentIndex = lstCareer.SelectedIndex;
				majorTrait currentMT = CurrentConfig.CarList[CurrentIndex]; //gets the trait to be loaded

				(this.FindName("nameBox") as TextBox).Text = currentMT.Name; //sets text to the name from the current MajorTrait object
				(this.FindName("descBox") as TextBox).Text = currentMT.Description;  //sets text to the description from the current MajorTrait object

				foreach (AmountUID AffRes in currentMT.AffectedResources)
				{
					OnClick_AddGrantedResouce(sender, e);
				}
				int ind = 0;
				string TempUID;
				foreach (StackPanel PANEL in (this.FindName("ListGrantedResources") as ListView).Items)
				{
					foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
					{
						foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
						{
							AmountUID tempAffRes = currentMT.AffectedResources[ind];
							box.SelectedIndex = CurrentConfig.ResList.FindIndex(i => string.Equals(i.UID, tempAffRes.UID)); //selects the starter resources in the comboboxes
							textBox.Text = tempAffRes.Amount.ToString(); //sets the right amounts in the textboxes
						}
					}
					ind++;
				}

				(this.FindName("costBox") as TextBox).Text = currentMT.Cost.ToString();
				foreach (string CostType in currentMT.CostTypes)
				{
					OnClick_AddCostType(sender, e);
				}
				ind = 0;
				foreach (ComboBox BOX in (this.FindName("ListCosts") as ListView).Items)
				{
					TempUID = currentMT.CostTypes[ind];
					BOX.SelectedIndex = CurrentConfig.ResList.FindIndex(i => string.Equals(i.UID, TempUID));
					ind++;
				}

                foreach (string FreeAbil in currentMT.FreeAbilities)    //makes the needed comboboxes to hold the free abilities
                {
                    OnClickAddStarterAbilities(sender, e);
                }
                ind = 0;
                foreach (ComboBox BOX in (this.FindName("ListStarterAbilities") as ListView).Items)
                {
                    TempUID = currentMT.FreeAbilities[ind];
                    BOX.SelectedIndex = CurrentConfig.AbiList.FindIndex(i => string.Equals(i.UID, TempUID));    //selects the free abilities in the comboboxes
                    ind++;
                }

                foreach (string str in currentMT.Exclusions)
				{
					string[] typeid = str.Split("-/");
					if (typeid[0] == "RacList")
					{
						Button nsender = new Button() { Content = "ADD RACE" };
						OnClick_AddExclusion(nsender, e);
					}
					else if (typeid[0] == "AbiList")
					{
						Button nsender = new Button() { Content = "ADD ABILITY" };
						OnClick_AddExclusion(nsender, e);
					}
					else if (typeid[0] == "CarList")
					{
						Button nsender = new Button() { Content = "ADD CAREER" };
						OnClick_AddExclusion(nsender, e);
					}
				}

				ind = 0;
				ListView ltr = (this.FindName("ListExclusion") as ListView);
				foreach (ComboBox box in ltr.Items)
				{
					string tempID = currentMT.Exclusions[ind]; //test
					string[] ID = tempID.Split("-/");
					if (ID[0] == "CarList")
					{
						box.SelectedIndex = CurrentConfig.CarList.FindIndex(i => string.Equals(i.UID, tempID));
					}
					else if (ID[0] == "RacList")
					{
						box.SelectedIndex = CurrentConfig.RacList.FindIndex(i => string.Equals(i.UID, tempID));
					}
					else if (ID[0] == "RelList")
					{
						box.SelectedIndex = CurrentConfig.RelList.FindIndex(i => string.Equals(i.UID, tempID));
					}
					else if (ID[0] == "AbiList")
					{
						box.SelectedIndex = CurrentConfig.AbiList.FindIndex(i => string.Equals(i.UID, tempID));
					}
					ind++;
				}

				foreach (AmountUID Disc in currentMT.Discounts)
				{
					string[] Condui = Disc.UID.Split("-/");
					if (Condui[0] == "RacList")
					{
						Button nsender = new Button() { Content = "ADD RACE" };
						OnClick_AddDiscount(nsender, e);
					}
					else if (Condui[0] == "AbiList")

					{
						Button nsender = new Button() { Content = "ADD ABILITY" };
						OnClick_AddDiscount(nsender, e);
					}
					else if (Condui[0] == "CarList")
					{
						Button nsender = new Button() { Content = "ADD CAREER" };
						OnClick_AddDiscount(nsender, e);
					}

				}
				ind = 0;
				foreach (StackPanel PANEL in (this.FindName("ListDiscounts") as ListView).Items)
				{
					foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
					{
						foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
						{
							AmountUID tempDisc = currentMT.Discounts[ind];
							string[] ID = tempDisc.UID.Split("-/");
							if (ID[0] == "CarList")
							{
								box.SelectedIndex = CurrentConfig.CarList.FindIndex(i => string.Equals(i.UID, tempDisc.UID));
							}
							else if (ID[0] == "RacList")
							{
								box.SelectedIndex = CurrentConfig.RacList.FindIndex(i => string.Equals(i.UID, tempDisc.UID));
							}
							else if (ID[0] == "AbiList")
							{
								box.SelectedIndex = CurrentConfig.AbiList.FindIndex(i => string.Equals(i.UID, tempDisc.UID));
							}
							textBox.Text = tempDisc.Amount.ToString();  //sets the right amounts in the textboxes
						}
					}
					ind++;
				}

                string[] id;
                ind = 0;
                foreach (List<string> list in currentMT.Dependencies)
                {
                    OnClickAddRequirmentsList(sender, e);
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
                        OnClickAddRequirment(sender, e);
                    }
                    ind++;
                }

                ind = 0;
                int ind2 = 0;
                foreach (StackPanel PANEL in (this.FindName("ListRequirements") as ListView).Items)
                {
                    foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
                    {
                        box.SelectedItem = CurrentConfig.GetTrait(currentMT.Dependencies[ind][ind2]);
                        ind2++;
                    }
                    ind2 = 0;
                    ind++;
                }

            }
			else
			{
				CurrentIndex = -1;
				ListRequirements.Items.Clear();
				ListGrantedResources.Items.Clear();
				ListDiscounts.Items.Clear();
				ListCosts.Items.Clear();
				ListExclusion.Items.Clear();
                ListStarterAbilities.Items.Clear();
            }
		}

		private void OnClickSaveCareer(object sender, EventArgs e)
		{
			SaveCareer();
		}

		private void SaveCareer(int index = -1)
		{
			int SelIndex = lstCareer.SelectedIndex;  //saves selected Career so it is not lost

			string UID = "";
			if (index == -1)    //is true when funtion is called via a button
			{
				if (lstCareer.SelectedIndex >= 0)
				{
					UID = CurrentConfig.CarList[lstCareer.SelectedIndex].UID;    //uses the selected index to find the wanted UID
					index = CurrentConfig.CarList.FindIndex(i => string.Equals(i.UID, UID));
				}
			}
			else
			{
				UID = CurrentConfig.CarList[index].UID; //uses the given index to find the wanted UID
			}
			if (UID != "")
			{
				majorTrait currentMT = CurrentConfig.GetTrait(UID);
				currentMT.deleteContent();

				currentMT.Name = (this.FindName("nameBox") as TextBox).Text;
				currentMT.Description = (this.FindName("descBox") as TextBox).Text;
				currentMT.Cost = int.Parse((this.FindName("costBox") as TextBox).Text);

				foreach (ComboBox BOX in (this.FindName("ListStarterAbilities") as ListView).Items)
				{
					if (BOX.SelectedIndex >= 0)
					{
						string TempUID = CurrentConfig.AbiList[BOX.SelectedIndex].UID;
						currentMT.FreeAbilities.Add(TempUID); // saves the free abilities
					}
				}

				foreach (StackPanel PANEL in (this.FindName("ListGrantedResources") as ListView).Items)
				{
					foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
					{
						foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
						{
							if (box.SelectedIndex >= 0 & textBox.Text != "")
							{
								string TempUID = CurrentConfig.ResList[box.SelectedIndex].UID;  //gets the affected rescource
								int TempVal = int.Parse(textBox.Text);  //gets the value
								currentMT.AffectedResources.Add(new AmountUID(TempUID, TempVal));   //saves the affected rescources and their values
							}
						}
					}
				}

				foreach (ComboBox BOX in (this.FindName("ListCosts") as ListView).Items)
				{
					if (BOX.SelectedIndex >= 0)
					{
						string TempUID = CurrentConfig.ResList[BOX.SelectedIndex].UID;
						currentMT.CostTypes.Add(TempUID);
					}
				}

				foreach (StackPanel PANEL in (this.FindName("ListDiscounts") as ListView).Items)
				{
					foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
					{
						foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
						{
							if (box.SelectedItem != null & textBox.Text != "")
							{
								currentMT.Discounts.Add(new AmountUID((box.SelectedItem as majorTrait).UID, int.Parse(textBox.Text)));
							}
						}
					}
				}

				foreach (ComboBox box in (this.FindName("ListExclusion") as ListView).Items.OfType<ComboBox>())
				{
					if (box.SelectedItem != null)
					{
						currentMT.Exclusions.Add((box.SelectedItem as majorTrait).UID);
					}
				}

				int ind = 0;
				foreach (StackPanel PANEL in (this.FindName("ListRequirements") as ListView).Items)
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


				CurrentConfig.CarList[index] = currentMT;
				CareerCollection.Clear(); // clears the list
				foreach (majorTrait Career in CurrentConfig.CarList)  //rewrites the list.
				{
					CareerCollection.Add(Career);
				}

				lstCareer.SelectedIndex = SelIndex;  //applies saved Career selection
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
	}
}
