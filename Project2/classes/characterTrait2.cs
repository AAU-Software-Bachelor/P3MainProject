using System;
using System.Collections.Generic;

class characterTrait
{
	
	public characterTrait(string Name, string Image, string Desciption, int uid)
	{
		name  = Name;
		image = Image;
		description = Desciption;
		UID = uid; //1**** for misc 2**** for resouce 3**** for ability 4**** for race
	}

	public string name;
	public string image;
	public string description;
	public int UID;

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
					  List<string> Exclusions, int Type, List<string> Dependencies, List<string> Discounts, int Cost, List<string> AffectedResources, List<int> FreeAbilities, int Costtype)
					  : base(Name, Image, Desciption, uid)
	{
		int costType = Costtype;
		int cost = Cost;
		int type = Type;
		object dependentDiscount = new { };
		exclusions = new List<string>();
		affectedResources = new List<string>();
		freeAbilities = new List<int>();
	}
	public List<string> exclusions {get; set;}
	public int type {get; set;}
	public List<string> dependencies = new List<string>();
	public List<string> discounts = new List<string>();
	public int cost {get; set;}
	public List<string> affectedResources = new List<string>();
	public List<int> freeAbilities = new List<int>();
	public int Costtype {get; set;}

	public void addAffectedResources(string uid, int ammount){
		this.affectedResources.Add(uid);
	}
	public void addFreeAbilities(int UID)
	{
		this.freeAbilities.Add(UID);
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
