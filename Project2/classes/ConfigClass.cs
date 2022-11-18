using System;
using System.Collections.Generic;

class config
{
	public config()
	{
		
	}
	public List<majorTrait> MTList = new List<majorTrait>();
	public List<resource> ResList = new List<resource>();

	public bool saveMajorTrait(majorTrait MT)
	{
		try {
			this.MTList.Add(MT);
			return true;
		}
		catch { return false; }
	}
	public bool saveresource(resource Res)
	{
		try
		{
			this.ResList.Add(Res);
			return true;
		}
		catch { return false; }
	}
}