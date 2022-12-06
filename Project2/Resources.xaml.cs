using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
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
using System.Xml.Linq;
using static Project2.ResourcePage;
using static Project2.resourceTrait;
using static Project2.GalleryWindow;
using static Project2.galleryIcon;



namespace Project2
{
	/// <summary>
	/// Interaction logic for Recources.xaml
	/// </summary>
	public partial class ResourcePage : Page
	{
		public ResourcePage(config currentConfig)
		{
			CurrentConfig = currentConfig;
			InitializeComponent();
			ResourceCollection = new ObservableCollection<resourceTrait>();
			foreach (resourceTrait Resource in CurrentConfig.ResList) //adds all Recources to ObservableCollection ResourceCollection
			{
				ResourceCollection.Add(Resource);
			}
			lstResources.ItemsSource = ResourceCollection;
			CurrentIndex = -1;  //skip the next use of CurrentIndex
			lstResources.SelectedIndex = 0;
		}
		config CurrentConfig { get; set; }
		int CurrentIndex { get; set; }  //keeps track of what index to use
		public ObservableCollection<resourceTrait> ResourceCollection;

		private void ResourceMainMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			MainWindow mainWindow = new MainWindow(CurrentConfig);
			Application.Current.MainWindow.Content = mainWindow;
		}

		private void btnResource_ClickAdd(object sender, RoutedEventArgs e)
		{
			resourceTrait newResource = new resourceTrait(CurrentConfig.newUID("ResList")) { Name = "new resource"};
			ResourceCollection.Add(newResource);
			CurrentConfig.saveToList(newResource);
			lstResources.SelectedIndex = ResourceCollection.Count - 1;
		}

		private void btnResource_ClickDelete(object sender, RoutedEventArgs e)
		{
			var index = lstResources.SelectedIndex;
			if (index >= 0)
			{
				ResourceCollection.Remove(CurrentConfig.GetTrait(ResourceCollection[index].UID, true)); //gets the race to be deleteted via GetTrait while it deletes it, and deletes its counterpart in newrace
			}
		}

        private void OnResourceChanged (object sender, RoutedEventArgs e)
		{
			int SelIndex = lstResources.SelectedIndex;  //saves selected resource so it is not lost
			if (lstResources.SelectedIndex >= 0)    //lstResources.SelectedIndex returns -1 if nothing is selected
			{
				if (CurrentIndex >= 0)  //skips saving the previus selected resource if -1
				{
					SaveResource(CurrentIndex);

				}
				CurrentIndex = lstResources.SelectedIndex;
				if (CurrentConfig.ResList.Count > 0)
				{
					resourceTrait currentRT = CurrentConfig.ResList[CurrentIndex]; //gets the trait to be loaded
					if (currentRT.Image != string.Empty)
					{
						(this.FindName("ChosenImage") as Image).Source = new BitmapImage(new Uri(currentRT.Image, UriKind.Absolute));
					}
					else
					{
						(this.FindName("ChosenImage") as Image).Source = new BitmapImage(new Uri(CurrentConfig.placeholderImage, UriKind.Relative));
					}
					(this.FindName("nameBox") as TextBox).Text = currentRT.Name; //sets text to the name from the current MajorTrait object
					(this.FindName("descBox") as TextBox).Text = currentRT.Description;  //sets text to the description from the current MajorTrait object
				

					foreach (RadioButton rd in (this.FindName("GridRadioButtons") as Grid).Children.OfType<RadioButton>())
					{
						if (int.Parse(rd.Tag.ToString()) == currentRT.type)
						{
							rd.IsChecked = true;
						}
					}
				}
            }
			else
			{
				CurrentIndex = -1;
			}
			lstResources.SelectedIndex = SelIndex;  //applies saved resource selection
		}

		public void ChangeIcon_click(object sender, RoutedEventArgs e)
		{
			GalleryWindow newWindow = new GalleryWindow(CurrentConfig);
		
			string imgSource = newWindow.uploadFile(sender, e);
			System.Diagnostics.Debug.WriteLine("we have an image at: " +imgSource);
            ChosenImage.Source = new BitmapImage(new Uri(imgSource, UriKind.Absolute));
			CurrentConfig.ResList[CurrentIndex].Image = imgSource;
        }

        private void OnClickSaveResource(object sender, RoutedEventArgs e)
		{
			SaveResource();
		}

		/// <summary>
		/// Saves everything in the indexed race to the current config. if no index is given then it saves the currently selected race.
		/// </summary>
		private void SaveResource(int index = -1)
		{
			int SelIndex = lstResources.SelectedIndex;  //saves selected race so it is not lost

			string UID = "";
			if (index == -1)    //is true when funtion is called via a button
			{
				if (lstResources.SelectedIndex >= 0)
				{
					UID = CurrentConfig.ResList[lstResources.SelectedIndex].UID;    //uses the selected index to find the wanted UID
					index = CurrentConfig.ResList.FindIndex(i => string.Equals(i.UID, UID));
				}
			}
			else
			{
				UID = CurrentConfig.ResList[index].UID; //uses the given index to find the wanted UID
			}
			if (UID != "")
			{
				resourceTrait currentRT = CurrentConfig.GetTrait(UID);

				currentRT.Name = (this.FindName("nameBox") as TextBox).Text;
				currentRT.Description = (this.FindName("descBox") as TextBox).Text;

				foreach (RadioButton rd in (this.FindName("GridRadioButtons") as Grid).Children.OfType<RadioButton>())
				{
					if (rd.IsChecked == true)
					{
						currentRT.type = int.Parse(rd.Tag.ToString());
					}
				}

				CurrentConfig.ResList[index] = currentRT;
				ResourceCollection.Clear();    // clears the list
				foreach (resourceTrait res in CurrentConfig.ResList)  //rewrites the list.
				{
					ResourceCollection.Add(res);
				}


				CurrentConfig.TestWriteToJson("testConfig.json");
				lstResources.SelectedIndex = SelIndex;  //applies saved resource selection
			}
		}
	}
}

