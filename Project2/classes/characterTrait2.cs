using System;
using System.Collections.Generic;


public class characterTrait
{

	public characterTrait(string uid)
	{
		UID = uid; //r-**** for resource 0-**** for misc 1-**** for ability 2-**** for race
	}

	public string name;
	public string image;
	public string description;
	public string UID;

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

	}

	class affectedResource
	{
		public affectedResource(string uid, int amount)
		{
			string UID = uid;
			int Amount = amount;
		}
	}

	class discount
	{
		public discount(string CondUid)
		{
			string CondUID = CondUid;
		}
		public string CondUID = new string("");
		public int Amount = new int();
		public list<string> CostUID = new list<string>();
	}

	public string Type = new string("");
	public int cost = new int();
	public List<string> CostTypes = new List<string>();
	public List<List<string>> dependecy = new List<List<string>>();
	public List<string> freeAbilities = new List<string>();
	List<discount> discounts = new List<discount>();
	List<affectedResource> affectedResources = new List<affectedResource>();
	public List<dynamic> test = new();

	public void addAffectedResources(string uid, int amount)
	{
		this.affectedResources.Add(new affectedResource(uid, amount));
	}
	public void addFreeAbilities(string uid)
	{
		this.freeAbilities.Add(uid);
	}
	public void addDiscount(string Conduid)
	{
		this.discounts.Add(new discount(Conduid));
	}
	public void addDiscountType(string CondUid, list<string> CostUid)
    {
		foreach (discount id in discounts)
        {
			if (id.CondUID == CondUid)
            {
				foreach (string cost in CostUid)
                {
					id.CostUID.add(cost);
                }
				break
            }
        }
    }
	public void addCostTypes(string uid)
	{
		this.CostTypes.Add(uid);
	}
	public void deleteContent()
	{
		this.name = "";
		this.image = "";
		this.description = "";
		this.cost = new int();
		this.CostTypes = new List<string>();
		this.freeAbilities = new List<string>();
		this.discounts = new List<discount>();
		this.affectedResources = new List<affectedResource>();

	}
}

class resource : characterTrait
{
	public resource(string uid, int Type) : base(uid)
	{
		type = Type;
	}

	public int type { get; set; }
}
