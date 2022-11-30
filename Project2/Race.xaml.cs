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
using static Project2.Race;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;

namespace Project2
{
	/// <summary>
	/// Interaction logic for Race.xaml
	/// </summary>
	public partial class Race : Page
	{

		public Race(config currentConfig) //Race window constructor
		{
			CurrentConfig = currentConfig;
			InitializeComponent();
			newrace = new ObservableCollection<majorTrait>();
			foreach (majorTrait race in CurrentConfig.RacList) //adds all races to ObservableCollection newrace
			{
				newrace.Add(race);
			}
			lstRaces.ItemsSource = newrace;
			CurrentIndex = -1;	//skip the next use of CurrentIndex
			lstRaces.SelectedIndex = 0;
		}
		public config CurrentConfig { get; set; }
		int CurrentIndex { get; set; }	//keeps track of what index to use
		private ObservableCollection<majorTrait> newrace;   //itemSource for lstRaces ListVeiw

		/// <summary>
		/// sets page to MainWindow
		/// </summary>
		private void RaceMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			SaveRace();
			MainWindow mainWindow = new MainWindow(CurrentConfig);
			Application.Current.MainWindow.Content = mainWindow;
		}


		/// <summary>
		/// adds a new empty race to Current config and newrace
		/// </summary>
		private void btnRaces_ClickAdd(object sender, RoutedEventArgs e)
		{
			int i = newrace.Count + 1;
			majorTrait tempRace = new majorTrait(CurrentConfig.newUID("Race")) { Name = "new race" };	//makes the new race object
			newrace.Add(tempRace);
			CurrentConfig.saveToList(tempRace);
			lstRaces.SelectedIndex = newrace.Count-1;

		}
        /// <summary>
		/// deletes race from both CurrentConfig and newrace
		/// </summary>
        private void btnRaces_ClickDelete(object sender, RoutedEventArgs e)
		{
            var index = lstRaces.SelectedIndex;
			if (lstRaces.SelectedIndex >= 0)
			{
				newrace.Remove(CurrentConfig.GetTrait(newrace[index].UID, true));	//gets the race to be deleteted via GetTrait while it deletes it, and deletes its counterpart in newrace
			}	
		}

		/// <summary>
		/// adds a combobox with all Abilities fron CurrentConfig as selectables
		/// </summary>
		private void OnClickAddStarterAbilities(object sender, RoutedEventArgs e)
		{
			int SelIndex = lstRaces.SelectedIndex;	//saves selected race so it is not lost
			this.InitializeComponent();
			ComboBox comboBox = new ComboBox();
			comboBox.IsReadOnly = true;
			comboBox.IsDropDownOpen = false;
			comboBox.Margin = new Thickness(5, 5, 0, 0);
			comboBox.Height = 24;
			comboBox.Width = 185;
			comboBox.DisplayMemberPath = "Name";
			foreach (majorTrait abi in CurrentConfig.AbilList)	//adds all abilities from CurrentConfig to the combobox
			{
				comboBox.Items.Add(abi);
			}

			this.ListStarterAbilities.Items.Add(comboBox);
			lstRaces.SelectedIndex = SelIndex;	//applies saved race selection
		}
		/// <summary>
		/// Deletes selected starterAbility
		/// </summary>
		private void OnClickDeleteStarterAbilities(object sender, RoutedEventArgs e)
		{
			var index = ListStarterAbilities.SelectedIndex;
			if(index >= 0) 
			{
				ListStarterAbilities.Items.RemoveAt(index);
			}
		}

		/// <summary>
		/// add a combobox with all Resources fron CurrentConfig as selectables, together with a numer only textbox
		/// </summary>
		private void OnClickAddStarterResources(object sender, RoutedEventArgs e)
		{
			int SelIndex = lstRaces.SelectedIndex;  //saves selected race so it is not lost
			this.InitializeComponent();
			StackPanel stackPanel = new StackPanel();
			stackPanel.Orientation = Orientation.Horizontal;

			ComboBox comboBoxOne = new ComboBox();	//starts on the recouse combobox
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


			stackPanel.Children.Add(comboBoxOne);	//makes the combobox a child of the stackpanel

			TextBox textBox = new TextBox();	//starts on the number only textbox
			textBox.Margin = new Thickness(15, 13, 0, 0);
			textBox.Width = 40;
			textBox.Height = 24;
			textBox.VerticalAlignment = VerticalAlignment.Top;
			textBox.TextChanged += NumberValidationTextBox;
			


            stackPanel.Children.Add(textBox);   //makes the textbox a child of the stackpanel

			StackPanel childStackPanel = new StackPanel();
			childStackPanel.Orientation = Orientation.Vertical;

			Image imagePlus = new Image();
			imagePlus.Height = 20;
			imagePlus.Width = 20;
			imagePlus.Margin = new Thickness(1, 5, 0, 0);
			imagePlus.Stretch = Stretch.Fill;
			childStackPanel.Children.Add(imagePlus);

			Image imageMinus = new Image();
			imageMinus.Height = 20;
			imageMinus.Width = 20;
			imageMinus.Margin = new Thickness(1, 0, 0, 0);
			imageMinus.Stretch = Stretch.Fill;
			childStackPanel.Children.Add(imageMinus);
			
			stackPanel.Children.Add(childStackPanel);

			this.ListStarterResources.Items.Add(stackPanel);
			lstRaces.SelectedIndex = SelIndex;	//applies saved race selection
		}

