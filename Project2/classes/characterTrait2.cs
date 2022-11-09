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
        // no idea whats going on here
        foreach (majorTrait item in nameofconfig.MTList)
        {
            
        }  
        name = "";
        image = "";
        description = "";
        UID = 0;
    }
    
    public void SaveToConfig()
    {
        
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
    public List<List<string>> dependencies {get; set;}
    public List<string> discounts {get; set;}
    public int cost {get; set;}
    public List<int> affectedResources {get; set;}
    public List<int> freeAbilities {get; set;}

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