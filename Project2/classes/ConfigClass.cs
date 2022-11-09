class config
{
    public config(List<majorTrait> majorTraitList, List<resource> resourceList)
    {
        MTList = majorTraitList;
        ResList = resourceList;
    }

    public List<majorTrait> MTList {get; set;}
    public List<resource> ResList {get; set;}
}