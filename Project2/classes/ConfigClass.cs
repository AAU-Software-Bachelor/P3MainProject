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
			ResList = new List<resource>();
		}
		public List<majorTrait> RacList { get; set; }
		public List<majorTrait> AbilList { get; set; }
		public List<majorTrait> CarList { get; set; }
		public List<majorTrait> RelList { get; set; }
		public List<resource> ResList { get; set; }

		public void TestWriteToJson(string fileName)
        {
			majorTrait TestMT = new majorTrait("Ability-/GUID2");
			TestMT.Name = "SMITE";
			TestMT.Description = "slaa gud modstander med hellig styrke";
			TestMT.Type = "Ability";
			TestMT.cost = 4;
			TestMT.CostTypes = new List<string>() { "Resource-/GUID4", "Resource-/GUID5" };
			TestMT.dependency = new List<List<string>>()
			{
				new List<string>(){ "Career-/GUID1", "Career-/GUID2"},
				new List<string>(){ "Ability-/GUID7", "Ability-/GUID6" },
				new List<string>(){ "Career-/GUID3", "Ability-/GUID9" }
			};
			TestMT.freeAbilities = new List<string>() { "Ability-/GUID5" };
			TestMT.addDiscount("Career-/GUID2");
			TestMT.discounts[0].Amount = 2;
			TestMT.addDiscountType("Career-/GUID2", new List<string>() { "Resource-/GUID4", "Resource-/GUID5" });
			TestMT.addAffectedResources("Resource-/GUID1", 2);
			this.RacList.Add(TestMT);

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				WriteIndented = true
			};


			string jsonString = JsonSerializer.Serialize(TestMT, options);
			File.WriteAllText(fileName, jsonString);
        }

		public string newUID(string type)
		{
			return type  + "-/" + Guid.NewGuid().ToString();
		}

		public dynamic GetTrait(string uid)
        {
			string[] id = uid.Split("-/"); // "race", "religion", "career", "ability", "Resource"
			switch (id[0])
			{
				case "Race":
					return RacList.Find(i => string.Equals(i.UID, id[1]));
					break;

				case "Religion":
					return RelList.Find(i => string.Equals(i.UID, id[1]));
					break;

				case "Career":
					return CarList.Find(i => string.Equals(i.UID, id[1]));
					break;

				case "Ability":
					return AbilList.Find(i => string.Equals(i.UID, id[1]));
					break;

				case "Resource":
					return ResList.Find(i => string.Equals(i.UID, id[1]));
					break;

				default: return false; 

			}
		}



		bool saveToList(dynamic trait)
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
