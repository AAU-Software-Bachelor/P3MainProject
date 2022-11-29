using System;
using System.Collections.Generic;


namespace Project2
{
	public class characterTrait
	{

		public characterTrait(string uid)
		{
			UID = uid; //r-**** for resource 0-**** for misc 1-**** for ability 2-**** for race
			Image = "";
			Name = "";
			Description = "";
		}

		public string Name { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public string UID { get; set; }

		public void SaveToConfig(int configID)
		{
			Console.WriteLine("send to this (" + configID + ") configuration file");
		}
		public void properDelete()
		{

		}

	}

	public class majorTrait : characterTrait
	{
		public majorTrait(string uid) : base(uid)
		{
			Type = new string("");
			cost = new int();
			CostTypes = new List<string>();
			dependency = new List<List<string>>();
			freeAbilities = new List<string>();
			discounts = new List<discount>();
			affectedResources = new List<affectedResource>();
		}

		public class affectedResource
		{
			public affectedResource(string uid, int amount)
			{
				UID = uid;
				Amount = amount;
			}
			public string UID { get; set; }
			public int Amount { get; set; }
		}

		public class discount
		{
			public discount(string Conduid)
			{
				CondUID = Conduid;
				Amount = 0;
				CostUID = new List<string>();
			}
			public List<string> CostUID { get; set; }
			public string CondUID { get; set; }
			public int Amount { get; set; }

		}

		public string Type { get; set; }
		public int cost { get; set; }
		public List<string> CostTypes { get; set; }
		public List<List<string>> dependency { get; set; }
		public List<string> freeAbilities { get; set; }
		public List<discount> discounts { get; set; }
		public List<affectedResource> affectedResources { get; set; }

		public void addAffectedResources(string uid, int amount)
		{
			this.affectedResources.Add(new affectedResource(uid, amount));
		}
		public void addDiscount(string Conduid)
		{
			this.discounts.Add(new discount(Conduid));
		}
		public void addDiscountType(string CondUid, List<string> CostUid)
		{
			foreach (discount id in discounts)
			{
				if (id.CondUID == CondUid)
				{
					foreach (string cost in CostUid)
					{
						id.CostUID.Add(cost);
					}
					break;
				}
			}
		}
		public void addCostTypes(string uid)
		{
			this.CostTypes.Add(uid);
		}
		public void deleteContent()
		{
			this.Name = "";
			this.Image = "";
			this.Description = "";
			this.cost = new int();
			this.CostTypes = new List<string>();
			this.freeAbilities = new List<string>();
			this.discounts = new List<discount>();
			this.affectedResources = new List<affectedResource>();

		}
	}

	public class resource : characterTrait
	{
		public resource(string uid, int Type) : base(uid)
		{
			type = Type;
		}

		public int type { get; set; }
	}
}