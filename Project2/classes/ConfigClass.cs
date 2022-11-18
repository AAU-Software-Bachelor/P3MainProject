using System;
using System.Collections.Generic;

class config
{
	public config()
	{
		MTList = majorTraitList;
		ResList = resourceList;
	}
	public List<majorTrait> MTList = new List<majorTrait>();
	public List<resource> ResList = new List<resource>();

	public bool saveMajorTrait(majorTrait MT)
	{
		try {
			this.MTList.Add(MT);
			return 1;
		}
        catch { return 0; }
	}
	public bool saveresource(resource Res)
	{
		try
		{
			this.ResList.Add(Res);
			return 1;
		}
        catch { return 0; }
	}
}