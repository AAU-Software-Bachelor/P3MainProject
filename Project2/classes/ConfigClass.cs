using System;
using System.Collections.Generic;

<<<<<<< HEAD
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
=======
class config
{
	public config()
	{
		
	}
	public List<majorTrait> MTList = new List<majorTrait>();
	public List<resource> ResList = new List<resource>();

	public bool saveMajorTrait(majorTrait MT)
	{
		try {
			this.MTList.Add(MT);
			return true;
		}
		catch { return false; }
	}
	public bool saveresource(resource Res)
	{
		try
		{
			this.ResList.Add(Res);
			return true;
		}
		catch { return false; }
>>>>>>> master
	}
}