		/// <summary>
		/// deletes selected Starter Resources 
		/// </summary>
		private void OnClickDeleteStarterResources(object sender, RoutedEventArgs e)
		{
			var index = ListStarterResources.SelectedIndex;
			if (index >= 0)
			{
				ListStarterResources.Items.RemoveAt(index);
			}
		}
		/// <summary>
		///		Saves the current race (if it exists) via SaveRace() and loads up the new one that was clicked.
		/// </summary>
		private void OnRaceChanged(object sender, RoutedEventArgs e)
		{
			int SelIndex = lstRaces.SelectedIndex;	//saves selected race so it is not lost
			if (lstRaces.SelectedIndex >= 0)    //lstRaces.SelectedIndex returns -1 if nothing is selected
			{
				if (CurrentIndex >= 0)	//skips saving the previus selected race if -1
				{
					SaveRace(CurrentIndex);
					ListStarterAbilities.Items.Clear();
					ListStarterResources.Items.Clear();
				}
				CurrentIndex = lstRaces.SelectedIndex;
				majorTrait currentMT = CurrentConfig.RacList[CurrentIndex];	//gets the trait to be loaded

				(this.FindName("nameBox") as TextBox).Text = currentMT.Name; //sets text to the name from the current MajorTrait object

				(this.FindName("playerReqBox") as TextBox).Text = currentMT.playerReq; //sets text to the PlayerReq from the current MajorTrait object
                (this.FindName("descBox") as TextBox).Text = currentMT.Description;  //sets text to the description from the current MajorTrait object

                foreach (string FreeAbil in currentMT.freeAbilities)	//makes the needed comboboxes to hold the free abilities
				{
					OnClickAddStarterAbilities(sender, e);
				}
				int ind = 0;
				string TempUID;
				foreach (ComboBox BOX in (this.FindName("ListStarterAbilities") as ListView).Items)
				{
					TempUID = currentMT.freeAbilities[ind];
					BOX.SelectedIndex = CurrentConfig.AbilList.FindIndex(i => string.Equals(i.UID, TempUID));	//selects the free abilities in the comboboxes
					ind++;
				}
				foreach (AffectedResource affRes in currentMT.affectedResources)	//makes the needed comboboxes to hold the starter resources
                {
					OnClickAddStarterResources(sender, e);
				}
				ind = 0;
				foreach (StackPanel PANEL in (this.FindName("ListStarterResources") as ListView).Items)
				{
					foreach (ComboBox box in PANEL.Children.OfType<ComboBox>())
					{
						foreach (TextBox textBox in PANEL.Children.OfType<TextBox>())
						{
							AffectedResource tempAffRes = currentMT.affectedResources[ind];
							box.SelectedIndex = CurrentConfig.ResList.FindIndex(i => string.Equals(i.UID, tempAffRes.UID)); //selects the starter resources in the comboboxes
							textBox.Text = tempAffRes.Amount.ToString(); //sets the right amounts in the textboxes
						}
					}
					ind++;
				}
			}
			else { 
				CurrentIndex = -1;
				ListStarterAbilities.Items.Clear();
				ListStarterResources.Items.Clear();
            }
			lstRaces.SelectedIndex = SelIndex;  //applies saved race selection
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
			int SelIndex = lstRaces.SelectedIndex;	//saves selected race so it is not lost

			string UID = "";
			if (index == -1)	//is true when funtion is called via a button
			{
				if (lstRaces.SelectedIndex >= 0)
				{
					UID = CurrentConfig.RacList[lstRaces.SelectedIndex].UID;    //uses the selected index to find the wanted UID
					index = CurrentConfig.RacList.FindIndex(i => string.Equals(i.UID, UID));
				}
			}
			else
			{
				UID = CurrentConfig.RacList[index].UID;	//uses the given index to find the wanted UID
			}
			if (UID != "")
			{
				majorTrait currentMT = CurrentConfig.GetTrait(UID);
				currentMT.deleteContent();

				currentMT.Type = "Race";
				currentMT.Name = (this.FindName("nameBox") as TextBox).Text;
				currentMT.playerReq = (this.FindName("playerReqBox") as TextBox).Text;
				currentMT.Description = (this.FindName("descBox") as TextBox).Text;

				foreach (ComboBox BOX in (this.FindName("ListStarterAbilities") as ListView).Items)
				{
					if (BOX.SelectedIndex >= 0)
					{
						string TempUID = CurrentConfig.AbilList[BOX.SelectedIndex].UID;
						currentMT.freeAbilities.Add(TempUID); // saves the free abilities
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
								int TempVal = int.Parse(textBox.Text);	//gets the value
								currentMT.addAffectedResources(TempUID, TempVal);	//saves the affected rescources and their values
							}
						}
					}
				}
				CurrentConfig.RacList[index] = currentMT;
				newrace.Clear();	// clears the list
				foreach (majorTrait race in CurrentConfig.RacList)	//rewrites the list.
				{
					newrace.Add(race);
				}


				CurrentConfig.TestWriteToJson("testConfig.json");
				lstRaces.SelectedIndex = SelIndex;  //applies saved race selection
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
