using System;
using System.Collections.Generic;

<<<<<<< HEAD

class characterTrait
{

	public characterTrait(string uid)
	{
		UID = uid; //r-**** for resource 0-**** for misc 1-**** for ability 2-**** for race
=======
class characterTrait
{

	public characterTrait(int uid)
	{
		UID = uid; //1**** for misc 2**** for resouce 3**** for ability 4**** for race
>>>>>>> master
	}

	public string name;
	public string image;
	public string description;
<<<<<<< HEAD
	public string UID;
=======
	public int UID;
>>>>>>> master

	public void SaveToConfig(int configID)
	{
		Console.WriteLine("send to this (" + configID + ") configuration file");
	}
<<<<<<< HEAD
	public void properDelete()
	{

	}
=======
>>>>>>> master

}

class majorTrait : characterTrait
{
<<<<<<< HEAD
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
=======
	public majorTrait(int uid) : base(uid)
	{

	}
	public int cost = new int();
	public List<int> Costtype = new List<int>();
	public List<int> freeAbilities = new List<int>();
	public List<string> exclusions = new List<string>();
	public List<string> dependencies = new List<string>();
	public List<string> discounts = new List<string>();
	public List<string> affectedResources = new List<string>();

	public void addAffectedResources(string uid, int ammount)
	{
		this.affectedResources.Add(uid);
	}
	public void addFreeAbilities(int uid)
	{
		this.freeAbilities.Add(uid);
	}
	public void addExclusions(string uid)
	{
		this.exclusions.Add(uid);
	}
	public void addDependency(string uid, int value)
	{
		this.dependencies.Add(uid);
		this.discounts.Add(value);
	}
	public void addCosttype(string uid)
	{
		this.Costtype.Add(uid);
>>>>>>> master
	}
	public void deleteContent()
	{
		this.name = "";
		this.image = "";
		this.description = "";
<<<<<<< HEAD
		this.dependency = "";
		this.freeAbilities = new List<string>();
		this.Cost = new List<cost>();
		this.discounts = new List<discount>();
		this.affectedResources = new List<affectedResource>();
=======
		this.cost = 0;
		this.Costtype = new List<int>();
		this.freeAbilities = new List<int>();
		this.exclusions = new List<string>();
		this.dependencies = new List<string>();
		this.discounts = new List<string>();
		this.affectedResources = new List<string>();
>>>>>>> master

	}
}

class resourceTrait : characterTrait
{
<<<<<<< HEAD
	public resourceTrait(string uid, int Type) : base(uid)
=======
	public resource(int uid, int Type) : base(uid)
>>>>>>> master
	{
		type = Type;
	}

	public int type { get; set; }
}
