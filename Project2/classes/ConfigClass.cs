using System;
using System.Collections;
using System.Collections.Generic;

namespace Project2
{
	public class config
	{
		public config()
		{
			MTList.Add(new majorTrait[100]);
			MTList.Add(new majorTrait[100]);
			MTList.Add(new majorTrait[100]);
			MTList.Add(new majorTrait[100]);
		}

		public ArrayList TraitList = new ArrayList();
		public List<majorTrait[]> MTList = new List<majorTrait[]>() ; //inaccesible in race menu due to its protection level
		resource[] ResArr = new resource[100];

		public void TestWriteToJson(string fileName)
        {
			majorTrait TestMT = new majorTrait(1-01);
			TestMT.name = "SMITE";
			TestMT.description = "slå gud modstander med hellig styrke"
			TestMT.Type = "Ability";
			TestMT.cost = 4;
			TestMT.CostTypes = new list<string>() { "KrigerXP", "præstXP"};
			TestMT.dependecy = new List<List<string>>()
            {
				new List<string>(){"kriger", "præst"}
				new List<string>(){"vildt slag", "hellig slag"}
            };
			TestMT.freeAbilities = new List<string>() {"hellig inderlighed"};
			TestMT.addDiscount("2-01");
			TestMT.discounts[0].Amount = 2;
			TestMT.discounts[0].CostUID.add("r-04");
			TestMT.discounts[0].CostUID.add("r-05");
			TestMT.addAffectedResources("r-01",2);

			string jsonString = JsonSerializer.Serialize(TestMT);
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