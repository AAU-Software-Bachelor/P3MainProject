using System;
using System.Collections.Generic;


class characterTrait
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

class majorTrait : characterTrait
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

	class cost
	{
		public cost(string T, string uid, int amount)
		{
			string Type = T;
			string UID = uid;
			int Amount = amount;
		}
	}
	class discount
	{
		public discount(string uid, string type, int amount)
		{
			string Type = type;
			string UID = uid;
			int Amount = amount;
		}
	}
	public string dependency = new string("");                          //  !(Exc1 & Exc2 & Exc..n) & ( (Dep1 & Dep2) | (Dep3 | Dep4) & Dep..n )
	public List<string> freeAbilities = new List<string>();
	List<cost> Cost = new List<cost>();
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
	public void addDiscount(string uid, string type, int value)
	{
		this.discounts.Add(new discount(uid, type, value));
	}
	public void addCosttype(string Type, string uid, int amount)
	{
		this.Cost.Add(new cost(Type, uid, amount));
	}
	public void deleteContent()
	{
		this.name = "";
		this.image = "";
		this.description = "";
		this.dependency = "";
		this.freeAbilities = new List<string>();
		this.Cost = new List<cost>();
		this.discounts = new List<discount>();
		this.affectedResources = new List<affectedResource>();

	}
}

class resourceTrait : characterTrait
{
	public resourceTrait(string uid, int Type) : base(uid)
	{
		type = Type;
	}

	public int type { get; set; }
}
