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
    public string name{get; set;}
    public string image{get; set;}
    public string description{get; set;}
    public int UID{get; set;}

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
                      List<int> Exclusions, int Type, List<List<string>> Dependencies, List<string> Discounts, int Cost, List<int> AffectedResources, List<int> FreeAbilities)
                      : base(Name, Image, Desciption, uid)
    {
        exclusions = Exclusions;
        type = Type;
        dependencies = Dependencies;
        discounts = Discounts;
        cost = Cost;
        affectedResources = AffectedResources;
        freeAbilities = FreeAbilities;
    }
    public List<string> exclusions {get; set;}
    public int type {get; set;}
    public List<string> dependencies = new List<string>();

    public List<string> Discounts = new List<string>();
    public int cost {get; set;}
    public List<string> AffectedResources = new List<string>();
    public List<int> FreeAbilities = new List<int>();

    public void addDepen(string uuid){
        this.dependencies.Add(uuid);
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