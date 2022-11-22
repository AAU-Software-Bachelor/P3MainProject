using System;
using System.Collections.Generic;

class characterTrait
{
	
	public characterTrait(string Name, string Image, string Desciption, int uid)
	{
		string name  = Name;
		string image = Image;
		string description = Desciption;
		int UID = uid; //1**** for misc 2**** for resouce 3**** for ability 4**** for race
	}

	public void DeleteTrait()
	{ 
		this.name = "";
		this.image = "";
		this.description = "";
		this.UID = 0;
	}
	
	public void SaveToConfig(int configID)
	{
		Console.WriteLine("send to this (" + configID + ") configuration file");
	}

}

class majorTrait : characterTrait
{
	public majorTrait(string Name, string Image, string Desciption, int uid,
					  List<string> Exclusions, int Type, List<string> Dependencies, List<string> Discounts, int Cost, List<string> AffectedResources, List<int> FreeAbilities)
					  : base(Name, Image, Desciption, uid)
	{
		int costType = Costtype;
		int cost = Cost;
		int type = Type;
		object dependentDiscount = new { };
		List<string> exclusions = new List<string>();
		List<string> AffectedResources = new List<string>();
		List<int> FreeAbilities = new List<int>();
	}
	public List<string> exclusions {get; set;}
	public int type {get; set;}
	public List<string> dependencies = new List<string>();
	public List<string> discounts = new List<string>();
	public int cost {get; set;}
	public List<string> affectedResources = new List<string>();
	public List<int> freeAbilities = new List<int>();

	public void addAffectedResources(string uid, int ammount){
		this.AffectedResources.Add(uid);
	}
	public void addFreeAbilities(string uid)
	{
		this.FreeAbilities.Add(uid);
	}
	public void addExclusions(string uid)
	{
		this.exclusions.Add(uid);
	}
	
}

class resource : characterTrait
{
	public resource(string Name, string Image, string Desciption, int uid, int Type) : base(Name, Image, Desciption, uid)
	{
		type = Type;
	}

	public int type {get; set;}
}
