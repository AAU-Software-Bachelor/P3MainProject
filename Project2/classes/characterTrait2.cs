using System;
using System.Collections.Generic;

class characterTrait
{

	public characterTrait(int uid)
	{
		UID = uid; //1**** for misc 2**** for resouce 3**** for ability 4**** for race
	}

	public string name;
	public string image;
	public string description;
	public int UID;

	public void SaveToConfig(int configID)
	{
		Console.WriteLine("send to this (" + configID + ") configuration file");
	}

}

class majorTrait : characterTrait
{
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
	}
	public void deleteContent()
	{
		this.name = "";
		this.image = "";
		this.description = "";
		this.cost = 0;
		this.Costtype = new List<int>();
		this.freeAbilities = new List<int>();
		this.exclusions = new List<string>();
		this.dependencies = new List<string>();
		this.discounts = new List<string>();
		this.affectedResources = new List<string>();

	}
}

class resource : characterTrait
{
	public resource(int uid, int Type) : base(uid)
	{
		type = Type;
	}

	public int type { get; set; }
}
