using System;
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

		List<majorTrait[]> MTList = new List<majorTrait[]>(); //inaccesible in race menu due to its protection level
		resource[] ResArr = new resource[100];

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