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
			MTList = new List<majorTrait[]>()
			{
				new majorTrait[100],
				new majorTrait[100],
				new majorTrait[100],
				new majorTrait[100]
            };
			ResArr = new resource[100];
		}
		
		public List<majorTrait[]> MTList { get; set; }
		public resource[] ResArr { get; set; }

		public void TestWriteToJson(string fileName)
        {
			majorTrait TestMT = new majorTrait("1-01");
			TestMT.name = "SMITE";
			TestMT.description = "slaa gud modstander med hellig styrke";
			TestMT.Type = "Ability";
			TestMT.cost = 4;
			TestMT.CostTypes = new List<string>() { "KrigerXP", "praestXP"};
			TestMT.dependecy = new List<List<string>>()
			{
				new List<string>(){"kriger", "praest"},
				new List<string>(){"vildt slag", "hellig slag"}
            };
			TestMT.freeAbilities = new List<string>() {"hellig inderlighed"};
			TestMT.addDiscount("2-01");
			TestMT.discounts[0].Amount = 2;
			TestMT.addDiscountType("2-01", new List<string>() { "r-04", "r-05" });
			TestMT.addAffectedResources("r-01",2);
			this.MTList[1][01] = TestMT;

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				WriteIndented = true
			};


			string jsonString = JsonSerializer.Serialize(TestMT, options);
			File.WriteAllText(fileName, jsonString);
        }

		public int getUID(string type)
		{
			if (type == "r")
			{
				for (int i = 0; i < 100; i++)
				{
					if (ResArr[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int i = 0; i < 100; i++)
				{
					if (MTList[int.Parse(type)][i] == null)
					{
						return i;
					}
				}
			}
			return -1;
		}

		bool saveToList(majorTrait trait)
		{
			string[] id = trait.UID.Split('-');

			MTList[int.Parse(id[0])][int.Parse(id[1])] = trait;
			return true;

		}
		bool saveResToList(resource trait)
		{
			string[] id = trait.UID.Split('-');

			ResArr[int.Parse(id[1])] = trait;
			return true;
		}
	}
}
