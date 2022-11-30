using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Project2
{
	public class config
	{
		public config()
		{
			RacList = new List<majorTrait>();
			AbilList = new List<majorTrait>();
			CarList = new List<majorTrait>();
			RelList = new List<majorTrait>();
			ResList = new List<resourceTrait>();
		}
		public List<majorTrait> RacList { get; set; }
		public List<majorTrait> AbilList { get; set; }
		public List<majorTrait> CarList { get; set; }
		public List<majorTrait> RelList { get; set; }
		public List<resourceTrait> ResList { get; set; }

		public void TestWriteToJson(string fileName)
        {

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				WriteIndented = true
			};
			
			string jsonString = JsonSerializer.Serialize(this, options);
			File.WriteAllText(fileName, jsonString);
        }

		public string newUID(string type)
		{
			return type  + "-/" + Guid.NewGuid().ToString();
		}

		public dynamic GetTrait(string uid, bool isDelete = false)
        {
			string[] id = uid.Split("-/"); // "race", "religion", "career", "ability", "Resource"
			int index;
			switch (id[0])
			{
				case "Race":
					index = RacList.FindIndex(i => string.Equals(i.UID, uid));
					majorTrait SelRac = this.RacList[index];
					if (isDelete)
                    {
						this.RacList.RemoveAt(index);
                    }
					return SelRac;

				case "Religion":
					index = RelList.FindIndex(i => string.Equals(i.UID, uid));
					majorTrait SelRel = this.RelList[index];
					if (isDelete)
					{
						this.RelList.RemoveAt(index);
					}
					return SelRel;

				case "Career":
					index = CarList.FindIndex(i => string.Equals(i.UID, uid));
					majorTrait SelCar = this.CarList[index];
					if (isDelete)
					{
						this.CarList.RemoveAt(index);
					}
					return SelCar;

				case "Ability":
					index = AbilList.FindIndex(i => string.Equals(i.UID, uid));
					majorTrait SelAbi = this.AbilList[index];
					if (isDelete)
					{
						this.AbilList.RemoveAt(index);
					}
					return SelAbi;

				case "Resource":
					index = ResList.FindIndex(i => string.Equals(i.UID, uid));
					resourceTrait SelRes = this.ResList[index];
					if (isDelete)
					{
						this.ResList.RemoveAt(index);
					}
					return SelRes;

				default: return false; 

			}
		}



		public bool saveToList(dynamic trait)
		{
			string[] id = trait.UID.Split("-/"); // "race", "religion", "career", "ability"

			switch (id[0])
			{
				case "Race":
					RacList.Add(trait);
					break;

				case "Religion":
					RelList.Add(trait);
					break;

				case "Career":
					CarList.Add(trait);
                    break;

				case "Ability":
					AbilList.Add(trait);
                    break;

				case "Resource":
					ResList.Add(trait);
					break ;

				default: return false;

			}
			return true;

		}

	}
}
