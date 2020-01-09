using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace Entities
{
	public partial class Security
	{
		[NotMapped]
		public static SecurityPropertyNames PropertyNames = new SecurityPropertyNames();

		[Key]
		public int Identifier { get; set; }

		[NotMapped]
		public bool IsNew { get{ return this.Identifier == 0; } }

		[MaxLength(255)]
		public String Name { get; set; }

		[MaxLength(5)]
		public String Symbol { get; set; }

		public Decimal MarketCap { get; set; }

		[MaxLength(50)]
		public String Sector { get; set; }

		[MaxLength(50)]
		public String AssetClass { get; set; }

		public bool PairEligible { get; set; }

		public Sectors SectorEnum { get; set; }

		public AssetClasses AssetClassEnum { get; set; }

		public virtual List<DayCandlestick> DailyCandlesticks { get; set; }
		public virtual List<WeekCandlestick> WeeklyCandlesticks { get; set; }

		#region Constructor
		public  Security()
		{
		}
		#endregion

		#region Comparisons
		public static bool operator ==(Security entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return true;
			}
			else if ((object)entity != null && obj is Security && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier == ((Security)obj).Identifier);
			}
			else
			{
				return false;
			}
		}

		public static bool operator !=(Security entity, object obj)
		{
			if ((object)entity == null && obj == null)
			{
				return false;
			}
			else if ((object)entity != null && obj is Security && entity.GetType() == obj.GetType())
			{
				return (entity.Identifier != ((Security)obj).Identifier);
			}
			else
			{
				return true;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is Security && this.GetType() == obj.GetType())
			{
				return (this.Identifier == ((Security)obj).Identifier);
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
