using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using static Project2.majorTrait;
using System.Windows;

namespace Project2
{
	public class unitTest
	{
		int NTests;
		int NComplete;

		public void unitTests()
		{
			NTests = 0;
			NComplete = 0;

			ConfigUIDTest();
			TraitTest();
			ConfigSaveTest();
			ConfigGetTest();
			RacePageTest();
			RaceAddTest();
			RaceDeleteTest();
			WriteReadTest();

            Debug.WriteLine("Number of Tests Run: " + NTests);
			Debug.WriteLine("Number of Tests Complete: " + NComplete);
		}
		private bool ConfigUIDTest()
		{//setup
			Debug.WriteLine("Start of Config UID Test");
			NTests++;
			config CurrentConfig = new config();

			try
			{//try to use funktion
				string UID = CurrentConfig.newUID("RacList");
				string[] ids = UID.Split("-");

				if (ids.Length == 6 & ids[0] == "RacList")//test if funktion hapened
				{
					NComplete++;
					return true;
				}
				else
				{//fails if funktion did not work correctly
					Debug.WriteLine("ERROR in newUID: effect of funktion not correct");
					return false;
				}
			}
			catch (Exception ex)
			{//fails if funktion creates an error
				Debug.WriteLine("ERROR in newUID: " + ex.Message);
				return false;
			}
		}
		private bool ConfigSaveTest()
		{//setup
			Debug.WriteLine("Start of Config Save Test");
			NTests++;
			config CurrentConfig = new config();
			string UID = CurrentConfig.newUID("RacList");
			majorTrait trait = new majorTrait(UID);

			try
			{//try to use funktion
				CurrentConfig.saveToList(trait);


				if (CurrentConfig.RacList[0] == trait)//test if funktion hapened
				{
					NComplete++;
					return true;
				}
				else
				{//fails if funktion did not work correctly
					Debug.WriteLine("ERROR in saveToList: effect of funktion not correct");
					return false;
				}
			}
			catch (Exception ex)
			{//fails if funktion creates an error
				Debug.WriteLine("ERROR in saveToList: " + ex.Message);
				return false;
			}
		}
		private bool ConfigGetTest()
		{//setup
			Debug.WriteLine("Start of Config Get Test");
			NTests++;
			config CurrentConfig = new config();
			string UID = CurrentConfig.newUID("RacList");
			majorTrait trait = new majorTrait(UID);
			CurrentConfig.saveToList(trait);

			try
			{//try to use funktion
				majorTrait Trait = CurrentConfig.GetTrait(UID);


				if (Trait == trait)//test if funktion hapened
				{
					NComplete++;
					return true;
				}
				else
				{//fails if funktion did not work correctly
					Debug.WriteLine("ERROR in GetTrait: effect of funktion not correct");
					return false;
				}
			}
			catch (Exception ex)
			{//fails if funktion creates an error
				Debug.WriteLine("ERROR in GetTrait: " + ex.Message);
				return false;
			}
		}
		private bool TraitTest()
		{//setup
			Debug.WriteLine("Start of MajorTrait Tests");
			NTests++;

			try
			{//try to use funktion
				majorTrait trait = new majorTrait("Unique ID");
				bool Disc = Enumerable.SequenceEqual(trait.Discounts, new List<AmountUID>());
				bool Exc = Enumerable.SequenceEqual(trait.Exclusions, new List<string>());
				bool Depe = Enumerable.SequenceEqual(trait.Dependencies, new List<List<string>>());
				if (trait.UID == "Unique ID" & trait.Image == "" & trait.Name == "" & trait.PlayerReq == "" & Disc & Exc & Depe)//test if funktion hapened
				{
					NComplete++;
					return true;
				}
				else
				{//fails if funktion did not work correctly
					Debug.WriteLine("ERROR in majorTrait: effect of object not correct");
					return false;
				}
			}
			catch (Exception ex)
			{//fails if funktion creates an error
				Debug.WriteLine("ERROR in majorTrait: " + ex.Message);
				return false;
			}
		}
		private bool RacePageTest()
		{//setup
			Debug.WriteLine("Start of Race Page Test");
			NTests++;
			config Conf = new config();
			majorTrait Trait = new majorTrait(Conf.newUID("RacList"));
			Conf.saveToList(Trait);

			try
			{//try to use funktion
				RacePage page = new RacePage(Conf);


				if (page.RaceCollection.Count == 1 & page.RaceCollection[0] == Trait)//test if funktion hapened
				{
					NComplete++;
					return true;
				}
				else
				{//fails if funktion did not work correctly
					Debug.WriteLine("ERROR in FuncName: effect of funktion not correct");
					return false;
				}
			}
			catch (Exception ex)
			{//fails if funktion creates an error
				Debug.WriteLine("ERROR in FuncName: " + ex.Message);
				return false;
			}
		}
		private bool RaceAddTest()
		{//setup
			Debug.WriteLine("Start of btnRaces_ClickAdd Test");
			NTests++;
			config Conf = new config();
			majorTrait Trait = new majorTrait(Conf.newUID("RacList"));
			Conf.saveToList(Trait);
			RacePage page = new RacePage(Conf);
            object sender = new object();
			RoutedEventArgs e = new RoutedEventArgs();

            try
			{//try to use funktion
				page.btnRaces_ClickAdd(sender, e);

				if (page.RaceCollection.Count == 2 & page.RaceCollection[1].Name == "new race" & page.CurrentConfig.RacList.Count == 2 & page.CurrentConfig.RacList[1].Name == "new race")//test if funktion hapened
				{
					NComplete++;
					return true;
				}
				else
				{//fails if funktion did not work correctly
					Debug.WriteLine("ERROR in btnRaces_ClickAdd: effect of funktion not correct");
					return false;
				}
			}
			catch (Exception ex)
			{//fails if funktion creates an error
				Debug.WriteLine("ERROR in btnRaces_ClickAdd: " + ex.Message);
				return false;
			}
		}
        private bool RaceDeleteTest()
        {//setup
            Debug.WriteLine("Start of btnRaces_ClickDelete Test");
            NTests++;
            config Conf = new config();
            majorTrait Trait = new majorTrait(Conf.newUID("RacList"));
            Conf.saveToList(Trait);
            RacePage page = new RacePage(Conf);
            object sender = new object();
            RoutedEventArgs e = new RoutedEventArgs();

            try
            {//try to use funktion
				page.btnRaces_ClickDelete(sender, e);

                if (page.RaceCollection.Count == 0 & page.CurrentConfig.RacList.Count == 0)//test if funktion hapened
                {
                    NComplete++;
                    return true;
                }
                else
                {//fails if funktion did not work correctly
                    Debug.WriteLine("ERROR in btnRaces_ClickDelete: effect of funktion not correct");
                    return false;
                }
            }
            catch (Exception ex)
            {//fails if funktion creates an error
                Debug.WriteLine("ERROR in btnRaces_ClickDelete: " + ex.Message);
                return false;
            }
        }
        private bool WriteReadTest()
        {//setup
            Debug.WriteLine("Start of Write and Read Test");
            NTests++;
            config Conf = new config();
            majorTrait Trait = new majorTrait(Conf.newUID("RacList"));
            Conf.saveToList(Trait);
			MainWindow page = new MainWindow(Conf);
            object sender = new object();
            RoutedEventArgs e = new RoutedEventArgs();

            try
			{//try to use funktion

				page.onclickSave(sender, e);
				try
				{
					page.onclickLoad(sender, e);
					if (Conf.Equals(page.CurrentConfig))//test if funktion hapened
					{
						NComplete++;
						return true;
					}
					else
					{//fails if funktion did not work correctly
						Debug.WriteLine("ERROR in onclickSave or onclickLoad: effect of funktions not correct");
						return false;
					}
				}
				catch (Exception ex)
                {//fails if funktion creates an error
                    Debug.WriteLine("ERROR in onclickLoad: " + ex.Message);
                    return false;
                }
			}
			catch (Exception ex)
			{//fails if funktion creates an error
				Debug.WriteLine("ERROR in onclickSave: " + ex.Message);
				return false;
			}
		}
        /*private bool Test()
        {//setup
            Debug.WriteLine("Start of btnRaces_ClickDelete Test");
            NTests++;

            try
            {//try to use funktion

                if (NTests >= 0)//test if funktion hapened
                {
                    NComplete++;
                    return true;
                }
                else
                {//fails if funktion did not work correctly
                    Debug.WriteLine("ERROR in btnRaces_ClickDelete: effect of funktion not correct");
                    return false;
                }
            }
            catch (Exception ex)
            {//fails if funktion creates an error
                Debug.WriteLine("ERROR in btnRaces_ClickDelete: " + ex.Message);
                return false;
            }
        }*/
    }
}
