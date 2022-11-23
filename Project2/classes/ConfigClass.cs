using System;

class config
{
    public config(List<majorTrait> majorTraitList, List<resource> resourceList)
    {
        MTList.Add(new majorTrait[100], new majorTrait[100], new majorTrait[100], new majorTrait[100])
    }

    public List<majorTrait[100]> MTList = new List<majorTrait[100]>();
    public resource[100] ResArr = new resource[100];

    public int getUID(string type)
    {
        if (type == "r")
        {
            for(int i = 0; i < 100; i++)
            {
                if (ResArr[i] == null)
                {
                    return i;
                }
            }
        }
        else
        {
            for(int i = 0; i < 100; i++)
            {
                if (MTList[int(type)][i] == null)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    public bool saveToList(object trait)
    {
        string[] id = trait.UID.split('-');
        if (id[0] == "r") //r-****
        {
            ResArr[int(id[1])] = trait;
            return true;
        }
        else
        {
            MTList[int(id[0])][int(id[1])] = trait;
            return true;
        }
        return false;
    }
}