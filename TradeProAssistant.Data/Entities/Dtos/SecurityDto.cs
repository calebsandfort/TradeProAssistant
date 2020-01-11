using System;
using System.Collections.Generic;
using Enums;

namespace Entities.Dtos
{
	public class SecurityDto
	{
		public int Identifier { get; set; }
		public bool IsNew { get{ return this.Identifier == 0; } }
		public String Name { get; set; }
		public String Symbol { get; set; }
		public Decimal MarketCap { get; set; }
		public Decimal CurrentPrice { get; set; }
		public String Sector { get; set; }
		public String AssetClass { get; set; }
		public bool PairEligible { get; set; }

		public Sectors SectorEnum { get; set; }

		public AssetClasses AssetClassEnum { get; set; }


		#region Comparisons
		public static bool operator ==(SecurityDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is SecurityDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((SecurityDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(SecurityDto entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is SecurityDto && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((SecurityDto)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is SecurityDto && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((SecurityDto)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
	}
}
