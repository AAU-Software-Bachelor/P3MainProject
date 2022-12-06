using System;
using System.Collections.Generic;


namespace Project2
{
	public class characterTrait
	{

		public characterTrait(string uid)
		{
			UID = uid; //r-**** for resource 0-**** for misc 1-**** for ability 2-**** for race
			Image = "";
			Name = "";
			Description = "";
		}

		public string Name { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public string UID { get; set; }

		public void SaveToConfig(int configID)
		{
			Console.WriteLine("send to this (" + configID + ") configuration file");
		}
		public void properDelete()
		{

		}

	}

	public class majorTrait : characterTrait
	{
		public majorTrait(string uid) : base(uid)
		{
			Cost = new int();
			CostTypes = new List<string>();
            Exlusions = new List<string>();
            Dependency = new List<List<string>>();
			FreeAbilities = new List<string>();
			Discounts = new List<AmountUID>();
			AffectedResources = new List<AmountUID>();
			PlayerReq = new string("");
		}

		public class AmountUID
		{
			public AmountUID(string uid, int amount)
			{
				UID = uid;
				Amount = amount;
			}
			public string UID { get; set; }
			public int Amount { get; set; }
		}


		public int Cost { get; set; }
		public List<string> CostTypes { get; set; }
        public List<string> Exlusions { get; set; }
        public List<List<string>> Dependency { get; set; }
		public List<string> FreeAbilities { get; set; }
		public List<AmountUID> Discounts { get; set; }
		public List<AmountUID> AffectedResources { get; set; }
		public string PlayerReq { get; set; }

		public void addAffectedResources(string uid, int amount)
		{
			this.AffectedResources.Add(new AmountUID(uid, amount));
		}
		public void addDiscount(string Conduid, int amount)
		{
			this.Discounts.Add(new AmountUID(Conduid, amount));
		}
		
		public void addCostTypes(string uid)
		{
			this.CostTypes.Add(uid);
		}
		public void deleteContent()
		{
			this.Name = "";
			this.Image = "";
			this.Description = "";
			this.Cost = new int();
			this.CostTypes = new List<string>();
            this.Exlusions = new List<string>();
            this.FreeAbilities = new List<string>();
			this.Discounts = new List<AmountUID>();
			this.AffectedResources = new List<AmountUID>();
			this.Dependency = new List<List<string>>();
        }
	}

	public class resourceTrait : characterTrait
	{
		public resourceTrait(string uid) : base(uid)
		{
		}

		public int Type { get; set; }
	}
}